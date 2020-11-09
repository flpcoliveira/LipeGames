using System;

namespace LipeGames.Dominio.Excecoes
{
    public class AutenticacaoExcecao: Exception
    {
        public AutenticacaoExcecao(string message) : base(message)
        {

        }
    }
}
