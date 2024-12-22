using CleanArchitecture.Application.Features.Directors.Queries.Vms;
using CleanArchitecture.Application.Features.Shared.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.Directors.Queries.PaginationDirector
{
    public class PaginationDirectorsQuery : PaginationBaseQuery, IRequest<PaginationVm<DirectorsVm>>
    {
    }
}
