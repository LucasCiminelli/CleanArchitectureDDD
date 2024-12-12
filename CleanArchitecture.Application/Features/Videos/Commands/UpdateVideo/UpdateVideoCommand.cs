using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.Videos.Commands.UpdateVideo
{
    public class UpdateVideoCommand : IRequest
    {

        public int Id { get; set; }
        public string? Nombre { get; set; }

        public int StreamerId { get; set; }


    }
}
