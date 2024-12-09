using CleanArchitecture.Application.Features.Directors.Queries.GetDirectorList;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.Directors.Queries.GetDirectorByNombre
{
    public class GetDirectorByNombreQuery : IRequest<DirectorsVm>
    {

        public string _Nombre { get; set; }

        public GetDirectorByNombreQuery(string nombre)
        {
            _Nombre = nombre;
        }
    }
}
