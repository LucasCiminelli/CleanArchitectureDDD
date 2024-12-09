using CleanArchitecture.Application.Features.Actors.Queries.GetActorsList;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.Actors.Queries.GetActorByNombre
{
    public class GetActorByNombreQuery : IRequest<ActorsVm>
    {

        public string _Nombre { get; set; }

        public GetActorByNombreQuery(string nombre)
        {
            _Nombre = nombre;
        }

    }
}
