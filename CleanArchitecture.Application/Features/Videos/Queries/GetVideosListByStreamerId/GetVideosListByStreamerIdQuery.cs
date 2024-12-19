using CleanArchitecture.Application.Features.Videos.Queries.Vms;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.Videos.Queries.GetVideosListByStreamerId
{
    public class GetVideosListByStreamerIdQuery : IRequest<List<VideosVm>>
    {

        public int _StreamerId { get; set; }

        public GetVideosListByStreamerIdQuery(int? streamerId)
        {
            _StreamerId = streamerId ?? throw new ArgumentException($"Id: {streamerId} no encontrado");
        }
    }
}
