using LipeGames.Dominio.Dto;
using LipeGames.Dominio.Interfaces.Servicos;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LipeGames.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AmigoController : ControllerBase
    {
        private readonly IAmigoServico _servico;

        public AmigoController(IAmigoServico servico) => _servico = servico;

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
        public async Task<IActionResult> Criar([FromForm] AmigoDto amigo)
        {
            var resultado = await _servico.Criar(amigo);
            return Ok(resultado);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Alterar(int id, [FromForm] AmigoDto amigo)
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
