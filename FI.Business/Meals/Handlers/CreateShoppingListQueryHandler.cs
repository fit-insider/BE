using FI.Data;
using System.Linq;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using FI.Data.Models.Users;
using FI.Infrastructure.Models.Exceptions;
using Microsoft.EntityFrameworkCore;
using FI.Data.Models.Meals;
using FI.Business.Meals.Queries;
using DinkToPdf;
using FI.Business.Meals.Utils;
using System.IO;
using DinkToPdf.Contracts;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace FI.Business.Meals.Handlers
{
    public class CreateShoppingListQueryHandler : IRequestHandler<CreateShoppingListQuery, byte[]>
    {
        private readonly FIContext _context;
        private IConverter _converter;
        private User _dbUser;

        public CreateShoppingListQueryHandler(FIContext context, IConverter converter)
        {
            _context = context;
            _converter = converter;
        }

        public async Task<byte[]> Handle(CreateShoppingListQuery request, CancellationToken token)
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

            var globalSettings = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings { Top = 10 },
                DocumentTitle = "Shopping List"
            };

            string stylesheet = Path.Combine(Directory.GetCurrentDirectory(), "Assets", "list-style.css");
            var objectSettings = new ObjectSettings
            {
                PagesCount = true,
                HtmlContent = PdfTemplateGenerator.GetHTMLShoppingListString(createShoppingList(mealplan)),
                WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet = stylesheet }
            };

            var pdf = new HtmlToPdfDocument()
            {
                GlobalSettings = globalSettings,
                Objects = { objectSettings }
            };

            var file = _converter.Convert(pdf);

            return file;
        }


        private List<Ingredient> createShoppingList(Mealplan mealplan)
        {
            List<Ingredient> ingredients = new List<Ingredient>();

            foreach(Day day in mealplan.DailyMeals)
            {
                foreach(Meal meal in day.Meals)
                {
                    foreach(Ingredient ing in meal.Ingredients)
                    {
                        if (ing.Weight > 0.2)
                        {
                            ing.Text = removeUnit(ing.Text).ToLower();
                            if (ingredients.Any(ingredient => ingredient.Text == ing.Text))
                            {
                                Ingredient i = ingredients.Where(ingredient => ingredient.Text == ing.Text).First();
                                i.Weight += ing.Weight;
                            }
                            else
                            {
                                ingredients.Add(ing);
                            }
                        }
                    }
                }
            }

            return ingredients;
        }

        public static bool isUnitWord(string val)
        {
            Regex regex = new Regex(@"cup|teaspoon|cups|tablespoon|\*|tbsp|tsp|serving|clove|tablespoons|teaspoons");

            return regex.IsMatch(val);
        }
        public static bool isNumber(string val)
        {
            Regex regex = new Regex(@"\d+");

            return regex.IsMatch(val);
        }

        public static bool isFraction(string val)
        {
            Regex regex = new Regex(@"\d+(\.\d+)?/\d+(\.\d+)?");

            return regex.IsMatch(val);
        }

        public static bool isProcent(string val)
        {
            Regex regex = new Regex(@"^*\u00BC-\u00BE\u2150-\u215E$");
            return regex.IsMatch(val);
        }

        public static string removeUnit(string val)
        {
            var words = val.Split(' ');
            int position = 0;
            while (isNumber(words[position]) || isFraction(words[position]) || isProcent(words[position]) || isUnitWord(words[position]))
            {
                position++;
            }
            return string.Join(" ", words.Skip(position).ToArray());
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
