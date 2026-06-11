using API_AgendaDeContatos.Enums;
using API_AgendaDeContatos.Models;

namespace API_AgendaDeContatos.Services;

public interface IContatoService
{
    Task<List<Contato>> BuscaTodos(int pagina, int quantidade);
    Task<int> TotalContatos();
    Task<Contato?> BuscaPorId(int id);
    Task<List<Contato>> BuscaPorNome(string nome);
    Task<List<Contato>> BuscaFavoritos();
    Task<List<Contato>> BuscaPorCategoriaENome(string nome, ECategoria categoria, int pagina, int quantidade);
    Task<Contato> AdicionaContato(Contato contato);
    Task AtualizaContato(int id, Contato contato);
    Task AtualizaFavorito(int id, Contato contato);
    Task RemoveContato(int id);
}
