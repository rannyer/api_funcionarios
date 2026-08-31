using Api03.infra;
using Api03.models;
using Microsoft.EntityFrameworkCore;

namespace Api03.repositories;

public interface IEnderecoRepository
{
    Task<List<Endereco>> ListarPorFuncionarioAsync(int funcionarioId);
    Task<Endereco?> ObterPorIdAsync(int id);
    Task AdicionarAsync(Endereco endereco);
    Task AtualizarAsync(Endereco endereco);
    Task RemoverAsync(Endereco endereco);
}

public class EnderecoRepository : IEnderecoRepository
{
    
    private readonly AppDbContext _context;
    
    public EnderecoRepository(AppDbContext context)
    {
        this._context = context;
    }
    
    public async Task<List<Endereco>> ListarPorFuncionarioAsync(int funcionarioId)
    {
        return await _context.Enderecos
            .Where(e => e.FuncionarioId == funcionarioId)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Endereco?> ObterPorIdAsync(int id)
    {
        return await _context.Enderecos.FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task AdicionarAsync(Endereco endereco)
    {
        _context.Enderecos.Add(endereco);
        await _context.SaveChangesAsync();
    }

    public async Task AtualizarAsync(Endereco endereco)
    {
        _context.Enderecos.Update(endereco);
        await _context.SaveChangesAsync();
    }

    public async Task RemoverAsync(Endereco endereco)
    {
        _context.Enderecos.Remove(endereco);
        await _context.SaveChangesAsync();
    }
}