using LipeGames.Dominio.Dto.Autenticacao;
using System.Collections.Generic;

namespace LipeGames.Dominio.Dto
{
    public class InformacoesAcessoDto
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public string Token { get; set; }

        public IEnumerable<AcessosDto> Acessos { get; set; }
    }
}
