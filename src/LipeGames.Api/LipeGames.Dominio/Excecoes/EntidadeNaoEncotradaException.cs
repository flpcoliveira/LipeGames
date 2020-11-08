using System;

namespace LipeGames.Dominio.Excecoes
{
    class EntidadeNaoEncotradaException : Exception
    {
        public EntidadeNaoEncotradaException(string message) : base(message)
        {
        }
    }
}
