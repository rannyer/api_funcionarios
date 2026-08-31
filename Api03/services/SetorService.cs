using Api03.models;
using Api03.repositories;

namespace Api03.services;


public interface ISetorService
{
    Task<List<Setor>> ListarAsync();
    Task<Setor?> ObterPorIdAsync(int id);
    Task<Setor> CriarAsync(Setor setor);
    Task<bool> AtualizarAsync(int id, Setor setor);
    Task<(bool Removido, string? Erro)> RemoverAsync(int id);
}

public class SetorService : ISetorService
{
    private readonly ISetorRepository _repository;

    public SetorService(ISetorRepository setorRepository)
    {
        _repository = setorRepository;
    }
    
    public Task<List<Setor>> ListarAsync() => _repository.ListarAsync();

    public Task<Setor?> ObterPorIdAsync(int id) => _repository.ObterPorIdAsync(id);

    public async Task<Setor> CriarAsync(Setor setor)
    {
        await _repository.AdicionarAsync(setor);
        return setor;
    }

    public async Task<bool> AtualizarAsync(int id, Setor setor)
    {
        var existente = await _repository.ObterPorIdAsync(setor.Id);
        if (existente == null) return false;
        
        existente.Nome = setor.Nome;
        await _repository.AtualizarAsync(existente);
        return true;
    }

    public async Task<(bool Removido, string? Erro)> RemoverAsync(int id)
    {
        var setor = await _repository.ObterPorIdAsync(id);
        if (setor is null) return (false, null);

        if (setor.Funcionarios.Any())
            return (false, "Nao é possivel remover um setor que possui funcionarios vinculados");

        await _repository.RemoverAsync(setor);
        return (true, null);
    }
}