using API_AgendaDeContatos.Enums;
using API_AgendaDeContatos.Models;

namespace API_AgendaDeContatos.Repositories;

public interface IContatoRepository
{
    Task<List<Contato>> BuscaTodos(int pagina, int quantidade);
    Task<int> TotalContatos();
    Task<Contato?> BuscaPorId(int id);
    Task<List<Contato>> BuscaPorNomeESobrenome(string nome, string sobrenome);
    Task<List<Contato>> BuscaFavoritos();
    Task<List<Contato>> BuscaPorCategoriaENome(string nome, ECategoria categoria, int pagina, int quantidade);
    Task AdicionaContato(Contato contato);
    Task AtualizaContato(int id, Contato contato);
    Task AtualizaFavorito(int id, Contato contato);
    Task RemoveContato(int id);
}
