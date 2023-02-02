using Core.Interfaces.Specifications;
using Core.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class SpecificationEvaluator<TEntity> where TEntity : BaseEntity
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecification<TEntity> specification)
        {
            var query = inputQuery;

            if (specification.Criteria!=null)
            {
                query = query.Where(specification.Criteria);
            }

            if (specification.IncludeStrings != null)
            {
                query = specification.IncludeStrings.Aggregate(query,
                                                              (current, include) =>
                                         current.Include(include));
            }

            if (specification.OrderByAsc!=null)
            {
                query = query.OrderBy(specification.OrderByAsc);
            }

            if (specification.OrderByDesc != null)
            {
                query = query.OrderByDescending(specification.OrderByDesc);
            }

            if (specification.isPagingEnabled)
            {
                query = query.Skip(specification.Skip).Take(specification.Take);
            }

            query = specification.Includes.Aggregate(query, (current, include) => current.Include(include));

            return query;
        }
    }
}
