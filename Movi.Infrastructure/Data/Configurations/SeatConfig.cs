using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Movi.Core.Domain.Entities;

namespace Movi.Infrastructure.Data.Configurations;

public class SeatConfig : IEntityTypeConfiguration<Seat>
{
    public void Configure(EntityTypeBuilder<Seat> builder)
    {
        // Configure the primary key
        builder.HasKey(s => s.Id);

        // Configure properties
        builder.Property(s => s.SeatNumber)
            .IsRequired()   // Make SeatNumber a required field
            .HasMaxLength(5);  // Limit the length of SeatNumber (e.g., "A1", "B10")

        builder.Property(s => s.IsAvailable)
            .IsRequired();  // IsAvailable should always have a value

        // Configure the relationship with Showtime
        builder.HasOne(s => s.Showtime)
            .WithMany(st => st.Seats)
            .HasForeignKey(s => s.ShowtimeId)
            .OnDelete(DeleteBehavior.Cascade);  // Cascade delete, seats will be deleted if the showtime is deleted

        // Indexes (optional, but useful for quick lookups of available seats for a showtime)
        builder.HasIndex(s => new { s.ShowtimeId, s.IsAvailable })
            .HasDatabaseName("IX_Seats_Showtime_Availability");

        // Table configuration (optional)
        builder.ToTable("Seats");
    }
}
