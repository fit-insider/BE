using System.Threading.Tasks;
using FI.API.Requests.Versions;
using FI.Business.Versions.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FI.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VersionController : Controller
    {
        private readonly IMediator _mediator;

        public VersionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("get-version")]
        public async Task<ActionResult<string>> GetVersion([FromQuery] GetVersionRequest request)
        {
            var result = await _mediator.Send(request.ToQuery());

            return Ok(result);
        }

        [HttpGet("last-version")]
        public async Task<ActionResult<string>> GetVersion()
        {
            var result = await _mediator.Send(new GetLastVersionQuery { });

            return Ok(result);
        }
    }
}
