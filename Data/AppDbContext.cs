using Microsoft.EntityFrameworkCore;
using Iluminatta.Api.Models;

namespace Iluminatta.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Cartao> Cartoes { get; set; }
    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<ImagemProduto> ImagensProdutos { get; set; }
    public DbSet<ItemPedido> ItensPedidos { get; set; }
    public DbSet<Marca> Marcas { get; set; }
    public DbSet<Pedido> Pedidos { get; set; }
    public DbSet<Produto> Produtos { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }
}