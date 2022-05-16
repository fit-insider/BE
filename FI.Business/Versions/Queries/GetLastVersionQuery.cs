using FI.Business.Versions.Models;
using MediatR;

namespace FI.Business.Versions.Queries
{
    public class GetLastVersionQuery : IRequest<VersionCode>
    {
        public string Name { get; set; }
    }
}
