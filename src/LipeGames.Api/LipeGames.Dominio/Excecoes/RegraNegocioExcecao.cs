using System;
using System.Collections.Generic;

namespace LipeGames.Dominio.Excecoes
{
    public class RegraNegocioExcecao: Exception
    {
        public Dictionary<string, string> Mensagens { get; set; }

        public RegraNegocioExcecao(string message) : base(message)
        {

        }

        public RegraNegocioExcecao(string message, Dictionary<string, string> mensagens) : base(message)
        {
            Mensagens = mensagens;
        }
    }
}
