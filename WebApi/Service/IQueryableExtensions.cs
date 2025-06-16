using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace WebApi.Service
{
    public static class IQueryableExtensions
    {
        public static IQueryable<TEntity> ApplyIncludes<TEntity>(
            this IQueryable<TEntity> query,
            string[] allowedIncludes,
            string? include)
            where TEntity : class
        {
            if (string.IsNullOrWhiteSpace(include)) return query;

            var includes = include.Split(',', StringSplitOptions.RemoveEmptyEntries)
                                  .Select(i => i.Trim())
                                  .Where(i => allowedIncludes.Contains(i));

            foreach (var inc in includes)
            {
                query = query.Include(inc);
            }

            return query;
        }
        //Complete the logic
        public static IQueryable<TEntity> ApplyFilters<TEntity>(this IQueryable<TEntity> query,
                                                                string[] allowedFilters,
                                                                string? filter)
        where TEntity : class
        {

            return query;
        }


        public static IQueryable<T> ApplySearch<T>(
            this IQueryable<T> query,
            string search,
            string[] fields)
        {
            if (string.IsNullOrWhiteSpace(search) || fields == null || fields.Length == 0)
                return query;

            var parameter = Expression.Parameter(typeof(T), "x");
            var toLower = typeof(string).GetMethod(nameof(string.ToLower), Type.EmptyTypes)!;
            var contains = typeof(string).GetMethod(nameof(string.Contains), new[] { typeof(string) })!;
            var searchLower = Expression.Constant(search.ToLower());

            Expression? predicate = null;

            foreach (var field in fields)
            {
                Expression containsExpression;
                var property = Expression.Property(parameter, field);
                var toLowerCall = Expression.Call(property, toLower);
                containsExpression = Expression.Call(toLowerCall, contains, searchLower);
                predicate = predicate == null ? containsExpression : Expression.OrElse(predicate, containsExpression);
            }

            var lambda = Expression.Lambda<Func<T, bool>>(predicate!, parameter);
            return query.Where(lambda);
        }

        public static IQueryable<T> ApplySorting<T>(
            this IQueryable<T> query,
            string sortBy,
            string? sortOrder)
        {
            var sortFields = sortBy.Split(',', StringSplitOptions.RemoveEmptyEntries);
            var sortOrders = (sortOrder ?? string.Empty).Split(',', StringSplitOptions.RemoveEmptyEntries);

            IOrderedQueryable<T>? orderedQuery = null;

            for (int i = 0; i < sortFields.Length; i++)
            {
                var field = sortFields[i].Trim();
                var isDescending = sortOrders.Length > i && sortOrders[i].Trim().Equals("desc", StringComparison.OrdinalIgnoreCase);

                if (i == 0)
                {
                    orderedQuery = isDescending
                        ? query.OrderByDescending(e => EF.Property<object>(e, field))
                        : query.OrderBy(e => EF.Property<object>(e, field));
                }
                else
                {
                    orderedQuery = isDescending
                        ? orderedQuery!.ThenByDescending(e => EF.Property<object>(e, field))
                        : orderedQuery!.ThenBy(e => EF.Property<object>(e, field));
                }
            }

            return orderedQuery ?? query;
        }

        public static async Task<PagedResult<T>> ToPagedResultAsync<T>(
            this IQueryable<T> query,
            int page,
            int pageSize)
        {
            var total = await query.CountAsync();
            var items = await query.Skip((page - 1) * pageSize)
                                   .Take(pageSize)
                                   .ToListAsync();

            return new PagedResult<T>
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalCount = total,
                Data = items
            };
        }
    }
}
