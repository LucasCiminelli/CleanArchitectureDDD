using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.Actors.Commands.CreateActor
{
    public class CreateCommandValidator : AbstractValidator<CreateDirectorCommand>
    {
        public CreateCommandValidator()
        {

            RuleFor(d => d.Nombre)
                .NotNull()
                .NotEmpty()
                .WithMessage("{Nombre} no puede ser null ni empty.");

            RuleFor(d => d.Apellido)
                .NotNull()
                .NotEmpty()
                .WithMessage("{Apellido} no puede ser null ni empty");
        }
    }
}
