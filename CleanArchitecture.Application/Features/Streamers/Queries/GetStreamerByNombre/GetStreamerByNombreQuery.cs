using CleanArchitecture.Application.Features.Streamers.Queries.Vms;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.Streamers.Queries.GetStreamerByNombre
{
    public class GetStreamerByNombreQuery : IRequest<StreamersVm>
    {

        public string _Nombre { get; set; } = string.Empty;

        public GetStreamerByNombreQuery(string nombre)
        {
            _Nombre = nombre;
        }
    }
}
