using API.DTO.Request;
using API.DTO.Response;
using Database.Models;
using Microsoft.AspNetCore.Mvc;
using Repository;
using Services.Email;

namespace API.Controllers;

/// <summary>
/// Controlador para gerenciar operações relacionadas a usuários.
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class UsuarioController : ControllerBase
{
    private readonly IRepository<Usuario> _usuarioRepository;
    private readonly IEmailService _emailService;

    public UsuarioController(IRepository<Usuario> usuarioRepository, IEmailService emailService)
    {
        _usuarioRepository = usuarioRepository;
        _emailService = emailService;
    }

    /// <summary>
    /// Retorna todos os usuários cadastrados.
    /// </summary>
    /// <returns>Lista de usuários.</returns>
    /// <response code="200">Retorna a lista de usuários.</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var usuarios = await _usuarioRepository.GetAllAsync(u => u.ConsumosEnergeticos);
        var usuarioResponses = usuarios.Select(u => new UsuarioResponse
        {
            Id = u.Id,
            FirebaseId = u.FirebaseId,
            Nome = u.Nome,
            Email = u.Email,
            DataCadastro = u.DataCadastro,
            ConsumosEnergeticos = u.ConsumosEnergeticos.Select(ce => new ConsumoEnergeticoResponse
            {
                Id = ce.Id,
                Mes = ce.Mes,
                Ano = ce.Ano,
                ConsumoKWh = ce.ConsumoKWh,
                UsuarioId = ce.UsuarioId
            }).ToList()
        }).ToList();

        return Ok(usuarioResponses);
    }

    /// <summary>
    /// Retorna um usuário pelo ID.
    /// </summary>
    /// <param name="id">ID do usuário.</param>
    /// <returns>Usuário correspondente ao ID fornecido.</returns>
    /// <response code="200">Retorna o usuário.</response>
    /// <response code="404">Usuário não encontrado.</response>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        var usuario = await _usuarioRepository.GetByIdAsync(id, u => u.ConsumosEnergeticos);
        if (usuario == null)
        {
            return NotFound("Usuário não encontrado");
        }

        var usuarioResponse = new UsuarioResponse
        {
            Id = usuario.Id,
            FirebaseId = usuario.FirebaseId,
            Nome = usuario.Nome,
            Email = usuario.Email,
            DataCadastro = usuario.DataCadastro,
            ConsumosEnergeticos = usuario.ConsumosEnergeticos.Select(ce => new ConsumoEnergeticoResponse
            {
                Id = ce.Id,
                Mes = ce.Mes,
                Ano = ce.Ano,
                ConsumoKWh = ce.ConsumoKWh,
                UsuarioId = ce.UsuarioId
            }).ToList()
        };

        return Ok(usuarioResponse);
    }

    /// <summary>
    /// Cria um novo usuário.
    /// </summary>
    /// <param name="usuarioRequest">Dados do usuário a ser criado.</param>
    /// <returns>Usuário criado.</returns>
    /// <response code="201">Usuário criado com sucesso.</response>
    /// <response code="400">Dados inválidos.</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] UsuarioRequest usuarioRequest)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var usuario = new Usuario
        {
            FirebaseId = usuarioRequest.FirebaseId,
            Nome = usuarioRequest.Nome,
            Email = usuarioRequest.Email
        };

        await _usuarioRepository.AddAsync(usuario);

        var subject = "Bem-vindo(a)!";
        var body = $"Olá {usuario.Nome}, bem-vindo(a) ao EcoTrack!";
        await _emailService.SendEmailAsync(usuario.Email, subject, body);

        return CreatedAtAction(nameof(GetById), new { id = usuario.Id }, usuario);
    }

    /// <summary>
    /// Atualiza um usuário existente.
    /// </summary>
    /// <param name="id">ID do usuário a ser atualizado.</param>
    /// <param name="usuarioRequest">Novos dados do usuário.</param>
    /// <returns>Resultado da operação.</returns>
    /// <response code="204">Usuário atualizado com sucesso.</response>
    /// <response code="404">Usuário não encontrado.</response>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, [FromBody] UsuarioRequest usuarioRequest)
    {
        var usuario = await _usuarioRepository.GetByIdAsync(id);
        if (usuario == null)
        {
            return NotFound("Usuário não encontrado");
        }

        usuario.FirebaseId = usuarioRequest.FirebaseId;
        usuario.Nome = usuarioRequest.Nome;
        usuario.Email = usuarioRequest.Email;

        await _usuarioRepository.UpdateAsync(usuario);
        return NoContent();
    }

    /// <summary>
    /// Deleta um usuário pelo ID.
    /// </summary>
    /// <param name="id">ID do usuário a ser deletado.</param>
    /// <returns>Resultado da operação.</returns>
    /// <response code="204">Usuário deletado com sucesso.</response>
    /// <response code="404">Usuário não encontrado.</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var usuario = await _usuarioRepository.GetByIdAsync(id);
        if (usuario == null)
        {
            return NotFound("Usuário não encontrado");
        }

        await _usuarioRepository.DeleteAsync(usuario);
        return NoContent();
    }
}
