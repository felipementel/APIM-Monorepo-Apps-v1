
using DEPLOY.CarApp.API.Infra.Database;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

namespace DEPLOY.CarApp.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            builder.Services.AddRouting(opt =>
            {
                opt.LowercaseUrls = true;
                opt.LowercaseQueryStrings = true;
            });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.EnableAnnotations();
            });

            builder.Services.Configure<ScalarOptions>(options => options.Title = "Canal DEPLOY Scalar");

            builder.Services.AddDbContext<Context>(options =>
            {
                options.UseInMemoryDatabase("CarApp");
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

                app.UseSwagger(options =>
                {
                    options.RouteTemplate = "openapi/{documentName}.json"; // Scalar
                });

                app.MapScalarApiReference();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
