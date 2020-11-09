using LipeGames.Dominio.Dto;
using LipeGames.Dominio.Dto.Autenticacao;
using System.Threading.Tasks;

namespace LipeGames.Dominio.Interfaces.Servicos
{
    public interface IAutenticacaoServico
    {
        public Task<InformacoesAcessoDto> Login(UsuarioLoginDto usuarioLogin);

        public Task<InformacoesAcessoDto> Registrar(UsuarioRegistroDto usuarioRegistro);
    }
}
