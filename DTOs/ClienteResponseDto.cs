namespace Iluminatta.Api.DTOs
{
    public class ClienteResponseDto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Sobrenome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Cep { get; set; } = string.Empty;
    }
}