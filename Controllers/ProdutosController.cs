using Iluminatta.Api.Data;
using Iluminatta.Api.DTOs;
using Iluminatta.Api.Models;
using Microsoft.AspNetCore.Authorization;
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
    public async Task<IActionResult> BuscaProdutos()
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
    public async Task<IActionResult> BuscaProdutoPorId(int id)
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

    
    [HttpPost]
    [Authorize]
    async Task<ActionResult<ProdutoResponseDto>> CriarProduto([FromBody] ProdutoCreateDto dto)
    {
        var marcaExiste = await _context.Marcas.AnyAsync(m => m.Id == dto.MarcaId);
        if (!marcaExiste)
        {
            return BadRequest(new { mensagem = "A marca informada não existe." });
        }
        var produto = new Produto
        {
            Nome = dto.Nome,
            Preco = dto.Preco,
            Estoque = dto.Estoque,
            MarcaId = dto.MarcaId,
            CategoriaId = dto.CategoriaId
        };

        _context.Produtos.Add(produto);
        await _context.SaveChangesAsync();
        
        var responseDto = new ProdutoResponseDto
        {
            Id = produto.Id,
            Nome = produto.Nome,
            Preco = produto.Preco,
            Estoque = produto.Estoque,
            MarcaId = produto.MarcaId,
            CategoriaId = produto.CategoriaId
        };
    return CreatedAtAction(nameof(BuscaProdutoPorId), new { id = produto.Id }, responseDto);
    }

    [HttpPost("{id}/imagens")]
    [Authorize]
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
    [Authorize]
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

    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> EditarProduto(int id, [FromBody] ProdutoUpdateDto dto)
    {
        var produto = await _context.Produtos.FindAsync(id);
        if (produto == null)
        {
            return NotFound(new { mensagem = "Produto não encontrado." });
        }

        var marcaExiste = await _context.Marcas.AnyAsync(m => m.Id == dto.MarcaId);
        if (!marcaExiste)
        {
            return BadRequest(new { mensagem = "A marca informada não existe." });
        }

        var categoriaExiste = await _context.Categorias.AnyAsync(c => c.Id == dto.CategoriaId);
        if (!categoriaExiste)
        {
            return BadRequest(new { mensagem = "A categoria informada não existe." });
        }

        produto.Nome = dto.Nome;
        produto.Modelo = dto.Modelo;
        produto.Descricao = dto.Descricao;
        produto.Preco = dto.Preco;
        produto.Estoque = dto.Estoque;
        produto.MarcaId = dto.MarcaId;
        produto.CategoriaId = dto.CategoriaId;

        _context.Produtos.Update(produto);
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            return Conflict(new { mensagem = "Ocorreu um conflito de concorrência. Este produto foi alterado por outro usuário. Por favor, atualize a página e tente novamente." });
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize]
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