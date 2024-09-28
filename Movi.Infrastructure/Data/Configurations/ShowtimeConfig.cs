using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Movi.Core.Domain.Entities;

namespace Movi.Infrastructure.Data.Configurations;

public class ShowtimeConfig : IEntityTypeConfiguration<Showtime>
{
    public void Configure(EntityTypeBuilder<Showtime> builder)
    {
        // Define primary key
        builder.HasKey(s => s.Id);

        // Configure StartTime as required
        builder.Property(s => s.StartTime)
               .IsRequired();

        // Define relationship with Movie
        builder.HasOne(s => s.Movie)
               .WithMany(m => m.Showtimes)
               .HasForeignKey(s => s.MovieId)
               .OnDelete(DeleteBehavior.Cascade);  // Cascade delete showtimes if a movie is deleted

        // Define relationship with CinemaHall
        builder.HasOne(s => s.CinemaHall)
               .WithMany(ch => ch.Showtimes)
               .HasForeignKey(s => s.CinemaHallId)
               .OnDelete(DeleteBehavior.Cascade);  // Cascade delete showtimes if a cinema hall is deleted
    }
}
