using Api03.models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Api03.controllers;


[ApiController]
[Route("api/funcionario/{funcionarioId:int}/[controller]")]
public class EnderecoController : ControllerBase
{
    private readonly IEnderecoService _service;

    public EnderecoController(IEnderecoService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<Endereco>> Listar(int funcionarioId)
    {
        var enderecos = await _service.ListarPorFuncionarioAsync(funcionarioId);
        return enderecos is null ? NotFound() : Ok(enderecos);
    }

    [HttpGet("{id:int}", Name = "ObterEndereco")]
    public async Task<ActionResult<Endereco>> Obter(int funcionarioId, int id)
    {
        var endereco = await _service.ObterAsync(id);
        
        if(endereco is null || endereco.FuncionarioId != funcionarioId)
            return NotFound();
        return Ok(endereco);
    
    }
    
    [HttpPost]
    public async Task<ActionResult<Endereco>> Inserir(int funcionarioId, Endereco endereco)
    {
        endereco.FuncionarioId = funcionarioId;
        var novoEndereco = await _service.CriarAsync(funcionarioId, endereco);
        
        if(novoEndereco is null)
            return NotFound(new { mensagem = "Funcionário não encontrado" });
        
        return CreatedAtRoute("ObterEndereco", 
            new { funcionarioId = funcionarioId, id = novoEndereco.Id }, novoEndereco);
    }
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Atualizar(int id, [FromBody] Endereco endereco)
    {
        var enderecoExistente = await _service.ObterAsync(id);
        if (enderecoExistente is null)
            return NotFound();

        endereco.Id = id;
        var enderecoAtualizado = await _service.AtualizarAsync(id, endereco);
        return Ok(enderecoAtualizado);
    }
    
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Remover(int id)
    {
        var enderecoExistente = await _service.ObterAsync(id);
        if (enderecoExistente is null)
            return NotFound();

        var enderecoRemovido = await _service.RemoverAsync(id);
        return NoContent();
    }

}