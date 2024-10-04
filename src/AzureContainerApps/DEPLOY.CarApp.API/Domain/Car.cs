using System.Text.Json.Serialization;

namespace DEPLOY.CarApp.API.Domain
{
    public class Car
    {
        [JsonConstructor]
        public Car(Guid id, string make, string model, long year)
        {
            Id = id;
            Make = make;
            Model = model;
            Year = year;
            Sold = false;
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
        }

        public Car(string make, string model, long year)
        {
            Id = Guid.NewGuid();
            Make = make;
            Model = model;
            Year = year;
            Sold = false;
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
        }

        public Guid Id { get; init; }

        public string Make { get; init; }

        public string Model { get; init; }

        public long Year { get; init; }

        public bool Sold { get; private set; }

        public DateTime CreatedAt { get; init; }

        public DateTime UpdatedAt { get; private set; }

        public void Update(string make, string model, long year, bool sold)
        {
            UpdatedAt = DateTime.Now;
        }

        public void Sell()
        {
            Sold = true;
            UpdatedAt = DateTime.Now;
        }
    }
}
