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

        public string _Username { get; set; } = string.Empty;

        public GetStreamersListQuery(string username)
        {
            _Username = username;
        }
    }
}
