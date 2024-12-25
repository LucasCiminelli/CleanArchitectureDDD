using CleanArchitecture.Application.Features.Actors.Queries.Vms;
using CleanArchitecture.Application.Features.Shared.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.Actors.Queries.PaginationActor
{
    public class PaginationActorQuery : PaginationBaseQuery, IRequest<PaginationVm<ActorsVm>>
    {

    }
}
