using Iluminatta.Api.Data;
using Iluminatta.Api.DTOs;
using Iluminatta.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace Iluminatta.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientesController : ControllerBase
{
    private readonly AppDbContext _context;

    public ClientesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetClientes()
    {
        var clientes = await _context.Clientes
            .Select(c => new
            {
                c.Id,
                c.Nome,
                c.Sobrenome,
                c.Genero,
                c.DataNascimento,
                c.Cpf,
                c.Cep,
                c.Cidade,
                c.Bairro,
                c.Rua,
                c.Numero
            })
            .ToListAsync();

        return Ok(clientes);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetClientePorId(int id)
    {
        var cliente = await _context.Clientes
            .Where(c => c.Id == id)
            .Select(c => new
            {
                c.Id,
                c.Nome,
                c.Sobrenome,
                c.Genero,
                c.DataNascimento,
                c.Cpf,
                c.Cep,
                c.Cidade,
                c.Bairro,
                c.Rua,
                c.Numero
            })
            .FirstOrDefaultAsync();

        if (cliente == null)
        {
            return NotFound(new { mensagem = "Cliente não encontrado." });
        }

        return Ok(cliente);
    }

    [HttpPost]
    public async Task<IActionResult> CriarCliente(ClienteDto clienteDto)
    {
        var cliente = new Cliente
        {
            Nome = clienteDto.Nome,
            Sobrenome = clienteDto.Sobrenome,
            Genero = clienteDto.Genero,
            DataNascimento = clienteDto.DataNascimento,
            Cpf = clienteDto.Cpf,
            Cep = clienteDto.Cep,
            Cidade = clienteDto.Cidade,
            Bairro = clienteDto.Bairro,
            Rua = clienteDto.Rua,
            Numero = clienteDto.Numero
        };

        _context.Clientes.Add(cliente);

        await _context.SaveChangesAsync();

        return Ok(cliente);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> EditarCliente(int id, ClienteDto clienteDto)
    {
        var cliente = await _context.Clientes
            .FirstOrDefaultAsync(c => c.Id == id);

        if (cliente == null)
        {
            return NotFound(new { mensagem = "Cliente não encontrado." });
        }

        cliente.Nome = clienteDto.Nome;
        cliente.Sobrenome = clienteDto.Sobrenome;
        cliente.Genero = clienteDto.Genero;
        cliente.DataNascimento = clienteDto.DataNascimento;
        cliente.Cpf = clienteDto.Cpf;
        cliente.Cep = clienteDto.Cep;
        cliente.Cidade = clienteDto.Cidade;
        cliente.Bairro = clienteDto.Bairro;
        cliente.Rua = clienteDto.Rua;
        cliente.Numero = clienteDto.Numero;

        await _context.SaveChangesAsync();

        return Ok(cliente);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> ExcluirCliente(int id)
    {
        var cliente = await _context.Clientes
            .FirstOrDefaultAsync(c => c.Id == id);

        if (cliente == null)
        {
            return NotFound(new { mensagem = "Cliente não encontrado." });
        }

        _context.Clientes.Remove(cliente);

        await _context.SaveChangesAsync();

        return Ok(new { mensagem = "Cliente excluído com sucesso." });
    }
}