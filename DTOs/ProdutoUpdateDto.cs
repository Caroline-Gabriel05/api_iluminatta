using System.ComponentModel.DataAnnotations;

namespace Iluminatta.Api.DTOs
{
    public class ProdutoUpdateDto
    {
        [Required(ErrorMessage = "O nome do produto é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres.")]
        public string Nome { get; set; } = string.Empty;

        public string? Modelo { get; set; }
        public string? Descricao { get; set; }

        [Range(0.01, 1000000.00, ErrorMessage = "O preço deve ser maior que zero.")]
        public decimal Preco { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "O estoque não pode ser negativo.")]
        public int Estoque { get; set; }

        [Required(ErrorMessage = "A Marca é obrigatória.")]
        public int MarcaId { get; set; }

        [Required(ErrorMessage = "A Categoria é obrigatória.")]
        public int CategoriaId { get; set; }
    }
}