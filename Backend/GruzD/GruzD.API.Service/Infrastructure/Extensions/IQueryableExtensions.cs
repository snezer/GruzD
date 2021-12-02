using GruzD.DataModel.Paging;
using System;
using System.Linq;
using System.Linq.Expressions;


namespace GruzD.Web.Infrastructure.Extensions
{
    public static class IQueryableExtensions
    {
        public static IQueryable<TEntity> ApplyOrderBy<TEntity>(this IQueryable<TEntity> source, PagedFilter pagedFilter)
        {
            if (string.IsNullOrEmpty(pagedFilter?.SortBy))
            {
                return source;
            }

            try
            {
                string command = pagedFilter.SortDirection == SortDirection.Desc ? "OrderByDescending" : "OrderBy";
                var type = typeof(TEntity);
                var property = type.GetProperty(pagedFilter.SortBy);
                var parameter = Expression.Parameter(type, "p");
                var propertyAccess = Expression.MakeMemberAccess(parameter, property);
                var orderByExpression = Expression.Lambda(propertyAccess, parameter);
                var resultExpression = Expression.Call(typeof(Queryable), command, new Type[] { type, property.PropertyType }, source.Expression, Expression.Quote(orderByExpression));
                return source.Provider.CreateQuery<TEntity>(resultExpression);
            }
            catch (Exception)
            {
                return source;
            }
        }
    }
}
