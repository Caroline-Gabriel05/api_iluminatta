using Iluminatta.Api.Data;
using Iluminatta.Api.DTOs;
using Iluminatta.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Iluminatta.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriasController : ControllerBase
{
    private readonly AppDbContext _context;

    public CategoriasController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetCategorias()
    {
        var categorias = await _context.Categorias
        .Select(c => new
        {
            c.Id,
            c.Nome
        })
        .ToListAsync();
        return Ok(categorias);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCategoriaPorId(int id)
    {
        var categoria = await _context.Categorias
            .Where(p => p.Id == id)
            .Select(p => new
            {
                p.Id,
                p.Nome,
            })
            .FirstOrDefaultAsync();

        if (categoria == null)
        {
            return NotFound(new { mensagem = "Categoria não encontrada." });
        }

        return Ok(categoria);
    }

    [HttpPost]
    public async Task<IActionResult> CriarCategoria(CategoriaDto categoriaDto)
    {
        var categoria = new Categoria
        {
            Nome = categoriaDto.Nome,
        };

        _context.Categorias.Add(categoria);
        await _context.SaveChangesAsync();
        return Ok(categoria);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> EditarCategoria(int id, CategoriaDto categoriaDto)
    {
        var categoria = await _context.Categorias
            .FirstOrDefaultAsync(p => p.Id == id);
        if (categoria == null)
        {
            return NotFound(new { mensagem = "Categoria não encontrada."});
        }

        categoria.Nome = categoriaDto.Nome;

        await _context.SaveChangesAsync();

        return Ok(categoria);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> ExcluirCategoria(int id)
    {
        var categoria = await _context.Categorias
            .FirstOrDefaultAsync(p => p.Id == id);
        if(categoria == null)
        {
            return NotFound(new { mensagem = "Categoria não encontrada."});
        }
        _context.Categorias.Remove(categoria);
        await _context.SaveChangesAsync();
        return Ok(new { mensagem = "Categoria excluída com sucesso."});
    }
}