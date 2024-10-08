using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Movi.Core.Domain.Entities;

namespace Movi.Infrastructure.Data.Configurations;

public class CinemaHallConfig : IEntityTypeConfiguration<CinemaHall>
{
    public void Configure(EntityTypeBuilder<CinemaHall> builder)
    {
        // Define primary key
        builder.HasKey(ch => ch.Id);

        // Define properties
        builder.Property(ch => ch.Name)
            .IsRequired()          // Name is required
            .HasMaxLength(100);     // Limit the length of the Name property

        builder.Property(ch => ch.TotalSeats)
            .IsRequired();          // TotalSeats is required

        // Define one-to-many relationship with Showtime
        builder.HasMany(ch => ch.Showtimes)
            .WithOne(s => s.CinemaHall)
            .HasForeignKey(s => s.CinemaHallId)
            .OnDelete(DeleteBehavior.Cascade);  // Optional: Configure cascading delete behaviorthrow new NotImplementedException();
    }
}
