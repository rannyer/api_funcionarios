using Api03.models;
using Api03.services;
using Microsoft.AspNetCore.Mvc;

namespace Api03.controllers;


[ApiController]
[Route("api/[controller]")]
public class SetoresController : ControllerBase
{
    private readonly ISetorService _service;
 
    public SetoresController(ISetorService service) => _service = service;
 
    [HttpGet]
    public async Task<ActionResult<List<Setor>>> Listar() =>
        Ok(await _service.ListarAsync());
 
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Setor>> Obter(int id)
    {
        var setor = await _service.ObterPorIdAsync(id);
        return setor is null ? NotFound() : Ok(setor);
    }
 
    [HttpPost]
    public async Task<ActionResult<Setor>> Criar(Setor setor)
    {
        var criado = await _service.CriarAsync(setor);
        return CreatedAtAction(nameof(Obter), new { id = criado.Id }, criado);
    }
 
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Atualizar(int id, Setor setor)
    {
        var atualizado = await _service.AtualizarAsync(id, setor);
        return atualizado ? NoContent() : NotFound();
    }
 
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Remover(int id)
    {
        var (removido, erro) = await _service.RemoverAsync(id);
 
        if (removido) return NoContent();
        return erro is null ? NotFound() : BadRequest(new { mensagem = erro });
    }
}
