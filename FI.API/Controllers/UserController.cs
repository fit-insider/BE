using FI.API.Requests.Users;
using FI.Business.Users.Models;
using FI.Business.Users.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FI.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("add-user")]
        public async Task<ActionResult<bool>> AddUser([FromBody] AddUserRequest request)
        {
            var result = await _mediator.Send(request.ToCommand());

            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDetail>> Login([FromBody] LoginRequest request)
        {
            var result = await _mediator.Send(request.ToQuery());

            return Ok(result);
        }

        [HttpGet("display/{id}")]
        public async Task<ActionResult<UserIdentifier>> GetUserDetails([FromRoute] int id)
        {
            var result = await _mediator.Send(new GetUserDetailsQuery { UserId = id });

            return Ok(result);
        }

        [HttpPut("edit/{id}")]
        public async Task<ActionResult<UserDetail>> EditUser([FromRoute] int id, [FromBody] EditUserRequest request)
        {
            request.Id = id;

            var result = await _mediator.Send(request.ToCommand());

            return Ok(result);
        }
    }
}
