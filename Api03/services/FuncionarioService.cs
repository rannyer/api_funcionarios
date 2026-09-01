using Api03.dtos;
using Api03.models;
using Api03.repositories;

namespace Api03.services;

public interface IFuncionarioService
{
     Task<List<Funcionario>> ListarAsync(int? setorId);
     Task<Funcionario> ObterPorIdAsync(int id);
     Task<(Funcionario? Criado, string? Erro)> CriarAsync(FuncionarioDto funcionario);
     Task<(bool Atualizado, string? Erro)> AtualizarAsync(int id, Funcionario funcionario);
     Task<bool> RemoverAsync(int id);
}

public class FuncionarioService : IFuncionarioService
{
     private readonly IFuncionarioioRepository _repository;
     private readonly ISetorRepository _setorRepository;

     public FuncionarioService(IFuncionarioioRepository repository, ISetorRepository setorRepository)
     {
          _repository = repository;
          _setorRepository = setorRepository;
     }
     
     public Task<List<Funcionario>> ListarAsync(int? setorId) =>
          setorId.HasValue
               ? _repository.ListarPorSetorAsync(setorId.Value)
               : _repository.ListarAsync();

     public Task<Funcionario> ObterPorIdAsync(int id) =>
          _repository.ObterPorIdAsync(id);
     

     public async Task<(Funcionario? Criado, string? Erro)> CriarAsync(FuncionarioDto dto)
     {
          if (!await _setorRepository.ExisteAsync(dto.SetorId))
               return (null, $"Setor {dto.SetorId} não encontrado.");

          var funcionario = new Funcionario
          {
               Nome = dto.Nome,
               Salario = dto.Salario,
               SetorId = dto.SetorId
          };
          
          funcionario.Setor = null;
          funcionario.Enderecos.Clear();
          
          await _repository.AdicionarAsync(funcionario);
          return (await _repository.ObterPorIdAsync(funcionario.Id), null);

     }

     public async Task<(bool Atualizado, string? Erro)> AtualizarAsync(int id, Funcionario funcionario)
     {
          var existente = await _repository.ObterPorIdAsync(id);
          if (existente == null) return (false, null);
          
          if (!await _setorRepository.ExisteAsync(funcionario.SetorId))
               return (false, $"Setor {funcionario.SetorId} não encontrado.");
          
          existente.Nome = funcionario.Nome;
          existente.Salario = funcionario.Salario;
          existente.SetorId = funcionario.SetorId;
          await _repository.AtualizarAsync(existente);
          
          return (true, null);
     }

     public  async Task<bool> RemoverAsync(int id)
     {
          var funcionario = await _repository.ObterPorIdAsync(id);
          if (funcionario is null) return false;
          
          await _repository.RemoverAsync(funcionario);
          return true;
     }
}