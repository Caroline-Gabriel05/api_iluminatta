namespace Iluminatta.Api.Models
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Genero { get; set; }
        public DateOnly DataNascimento { get; set; }
        public int Cpf { get; set; }
        public int Cep { get; set; }
        public string Cidade { get; set; }
        public string Bairro { get; set; }
        public string Rua { get; set; }
        public int Numero { get; set; }

        public ICollection<Usuario> Usuarios { get; set; }
        public ICollection<Cartao> Cartoes { get; set; }
    }
}