using System.ComponentModel.DataAnnotations;

namespace Iluminatta.Api.DTOs;

public class PagamentoDto
{
    
    [Range(1, int.MaxValue, ErrorMessage = "O ID do pedido deve ser maior que zero.")]
    public int PedidoId { get; set; }

    [Required(ErrorMessage = "O método de pagamento é obrigatório.")]
    [StringLength(30, ErrorMessage = "O método de pagamento deve ter no máximo 30 caracteres.")]
    public string MetodoPagamento { get; set; } = string.Empty;
}