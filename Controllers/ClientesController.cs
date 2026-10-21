using Iluminatta.Api.Data;
using Iluminatta.Api.DTOs;
using Iluminatta.Api.Models;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    public async Task<ActionResult<IEnumerable<ClienteResponseDto>>> BuscarClientes()
    {
        var clientes = await _context.Clientes
            .Select(c => new ClienteResponseDto
            {
                Id = c.Id,
                Nome = c.Nome,
                Sobrenome = c.Sobrenome,
                Email = c.Email,
                Cep = c.Cep
            })
            .ToListAsync();

        return Ok(clientes);
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<ActionResult<ClienteResponseDto>> BuscarClientePorId(int id)
    {
        var cliente = await _context.Clientes.FindAsync(id);

        if (cliente == null)
        {
            return NotFound();
        }

        var dto = new ClienteResponseDto
        {
            Id = cliente.Id,
            Nome = cliente.Nome,
            Sobrenome = cliente.Sobrenome,
            Email = cliente.Email,
            Cep = cliente.Cep
        };

        return Ok(dto);
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<ClienteResponseDto>> CriarCliente([FromBody] ClienteCreateDto dto)
    {
        var cpfExiste = await _context.Clientes.AnyAsync(c => c.Cpf == dto.Cpf);
        if (cpfExiste)
        {
            return BadRequest(new { mensagem = "Já existe um cliente cadastrado com este CPF." });
        }

        var cliente = new Cliente
        {
            Nome = dto.Nome,
            Sobrenome = dto.Sobrenome,
            Genero = dto.Genero,
            DataNascimento = dto.DataNascimento,
            Cpf = dto.Cpf,
            Cep = dto.Cep,
            Cidade = dto.Cidade,
            Bairro = dto.Bairro,
            Rua = dto.Rua,
            Numero = dto.Numero,
            Email = dto.Email
        };

        _context.Clientes.Add(cliente);
        await _context.SaveChangesAsync();

        var responseDto = new ClienteResponseDto
        {
            Id = cliente.Id,
            Nome = cliente.Nome,
            Sobrenome = cliente.Sobrenome,
            Email = cliente.Email,
            Cep = cliente.Cep
        };

        return CreatedAtAction(nameof(BuscarClientePorId), new { id = cliente.Id }, responseDto);
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> EditarCliente(int id, ClienteUpdateDto clienteDto)
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
        cliente.Cep = clienteDto.Cep;
        cliente.Cidade = clienteDto.Cidade;
        cliente.Bairro = clienteDto.Bairro;
        cliente.Rua = clienteDto.Rua;
        cliente.Numero = clienteDto.Numero;
        cliente.Email = clienteDto.Email;

        await _context.SaveChangesAsync();

        return Ok(cliente);
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> ExcluirCliente(int id)
    {
        var cliente = await _context.Clientes
            .Include(c => c.Pedidos)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (cliente == null)
        {
            return NotFound(new { mensagem = "Cliente não encontrado." });
        }

        if (cliente.Pedidos != null && cliente.Pedidos.Any())
        {
            return Conflict(new { mensagem = "Não é possível excluir este cliente pois existem pedidos vinculados ao histórico dele." });
        }
        _context.Clientes.Remove(cliente);

        await _context.SaveChangesAsync();

        return NoContent();
    }
}