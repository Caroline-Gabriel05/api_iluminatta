namespace Iluminatta.Api.Models
{
    public class ImagemProduto
    {
        public int Id { get; set; }
        public int ProdutoId { get; set; }
        public string CaminhoImagem { get; set; }
        public Produto Produto { get; set; }
    }
}