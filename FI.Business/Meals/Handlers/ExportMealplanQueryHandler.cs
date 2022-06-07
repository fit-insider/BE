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
using FI.Business.Meals.Queries;
using DinkToPdf;
using FI.Business.Meals.Utils;
using System.IO;
using DinkToPdf.Contracts;
using System.Reflection;

namespace FI.Business.Meals.Handlers
{
    public class ExportMealplanQueryHandler : IRequestHandler<ExportMealplanQuery, byte[]>
    {
        private readonly FIContext _context;
        private IConverter _converter;
        private User _dbUser;

        public ExportMealplanQueryHandler(FIContext context, IConverter converter)
        {
            _context = context;
            _converter = converter;
        }

        public async Task<byte[]> Handle(ExportMealplanQuery request, CancellationToken token)
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
                DocumentTitle = "mealplan"
            };

            string stylesheet = Path.Combine(Directory.GetCurrentDirectory(), "Assets", "style.css");
            var objectSettings = new ObjectSettings
            {
                PagesCount = true,
                HtmlContent = PdfTemplateGenerator.GetHTMLMealplanString(mealplan),
                WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet = stylesheet },
                HeaderSettings = { FontName = "Arial", FontSize = 9, Right = "Page [page] of [toPage]", Line = true },
                FooterSettings = { FontName = "Arial", FontSize = 9, Line = true, Center = "Generated mealplan" }
            };

            var pdf = new HtmlToPdfDocument()
            {
                GlobalSettings = globalSettings,
                Objects = { objectSettings }
            };

            var file = _converter.Convert(pdf);

            return file;
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
