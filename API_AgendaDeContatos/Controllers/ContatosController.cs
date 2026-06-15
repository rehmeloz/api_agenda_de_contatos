using API_AgendaDeContatos.DTOs;
using API_AgendaDeContatos.Enums;
using API_AgendaDeContatos.Models;
using API_AgendaDeContatos.Services;
using Microsoft.AspNetCore.Mvc;

namespace API_AgendaDeContatos.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContatosController : Controller
    {
        public readonly IContatoService _service;
        public readonly ILogger<ContatosController> _logger;

        public ContatosController(IContatoService service, ILogger<ContatosController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> BuscaTodos([FromQuery] int pagina = 1, [FromQuery] int quantidade = 5)
        {
            _logger.LogInformation($"Buscando todos os contatos da página {pagina}, quantidade: {quantidade}");

            var contatos = await _service.BuscaTodos(pagina, quantidade);
            var totalContatos = await _service.TotalContatos();

            return Ok(new
                {
                    totalContatos,
                    pagina,
                    quantidade,
                    contatos
                }
            );
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> BuscaPorId(int id)
        {
            _logger.LogInformation($"Buscando contato de id: {id}");

            var contato = await _service.BuscaPorId(id);

            if (contato == null)
            {
                _logger.LogWarning($"Contato de id {id} não encontrado!");
                return NotFound();

            }

            return Ok(contato);
        }

        [HttpGet("buscaPorNome")]
        public async Task<IActionResult> BuscaPorNome([FromQuery] string nome)
        {
            _logger.LogInformation($"Buscando contato de nome: {nome}");

            var contato = await _service.BuscaPorNome(nome);

            if (!contato.Any())
            {
                _logger.LogWarning($"Contato de nome {nome} não encontrado!");
                return NotFound();
            }


            return Ok(contato);
        }

        [HttpGet("buscaFavoritos")]
        public async Task<IActionResult> BuscaFavoritos()
        {
            _logger.LogInformation($"Buscando contatos favoritos");

            var contatosFavoritos = await _service.BuscaFavoritos();

            if (!contatosFavoritos.Any())
            {
                _logger.LogWarning("Contatos favoritos não identificados");
                return NotFound();
            }

            return Ok(contatosFavoritos);
        }

        [HttpGet("buscaPorNomeECategoria")]
        public async Task<IActionResult> BuscaPorCategoriaENome([FromQuery] string nome, [FromQuery] ECategoria categoria, int pagina = 1, int quantidade = 5)
        {
            _logger.LogInformation("Buscando contato por Nome e Categoria");

            var contatos = await _service.BuscaPorCategoriaENome(nome, categoria, pagina, quantidade);

            if (!contatos.Any())
            {
                _logger.LogWarning("Nenhum contato foi retornado");
                return NotFound();
            }
                
            return Ok(contatos);
        }

        [HttpPost]
        public async Task<IActionResult> AdicionaContato(CriaContatoDto contatoDto)
        {
            _logger.LogInformation($"Adicionando contato");

            var contato = new Contato
            {
                Nome = contatoDto.Nome,
                Telefone = contatoDto.Telefone,
                Email = contatoDto.Email,
                Favorito = contatoDto.Favorito,
                Categoria = contatoDto.Categoria,
                Sobrenome = contatoDto.Sobrenome
            };

            var novoContato = await _service.AdicionaContato(contato);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizaContato(int id, AtualizaContatoDto contatoDto)
        {
            try
            {
                _logger.LogInformation($"Atualizando contato de id: {id}");

                var contato = new Contato
                {
                    Nome = contatoDto.Nome,
                    Telefone = contatoDto.Telefone,
                    Email = contatoDto.Email,
                    Favorito = contatoDto.Favorito,
                    Categoria = contatoDto.Categoria,
                    Sobrenome = contatoDto.Sobrenome
                };

                await _service.AtualizaContato(id, contato);
                return Ok();
            }
            catch(KeyNotFoundException ex)
            {
                _logger.LogWarning($"Contato de id: {id} não encontrado!");
                return NotFound(ex.Message);
            }
        }

        [HttpPatch("atualizaFavorito/{id}")]
        public async Task<IActionResult> AtualizaFavorito(int id, AtualizaFavoritoDto contatoDto)
        {
            try
            {
                _logger.LogInformation($"Atualizando favoritos do contato de id: {id}");

                var contato = new Contato
                {
                    Favorito = contatoDto.Favorito
                };

                await _service.AtualizaFavorito(id, contato);
                return Ok();
            }
            catch(KeyNotFoundException ex)
            {
                _logger.LogWarning($"Contato de id: {id} não encontrado para atualizar favorito!");
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveContato(int id)
        {
            try
            {
                _logger.LogInformation($"Removendo contato de id: {id}");

                await _service.RemoveContato(id);
                return Ok();
            }
            catch(KeyNotFoundException ex)
            {
                _logger.LogWarning($"Contato de id: {id} não encontrado para remoção!");
                return NotFound(ex.Message);
            }
        }
    }
}
