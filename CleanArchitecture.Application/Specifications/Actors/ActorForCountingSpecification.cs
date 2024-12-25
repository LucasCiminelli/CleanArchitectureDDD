using CleanArchitecture.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Specifications.Actors
{
    public class ActorForCountingSpecification : BaseSpecification<Actor>
    {
        public ActorForCountingSpecification(ActorSpecificationParams actorParams)
            : base(
                  x => string.IsNullOrEmpty(actorParams.Search) || x.Nombre!.Contains(actorParams.Search)
                  )
        {
        }
    }
}
