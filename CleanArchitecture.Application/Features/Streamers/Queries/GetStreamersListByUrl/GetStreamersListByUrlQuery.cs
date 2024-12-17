using CleanArchitecture.Application.Features.Streamers.Queries.Vms;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.Streamers.Queries.GetStreamersListByUrl
{
    public class GetStreamersListByUrlQuery : IRequest<List<StreamersVm>>
    {

        public string? _Url {  get; set; }

        public GetStreamersListByUrlQuery(string? url)
        {
            _Url = url ?? throw new ArgumentNullException(nameof(url));
        }


    }
}
