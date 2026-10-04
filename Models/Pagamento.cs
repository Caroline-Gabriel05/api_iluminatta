namespace Iluminatta.Api.Models
{
    public class Pagamento
    {
        public int Id { get; set; }

        public int PedidoId { get; set; }

        public string MercadoPagoId { get; set; }

        public decimal Valor { get; set; }

        public string Status { get; set; }

        public string MetodoPagamento { get; set; }

        public DateTime DataPagamento { get; set; }

        public Pedido Pedido { get; set; }
    }
}