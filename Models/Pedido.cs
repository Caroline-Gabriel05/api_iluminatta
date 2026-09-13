namespace Iluminatta.Api.Models
{
    public class Pedido
    {
        public int Id { get; set; }
        public int QuantidadeItens { get; set; }
        public DateOnly DataPedido { get; set; }
        public int ClienteId { get; set; }
        public int Parcelas { get; set; }
        public decimal ValorTotal { get; set; }
        public string Status { get; set; }

        public ICollection<ItemPedido> ItensPedidos { get; set; }
    }
}