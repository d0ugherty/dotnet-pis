using DotNetPIS.Domain.Models.GTFS;
using Microsoft.EntityFrameworkCore;

namespace DotNetPIS.Data;

public class Context : DbContext
{
    public DbSet<Source> Sources { get; set; } = null!;

    public DbSet<Agency> Agencies { get; set; } = null!;

    public DbSet<Route> Routes { get; set; } = null!;

    public DbSet<Stop> Stops { get; set; } = null!;

    public DbSet<StopTime> StopTimes { get; set; } = null!;

    public DbSet<Calendar> Calendars { get; set; } = null!;

    public DbSet<CalendarDate> CalendarDates { get; set; } = null!;

    public DbSet<Fare> Fares { get; set; } = null!;

    public DbSet<FareAttributes> FareAttributesTbl { get; set; } = null!;

    public DbSet<Shape> Shapes { get; set; } = null!;

    public DbSet<Trip> Trips { get; set; } = null!;

    public DbSet<TripShape> TripShapes { get; set; } = null!;

    public Context(DbContextOptions options) : base(options)
    {
        //Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Route>()
            .HasKey(r => r.Id);

        modelBuilder.Entity<Trip>()
            .HasKey(t => t.Id);

        modelBuilder.Entity<Shape>()
            .HasKey(s => s.Id);

        modelBuilder.Entity<Stop>()
            .HasKey(s => s.Id);

        modelBuilder.Entity<Fare>()
            .HasKey(f => f.Id);
        
        modelBuilder.Entity<Agency>()
            .HasKey(a => a.Id);
        
        modelBuilder.Entity<Route>()
            .HasMany(route => route.Trips)
            .WithOne(trip => trip.Route)
            .HasForeignKey(trip => trip.RouteId);

        modelBuilder.Entity<Trip>()
            .HasMany(trip => trip.StopTimes)
            .WithOne(st => st.Trip)
            .HasForeignKey(st => st.TripId);

        modelBuilder.Entity<Shape>()
            .HasOne(shape => shape.Source)
            .WithMany()
            .HasForeignKey(shape => shape.SourceId);

        modelBuilder.Entity<TripShape>()
            .HasKey(ts => new { ts.TripId, ts.ShapeId });
        
        modelBuilder.Entity<TripShape>()
            .HasOne(ts => ts.Trip)
            .WithMany(t => t.TripShapes)
            .HasForeignKey(ts => ts.TripId);

        modelBuilder.Entity<TripShape>()
            .HasOne(ts => ts.Shape)
            .WithMany()
            .HasForeignKey(ts => ts.ShapeId);

        modelBuilder.Entity<Stop>()
            .HasMany(stop => stop.StopTimes)
            .WithOne(st => st.Stop)
            .HasForeignKey(st => st.StopId);

        modelBuilder.Entity<Source>()
            .HasMany(source => source.Agencies)
            .WithOne(agency => agency.Source)
            .HasForeignKey(agency => agency.SourceId);
    }
}