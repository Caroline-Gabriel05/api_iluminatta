using Iluminatta.Api.Data;
using Iluminatta.Api.DTOs;
using Iluminatta.Api.Models;
using Iluminatta.Api.Constants;
using Microsoft.EntityFrameworkCore;

namespace Iluminatta.Api.Services;

public class PagamentoService
{
    private readonly AppDbContext _context;

    public PagamentoService(AppDbContext context)
    {
        _context = context;
    }

    
    public async Task<Pagamento> CriarPagamento(PagamentoDto pagamentoDto)
    {
        var pedido = await _context.Pedidos
            .FirstOrDefaultAsync(p => p.Id == pagamentoDto.PedidoId);

        if (pedido == null)
        {
            throw new KeyNotFoundException("Pedido não encontrado.");
        }

        if (string.Equals(
            pedido.Status,
            "Cancelado",
            StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException(
                "Não é possível pagar um pedido cancelado.");
        }

        if (pedido.ValorTotal <= 0)
        {
            throw new InvalidOperationException(
                "O valor total do pedido deve ser maior que zero.");
        }

        var pagamento = new Pagamento
        {
            PedidoId = pedido.Id,
            Valor = pedido.ValorTotal,
            Status = StatusPagamento.Pendente,
            MetodoPagamento = pagamentoDto.MetodoPagamento,

            DataPagamento = null
        };


        var pagamentoExistente = await _context.Pagamentos
            .AnyAsync(p =>
                p.PedidoId == pedido.Id &&
                (p.Status == StatusPagamento.Pendente ||
                p.Status == StatusPagamento.Aprovado));

        if (pagamentoExistente)
        {
            throw new InvalidOperationException(
                "Este pedido já possui um pagamento pendente ou aprovado.");
        }

        _context.Pagamentos.Add(pagamento);
        await _context.SaveChangesAsync();

        return pagamento;
    }

    public async Task<Pagamento?> BuscarPagamentoPorId(int id)
    {
        return await _context.Pagamentos
            .FirstOrDefaultAsync(p => p.Id == id);
    }
}