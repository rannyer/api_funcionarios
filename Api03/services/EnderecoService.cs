
using Api03.models;
using Api03.repositories;

public interface IEnderecoService
{
    Task<List<Endereco>?> ListarPorFuncionarioAsync(int funcionarioId);
    Task<Endereco?> ObterAsync(int id);
    Task<Endereco?> CriarAsync(int funcionarioId, Endereco endereco);
    Task<bool> AtualizarAsync(int id, Endereco endereco);
    Task<bool> RemoverAsync(int id);
}
 
public class EnderecoService : IEnderecoService
{
    private readonly IEnderecoRepository _repository;
    private readonly IFuncionarioioRepository _funcionarioRepository;
 
    public EnderecoService(IEnderecoRepository repository, IFuncionarioioRepository funcionarioRepository)
    {
        _repository = repository;
        _funcionarioRepository = funcionarioRepository;
    }
 
    public async Task<List<Endereco>?> ListarPorFuncionarioAsync(int funcionarioId)
    {
        if (!await _funcionarioRepository.ExisteAsync(funcionarioId)) return null;
        return await _repository.ListarPorFuncionarioAsync(funcionarioId);
    }
 
    public Task<Endereco?> ObterAsync(int id) => _repository.ObterPorIdAsync(id);
 
    public async Task<Endereco?> CriarAsync(int funcionarioId, Endereco endereco)
    {
        if (!await _funcionarioRepository.ExisteAsync(funcionarioId)) return null;
 
     
        endereco.Funcionario = null;
        endereco.FuncionarioId = funcionarioId; 
 
        await _repository.AdicionarAsync(endereco);
        return endereco;
    }
 
    public async Task<bool> AtualizarAsync(int id, Endereco endereco)
    {
        var existente = await _repository.ObterPorIdAsync(id);
        if (existente is null) return false;
 
        existente.Pais = endereco.Pais;
        existente.Numero = endereco.Numero;
        existente.Bairro = endereco.Bairro;
        existente.Cidade = endereco.Cidade;
        existente.Rua = endereco.Rua;
        existente.Cep = endereco.Cep;
 
        await _repository.AtualizarAsync(existente);
        return true;
    }
 
    public async Task<bool> RemoverAsync(int id)
    {
        var endereco = await _repository.ObterPorIdAsync(id);
        if (endereco is null) return false;
 
        await _repository.RemoverAsync(endereco);
        return true;
    }
}