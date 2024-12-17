using CleanArchitecture.Application.Features.Streamers.Queries.Vms;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.Streamers.Queries.GetStreamersList
{
    public class GetStreamersListQuery : IRequest<List<StreamersVm>>
    {

        public string? _Username { get; set; }

        public GetStreamersListQuery(string username)
        {
            _Username = username ?? throw new ArgumentNullException(nameof(username));
        }
    }
}
