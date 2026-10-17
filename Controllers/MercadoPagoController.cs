
using Iluminatta.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Iluminatta.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MercadoPagoController : ControllerBase
{
    private readonly MercadoPagoService _mercadoPagoService;

    public MercadoPagoController(
        MercadoPagoService mercadoPagoService)
    {
        _mercadoPagoService = mercadoPagoService;
    }

    [HttpGet("testar-conexao")]
    public async Task<IActionResult> TestarConexao()
    {
        try
        {
            var resposta =
                await _mercadoPagoService.TestarConexaoAsync();

            return Ok(new
            {
                mensagem = "Consulta ao Mercado Pago realizada.",
                dados = resposta
            });
        }
        catch (HttpRequestException ex)
        {
            return StatusCode(502, new
            {
                mensagem = "Falha na comunicação com o Mercado Pago.",
                detalhe = ex.Message
            });
        }
    }
}
