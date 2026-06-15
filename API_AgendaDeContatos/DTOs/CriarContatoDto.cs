using API_AgendaDeContatos.Enums;

namespace API_AgendaDeContatos.DTOs;

public class CriaContatoDto
{
    public string Nome { get; set; } = string.Empty;
    public string Sobrenome { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public bool Favorito { get; set; }
    public ECategoria Categoria { get; set; }
}
