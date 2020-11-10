using FluentValidation;
using System;

namespace LipeGames.Dominio.Entidades.Validadores
{
    public class JogoValidator: AbstractValidator<Jogo>
    {
        public JogoValidator()
        {
            RuleFor(x => x.Nome)
                .NotEmpty()
                .WithMessage("Informar um nome válido");

            RuleFor(x => x.DataAquisicao)
                .NotNull()
                .WithMessage("Data de Aquisição é obrigatório")
                .LessThan(DateTime.Now)
                .WithMessage("Data de Aquisição não pode ser futura");
        }
    }
}
