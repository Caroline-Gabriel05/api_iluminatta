using Iluminatta.Api.Data;
using Iluminatta.Api.DTOs;
using Iluminatta.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Iluminatta.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProdutosController : ControllerBase
{
    private readonly AppDbContext _context;

    public ProdutosController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetProdutos()
    {
        var produtos = await _context.Produtos
        .Select(p => new
        {
            p.Id,
            p.Nome,
            p.Modelo,
            p.Descricao,
            p.Preco,
            p.Estoque,
            Categoria = p.Categoria != null ? p.Categoria.Nome : "Sem Categoria",
            Marca = p.Marca != null ? p.Marca.Nome : "Sem Marca",
            Imagens = p.ImagensProdutos
                .Select(i => i.CaminhoImagem)
                .ToList()
        })
        .ToListAsync();
        return Ok(produtos);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProdutoPorId(int id)
    {
        var produto = await _context.Produtos
            .Where(p => p.Id == id)
            .Select(p => new
            {
                p.Id,
                p.Nome,
                p.Modelo,
                p.Descricao,
                p.Preco,
                p.Estoque,
                Categoria = p.Categoria != null ? p.Categoria.Nome : "Sem Categoria",
                Marca = p.Marca != null ? p.Marca.Nome : "Sem Marca",
                Imagens = p.ImagensProdutos
                .Select(i => i.CaminhoImagem)
                .ToList()
            })
            .FirstOrDefaultAsync();

        if (produto == null)
        {
            return NotFound(new { mensagem = "Produto não encontrado." });
        }

        return Ok(produto);
    }

    [HttpPost("{id}/imagens")]
    public async Task<IActionResult> AdicionarImagem(int id, ImagemProdutoDto imagemDto)
    {
        var produto = await _context.Produtos
            .FirstOrDefaultAsync(p => p.Id == id);

        if (produto == null)
        {
            return NotFound(new { mensagem = "Produto não encontrado." });
        }

        var imagem = new ImagemProduto
        {
            ProdutoId = id,
            CaminhoImagem = imagemDto.CaminhoImagem
        };

        _context.ImagensProdutos.Add(imagem);

        await _context.SaveChangesAsync();

        return Ok(new
        {
            imagem.Id,
            imagem.ProdutoId,
            imagem.CaminhoImagem
        });
    }

    [HttpDelete("{produtoId}/imagens/{imagemId}")]
    public async Task<IActionResult> ExcluirImagem(int produtoId, int imagemId)
    {
        var imagem = await _context.ImagensProdutos
            .FirstOrDefaultAsync(i =>
                i.Id == imagemId &&
                i.ProdutoId == produtoId);

        if (imagem == null)
        {
            return NotFound(new { mensagem = "Imagem não encontrada para este produto." });
        }

        _context.ImagensProdutos.Remove(imagem);

        await _context.SaveChangesAsync();

        return Ok(new { mensagem = "Imagem excluída com sucesso." });
    }

    [HttpPost]
    public async Task<IActionResult> CriarProduto(ProdutoDto produtoDto)
    {
        var produto = new Produto
        {
            MarcaId = produtoDto.MarcaId,
            CategoriaId = produtoDto.CategoriaId,
            Nome = produtoDto.Nome,
            Modelo = produtoDto.Modelo,
            Descricao = produtoDto.Descricao,
            Preco = produtoDto.Preco,
            Estoque = produtoDto.Estoque
        };

        _context.Produtos.Add(produto);
        await _context.SaveChangesAsync();
        return Ok(produto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> EditarProduto(int id, ProdutoDto produtoDto)
    {
        var produto = await _context.Produtos
            .FirstOrDefaultAsync(p => p.Id == id);
        if (produto == null)
        {
            return NotFound(new { mensagem = "Produto não encontrado."});
        }

        produto.MarcaId = produtoDto.MarcaId;
        produto.CategoriaId = produtoDto.CategoriaId;
        produto.Nome = produtoDto.Nome;
        produto.Modelo = produtoDto.Modelo;
        produto.Descricao = produtoDto.Descricao;
        produto.Preco = produtoDto.Preco;
        produto.Estoque = produtoDto.Estoque;

        await _context.SaveChangesAsync();

        return Ok(produto);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> ExcluirProduto(int id)
    {
        var produto = await _context.Produtos
            .FirstOrDefaultAsync(p => p.Id == id);
        if(produto == null)
        {
            return NotFound(new { mensagem = "Produto não encontrado."});
        }
        _context.Produtos.Remove(produto);
        await _context.SaveChangesAsync();
        return Ok(new { mensagem = "Produto excluído com sucesso."});
    }
}