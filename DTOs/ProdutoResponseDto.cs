namespace Iluminatta.Api.DTOs
{
    public class ProdutoResponseDto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public decimal Preco { get; set; }
        public int Estoque { get; set; }
        public string? Descricao { get; set; }
        public int? MarcaId { get; set; }
        public string? NomeMarca { get; set; }
        public int? CategoriaId { get; set; }
        public string? NomeCategoria { get; set; }
    }
}