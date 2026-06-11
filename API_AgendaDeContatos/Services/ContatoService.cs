using API_AgendaDeContatos.Models;
using API_AgendaDeContatos.Repositories;

namespace API_AgendaDeContatos.Services;

public class ContatoService : IContatoService
{
    public readonly IContatoRepository _repository;

    public ContatoService(IContatoRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<Contato>> BuscaTodos(int pagina, int quantidade)
    {
        return await _repository.BuscaTodos(pagina, quantidade);
    }

    public async Task<int> TotalContatos() => await _repository.TotalContatos();

    public async Task<Contato?> BuscaPorId(int id)
    {
        return await _repository.BuscaPorId(id);
    }

    public async Task<List<Contato>> BuscaPorNome(string nome)
    {
        var contato = await _repository.BuscaPorNome(nome);

        if (!contato.Any())
            throw new KeyNotFoundException("O nome informado não foi identificado na agenda de contatos!");

        return contato;
    }
    
    public async Task<List<Contato>> BuscaFavoritos()
    {
        var contatosFavoritos = await _repository.BuscaFavoritos();

        if (!contatosFavoritos.Any())
            throw new Exception("Atualmente não existe contatos favoritos!");

        return contatosFavoritos;
    }

    public async Task<Contato> AdicionaContato(Contato contato)
    {
        await _repository.AdicionaContato(contato);
        return contato;
    }

    public async Task AtualizaContato(int id, Contato contatoAtualizado)
    {
        var contatoExistente = await _repository.BuscaPorId(id);

        if (contatoExistente == null)
            throw new KeyNotFoundException("Contato não encontrado");

        await _repository.AtualizaContato(id, contatoAtualizado);
    }

    public async Task AtualizaFavorito(int id, Contato contatoAtualizado)
    {
        var contato = await _repository.BuscaPorId(id);

        if (contato == null)
            throw new KeyNotFoundException("Contato não encontrado");

        await _repository.AtualizaFavorito(id, contatoAtualizado);
    }

    public async Task RemoveContato(int id)
    {
        var contatoExistente = await _repository.BuscaPorId(id);

        if (contatoExistente == null)
            throw new KeyNotFoundException("Contato não encontrado");

        await _repository.RemoveContato(id);
    }
}
