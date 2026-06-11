using API_AgendaDeContatos.Models;
using Microsoft.EntityFrameworkCore;

namespace API_AgendaDeContatos.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Contato> Contatos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Contato>().HasKey(c => c.Id); 

        modelBuilder.Entity<Contato>().Property(n => n.Nome).HasMaxLength(100).IsRequired();

        modelBuilder.Entity<Contato>().Property(f => f.Favorito).IsRequired();

        modelBuilder.Entity<Contato>().Property(c => c.Categoria).IsRequired();
    }
}
