using LipeGames.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace LipeGames.Teste
{
    public class JogoTest
    {
        [Fact]
        public void Test_Create_Basic_Instance()
        {
            var idEsperado = 1;
            var nomeEsperado = "João Santo Cristo";
            var entidade = new Jogo { Id = 1, Nome = nomeEsperado };

            Assert.Equal(idEsperado, entidade.Id);
            Assert.Equal(nomeEsperado, entidade.Nome);

        }
    }
}
