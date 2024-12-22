using CleanArchitecture.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Specifications.Directors
{
    public class DirectorSpecification : BaseSpecification<Director>
    {

        public DirectorSpecification(DirectorSpecificationParams directorParams)
            : base(x => string.IsNullOrEmpty(directorParams.Search) || x.Nombre!.Contains(directorParams.Search))
        {

            ApplyPaging(directorParams.PageSize * (directorParams.PageIndex - 1), directorParams.PageSize);

            if (string.IsNullOrEmpty(directorParams.Sort))
            {
                switch (directorParams.Sort)
                {
                    case "NombreAsc":
                        AddOrderBy(p => p.Nombre!);
                        break;
                    case "NombreDesc":
                        AddOrderByDescending(p => p.Nombre!);
                        break;
                    case "ApellidoAsc":
                        AddOrderBy(p => p.Apellido!);
                        break;
                    case "ApellidoDesc":
                        AddOrderByDescending(p => p.Apellido!);
                        break;
                    case "CreatedDateAsc":
                        AddOrderBy(p => p.CreatedDate!);
                        break;
                    case "CreatedDateDesc":
                        AddOrderByDescending(p => p.CreatedDate!);
                        break;
                    default:
                        AddOrderBy(p => p.Nombre!);
                        break;
                }
            }


        }

    }
}
