using FI.API.Requests.Meals;
using FI.API.Requests.Users;
using FI.Business.Meals.Queries;
using FI.Business.Users.Models;
using FI.Data.Models.Meals;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
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

        [HttpGet("mealplan/{userId}&{mealplanId}")]
        public async Task<ActionResult<Mealplan>> GetMealplan([FromRoute] string userId, [FromRoute] string mealplanId)
        {
            var result = await _mediator.Send(new GetMealplanQuery { UserId = userId, MealplanId = mealplanId });

            return Ok(result);
        }

        [HttpGet("mealplans/{userId}")]
        public async Task<ActionResult<UserIdentifier>> GetMealplans([FromRoute] string userId)
        {
            var result = await _mediator.Send(new GetMealplansQuery { UserId = userId });

            return Ok(result);
        }

        [HttpGet("export-mealplan/{userId}&{mealplanId}")]
        public async Task<ActionResult> ExportMealplan([FromRoute] string userId, [FromRoute] string mealplanId)
        {
            var result = await _mediator.Send(new ExportMealplanQuery { UserId = userId, MealplanId = mealplanId });

            return File(result, "application/pdf");
        }

        [HttpGet("create-shopping-list/{userId}&{mealplanId}")]
        public async Task<ActionResult> CreateShoppingList([FromRoute] string userId, [FromRoute] string mealplanId)
        {
            var result = await _mediator.Send(new CreateShoppingListQuery { UserId = userId, MealplanId = mealplanId });

            return File(result, "application/pdf");
        }

    }
}
