using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WarehouseWeb.Configuration
{
    public static class ProductExtensions
    {
        public static IQueryable<T> CountOut<T>(this IQueryable<T> query, out int count)
        {
            count = 0;
            try
            {
                count = query.Count();
            }
            catch
            {

            }
            return query;
        }
    }
}
