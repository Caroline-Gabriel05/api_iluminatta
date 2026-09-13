namespace Iluminatta.Api.Models
{
    public class Marca
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        public ICollection<Produto> Produtos { get; set; }
    }
}