using System.Collections.Generic;
using FI.Data.Models.Meals.DTOs;
using MediatR;


namespace FI.Business.Meals.Queries
{
    public class GetMealplansQuery : IRequest<List<MealplanDTO>>
    {
        public string UserId { get; set; }
    }
}
