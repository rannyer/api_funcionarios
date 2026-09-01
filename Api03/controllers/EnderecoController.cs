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

    [HttpGet("{id}", Name = "ObterEndereco")]
    public async Task<ActionResult<Endereco>> Obter(int funcionarioId, int id)
    {
        var endereco = await _service.ObterAsync(id);
        
        if(endereco is null || endereco.FuncionarioId != funcionarioId)
            return NotFound();
        return Ok(endereco);
    
    }
}