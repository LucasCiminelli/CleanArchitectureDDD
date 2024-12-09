using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.Actors.Queries.GetActorsList
{
    public class GetActorListQueryHandler
    {

        public string _Username { get; set; }

        public GetActorListQueryHandler(string username)
        {
            _Username = username;
        }
    }
}
