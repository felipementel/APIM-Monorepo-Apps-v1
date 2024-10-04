using DEPLOY.CarApp.API.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DEPLOY.CarApp.API.Infra.Database.Configurations
{
    public class CarConfiguration : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder
                .Property(x => x.Make)
                .IsRequired();

            builder
                .Property(x => x.Model)
                .IsRequired();

            builder
                .Property(x => x.Year)
                .IsRequired();
        }
    }
}
