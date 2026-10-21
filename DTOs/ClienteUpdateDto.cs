using System.ComponentModel.DataAnnotations;

namespace Iluminatta.Api.DTOs
{
    public class ClienteUpdateDto
    {
        [Required(ErrorMessage = "O nome é obrigatório.")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "O sobrenome é obrigatório.")]
        public string Sobrenome { get; set; } = string.Empty;

        public string? Genero { get; set; }
        public DateOnly DataNascimento { get; set; }

        [Required(ErrorMessage = "O CEP é obrigatório.")]
        [StringLength(9, MinimumLength = 8, ErrorMessage = "O CEP deve ter entre 8 e 9 caracteres.")]
        public string Cep { get; set; } = string.Empty;

        public string? Cidade { get; set; }
        public string? Bairro { get; set; }
        public string? Rua { get; set; }
        public string? Numero { get; set; }

        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "O formato do e-mail é inválido.")]
        public string Email { get; set; } = string.Empty;
    }
}