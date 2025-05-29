using Microsoft.EntityFrameworkCore;

namespace WebApi.Service
{
    public static class IQueryableExtensions
    {
        //relationship
        public static IQueryable<TEntity> ApplyIncludes<TEntity>(this IQueryable<TEntity> query,
                                                              string? include,
                                                              string?[] allowedIncludes)
        where TEntity : class
        {
            if (string.IsNullOrWhiteSpace(include))
                return query;

            var includeFields = include.Split(',', StringSplitOptions.RemoveEmptyEntries);

            foreach (var includeField in includeFields)
            {
                query = query.Include(includeField.Trim());
            }
            return query;
        }
        public static async Task<PagedResult<T>> GetQueryAsync<T>(this IQueryable<T> query,
                                                                  int page,
                                                                  int pageSize,
                                                                  string sortBy,
                                                                  string sortOrder)
        {
            var result = new PagedResult<T>
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalCount = await query.CountAsync()
            };

            var sortFields = sortBy.Split(',', StringSplitOptions.RemoveEmptyEntries);
            var sortOrders = sortOrder?.Split(',', StringSplitOptions.RemoveEmptyEntries);

            //sorting
            IOrderedQueryable<T>? orderedQuery = null;
            for (int i = 0; i < sortFields.Length; i++)
            {
                var field = sortFields[i].Trim();
                var descending = sortOrders != null && i < sortOrders.Length && sortOrders[i].ToLower() == "desc";

                if (i == 0)
                {
                    orderedQuery = descending
                        ? query.OrderByDescending(e => e != null ? EF.Property<object>(e, field) : null)
                        : query.OrderBy(e => e != null ? EF.Property<object>(e, field) : null);
                }
                else
                {
                    orderedQuery = descending
                        ? orderedQuery?.ThenByDescending(e => e != null ? EF.Property<object>(e, field) : null)
                        : orderedQuery?.ThenBy(e => e!= null ? EF.Property<object>(e, field) : null);
                }
            }

            var sortQuery =  orderedQuery ?? query;

            // pagination 
            result.Items = await sortQuery
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return result;
        }
    }
}
