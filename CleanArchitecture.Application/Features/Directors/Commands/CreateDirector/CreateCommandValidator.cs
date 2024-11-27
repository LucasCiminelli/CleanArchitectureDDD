using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.Directors.Commands.CreateDirector
{
    public class CreateCommandValidator : AbstractValidator<CreateDirectorCommand>
    {
        public CreateCommandValidator()
        {

            RuleFor(p=> p.Nombre)
                .NotEmpty()
                .NotNull().WithMessage("{Nombre} no puede ser nulo");

            RuleFor(p => p.Apellido)
               .NotEmpty()
               .NotNull().WithMessage("{Apellido} no puede ser nulo");


        }
    }
}
