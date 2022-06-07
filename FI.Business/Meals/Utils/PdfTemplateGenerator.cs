using System;
using System.Linq;
using System.Text;
using FI.Data.Models.Meals;

namespace FI.Business.Meals.Utils
{
    public static class PdfTemplateGenerator
    {
        public static string removeUnit(string  val)
        {
            var words = val.Split();
            return string.Join(" ", words.Skip(1).ToArray());
        }

        public static string GetHTMLMealplanString(Mealplan mealplan)
        {
            var sb = new StringBuilder();
            sb.Append(@"
                        <html>
                            <head>
                            </head>
                            <body>
                            <div class='header'><h1>Your generated meal plan</h1></div>
                    ");

            sb.AppendFormat(@"
                        <div class='mealplanInfo'>
                            <div class='header'><h2>Mealplan information</h2></div>
      
                            <p class='mealplanInfoLabel'>Gender: {0}</p>
                            <p class='mealplanInfoLabel'>Target: {1}</p>
                            <p class='mealplanInfoLabel'>Height: {2}</p>
                            <p class='mealplanInfoLabel'>Weight: {3}</p>
                            <p class='mealplanInfoLabel'>Age: {4}</p>
                            <p class='mealplanInfoLabel'>Body type: {5}</p>
                            <p class='mealplanInfoLabel'>Usual activity: {6}</p>
                            <p class='mealplanInfoLabel'>Phisical activity: {7}</p>
                            <p class='mealplanInfoLabel'>Sleep: {8}</p>
                             <p class='mealplanInfoLabel'>Daily water intake: {9}</p>
                             <p class='mealplanInfoLabel'>Meals number: {10}</p>
                        </div>",
                        mealplan.MealplanData.Gender,
                        mealplan.MealplanData.Target,
                        mealplan.MealplanData.Height,
                        mealplan.MealplanData.Weight,
                        mealplan.MealplanData.Age,
                        mealplan.MealplanData.Body,
                        mealplan.MealplanData.UsualActivity,
                        mealplan.MealplanData.PhisicalActivity,
                        mealplan.MealplanData.Sleep,
                        mealplan.MealplanData.WaterIntake,
                        mealplan.MealplanData.MealsCount
            );


            sb.AppendFormat(@"
                            <div class='header'><h2>Daily Requirements</h2></div>
                            <div class='mealplanRequirements'>
                                <div class='dailyRequirement' id='calories'>Calories: {0}</div>
                                <div class='dailyRequirement' id='protein'>Protein: {1}</div>
                                <div class='dailyRequirement' id='carbs'>Cabrs: {2}</div>
                                <div class='dailyRequirement' id='fat'>Fat: {3}</div>
                            </div>", Math.Round(mealplan.Calories), Math.Round(mealplan.Protein), Math.Round(mealplan.Carb), Math.Round(mealplan.Fat));


            for (int i = 0; i < mealplan.DailyMeals.Count(); i++)
            {
                sb.AppendFormat(@"
                                <div class='day'>
                                    <p class='dayTitle'>Day {0}</p>", i + 1);

                foreach (Meal meal in mealplan.DailyMeals.ElementAt(i).Meals)
                {
                    sb.AppendFormat(@"
                                <div class='mealinfo'>
                                    <p class='mealName'>{0}</p>", meal.Name);

                    foreach (Ingredient ing in meal.Ingredients)
                    {
                        sb.AppendFormat(@"
                                        <p class='ingredient'>{0} {1} gr</p>",
                                        removeUnit(ing.Text), Math.Ceiling(ing.Weight));
                    }

                    sb.Append(@"</div>");
                }
                sb.Append(@"</div>");
            }


            sb.Append(@"</body></html>");

            return sb.ToString();
        }
    }
}
