using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FI.Business.Versions.Models;
using FI.Business.Versions.Queries;
using FI.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FI.Business.Versions.Handlers
{
    public class GetLastVersionQueryHandler : IRequestHandler<GetLastVersionQuery, VersionCode>
    {
        private readonly FIContext _context;

        public GetLastVersionQueryHandler(FIContext context)
        {
            _context = context;
        }

        public async Task<VersionCode> Handle(GetLastVersionQuery request, CancellationToken cancellationToken)
        {
            return await _context.ApplicationVersions
                .OrderByDescending(v => v.Id)
                .ToVersionCode()
                .FirstOrDefaultAsync();
        }
    }
}
