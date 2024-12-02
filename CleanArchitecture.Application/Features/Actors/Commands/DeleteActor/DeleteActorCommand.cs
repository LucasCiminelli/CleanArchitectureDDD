using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.Actors.Commands.DeleteActor
{
    public class DeleteActorCommand : IRequest
    {
        public int Id { get; set; }

    }
}
