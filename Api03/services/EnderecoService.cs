using Api03.models;
using Api03.repositories;

namespace Api03.services;

public interface IEnderecoService
{
    Task<List<Endereco?>> ListarPorFuncionariAsync(int funcionarioId);
    Task<Endereco?> ObterPorIdAsync(int id);
    Task<Endereco?> CriarAsync(int funcionarioId, Endereco endereco);
    Task<Endereco?> AtualizarAsync(int id, Endereco endereco);
    Task<bool> RemoverAsync(int id);
}

public class EnderecoService : IEnderecoService
{
    private readonly IEnderecoRepository _repository;
    private readonly IFuncionarioioRepository  _funcionarioioRepository;

    public EnderecoService(IEnderecoRepository repository,  IFuncionarioioRepository funcionarioioRepository)
    {
        _repository = repository;
        _funcionarioioRepository = funcionarioioRepository;
    }
    
    
    public async Task<List<Endereco?>> ListarPorFuncionariAsync(int funcionarioId)
    {
        if(!await _funcionarioioRepository.ExisteAsync(funcionarioId)) return null;
        return await _repository.ListarPorFuncionarioAsync(funcionarioId);
    }

    public Task<Endereco?> ObterPorIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Endereco?> CriarAsync(int funcionarioId, Endereco endereco)
    {
        throw new NotImplementedException();
    }

    public Task<Endereco?> AtualizarAsync(int id, Endereco endereco)
    {
        throw new NotImplementedException();
    }

    public Task<bool> RemoverAsync(int id)
    {
        throw new NotImplementedException();
    }
}