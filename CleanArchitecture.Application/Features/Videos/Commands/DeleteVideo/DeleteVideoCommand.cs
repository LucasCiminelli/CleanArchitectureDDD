using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.Videos.Commands.DeleteVideo
{
    public class DeleteVideoCommand : IRequest
    {

        public int Id { get; set; }
    }
}
