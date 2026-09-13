namespace Iluminatta.Api.Models
{
    public class ItemPedido
    {
        public int Id { get; set; }
        public int PedidoId { get; set; }
        public int ProdutoId { get; set; }
        public int QuantidadeItens { get; set; }
        public Produto Produto { get; set; }
        public Pedido Pedido { get; set; }
    }
}