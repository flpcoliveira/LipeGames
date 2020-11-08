using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace LipeGames.Dominio.Entidades.Validadores
{
    class AmigoValidator: AbstractValidator<Amigo>
    {
        public AmigoValidator()
        {
            RuleFor(x => x.Nome)
                .NotEmpty()
                .WithMessage("Nome não pode estar vazio")
                .MaximumLength(60)
                .WithMessage("Nome deve ter no máximo 60 caracteres");
        }
    }
}
