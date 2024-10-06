using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Movi.Core.Domain.Entities;

namespace Movi.Infrastructure.Data.Configurations;

public class ReservationConfig : IEntityTypeConfiguration<Reservation>
{
    public void Configure(EntityTypeBuilder<Reservation> builder)
    {
        // Configure the primary key
        builder.HasKey(r => r.Id);

        // Configure the properties
        builder.Property(r => r.ReservedAt)
            .IsRequired();  // Make sure the reserved time is always provided

        // Configure relationships

        // Reservation has one Seat
        builder.HasOne(r => r.Seat)
            .WithMany()
            .HasForeignKey(r => r.SeatId)
            .OnDelete(DeleteBehavior.Restrict);  // Prevent deleting seat if a reservation exists

        // Reservation has one Showtime
        builder.HasOne(r => r.Showtime)
            .WithMany()
            .HasForeignKey(r => r.ShowtimeId)
            .OnDelete(DeleteBehavior.Cascade);  // Deleting a showtime will delete related reservations

        // Reservation has one User
        builder.HasOne(r => r.User)
            .WithMany()
            .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.Cascade);  // Deleting a user will delete their reservations

        // Indexes for optimizing queries (optional but recommended)
        builder.HasIndex(r => new { r.SeatId, r.ShowtimeId })
            .HasDatabaseName("IX_Reservations_Seat_Showtime")
            .IsUnique();  // Ensure a unique reservation for each seat and showtime combination

        // Table name (optional)
        builder.ToTable("Reservations");
    }
}
