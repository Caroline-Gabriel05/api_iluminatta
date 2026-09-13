namespace Iluminatta.Api.Models
{
    public class Produto
    {
        public int Id { get; set; }
        public int MarcaId { get; set; }
        public int CategoriaId { get; set; }
        public string Nome { get; set; }
        public string Modelo { get; set; }
        public string Descricao { get; set; }
        public decimal Preco { get; set; }
        public int Estoque { get; set; }

        public Categoria Categoria { get; set; }
        public Marca Marca { get; set; }

        public ICollection<ImagemProduto> ImagensProdutos { get; set; }
        public ICollection<ItemPedido> ItensPedidos { get; set; }
    }
}