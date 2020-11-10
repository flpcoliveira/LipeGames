using LipeGames.Dominio.Entidades;
using Xunit;

namespace LipeGames.Teste
{
    public class AmigoTest
    {
        [Fact]
        public void Test_Create_Basic_Instance()
        {
            var entidade = new Amigo { Id = 1, Nome = "Jo�o Santo Cristo" };

            Assert.Equal(1, entidade.Id);
            Assert.Equal("Jo�o do Santo Cristo", entidade.Nome);

        }
    }
}
