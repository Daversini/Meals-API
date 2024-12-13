using Meals_API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Meals_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MealController : ControllerBase
    {
        private MealRetriever? _mealRetriever;
        private static int _idChiamata = 0;

        [HttpGet("random")]
        public async Task<ActionResult> GetRandomMeal()
        {
            try
            {
                _mealRetriever = new MealRetriever();
                var meal = await _mealRetriever.RetrieveRandomMeal() ?? throw new Exception($"Risposta inesistente");

                _idChiamata++;
                Console.WriteLine("\nChiamata API " + _idChiamata);
                Console.WriteLine(meal);

                return Ok(meal);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    error = "Internal server error",
                    message = ex.Message
                });
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetMeal([FromQuery] string word)
        {
            try
            {
                _mealRetriever = new MealRetriever();
                var meal = await _mealRetriever.RetrieveMealByWord(word) ?? throw new Exception($"Risposta inesistente");

                if (meal.Meals == null) throw new Exception($"Nessun piatto trovato con il nome `{word}`");

                _idChiamata++;
                Console.WriteLine("\nChiamata API " + _idChiamata);
                Console.WriteLine(meal);

                return Ok(meal);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    error = "Internal server error",
                    message = ex.Message
                });
            }
        }
    }
}
