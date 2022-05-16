using System.Linq;

namespace FI.Data
{
    public static class GenericExtensions
    {
        public static IQueryable<T> GetPage<T>(this IQueryable<T> query, int pageNumber, int pageSize)
        {
            return query.Skip((pageNumber - 1) * pageSize).Take(pageSize);
        }
    }
}
