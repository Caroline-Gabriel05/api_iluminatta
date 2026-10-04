namespace Iluminatta.Api.DTOs;

public class ClienteDto
{
    public string Nome { get; set; }
    public string Sobrenome { get; set; }
    public string Genero { get; set; }
    public DateOnly DataNascimento { get; set; }
    public string Cpf { get; set; }
    public int Cep { get; set; }
    public string Cidade { get; set; }
    public string Bairro { get; set; }
    public string Rua { get; set; }
    public int Numero { get; set; }
}