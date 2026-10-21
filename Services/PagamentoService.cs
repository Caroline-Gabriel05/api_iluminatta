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

    public async Task<PagamentoResponseDto> CriarPagamento(PagamentoCreateDto dto)
    {
        var pedido = await _context.Pedidos.FindAsync(dto.PedidoId);
            if (pedido == null)
            {
                throw new KeyNotFoundException("Pedido não encontrado.");
            }

        var pagamentoExistente = await _context.Pagamentos
                .FirstOrDefaultAsync(p => p.PedidoId == dto.PedidoId && p.Status == "Pendente");

            if (pagamentoExistente != null)
            {
                return new PagamentoResponseDto
                {
                    Id = pagamentoExistente.Id,
                    PedidoId = pagamentoExistente.PedidoId,
                    Status = pagamentoExistente.Status,
                    Valor = pagamentoExistente.Valor
                };
            }

            var pagamento = new Pagamento
            {
                PedidoId = dto.PedidoId,
                Valor = pedido.ValorTotal,
                Status = "Pendente",
                MetodoPagamento = dto.MetodoPagamento,
                DataPagamento = DateTime.UtcNow,
                MercadoPagoId = null 
            };

            _context.Pagamentos.Add(pagamento);
            await _context.SaveChangesAsync();

            return new PagamentoResponseDto
            {
                Id = pagamento.Id,
                PedidoId = pagamento.PedidoId,
                Status = pagamento.Status,
                Valor = pagamento.Valor
            };
        }

    public async Task<Pagamento?> BuscarPagamentoPorId(int id)
    {
        return await _context.Pagamentos
            .FirstOrDefaultAsync(p => p.Id == id);
    }
}