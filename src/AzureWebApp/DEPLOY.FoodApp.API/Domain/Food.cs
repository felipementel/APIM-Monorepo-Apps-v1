using System.Text.Json.Serialization;

namespace DEPLOY.FoodApp.API.Domain
{
    public class Food
    {
        [JsonConstructor]
        public Food(
            Guid id,
            string name,
            decimal price,
            FoodType type)
        {
            Id = id;
            Name = name;
            Price = price;
            Type = type;
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
        }

        public Food(
            string name,
            decimal price,
            FoodType type)
        {
            Id = Guid.NewGuid();
            Name = name;
            Price = price;
            Type = type;
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
        }

        public Guid Id { get; init; }

        public decimal Price { get; init; }

        public FoodType Type { get; init; }

        public DateTime CreatedAt { get; init; }

        public DateTime UpdatedAt { get; private set; }

        public void Update(string make, string model, long year, bool sold)
        {
            UpdatedAt = DateTime.Now;
        }
    }

    public enum FoodType
    {
        Pizza,
        Burger,
        Pasta,
        Sushi,
        Salad
    }
}
