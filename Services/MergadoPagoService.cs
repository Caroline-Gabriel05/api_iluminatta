using System.Net.Http.Headers;

namespace Iluminatta.Api.Services;

public class MercadoPagoService
{
    private readonly HttpClient _httpClient;

    public MercadoPagoService(
        HttpClient httpClient,
        IConfiguration configuration
    )
    {
        _httpClient = httpClient;
        var accessToken = configuration[
            "MercadoPago:AccessToken"];

        if(string.IsNullOrWhiteSpace(accessToken))
        {
            throw new InvalidOperationException(
                "Acess Token do Merdado Pago não configurado."
            );
        }

        _httpClient.BaseAddress = new Uri("https://api.mercadopago.com/");

        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue(
                "Bearer",
                accessToken
            );
    }
    
    public async Task<string> TestarConexaoAsync()
    {
        var resposta = await _httpClient.GetAsync(
            "v1/payment_methods");

        var conteudo = await resposta.Content.ReadAsStringAsync();

        if (!resposta.IsSuccessStatusCode)
        {
            throw new HttpRequestException(
                $"Mercado Pago retornou HTTP {(int)resposta.StatusCode}: {conteudo}");
        }

        return conteudo;
    }

}