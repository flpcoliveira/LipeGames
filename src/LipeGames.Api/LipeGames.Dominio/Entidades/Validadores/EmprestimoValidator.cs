using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace LipeGames.Dominio.Entidades.Validadores
{
   public class EmprestimoValidator: AbstractValidator<Emprestimo>
    {
        public EmprestimoValidator()
        {
            RuleFor(x => x.AmigoId)
                .GreaterThan(0)
                .WithMessage("Informar um amigo válido");

            RuleFor(x => x.JogoId)
                .GreaterThan(0)
                .WithMessage("Informar um jogo válido");
            
            RuleFor(x => x.DataEmprestimo)
                .LessThanOrEqualTo(DateTime.Now)
                .WithMessage("A data do empréstimo deve ser anterior a data atual");
        }
    }
}
