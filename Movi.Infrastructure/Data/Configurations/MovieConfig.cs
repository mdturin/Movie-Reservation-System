using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Movi.Core.Domain.Entities;

namespace Movi.Infrastructure.Data.Configurations;

public class MovieConfig : IEntityTypeConfiguration<Movie>
{
    public void Configure(EntityTypeBuilder<Movie> builder)
    {
        // Define primary key
        builder.HasKey(m => m.Id);

        // Configure properties
        builder.Property(m => m.Title)
               .IsRequired()            // Title is required
               .HasMaxLength(200);       // Limit title length to 200 characters

        builder.Property(m => m.Description)
               .HasMaxLength(1000);      // Optional, limit description length to 1000 characters

        builder.Property(m => m.DurationInMinutes)
               .IsRequired();            // Duration is required

        builder.Property(m => m.Language)
               .HasMaxLength(50);        // Optional, limit language field to 50 characters

        builder.Property(m => m.Rating)
               .HasMaxLength(5);         // Optional, limit rating field to 5 characters (e.g., PG-13, R)

        // Configure TicketPrice precision
        builder.Property(m => m.TicketPrice)
               .HasColumnType("decimal(18, 2)");  // Decimal precision for price

        // Define one-to-many relationship with Showtimes
        builder.HasMany(m => m.Showtimes)
               .WithOne(s => s.Movie)
               .HasForeignKey(s => s.MovieId)
               .OnDelete(DeleteBehavior.Cascade);  // Optional: cascade delete showtimes if a movie is deleted
    }
}
