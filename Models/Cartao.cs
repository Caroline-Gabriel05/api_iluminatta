namespace Iluminatta.Api.Models
{
    public class Cartao
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public int Tipo { get; set; }
        public int Numero { get; set; }
        public string Nome { get; set; }
        public int Validade { get; set; }
        public int Cvv { get; set; }
        public Cliente Cliente { get; set; }
    }
}