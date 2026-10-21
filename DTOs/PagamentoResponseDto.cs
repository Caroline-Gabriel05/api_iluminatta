namespace Iluminatta.Api.DTOs
{
    public class PagamentoResponseDto
    {
        public int Id { get; set; }
        public int PedidoId { get; set; }
        public decimal Valor { get; set; }
        public string Status { get; set; } = string.Empty;
        public string? MetodoPagamento { get; set; }
        public string? MercadoPagoId { get; set; }
        public DateTime DataPagamento { get; set; }
    }
}