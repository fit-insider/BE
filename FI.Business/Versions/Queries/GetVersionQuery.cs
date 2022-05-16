using FI.Business.Versions.Models;
using MediatR;

namespace FI.Business.Versions.Queries
{
    public class GetVersionQuery : IRequest<VersionCode>
    {
        public string Name { get; set; }
    }
}
