using CleanArchitecture.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Specifications.Actors
{
    public class ActorsWithVideosSpecification : BaseSpecification<Actor>
    {
        public ActorsWithVideosSpecification()
        {

            AddInclude(p => p.Videos!);

        }
    }
}
