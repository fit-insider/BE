using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FI.Business.Meals.Queries;
using FI.Data;
using FI.Data.Models.Meals;
using FI.Data.Models.Meals.DTOs;
using FI.Data.Models.Users;
using FI.Infrastructure.Models.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FI.Business.Meals.Handlers
{
    internal class GetMealplanQueryHandler : IRequestHandler<GetMealplanQuery, MealplanDTO>
    {
        private readonly FIContext _context;
        private User _dbUser;

        public GetMealplanQueryHandler(FIContext context)
        {
            _context = context;
        }

        public async Task<MealplanDTO> Handle(GetMealplanQuery request, CancellationToken cancellationToken)
        {
            await ValidateIfUserExists(request.UserId);

            Mealplan mealplan = _context.Mealplans
                .Where(mp => mp.Id == request.MealplanId && mp.UserId == request.UserId)
                .Include(mealplan => mealplan.MealplanData)
                .Include(mealplan => mealplan.DailyMeals)
                    .ThenInclude(day => day.Meals)
                        .ThenInclude(meal => meal.Ingredients)
                .Include(mealplan => mealplan.DailyMeals)
                    .ThenInclude(day => day.Meals)
                        .ThenInclude(meal => meal.Nutrients)
                 .FirstOrDefault();

            if (mealplan is null)
            {
                throw new CustomException(ErrorCode.EditUser_User, "Mealplan does not exist!");
            }

            return mealplan.toDTO();
        }

        private async Task ValidateIfUserExists(string userId)
        {
            _dbUser = await _context.Users
                .Include(u => u.Detail)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (_dbUser is null)
            {
                throw new CustomException(ErrorCode.EditUser_User, "User does not exist!");
            }
        }
    }
}
