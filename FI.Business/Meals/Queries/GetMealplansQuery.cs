using System;
using FI.Business.Shared.Models;
using FI.Data.Models.Meals;
using MediatR;


namespace FI.Business.Meals.Queries
{
    public class GetMealplansQuery : IRequest<PageInfo<Mealplan>>
    {
        public int UserId { get; set; }
    }
}
