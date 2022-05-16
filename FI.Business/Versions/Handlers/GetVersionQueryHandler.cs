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
    public class GetVersionQueryHandler : IRequestHandler<GetVersionQuery, VersionCode>
    {
        private readonly FIContext _context;

        public GetVersionQueryHandler(FIContext context)
        {
            _context = context;
        }

        public async Task<VersionCode> Handle(GetVersionQuery request, CancellationToken cancellationToken)
        {
            return await _context.ApplicationVersions
                .Where(v => v.Name == request.Name)
                .ToVersionCode()
                .FirstOrDefaultAsync();
        }
    }
}
