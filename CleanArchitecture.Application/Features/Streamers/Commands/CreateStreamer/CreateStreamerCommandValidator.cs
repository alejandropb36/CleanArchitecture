using FluentValidation;

namespace CleanArchitecture.Application.Features.Streamers.Commands
{
    public class CreateStreamerCommandValidator: AbstractValidator<CreateStreamerCommand>
    {
        public CreateStreamerCommandValidator()
        {
            RuleFor(e => e.Nombre)
                .NotEmpty().WithMessage("{Nombre} no puede estar en blanco")
                .NotNull()
                .MaximumLength(50).WithMessage("{Nombre} no puede ser exceder los 50 caracteres");

            RuleFor(e => e.Url)
                .NotEmpty().WithMessage("{Url} no puede estar en blanco");
        }
    }
}
