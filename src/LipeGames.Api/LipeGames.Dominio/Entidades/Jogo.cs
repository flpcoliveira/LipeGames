using LipeGames.Dominio.Entidades.Abstracao;
using System;
using System.Collections.Generic;
using System.Text;

namespace LipeGames.Dominio.Entidades
{
    public class Jogo: EntidadeBase
    {
        public string Nome { get; set;  }

        public DateTime DataAquisicao { get; set; }
    }
}
