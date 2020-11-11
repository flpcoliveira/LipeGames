using FluentValidation;

namespace LipeGames.Dominio.Entidades.Validadores
{
    public class UsuarioValidator : AbstractValidator<Usuario>
    {
        public UsuarioValidator()
        {
            var lengthMessage = "A Senha deve ter entre 8  e 16 caracteres";

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email não pode estar vazio")
                .MaximumLength(60)
                .WithMessage("Nome deve ter no máximo 60 caracteres");

            RuleFor(x => x.Senha)
                .NotEmpty()
                .WithMessage("Senha não pode estar vazio")
                .MinimumLength(8)
                .WithMessage(lengthMessage)
                .MaximumLength(16)
                .WithMessage(lengthMessage);
        }
    }
}
