using System.ComponentModel.DataAnnotations;

namespace Iluminatta.Api.DTOs
{
    public class PagamentoCreateDto
    {
        [Required(ErrorMessage = "O ID do pedido é obrigatório.")]
        public int PedidoId { get; set; }

        [Required(ErrorMessage = "O método de pagamento é obrigatório.")]
        [StringLength(50, ErrorMessage = "O método de pagamento deve ter no máximo 50 caracteres.")]
        public string MetodoPagamento { get; set; } = string.Empty;
    }
}