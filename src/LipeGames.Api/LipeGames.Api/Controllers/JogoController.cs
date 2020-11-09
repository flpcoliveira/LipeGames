using LipeGames.Dominio.Dto;
using LipeGames.Dominio.Interfaces.Servicos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LipeGames.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class JogoController : ControllerBase
    {
        private readonly IJogoServico _servico;

        public JogoController(IJogoServico servico) => _servico = servico;

        [HttpGet("")]
        public async Task<IActionResult> Listar()
        {
            var resultado = await _servico.Listar();
            return Ok(resultado);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detalhar(int id)
        {
            var resultado = await _servico.Detalhar(id);
            return Ok(resultado);
        }

        [HttpPost("")]
        public async Task<IActionResult> Criar([FromForm] JogoDto jogo)
        {
            var resultado = await _servico.Criar(jogo);
            return Ok(resultado);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Alterar(int id, [FromForm] JogoDto amigo)
        {
            var resultado = await _servico.Alterar(id, amigo);
            return Ok(resultado);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Excluir(int id)
        {
            await _servico.Excluir(id);
            return Ok();
        }
    }
}
