﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Specifications
{
    public class BaseSpecification<T> : ISpecification<T>
    {
        public List<string> IncludeStrings { get; } = new List<string>();
        public BaseSpecification()
        {

        }
        public BaseSpecification(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }

        public Expression<Func<T, bool>> Criteria { get; }
        public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();

 

        protected void AddInclude(string includeString)
        {
            IncludeStrings.Add(includeString);
        }

        protected void AddInclude(Expression<Func<T, object>> includeExpression)
        { 
            Includes.Add(includeExpression);
        }

        public Expression<Func<T, object>> OrderByAsc { get; private set; }

        public Expression<Func<T, object>> OrderByDesc { get; private set; }


        protected void AddOrderBy(Expression<Func<T,object>> orderByExpression)
        {
            OrderByAsc = orderByExpression;
        }

        protected void AddOrderByDesc(Expression<Func<T, object>> orderByExpressionDesc)
        {
            OrderByDesc = orderByExpressionDesc;
        }

        public int Take { get; private set; }

        public int Skip { get; private set; }

        public bool isPagingEnabled { get; private set; }

        protected void ApplyPaging(int skip, int take)
        {
            Skip= skip;
            Take= take;
            isPagingEnabled= true;
        }
    }
}
