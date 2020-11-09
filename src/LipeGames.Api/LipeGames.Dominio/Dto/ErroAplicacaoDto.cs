using System.Collections.Generic;

namespace LipeGames.Dominio.Dto
{
    public class ErroAplicacaoDto
    {
        public string Resumo { get; set; }

        public Dictionary<string, string> Erros { get; set; }
    }
}
