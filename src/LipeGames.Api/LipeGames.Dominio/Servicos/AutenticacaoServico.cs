using AutoMapper;
using FluentValidation;
using LipeGames.Api.Dominio.Extensions;
using LipeGames.Dominio.Dto;
using LipeGames.Dominio.Dto.Autenticacao;
using LipeGames.Dominio.Entidades;
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
        IValidator<Usuario> _validator;
        private readonly JwtSettings _jwtSettings;

        public AutenticacaoServico(
            SignInManager<IdentityUser> gerenciadorAcesso,
            UserManager<IdentityUser> gerenciadorUsuario,
            IMapper mapper, 
            IValidator<Usuario> validator,
            IOptions<JwtSettings> jwtSettings
         )
        {
            _signInManager = gerenciadorAcesso;
            _userManager = gerenciadorUsuario;
            _mapper = mapper;
            _jwtSettings = jwtSettings.Value;
            _validator = validator;
        }

        public async Task<InformacoesAcessoDto> Login(UsuarioLoginDto usuarioLogin)
        {
            var usuario = _mapper.Map<UsuarioLoginDto, Usuario>(usuarioLogin);

            var validatorResult = await _validator.ValidateAsync(usuario);
            if (!validatorResult.IsValid)
            {
                var errosValidacao = validatorResult.Errors.ToDictionary(mensagem => mensagem.PropertyName, mensagem => mensagem.ErrorMessage);
                throw new RegraNegocioExcecao("Foram encontrados erros ao registrar o usuario", errosValidacao);
            }

            var identityUser = _mapper.Map<Usuario, IdentityUser>(usuario);


            var sigInResult = await _signInManager.PasswordSignInAsync(usuario.Email, usuario.Senha, false, true);

            if (sigInResult.IsLockedOut) throw new AutenticacaoExcecao("Acesso temporariamente bloqueado. Tente novamente em alguns instantes");

            if (!sigInResult.Succeeded) throw new AutenticacaoExcecao("Usuário e/ou senha inválidos");

            return await GerarToken(usuarioLogin.Email);
        }

        public async Task<InformacoesAcessoDto> Registrar(UsuarioRegistroDto usuarioRegistro)
        {

            var usuario = _mapper.Map<UsuarioRegistroDto, Usuario>(usuarioRegistro);

            var validatorResult = await _validator.ValidateAsync(usuario);
            if (!validatorResult.IsValid)
            {
                var errosValidacao = validatorResult.Errors.ToDictionary(mensagem => mensagem.PropertyName, mensagem => mensagem.ErrorMessage);
                throw new RegraNegocioExcecao("Foram encontrados erros ao registrar o usuario", errosValidacao);
            }

            if (usuario.ConfirmacaoSenhaIncorreta) throw new AutenticacaoExcecao("A confirmação de senha não coincide com a senha informada");

            var identityUser = _mapper.Map<Usuario, IdentityUser>(usuario);
            identityUser.Id = Guid.NewGuid().ToString();
            identityUser.EmailConfirmed = true;

            var identityResult = await _userManager.CreateAsync(identityUser, usuario.Senha);

            if (!identityResult.Succeeded) throw new AutenticacaoExcecao("Ocorreu um erro ao registrar usuário");

            return await GerarToken(identityUser.Email);
        }

        private async Task<InformacoesAcessoDto> GerarToken(string email)
        {
            try
            {
                var identityUser = await _userManager.FindByEmailAsync(email);
                var claims = await _userManager.GetClaimsAsync(identityUser);
                var userRoles = await _userManager.GetRolesAsync(identityUser);

                claims.Add(new Claim(JwtRegisteredClaimNames.Sub, identityUser.Id));
                claims.Add(new Claim(JwtRegisteredClaimNames.Email, identityUser.Email));
                claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

                foreach (var role in userRoles)
                {
                    claims.Add(new Claim("role", role));
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

                var tokenCodificado = tokenHandler.WriteToken(token);

                IEnumerable<AcessosDto> acessos = claims.Select(claim => new AcessosDto { Tipo = claim.Type, Valor = claim.Value });

                return new InformacoesAcessoDto
                {
                    Id = identityUser.Id,
                    Email = identityUser.Email,
                    Token = tokenCodificado,
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
