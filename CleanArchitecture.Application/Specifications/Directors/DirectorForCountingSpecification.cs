using CleanArchitecture.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Specifications.Directors
{
    public class DirectorForCountingSpecification : BaseSpecification<Director>
    {
        public DirectorForCountingSpecification(DirectorSpecificationParams directorParams)
            : base (x => string.IsNullOrEmpty(directorParams.Search) || x.Nombre!.Contains(directorParams.Search))
        {}
    }
}
