using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cet.Core.Data;
using System.Linq.Expressions;
using System.Reflection;

namespace Cet.EntityFramework.Adaptor
{
    public static class QueryableBase
    {
        public static IQueryable<TEntity> Where<TEntity>(this IQueryable<TEntity> source1, ISpecification<TEntity> specification) where TEntity : class
        {
            return source1.Where<TEntity>(specification.SatisfiedBy());
        }

        public static IQueryable<TEntity> Page<TEntity>(this IQueryable<TEntity> source1, Cet.Core.Data.PagingCriteria pagingCriteria)
        {
            return source1.Skip<TEntity>(pagingCriteria.PageSize * pagingCriteria.PageIndex).Take(pagingCriteria.PageSize);
        }

        public static IQueryable<TEntity> OrderBy<TEntity>(this IQueryable<TEntity> source1, Cet.Core.Data.SortingCriteria sortingCriteria)
        {
            IQueryable<TEntity> queryableEntity = source1;
            bool firstSorting = true;

            foreach (SortBy sortBy in sortingCriteria)
            {
                string[] props = sortBy.Name.Split('.');
                Type type = typeof(TEntity);
                ParameterExpression arg = Expression.Parameter(type, "x");
                Expression expr = arg;
                foreach (string prop in props)
                {
                    // use reflection (not ComponentModel) to mirror LINQ 
                    PropertyInfo pi = type.GetProperty(prop);
                    expr = Expression.Property(expr, pi);
                    type = pi.PropertyType;
                }

                Type delegateType = typeof(Func<,>).MakeGenericType(typeof(TEntity), type);
                LambdaExpression lambda = Expression.Lambda(delegateType, expr, arg);

                string methodName;
                if (!firstSorting)
                {
                    methodName = (sortBy.Direction == SortDirection.ASC) ? "ThenBy" : "ThenByDescending";
                }
                else
                {
                    methodName = (sortBy.Direction == SortDirection.ASC) ? "OrderBy" : "OrderByDescending";
                    firstSorting = false;
                }

                MethodInfo thenByMethod = typeof(Queryable)
                    .GetMethods().Single(
                                        method => method.Name == methodName
                                                && method.IsGenericMethodDefinition
                                                && method.GetGenericArguments().Length == 2
                                                && method.GetParameters().Length == 2);

                MethodInfo genericThenByMethod = thenByMethod.MakeGenericMethod(typeof(TEntity), type);
                queryableEntity = genericThenByMethod.Invoke(null, new object[] { queryableEntity, lambda }) as IQueryable<TEntity>;
            }
            return queryableEntity;
        }
    }
}
