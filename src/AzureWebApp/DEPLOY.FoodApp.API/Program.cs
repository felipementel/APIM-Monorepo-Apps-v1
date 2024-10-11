
using DEPLOY.FoodApp.API.Domain;
using DEPLOY.FoodApp.API.Infra.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Json;

namespace DEPLOY.FoodApp.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddAuthorization();

            //configure to reiceive enum as string
            //builder.Services.AddControllers().AddJsonOptions(options =>
            //{
            //    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            //});
            builder.Services.Configure<JsonOptions>(o => o.SerializerOptions.Converters.Add(new JsonStringEnumConverter()));
            builder.Services.Configure<Microsoft.AspNetCore.Mvc.JsonOptions>(o => o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

            builder.Services.AddRouting(opt =>
            {
                opt.LowercaseUrls = true;
                opt.LowercaseQueryStrings = true;
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
                    Summary = "/food/{food.Id}",
                    Description = "The food item to create",
                    Tags = new List<OpenApiTag> { new OpenApiTag { Name = "Food" } },
                    RequestBody = new OpenApiRequestBody
                    {
                        Content = new Dictionary<string, OpenApiMediaType>
                        {
                            ["application/json"] = new OpenApiMediaType
                            {
                                Schema = new OpenApiSchema
                                {
                                    Type = "object",
                                    Properties = new Dictionary<string, OpenApiSchema>
                                    {
                                        ["price"] = new OpenApiSchema
                                        {
                                            Type = "number",
                                            Format = "float",
                                            Example = new OpenApiFloat(2.35F)
                                        },
                                        ["type"] = new OpenApiSchema
                                        {
                                            Type = "string",
                                            Example = new OpenApiString("Pizza")
                                        }
                                    }
                                }
                            }
                        }
                    },
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
