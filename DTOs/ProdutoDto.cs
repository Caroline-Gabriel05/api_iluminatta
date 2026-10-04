namespace Iluminatta.Api.DTOs;

public class ProdutoDto
{
    public int MarcaId { get; set; }
    public int CategoriaId { get; set; }
    public string Nome { get; set; }
    public string Modelo { get; set; }
    public string Descricao { get; set; }
    public decimal Preco { get; set; }
    public int Estoque { get; set; }
}