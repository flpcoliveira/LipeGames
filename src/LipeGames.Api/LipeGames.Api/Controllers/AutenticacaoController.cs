using LipeGames.Dominio.Dto.Autenticacao;
using LipeGames.Dominio.Interfaces.Servicos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LipeGames.Api.Controllers
{

  
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class AutenticacaoController : ControllerBase
    {
        private readonly IAutenticacaoServico _servico;

        public AutenticacaoController(IAutenticacaoServico servico)
        {
            _servico = servico;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UsuarioLoginDto usuarioLogin)
        {
            var result = await _servico.Login(usuarioLogin);
            return Ok(result);
        }

        [HttpPost("registro")]
        public async Task<IActionResult> Registrar(UsuarioRegistroDto usuarioRegistro)
        {
            var result = await _servico.Registrar(usuarioRegistro);
            return Ok(result);
        }
    }
}
