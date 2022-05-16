using FI.Business.Versions.Models;
using System.Linq;
using FI.Data.Models.ApplicationVersions;

namespace FI.Business.Versions
{
    public static class VersionExtensions
    {
        public static IQueryable<VersionCode> ToVersionCode(this IQueryable<ApplicationVersion> query)
        {
            return query.Select(q => new VersionCode
            {
                Version = q.Version
            });
        }
    }
}
