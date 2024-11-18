using API.DTO.Request;
using API.DTO.Response;
using Database.Models;
using Microsoft.AspNetCore.Mvc;
using Repository;

namespace API.Controllers;

/// <summary>
/// Controlador para gerenciar operações relacionadas a consumos energéticos.
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class ConsumoEnergeticoController : ControllerBase
{
    private readonly IRepository<ConsumoEnergetico> _consumoEnergeticoRepository;

    public ConsumoEnergeticoController(IRepository<ConsumoEnergetico> consumoEnergeticoRepository)
    {
        _consumoEnergeticoRepository = consumoEnergeticoRepository;
    }

    /// <summary>
    /// Retorna todos os consumos energéticos cadastrados.
    /// </summary>
    /// <returns>Lista de consumos energéticos.</returns>
    /// <response code="200">Retorna a lista de consumos energéticos.</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var consumos = await _consumoEnergeticoRepository.GetAllAsync();
        var consumoResponses = consumos.Select(ce => new ConsumoEnergeticoResponse
        {
            Id = ce.Id,
            Mes = ce.Mes,
            Ano = ce.Ano,
            ConsumoKWh = ce.ConsumoKWh,
            UsuarioId = ce.UsuarioId
        }).ToList();

        return Ok(consumoResponses);
    }

    /// <summary>
    /// Retorna um consumo energético pelo ID.
    /// </summary>
    /// <param name="id">ID do consumo energético.</param>
    /// <returns>Consumo energético correspondente ao ID fornecido.</returns>
    /// <response code="200">Retorna o consumo energético.</response>
    /// <response code="404">Consumo energético não encontrado.</response>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        var consumo = await _consumoEnergeticoRepository.GetByIdAsync(id);
        if (consumo == null)
        {
            return NotFound("Consumo energético não encontrado");
        }

        var consumoResponse = new ConsumoEnergeticoResponse
        {
            Id = consumo.Id,
            Mes = consumo.Mes,
            Ano = consumo.Ano,
            ConsumoKWh = consumo.ConsumoKWh,
            UsuarioId = consumo.UsuarioId
        };

        return Ok(consumoResponse);
    }

    /// <summary>
    /// Cria um novo consumo energético.
    /// </summary>
    /// <param name="consumoRequest">Dados do consumo energético a ser criado.</param>
    /// <returns>Consumo energético criado.</returns>
    /// <response code="201">Consumo energético criado com sucesso.</response>
    /// <response code="400">Dados inválidos.</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] ConsumoEnergeticoRequest consumoRequest)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var consumo = new ConsumoEnergetico
        {
            Mes = consumoRequest.Mes,
            Ano = consumoRequest.Ano,
            ConsumoKWh = consumoRequest.ConsumoKWh,
            UsuarioId = consumoRequest.UsuarioId
        };

        await _consumoEnergeticoRepository.AddAsync(consumo);

        var consumoResponse = new ConsumoEnergeticoResponse
        {
            Id = consumo.Id,
            Mes = consumo.Mes,
            Ano = consumo.Ano,
            ConsumoKWh = consumo.ConsumoKWh,
            UsuarioId = consumo.UsuarioId
        };

        return CreatedAtAction(nameof(GetById), new { id = consumo.Id }, consumoResponse);
    }

    /// <summary>
    /// Atualiza um consumo energético existente.
    /// </summary>
    /// <param name="id">ID do consumo energético a ser atualizado.</param>
    /// <param name="consumoRequest">Novos dados do consumo energético.</param>
    /// <returns>Resultado da operação.</returns>
    /// <response code="204">Consumo energético atualizado com sucesso.</response>
    /// <response code="404">Consumo energético não encontrado.</response>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, [FromBody] ConsumoEnergeticoRequest consumoRequest)
    {
        var consumo = await _consumoEnergeticoRepository.GetByIdAsync(id);
        if (consumo == null)
        {
            return NotFound("Consumo energético não encontrado");
        }

        consumo.Mes = consumoRequest.Mes;
        consumo.Ano = consumoRequest.Ano;
        consumo.ConsumoKWh = consumoRequest.ConsumoKWh;
        consumo.UsuarioId = consumoRequest.UsuarioId;

        await _consumoEnergeticoRepository.UpdateAsync(consumo);
        return NoContent();
    }

    /// <summary>
    /// Deleta um consumo energético pelo ID.
    /// </summary>
    /// <param name="id">ID do consumo energético a ser deletado.</param>
    /// <returns>Resultado da operação.</returns>
    /// <response code="204">Consumo energético deletado com sucesso.</response>
    /// <response code="404">Consumo energético não encontrado.</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var consumo = await _consumoEnergeticoRepository.GetByIdAsync(id);
        if (consumo == null)
        {
            return NotFound("Consumo energético não encontrado");
        }

        await _consumoEnergeticoRepository.DeleteAsync(consumo);
        return NoContent();
    }
}
