namespace Iluminatta.Api.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public string User { get; set; }
        
        public Cliente Cliente { get; set; }
    }
}