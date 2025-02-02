﻿using CleanArchitecture.Application.Specifications;
using CleanArchitecture.Domain.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Specification
{
    public class SpecificationEvaluator<T> where T : BaseDomainModel //Solo se va a aplicar a aquellas clases que se hereden desde el BaseDomainModel
    {

        public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecification<T> spec)
        {

            if (spec.Criteria != null)
            {
                inputQuery = inputQuery.Where(spec.Criteria);
            }

            if (spec.OrderBy != null)
            {
               inputQuery = inputQuery.OrderBy(spec.OrderBy);

            }

            if (spec.OrderByDescending != null)
            {
               inputQuery = inputQuery.OrderBy(spec.OrderByDescending);

            }

            if (spec.isPagingEnable)
            {
                inputQuery = inputQuery.Skip(spec.Skip).Take(spec.Take);
            }


            inputQuery = spec.Inlcudes.Aggregate(inputQuery, (current, include) => current.Include(include));


            return inputQuery;


        }

    }
}
