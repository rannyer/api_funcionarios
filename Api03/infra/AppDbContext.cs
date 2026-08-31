using Api03.models;
using Microsoft.EntityFrameworkCore;

namespace Api03.infra;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    
    public DbSet<Setor> Setores => Set<Setor>();
    public DbSet<Funcionario> Funcionarios => Set<Funcionario>();
    public DbSet<Endereco> Enderecos => Set<Endereco>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Setor>()
            .HasMany(s => s.Funcionarios)
            .WithOne(s => s.Setor)
            .HasForeignKey(f => f.SetorId)
            .OnDelete(DeleteBehavior.Restrict);
        
        modelBuilder.Entity<Funcionario>()
            .HasMany(f => f.Enderecos)
            .WithOne(e => e.Funcionario)
            .HasForeignKey(e => e.FuncionarioId)
            .OnDelete(DeleteBehavior.Restrict);
    }
    
}