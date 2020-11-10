using LipeGames.Dominio.Entidades.Abstracao;
using System;

namespace LipeGames.Dominio.Entidades
{
    public class Emprestimo: EntidadeBase
    {
        public int AmigoId { get; set; }

        public int JogoId { get; set; }

        public DateTime DataEmprestimo { get; set; }
        
        public DateTime? DataDevolucao { get; set; }

        public Amigo Amigo { get; set; }

        public Jogo Jogo { get; set; }
    }
}
