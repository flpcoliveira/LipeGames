using LipeGames.Dominio.Entidades.Abstracao;
using System;
using System.Collections.Generic;
using System.Text;

namespace LipeGames.Dominio.Entidades
{
    class Emprestimo: EntidadeBase
    {
        public int AmigoId { get; set; }

        public int JogoId { get; set; }
        public Amigo Amigo { get; set; }

        public Jogo Jogo { get; set; }
    }
}
