using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.Actors.Commands.CreateActor
{
    public class CreateActorCommand : IRequest<int>
    {

        public string Nombre { get; set; } = string.Empty;

        public string Apellido { get; set; } = string.Empty;

       
    }
}
