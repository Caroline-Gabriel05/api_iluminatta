using Microsoft.EntityFrameworkCore;
using Iluminatta.Api.Models;
using Microsoft.EntityFrameworkCore.Query.Internal;

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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cartao>(entity =>
        {
            entity.ToTable("tb_cartao");
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Id)
                .HasColumnName("CODIGO");
            entity.Property(p => p.ClienteId)
                .HasColumnName("COD_CLIENTE");
            entity.Property(p => p.Tipo)
                .HasColumnName("TIPO");
            entity.Property(p => p.Numero)
                .HasColumnName("NUMERO");
            entity.Property(p => p.Nome)
                .HasColumnName("NOME");
            entity.Property(p => p.Validade)
                .HasColumnName("VALIDADE");
            entity.Property(p => p.Cvv)
                .HasColumnName("CVV");
        });
        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.ToTable("tb_codigo_prod");
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Id)
                .HasColumnName("COD_CATEGORIA");
            entity.Property(p => p.Nome)
                .HasColumnName("CATEGORIAS");
        });
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.ToTable("tb_cliente");
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Id)
                .HasColumnName("CODIGO");
            entity.Property(p => p.Nome)
                .HasColumnName("NOME");
            entity.Property(p => p.Sobrenome)
                .HasColumnName("SOBRENOME");
            entity.Property(p => p.Genero)
                .HasColumnName("GENERO");
            entity.Property(p => p.DataNascimento)
                .HasColumnName("NASCIMENTO");    
            entity.Property(p => p.Cpf)
                .HasColumnName("CPF");
            entity.Property(p => p.Cep)
                .HasColumnName("CEP");
            entity.Property(p => p.Cidade)
                .HasColumnName("CIDADE");
            entity.Property(p => p.Bairro)
                .HasColumnName("BAIRRO");
            entity.Property(p => p.Rua)
                .HasColumnName("RUA");
            entity.Property(p => p.Numero)
                .HasColumnName("NUMERO");
        });
        modelBuilder.Entity<ImagemProduto>(entity =>
        {
            entity.ToTable("tb_varias_img");
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Id)
                .HasColumnName("CODIGO");
            entity.Property(p => p.ProdutoId)
                .HasColumnName("CHAVE_IMG_PROD");
            entity.Property(p => p.CaminhoImagem)
                .HasColumnName("CAMINHO_IMG");
        });
        modelBuilder.Entity<ItemPedido>(entity =>
        {
            entity.ToTable("tb_item_carrinho");
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Id)
                .HasColumnName("CODIGO");
            entity.Property(p => p.PedidoId)
                .HasColumnName("CODIGO_CARRINHO");
            entity.Property(p => p.ProdutoId)
                .HasColumnName("COD_ITEM");
            entity.Property(p => p.QuantidadeItens)
                .HasColumnName("QUANT_ITEM");
        });
        modelBuilder.Entity<Marca>(entity =>
        {
            entity.ToTable("tb_marca");
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Id)
                .HasColumnName("CODIGO");
            entity.Property(p => p.Nome)
                .HasColumnName("MARCA");
        });
        modelBuilder.Entity<Pedido>(entity =>
        {
            entity.ToTable("tb_carrinho");
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Id)
                .HasColumnName("CODIGO");
            entity.Property(p => p.QuantidadeItens)
                .HasColumnName("QUANT_ITENS");
            entity.Property(p => p.DataPedido)
                .HasColumnName("DATA_PEDIDO");  
            entity.Property(p => p.ClienteId)
                .HasColumnName("COD_CLIENTE");
            entity.Property(p => p.Parcelas)
                .HasColumnName("PARCLAS");
            entity.Property(p => p.ValorTotal)
                .HasColumnName("VALOR_TOTAL");
            entity.Property(p => p.Status)
                .HasColumnName("STATU");
        });
        modelBuilder.Entity<Produto>(entity =>
        {
            entity.ToTable("tb_produto");

            entity.HasKey(p => p.Id);

            entity.Property(p => p.Id)
                .HasColumnName("COD_PRODUTO");

            entity.Property(p => p.MarcaId)
                .HasColumnName("MARCA");

            entity.Property(p => p.CategoriaId)
                .HasColumnName("CATEGORIAS");

            entity.Property(p => p.Nome)
                .HasColumnName("NOME");

            entity.Property(p => p.Modelo)
                .HasColumnName("MODELO");

            entity.Property(p => p.Descricao)
                .HasColumnName("DESCRICAO");

            entity.Property(p => p.Estoque)
                .HasColumnName("ESTOQUE");
        });
        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.ToTable("tb_usuario");
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Id)
                .HasColumnName("CODIGO");
            entity.Property(p => p.ClienteId)
                .HasColumnName("COD_CLIENTE");
            entity.Property(p => p.Login)
                .HasColumnName("LOGINUSU");
            entity.Property(p => p.Senha)
                .HasColumnName("SENHA");
            entity.Property(p => p.User)
                .HasColumnName("USER");
        });
    }
}