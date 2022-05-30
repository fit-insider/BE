using FI.API.Requests.Meals;
using FI.API.Requests.Users;
using FI.Business.Meals.Queries;
using FI.Business.Users.Models;
using FI.Data.Models.Meals;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FI.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class MealController : Controller
    {
        private readonly IMediator _mediator;

        public MealController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create")]
        public async Task<ActionResult<bool>> CreateMealplan([FromBody] CreateMealplanRequest request)
        {
            var result = await _mediator.Send(request.ToCommand());

            return Ok(result);
        }

        [HttpPost("mealplan/{mealplanId}")]
        public async Task<ActionResult<Mealplan>> GetMealplan([FromBody] GetMealplanRequest request)
        {
            var result = await _mediator.Send(request.ToQuery());

            return Ok(result);
        }

        [HttpGet("mealplans/{id}")]
        public async Task<ActionResult<UserIdentifier>> GetMealplans([FromRoute] string id)
        {
            var result = await _mediator.Send(new GetMealplansQuery { UserId = id });

            return Ok(result);
        }
    }
}
