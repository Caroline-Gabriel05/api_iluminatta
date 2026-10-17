using Iluminatta.Api.DTOs;
using Iluminatta.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Iluminatta.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PagamentosController : ControllerBase
{
    private readonly PagamentoService _pagamentoService;

    public PagamentosController(PagamentoService pagamentoService)
    {
        _pagamentoService = pagamentoService;
    }

    [HttpPost]
    public async Task<IActionResult> CriarPagamento(PagamentoDto pagamentoDto)
    {
        try
        {
            var pagamento = await _pagamentoService.CriarPagamento(pagamentoDto);

            return Ok(new
            {
                pagamento.Id,
                pagamento.PedidoId,
                pagamento.Valor,
                pagamento.Status,
                pagamento.MetodoPagamento,
                pagamento.MercadoPagoId,
                pagamento.DataPagamento
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new
            {
                mensagem = ex.Message
            });
        }
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> BuscarPagamentoPorId(int id)
    {
        var pagamento = await _pagamentoService.BuscarPagamentoPorId(id);

        if (pagamento == null)
        {
            return NotFound(new
            {
                mensagem = "Pagamento não encontrado."
            });
        }

        return Ok(new
        {
            pagamento.Id,
            pagamento.PedidoId,
            pagamento.Valor,
            pagamento.Status,
            pagamento.MetodoPagamento,
            pagamento.MercadoPagoId,
            pagamento.DataPagamento
        });
    }
}