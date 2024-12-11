using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.Videos.Commands.CreateVideo
{
    public class CreateVideoCommandValidator : AbstractValidator<CreateVideoCommand>
    {
        public CreateVideoCommandValidator()
        {

            RuleFor(v => v.Nombre)
                .NotEmpty()
                .NotNull()
                .WithMessage("El {Nombre} no puede ser vacio ni nulo");

            RuleFor(v => v.StreamerId)
                .NotEmpty()
                .NotNull()
                .WithMessage("El {StreamerId} no puede ser vacio ni nulo");
        }
    }
}
