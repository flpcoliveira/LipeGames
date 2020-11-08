using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LipeGames.Dominio.Dto.Autenticacao;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LipeGames.Api.Controllers
{



    [Route("api/[controller]")]
    [ApiController]
    public class AutenticacaoController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public AutenticacaoController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UsuarioLoginDto usuarioLogin)
        {
            var result = await _signInManager.PasswordSignInAsync(usuarioLogin.Email, usuarioLogin.Senha, false, true);

            if(result.Succeeded)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpPost("registro")]
        public async Task<IActionResult> Registrar(UsuarioRegistroDto usuarioRegistro)
        {
            var user = new IdentityUser
            {
                UserName = usuarioRegistro.Email,
                Email = usuarioRegistro.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                return Ok();
            }

            return BadRequest();
        }

    }
}
