using Api03.infra;
using Api03.models;
using Microsoft.EntityFrameworkCore;

namespace Api03.repositories;



public interface ISetorRepository
{
    Task<List<Setor>> ListarAsync();
    Task<Setor?> ObterPorIdAsync(int id);
    Task AdicionarAsync(Setor setor);
    Task<bool> ExisteAsync(int id);
    Task AtualizarAsync(Setor setor);
    Task RemoverAsync(Setor setor);
}

public class SetorRepository : ISetorRepository
{
    private readonly AppDbContext _context;
    
    public SetorRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<Setor>> ListarAsync()
    {
        return await _context
            .Setores.Include(s => s.Funcionarios)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Setor?> ObterPorIdAsync(int id)
    {
        return await _context
            .Setores.Include(s => s.Funcionarios)
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task AdicionarAsync(Setor setor)
    {
        _context.Setores.Add(setor);
        await _context.SaveChangesAsync();
    }
    public async Task<bool> ExisteAsync(int id)
    {
        return await _context.Setores.AnyAsync(s => s.Id == id);
    }
    public async Task AtualizarAsync(Setor setor)
    {
        _context.Setores.Update(setor);
        await _context.SaveChangesAsync();
    }
    public async Task RemoverAsync(Setor setor)
    {
            _context.Setores.Remove(setor);
            await _context.SaveChangesAsync();
    }

   

    
}