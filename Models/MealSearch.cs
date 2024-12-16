using System.ComponentModel.DataAnnotations;

namespace Meals_API.Models
{
    public class MealSearch
    {
        [Key]
        public int id { get; set; }
        public string? searchQuery { get; set; }
        public DateTime createdAt { get; set; } = DateTime.Now;
        public bool searchSuccess { get; set; }

        public override string ToString()
        {
            return $"Query: {searchQuery}\nCreated at: {createdAt}\nIs successful: {searchSuccess}";
        }
    }
}
