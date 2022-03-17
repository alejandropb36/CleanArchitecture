using FluentValidation;

namespace CleanArchitecture.Application.Features.Streamers.Commands.UpdateStreamer
{
    public class UpdateStreamerCommandValidator : AbstractValidator<UpdateStreamerCommand>
    {
        public UpdateStreamerCommandValidator()
        {
            RuleFor(e => e.Nombre)
                .NotNull().WithMessage("{Nombre} no permite nulos");
            RuleFor(e => e.Url)
                .NotNull().WithMessage("{Url} no permite nulos");
        }
    }
}
