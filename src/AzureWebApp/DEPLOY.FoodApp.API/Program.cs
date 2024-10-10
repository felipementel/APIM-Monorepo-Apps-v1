
using DEPLOY.FoodApp.API.Domain;
using DEPLOY.FoodApp.API.Infra.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;

namespace DEPLOY.FoodApp.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddAuthorization();

            //configure to reiceive enum as string
            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<Context>(options =>
            {
                options.UseInMemoryDatabase("FoodApp");
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.DisplayRequestDuration();
                });
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapGet("/foods", (Context context) =>
            {
                return Results.Ok(context.Foods.ToList());
            })
            .WithOpenApi();

            app.MapPost("/foods", (Context context, Food food) =>
            {
                context.Foods.Add(food);
                context.SaveChanges();
                return Results.Created($"/food/{food.Id}", food);
            })
                .WithOpenApi(operation => new OpenApiOperation
                {
                    Summary = "Creates a Food item.",
                    Description = @"
                    <summary>
                    Creates a Food item.
                    </summary>
                    <param name=""food"">The food item to create</param>
                    <returns>A newly created Food item</returns>
                    <remarks>
                    Sample request:

                        POST /food
                        {
                           ""price"": 2.35,
                           ""type"": ""Pizza""
                        }

                    </remarks>
                    <response code=""201"">Returns the newly created item</response>",
                    Tags = new List<OpenApiTag> { new OpenApiTag { Name = "Food" } },
                    Responses = new OpenApiResponses
                    {
                        ["201"] = new OpenApiResponse
                        {
                            Description = "Returns the newly created item",
                            Content = new Dictionary<string, OpenApiMediaType>
                            {
                                ["application/json"] = new OpenApiMediaType
                                {
                                    Example = new OpenApiObject
                                    {
                                        ["id"] = new OpenApiString("123e4567-e89b-12d3-a456-426614174000"),
                                        ["price"] = new OpenApiFloat(2.35F),
                                        ["type"] = new OpenApiString("Pizza")
                                    }
                                }
                            }
                        }
                    }
                });



            app.MapGet("/foods/{id}", (Context context, Guid id) =>
            {
                var food = context.Foods.Find(id);
                if (food == null)
                {
                    return Results.NotFound();
                }
                return Results.Ok(food);
            })
                .WithOpenApi();

            app.MapPut("/foods/{id}", (Context context, Guid id, Food food) =>
            {
                var existingFood = context.Foods.Find(id);
                if (existingFood == null)
                {
                    return Results.NotFound();
                }

                context.Entry(existingFood).CurrentValues.SetValues(food);
                context.Update(existingFood);
                context.SaveChanges();

                return Results.Ok(existingFood);
            })
                .WithOpenApi();

            app.MapDelete("/foods/{id}", (Context context, Guid id) =>
            {
                var food = context.Foods.Find(id);
                if (food == null)
                {
                    return Results.NotFound();
                }
                context.Foods.Remove(food);
                context.SaveChanges();
                return Results.NoContent();
            })
                .WithOpenApi();

            //
            app.MapGet("/foods/bug", (Context context) =>
            {
                throw new Exception("This is a bug");
            })
          .WithOpenApi();

            app.Run();
        }
    }
}
