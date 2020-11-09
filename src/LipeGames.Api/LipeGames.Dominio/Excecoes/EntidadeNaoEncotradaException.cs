
using System;

namespace LipeGames.Dominio.Excecoes
{
    public class EntidadeNaoEncotradaException : Exception
    {
        public EntidadeNaoEncotradaException(string message) : base(message)
        {
        }
    }
}
