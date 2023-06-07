using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace WarehouseWeb.Configuration
{
    public static class OrderByExtension
    {


        public static IQueryable<T> OrderByString<T>(this IQueryable<T> source, string propertyName, string sortDirection)
        {

            try
            {
                if (source == null || propertyName == null)
                {
                    return source;
                }

                propertyName = propertyName.First().ToString().ToUpper(CultureInfo.InvariantCulture) + propertyName.Substring(1);
                var type = typeof(T);
                var arg = Expression.Parameter(type, "x");

                var propertyInfo = type.GetProperty(propertyName);
                var mExpr = Expression.Property(arg, propertyInfo);
                type = propertyInfo.PropertyType;

                var delegateType = typeof(Func<,>).MakeGenericType(typeof(T), type);
                var lambda = Expression.Lambda(delegateType, mExpr, arg);

                var methodName = !string.IsNullOrEmpty(sortDirection) && sortDirection.ToLower(CultureInfo.InvariantCulture) == "desc" ? "OrderByDescending" : "OrderBy";
                var orderedSource = typeof(Queryable).GetMethods().Single(
                    method => method.Name == methodName
                            && method.IsGenericMethodDefinition
                            && method.GetGenericArguments().Length == 2
                            && method.GetParameters().Length == 2)
                    .MakeGenericMethod(typeof(T), type)
                    .Invoke(null, new object[] { source, lambda });

                return (IQueryable<T>)orderedSource;
            }
            catch (Exception)
            {
                return source;
            }
        }







        public static IEnumerable<T> orderBy<T>(this IEnumerable<T> list, string sortExpression)
        {
            sortExpression += "";
            string[] parts = sortExpression.Split(' ');
            bool descending = false;
            string property = "";

            if (parts.Length > 0 && parts[0] != "")
            {
                property = parts[0];

                if (parts.Length > 1)
                {
                    descending = parts[1].ToLower().Contains("desc");
                }

                PropertyInfo prop = typeof(T).GetProperty(property);

                if (prop == null)
                {
                    throw new Exception("No property '" + property + "' in + " + typeof(T).Name + "'");
                }

                if (descending)
                    return list.OrderByDescending(x => prop.GetValue(x, null));
                else
                    return list.OrderBy(x => prop.GetValue(x, null));
            }

            return list;
        }
    }
}
