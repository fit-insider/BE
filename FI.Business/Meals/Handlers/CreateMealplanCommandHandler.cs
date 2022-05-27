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
            await ValidateIfUserExists(command.UserId);

            Console.WriteLine(command);

            MealplanGenerator mealplanGenerator = new MealplanGenerator(_context);
            Mealplan mealplan = mealplanGenerator.GenerateMealPlan(command);

            //_context.Meals.Add(command.ToMeal(_user));

            await _context.SaveChangesAsync(token);
            return mealplan;
        }

        public async Task ValidateIfUserExists(int userId)
        {
            _user = await _context.Users
                .Where(u => u.Id == userId)
                .FirstOrDefaultAsync();

            if (_user is null)
            {
                throw new CustomException(ErrorCode.CreateMealplan_user, "User does not exist!");
            }
        }

        private Mealplan GenerateNewMealplan(CreateMealplanCommand command)
        {
            return new Mealplan();

        }
    }
}
