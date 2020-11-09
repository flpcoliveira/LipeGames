using System;
using System.Collections.Generic;
using System.Text;

namespace LipeGames.Dominio.Dto
{
    public class EmprestimoDto
    {
        public int Id { get; set; }
        public int AmigoId { get; set; }
        public string AmigoNome { get; set; }
        public int JogoId { get; set; }
        public string JogoNome { get; set; }
    }
}
