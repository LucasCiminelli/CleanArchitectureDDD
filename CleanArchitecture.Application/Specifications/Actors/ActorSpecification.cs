using CleanArchitecture.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Specifications.Actors
{
    public class ActorSpecification : BaseSpecification<Actor>
    {
        public ActorSpecification(ActorSpecificationParams actorParams)
            : base(
                  x => string.IsNullOrEmpty(actorParams.Search) || x.Nombre!.Contains(actorParams.Search)
                  )
        {

            ApplyPaging(actorParams.PageSize * (actorParams.PageIndex - 1), actorParams.PageSize);

            if (!string.IsNullOrEmpty(actorParams.Sort))
            {
                switch (actorParams.Sort)
                {
                    case "nombreAsc":
                        AddOrderBy(p => p.Nombre!);
                        break;
                    case "nombreDesc":
                        AddOrderByDescending(p => p.Nombre!);
                        break;
                    default:
                        AddOrderBy(p => p.CreatedDate!);
                        break;

                }



            }
        }
    }
}
