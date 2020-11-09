using AutoMapper;
using LipeGames.Api.Dominio.Extensions;
using LipeGames.Dominio.Dto;
using LipeGames.Dominio.Dto.Autenticacao;
using LipeGames.Dominio.Excecoes;
using LipeGames.Dominio.Interfaces.Servicos;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LipeGames.Dominio.Servicos
{
    public class AutenticacaoServico : IAutenticacaoServico
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IMapper _mapper;
        private readonly JwtSettings _jwtSettings;

        public AutenticacaoServico(SignInManager<IdentityUser> gerenciadorAcesso, UserManager<IdentityUser> gerenciadorUsuario, IMapper mapper, IOptions<JwtSettings> jwtSettings)
        {
            _signInManager = gerenciadorAcesso;
            _userManager = gerenciadorUsuario;
            _mapper = mapper;
            _jwtSettings = jwtSettings.Value;
        }

        public async Task<InformacoesAcessoDto> Login(UsuarioLoginDto usuarioLogin)
        {
            var requisicaoacessoUsuario = await _signInManager.PasswordSignInAsync(usuarioLogin.Email, usuarioLogin.Senha, false, true);

            if (!requisicaoacessoUsuario.Succeeded) throw new AutenticacaoExcecao("Usuário e/ou senha inválidos");

            return await GerarToken(usuarioLogin.Email);
        }

        public async Task<InformacoesAcessoDto> Registrar(UsuarioRegistroDto usuarioRegistro)
        {
            var user = _mapper.Map<UsuarioRegistroDto, IdentityUser>(usuarioRegistro);

            var result = await _userManager.CreateAsync(user);

            return await GerarToken(user.Email);
        }

        private async Task<InformacoesAcessoDto> GerarToken(string email)
        {
            try
            {
                var usuarioIdentity = await _userManager.FindByEmailAsync(email);
                var claims = await _userManager.GetClaimsAsync(usuarioIdentity);
                var userRoles = await _userManager.GetRolesAsync(usuarioIdentity);

                claims.Add(new Claim(JwtRegisteredClaimNames.Sub, usuarioIdentity.Id));
                claims.Add(new Claim(JwtRegisteredClaimNames.Email, usuarioIdentity.Email));
                claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

                foreach (var userRole in userRoles)
                {
                    claims.Add(new Claim("role", userRole));
                }


                var identityClaims = new ClaimsIdentity();

                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);

                var symmetricSecurityKey = new SymmetricSecurityKey(key);

                var singinCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Issuer = _jwtSettings.Issuer,
                    Audience = _jwtSettings.Audience,
                    Subject = identityClaims,
                    Expires = DateTime.UtcNow.AddHours(_jwtSettings.ExpirationHours),
                    SigningCredentials = singinCredentials
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);

                var encodedToken = tokenHandler.WriteToken(token);

                IEnumerable<AcessosDto> acessos = claims.Select(claim => new AcessosDto { Tipo = claim.Type, Valor = claim.Value });

                return new InformacoesAcessoDto
                {
                    Id = usuarioIdentity.Id,
                    Email = usuarioIdentity.Email,
                    Token = encodedToken,
                    Acessos = acessos
                };
            }
            catch 
            {
                throw new AutenticacaoExcecao($"Ocorreu erro ao gerar token para o email {email}");
            }
        }
    }
}
