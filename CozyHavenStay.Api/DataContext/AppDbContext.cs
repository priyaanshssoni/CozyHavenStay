using System;
using CozyHavenStay.Data.Models;
using Microsoft.EntityFrameworkCore;
using static CozyHavenStay.Data.Models.Booking;

namespace CozyHavenStay.Api.DataContext;

    public class AppDbContext : DbContext
    {
     public AppDbContext(DbContextOptions<AppDbContext> option) : base(option) { }

    public AppDbContext()
     {
     }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId);

            entity.HasIndex(u => u.Email)
                  .IsUnique();
            entity.HasIndex(u => u.Username)
            .IsUnique();
        });

        modelBuilder.Entity<City>()
                 .Property(h => h.ImageLinks)
                 .HasConversion(
                     v => string.Join(',', v),
                     v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList());

        //modelBuilder.Entity<City>()
        //  .HasMany(ho => ho.Hotels);

        modelBuilder.Entity<Hotel>()
                     .Property(h => h.ImageLinks)
                     .HasConversion(
                         v => string.Join(',', v),
                         v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList());

        modelBuilder.Entity<Hotel>().HasOne(h => h.Owner)
            .WithMany(u => u.Hotels)
            .HasForeignKey(h => h.OwnerId)
            .OnDelete(DeleteBehavior.NoAction);

       

        modelBuilder.Entity<Room>()
            .Property(h => h.ImageLinks)
            .HasConversion(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList());

        modelBuilder.Entity<Review>().HasOne(r => r.User)
            .WithMany(u => u.Reviews)
            .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Review>().HasOne(r => r.Hotel)
            .WithMany(h => h.Reviews)
            .HasForeignKey(r => r.HotelId)
            .OnDelete(DeleteBehavior.NoAction);


        modelBuilder.Entity<HotelAmenity>()
        .HasKey(ha => new { ha.HotelId, ha.AmenityId });

        modelBuilder.Entity<HotelAmenity>()
            .HasOne(ha => ha.Hotel)
            .WithMany(h => h.HotelAmenities)
            .HasForeignKey(ha => ha.HotelId);

        modelBuilder.Entity<HotelAmenity>()
            .HasOne(ha => ha.Amenity)
            .WithMany(a => a.HotelAmenity)
            .HasForeignKey(ha => ha.AmenityId);

        modelBuilder.Entity<Room>()
            .Property(r => r.Price)
            .HasColumnType("float")
            .HasPrecision(10, 2);

        modelBuilder.Entity<RoomAmenity>()
        .HasKey(ha => new { ha.RoomID, ha.AmenityId });

        modelBuilder.Entity<RoomAmenity>()
            .HasOne(ha => ha.Room)
            .WithMany(h => h.RoomAmenities)
            .HasForeignKey(ha => ha.RoomID);

        modelBuilder.Entity<RoomAmenity>()
            .HasOne(ha => ha.Amenity)
            .WithMany(a => a.RoomAmenity)
            .HasForeignKey(ha => ha.AmenityId);

        modelBuilder.Entity<Booking>().HasOne(b => b.User)
             .WithMany(u => u.Bookings)
             .HasForeignKey(b => b.UserId)
             .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Booking>().HasOne(b => b.Room)
            .WithMany(r => r.Bookings)
            .HasForeignKey(b => b.RoomId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Payment>()
               .Property(p => p.Amount)
               .HasColumnType("float")
               .HasPrecision(10, 2);

        modelBuilder.Entity<Booking>()
            .Property(r => r.TotalPrice)
            .HasColumnType("float")
            .HasPrecision(10, 2);

        modelBuilder.Entity<Hotel>()
        .HasOne(h => h.Cityy)
        .WithMany(c => c.Hotels)
        .HasForeignKey(h => h.CityId);



        modelBuilder.Entity<OwnerBookingDetails>(entity =>
        {
            entity.HasNoKey();
            entity.ToView("v_OwnerBookingDetails");
        });

    }
    public DbSet<User> Users{get; set;}
    public DbSet<City> Cities { get; set; }
    public DbSet<Hotel> Hotels { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<Amenity> Amenities { get; set; }
    public DbSet<HotelAmenity> HotelAmenities { get; set; }
    public DbSet<RoomAmenity> RoomAmenities { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<OwnerBookingDetails> OwnerBookingDetails { get; set; }

}


