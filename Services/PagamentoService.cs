using Iluminatta.Api.Data;
using Iluminatta.Api.DTOs;
using Iluminatta.Api.Models;
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
            throw new Exception("Pedido não encontrado.");
        }

        var pagamento = new Pagamento
        {
            PedidoId = pedido.Id,
            Valor = pedido.ValorTotal,
            Status = "Pendente",
            MetodoPagamento = pagamentoDto.MetodoPagamento,
            DataPagamento = DateTime.Now
        };

        _context.Pagamentos.Add(pagamento);

        await _context.SaveChangesAsync();

        return pagamento;
    }
}