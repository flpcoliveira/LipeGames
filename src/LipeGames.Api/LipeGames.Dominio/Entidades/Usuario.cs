using LipeGames.Dominio.Entidades.Abstracao;
using System;
using System.Collections.Generic;
using System.Text;

namespace LipeGames.Dominio.Entidades
{
    public class Usuario: EntidadeBase
    {
        public string Login { get; set; }

        public string Senha { private get; set; }

        public DateTime UltimoAcesso { get; set; }
    }
}
