using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.Videos.Commands.CreateVideo
{
    public class CreateVideoCommand : IRequest<int>
    {

        public string? Nombre { get; set; }

        public int StreamerId { get; set; }
    }
}
