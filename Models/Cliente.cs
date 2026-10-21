namespace Iluminatta.Api.Models
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Genero { get; set; }
        public DateOnly DataNascimento { get; set; }
        public string Cpf { get; set; }
        public string Cep { get; set; }
        public string Cidade { get; set; }
        public string Bairro { get; set; }
        public string Rua { get; set; }
        public string Numero { get; set; }
        public string? Email { get; set; }

        public ICollection<Pedido> Pedidos { get; set; }
        public ICollection<Usuario> Usuarios { get; set; }
    }
}