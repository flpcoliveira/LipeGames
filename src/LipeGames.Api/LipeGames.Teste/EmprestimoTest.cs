using LipeGames.Dominio.Entidades;
using System;
using Xunit;

namespace LipeGames.Teste
{
    public class EmprestimoTest
    {
        private Amigo Amigo { get; set; }

        private Jogo Jogo { get; set; }


        public EmprestimoTest()
        {
            Amigo = new Amigo { Id = 1, Nome = "Diego" };
            Jogo = new Jogo { Id = 1, Nome = "Super Mário World" };

        }

        [Fact]
        public void Create_Basic_Test()
        {
            int amigoId = Amigo.Id.GetValueOrDefault(0);
            int jogoId = Jogo.Id.GetValueOrDefault(0);
            DateTime dataEmprestimo = DateTime.Now;
            var emprestimo = new Emprestimo
            {
                Jogo = Jogo,
                JogoId = jogoId,
                Amigo = Amigo,
                AmigoId = amigoId,
                DataEmprestimo = dataEmprestimo
            };

            Assert.Equal(jogoId, emprestimo.JogoId);
            Assert.Equal(amigoId, emprestimo.AmigoId);
            Assert.Equal(Jogo, emprestimo.Jogo);
            Assert.Equal(Amigo, emprestimo.Amigo);
            Assert.Equal(dataEmprestimo, emprestimo.DataEmprestimo);
            Assert.Null(emprestimo.DataDevolucao);
        }

        [Fact]
        public void Create_With_Optional_Attributes_Assigned()
        {
            int id = 2;
            int amigoId = Amigo.Id.GetValueOrDefault(0);
            int jogoId = Jogo.Id.GetValueOrDefault(0);
            DateTime dataEmprestimo = DateTime.Now;
            DateTime dataDevolucao = dataEmprestimo.AddDays(5);
            var emprestimo = new Emprestimo
            {
                Id = id,
                Jogo = Jogo,
                JogoId = jogoId,
                Amigo = Amigo,
                AmigoId = amigoId,
                DataEmprestimo = dataEmprestimo,
                DataDevolucao = dataDevolucao
            };

            Assert.Equal(id, emprestimo.Id);
            Assert.Equal(dataDevolucao, emprestimo.DataDevolucao);

        }
    }
}
