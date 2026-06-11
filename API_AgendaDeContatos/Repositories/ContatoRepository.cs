using API_AgendaDeContatos.Data;
using Microsoft.EntityFrameworkCore;
using API_AgendaDeContatos.Models;

namespace API_AgendaDeContatos.Repositories;

public class ContatoRepository : IContatoRepository
{
    public readonly AppDbContext _context;

    public ContatoRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Contato>> BuscaTodos(int pagina, int quantidade)
    {
        return await _context.Contatos
            .Skip((pagina - 1) * quantidade)
            .Take(quantidade)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<int> TotalContatos() => await _context.Contatos.CountAsync();

    public async Task<Contato?> BuscaPorId(int id)
    {
        var contato = await _context.Contatos.FindAsync(id);

        return contato;
    }

    public async Task<List<Contato>> BuscaPorNome(string nome)
    {
        var contato = await _context.Contatos.Where(n => n.Nome.Contains(nome)).AsNoTracking().ToListAsync();

        return contato;
    }

    public async Task<List<Contato>> BuscaFavoritos()
    {
        var contatosFavoritos = await _context.Contatos.Where(c => c.Favorito).ToListAsync();

        return contatosFavoritos;
    }

    public async Task AdicionaContato(Contato contato)
    {
        _context.Contatos.Add(contato);
        await _context.SaveChangesAsync();
    }

    public async Task AtualizaContato(int id, Contato contatoAtualizado)
    {
        var contato = await _context.Contatos.FindAsync(id);

        if (contato == null) return;

        contato.Nome = contatoAtualizado.Nome;
        contato.Email = contatoAtualizado.Email;
        contato.Telefone = contatoAtualizado.Telefone;
        contato.Favorito = contatoAtualizado.Favorito;
        contato.Categoria = contatoAtualizado.Categoria;

        await _context.SaveChangesAsync();
    }

    public async Task AtualizaFavorito(int id, Contato contatoAtualizado)
    {
        var contato = await _context.Contatos.FindAsync(id);

        if(contato == null) return;

        contato.Favorito = contatoAtualizado.Favorito;

        await _context.SaveChangesAsync();
    }

    public async Task RemoveContato(int id)
    {
        var contato = await _context.Contatos.FindAsync(id);

        if (contato == null) return;

        _context.Contatos.Remove(contato);
        await _context.SaveChangesAsync();
    }
}
