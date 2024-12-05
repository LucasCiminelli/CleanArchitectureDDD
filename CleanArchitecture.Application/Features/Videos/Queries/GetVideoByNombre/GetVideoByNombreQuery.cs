using CleanArchitecture.Application.Features.Videos.Queries.GetVideosList;
using CleanArchitecture.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.Videos.Queries.GetVideoByNombre
{
    public class GetVideoByNombreQuery : IRequest<VideosVm>
    {

        public string _Nombre { get; set; }

        public GetVideoByNombreQuery(string nombre)
        {
            _Nombre = nombre;
        }
    }
}
