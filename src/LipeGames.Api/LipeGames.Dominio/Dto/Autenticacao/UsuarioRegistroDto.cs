using System;
using System.Collections.Generic;
using System.Text;

namespace LipeGames.Dominio.Dto.Autenticacao
{
    public class UsuarioRegistroDto
    {
        public string Email { get; set; }

        public string Senha { get; set; }

        public string Confirmacao { get; set; }
    }
}
