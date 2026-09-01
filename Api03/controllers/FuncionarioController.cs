using Api03.dtos;
using Api03.models;
using Api03.repositories;
using Api03.services;
using Microsoft.AspNetCore.Mvc;

namespace Api03.controllers;

[ApiController]
[Route("api/[controller]")]
public class FuncionarioController :ControllerBase
{
    private readonly IFuncionarioService _service;
    
    public FuncionarioController(IFuncionarioService service)
    {
        _service = service;
    }
    
    [HttpGet]
    public async Task<ActionResult<List<Funcionario>>> Listar([FromQuery] int? setorId) 
    {
        return Ok(await _service.ListarAsync(setorId));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Funcionario>> BuscarPorId(int id)
    {
        var funcionario = await _service.ObterPorIdAsync(id);
        return funcionario is null ? NotFound() : Ok(funcionario);
    }
    
    [HttpPost]
    public async Task<ActionResult<Funcionario>> Criar(FuncionarioDto dto)
    {
        var (criado, erro) = await _service.CriarAsync(dto);
        if (erro is not null) return BadRequest(new { mensagem = erro });
        return CreatedAtAction(nameof(BuscarPorId), new { id = criado!.Id }, criado);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Funcionario>> Atualizar(int id, Funcionario funcionario)
    {
        var (atualizado, erro) = await _service.AtualizarAsync(id, funcionario);
        if (atualizado) NoContent();
        return erro is null ? NotFound() : BadRequest(new {mensagem = erro});
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Remover(int id)
    {
        var removido = await _service.RemoverAsync(id);
        return removido ? NoContent() : NotFound();
    }



}