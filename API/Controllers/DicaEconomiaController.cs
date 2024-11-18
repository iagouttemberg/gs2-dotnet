using API.DTO.Request;
using Database.Models;
using Microsoft.AspNetCore.Mvc;
using Repository;

namespace API.Controllers;

/// <summary>
/// Controlador para gerenciar operações relacionadas a dicas de economia.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class DicasEconomiaController : ControllerBase
{
    private readonly IRepository<DicaEconomia> _dicaRepository;

    public DicasEconomiaController(IRepository<DicaEconomia> dicaRepository)
    {
        _dicaRepository = dicaRepository;
    }

    /// <summary>
    /// Retorna todas as dicas de economia cadastradas.
    /// </summary>
    /// <returns>Lista de dicas de economia.</returns>
    /// <response code="200">Retorna a lista de dicas de economia.</response>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var dicas = await _dicaRepository.GetAllAsync();
        return Ok(dicas);
    }

    /// <summary>
    /// Retorna uma dica de economia pelo ID.
    /// </summary>
    /// <param name="id">ID da dica de economia.</param>
    /// <returns>Dica de economia correspondente ao ID fornecido.</returns>
    /// <response code="200">Retorna a dica de economia.</response>
    /// <response code="404">Dica de economia não encontrada.</response>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var dica = await _dicaRepository.GetByIdAsync(id);
        if (dica == null)
        {
            return NotFound("Dica não encontrada");
        }
        return Ok(dica);
    }

    /// <summary>
    /// Cria uma nova dica de economia.
    /// </summary>
    /// <param name="dicaRequest">Dados da dica de economia a ser criada.</param>
    /// <returns>Dica de economia criada.</returns>
    /// <response code="201">Dica de economia criada com sucesso.</response>
    /// <response code="400">Dados inválidos.</response>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] DicaEconomiaRequest dicaRequest)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var dica = new DicaEconomia
        {
            Titulo = dicaRequest.Titulo,
            Descricao = dicaRequest.Descricao
        };

        await _dicaRepository.AddAsync(dica);
        return CreatedAtAction(nameof(GetById), new { id = dica.Id }, dica);
    }

    /// <summary>
    /// Atualiza uma dica de economia existente.
    /// </summary>
    /// <param name="id">ID da dica de economia a ser atualizada.</param>
    /// <param name="dicaRequest">Novos dados da dica de economia.</param>
    /// <returns>Resultado da operação.</returns>
    /// <response code="204">Dica de economia atualizada com sucesso.</response>
    /// <response code="404">Dica de economia não encontrada.</response>
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] DicaEconomiaRequest dicaRequest)
    {
        var dica = await _dicaRepository.GetByIdAsync(id);
        if (dica == null)
        {
            return NotFound("Dica não encontrada");
        }

        dica.Titulo = dicaRequest.Titulo;
        dica.Descricao = dicaRequest.Descricao;

        await _dicaRepository.UpdateAsync(dica);
        return NoContent();
    }

    /// <summary>
    /// Deleta uma dica de economia pelo ID.
    /// </summary>
    /// <param name="id">ID da dica de economia a ser deletada.</param>
    /// <returns>Resultado da operação.</returns>
    /// <response code="204">Dica de economia deletada com sucesso.</response>
    /// <response code="404">Dica de economia não encontrada.</response>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var dica = await _dicaRepository.GetByIdAsync(id);
        if (dica == null)
        {
            return NotFound("Dica não encontrada");
        }

        await _dicaRepository.DeleteAsync(dica);
        return NoContent();
    }
}
