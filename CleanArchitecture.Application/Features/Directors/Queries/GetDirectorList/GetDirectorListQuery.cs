using CleanArchitecture.Application.Features.Directors.Queries.Vms;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.Directors.Queries.GetDirectorList
{
    public class GetDirectorListQuery : IRequest<List<DirectorsVm>>
    {

        public string _Username { get; set; }

        public GetDirectorListQuery(string username)
        {
            _Username = username;
        }
    }
}
