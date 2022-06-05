using System;
using FI.Data;
using System.Linq;
using FI.Business.Meals.Commands;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using FI.Data.Models.Users;
using FI.Infrastructure.Models.Exceptions;
using Microsoft.EntityFrameworkCore;
using FI.Data.Models.Meals;
using FI.Data.Models.Meals.DTOs;

namespace FI.Business.Meals.Handlers
{
    public class CreateMealplanCommandHandler : IRequestHandler<CreateMealplanCommand, MealplanDTO>
    {
        private readonly FIContext _context;
        private User _user;

        public CreateMealplanCommandHandler(FIContext context)
        {
            _context = context;
        }

        public async Task<MealplanDTO> Handle(CreateMealplanCommand command, CancellationToken token)
        {
            MealplanGenerator mealplanGenerator = new MealplanGenerator(_context);
            MealplanDTO mealplan = mealplanGenerator.GenerateMealPlan(command);



            //_context.Meals.Add(command.ToMeal(_user));

            await _context.SaveChangesAsync(token);
            return mealplan;
        }
    }
}
