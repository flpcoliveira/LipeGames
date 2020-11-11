using LipeGames.Dominio.Entidades.Abstracao;
using System.ComponentModel.DataAnnotations.Schema;

namespace LipeGames.Dominio.Entidades
{
    public class Usuario : EntidadeBase
    {
        public string Email { get; set; }

        public string Senha { get; set; }

        public string ConfirmacaoSenha { get; set; }

        [NotMapped]
        public bool ConfirmacaoSenhaIncorreta
        {
            get
            {
                return !Senha.Equals(ConfirmacaoSenha);
            }
        }
    }
}
