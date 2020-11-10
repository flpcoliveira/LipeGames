using LipeGames.Dominio.Entidades;
using Xunit;

namespace LipeGames.Teste
{
    public class AmigoTest
    {
        [Fact]
        public void Test_Create_Basic_Instance()
        {
            var idEsperado = 1;
            var nomeEsperado = "João Santo Cristo";
            var entidade = new Amigo { Id = 1, Nome = nomeEsperado };

            Assert.Equal(idEsperado, entidade.Id);
            Assert.Equal(nomeEsperado, entidade.Nome);

        }
    }
}
