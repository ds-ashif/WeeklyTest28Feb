using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace FlightSearchEngine.Models;

public partial class FlightSearchContext : DbContext
{
    public FlightSearchContext()
    {
    }

    public FlightSearchContext(DbContextOptions<FlightSearchContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Flight> Flights { get; set; }

    public virtual DbSet<Hotel> Hotels { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=Mera_hai\\SQLEXPRESS;Database=FlightSearchDB;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Flight>(entity =>
        {
            entity.HasKey(e => e.FlightId).HasName("PK__Flights__8A9E14EE428F3949");

            entity.Property(e => e.Destination).HasMaxLength(100);
            entity.Property(e => e.FlightName).HasMaxLength(100);
            entity.Property(e => e.FlightType).HasMaxLength(50);
            entity.Property(e => e.PricePerSeat).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Source).HasMaxLength(100);
        });

        modelBuilder.Entity<Hotel>(entity =>
        {
            entity.HasKey(e => e.HotelId).HasName("PK__Hotels__46023BDF3ADC5A72");

            entity.Property(e => e.HotelName).HasMaxLength(100);
            entity.Property(e => e.HotelType).HasMaxLength(50);
            entity.Property(e => e.Location).HasMaxLength(100);
            entity.Property(e => e.PricePerDay).HasColumnType("decimal(18, 2)");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
