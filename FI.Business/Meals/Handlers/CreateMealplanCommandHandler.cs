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

namespace FI.Business.Meals.Handlers
{
    public class CreateMealplanCommandHandler : IRequestHandler<CreateMealplanCommand, Mealplan>
    {
        private readonly FIContext _context;
        private User _user;

        public CreateMealplanCommandHandler(FIContext context)
        {
            _context = context;
        }

        public async Task<Mealplan> Handle(CreateMealplanCommand command, CancellationToken token)
        {
            MealplanGenerator mealplanGenerator = new MealplanGenerator(_context);
            Mealplan mealplan = mealplanGenerator.GenerateMealPlan(command);

            //_context.Meals.Add(command.ToMeal(_user));

            await _context.SaveChangesAsync(token);
            return mealplan;
        }
    }
}
