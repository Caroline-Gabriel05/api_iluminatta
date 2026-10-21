using Iluminatta.Api.Data;
using Iluminatta.Api.DTOs;
using Iluminatta.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Iluminatta.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MarcasController : ControllerBase
{
    private readonly AppDbContext _context;

    public MarcasController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> BuscaMarcas()
    {
        var marcas = await _context.Marcas
        .Select(c => new
        {
            c.Id,
            c.Nome
        })
        .ToListAsync();
        return Ok(marcas);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> BuscaMarcaPorId(int id)
    {
        var marca = await _context.Marcas
            .Where(p => p.Id == id)
            .Select(p => new
            {
                p.Id,
                p.Nome,
            })
            .FirstOrDefaultAsync();

        if (marca == null)
        {
            return NotFound(new { mensagem = "Marca não encontrada." });
        }

        return Ok(marca);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CriarMarca(MarcaDto marcaDto)
    {
        var marca = new Marca
        {
            Nome = marcaDto.Nome,
        };

        _context.Marcas.Add(marca);
        await _context.SaveChangesAsync();
        return Ok(marca);
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> EditarMarca(int id, MarcaDto marcaDto)
    {
        var marca = await _context.Marcas
            .FirstOrDefaultAsync(p => p.Id == id);
        if (marca == null)
        {
            return NotFound(new { mensagem = "Marca não encontrada."});
        }

        marca.Nome = marcaDto.Nome;

        await _context.SaveChangesAsync();

        return Ok(marca);
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> ExcluirMarca(int id)
    {
        var marca = await _context.Marcas
            .FirstOrDefaultAsync(p => p.Id == id);
        if(marca == null)
        {
            return NotFound(new { mensagem = "Marca não encontrada."});
        }
        _context.Marcas.Remove(marca);
        await _context.SaveChangesAsync();
        return Ok(new { mensagem = "Marca excluída com sucesso."});
    }
}