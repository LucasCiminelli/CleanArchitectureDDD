using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace CleanArchitecture.Application.Features.Actors.Commands.UpdateActor
{
    public class UpdateActorCommandValidator : AbstractValidator<UpdateActorCommand>
    {
        public UpdateActorCommandValidator()
        {

            RuleFor(actor => actor.Nombre)
                .NotEmpty()
                .NotNull()
                .WithMessage("{Nombre} no puede ser null ni estar vacío");

            RuleFor(actor => actor.Apellido)
                .NotEmpty()
                .NotNull()
                .WithMessage("{Apellido} no puede ser null ni estar vacío");

        }
    }
}
