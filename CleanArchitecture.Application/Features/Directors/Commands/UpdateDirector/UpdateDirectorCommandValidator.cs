using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.Directors.Commands.UpdateDirector
{
    public class UpdateDirectorCommandValidator : AbstractValidator<UpdateDirectorCommand>
    {
        public UpdateDirectorCommandValidator()
        {

            RuleFor(d => d.Nombre)
                .NotEmpty()
                .NotNull()
                .WithMessage("{Nombre} no puede estar vacío ni ser nulo");

            RuleFor(d => d.Apellido)
               .NotEmpty()
               .NotNull()
               .WithMessage("{Apellido} no puede estar vacío ni ser nulo");

        }
    }
}
