﻿// <auto-generated />
using DotNetPIS.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DotNetPIS.Data.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20240626194512_TripsnShapes6")]
    partial class TripsnShapes6
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.0-preview.5.24306.3");

            modelBuilder.Entity("DotNetPIS.Domain.Models.GTFS.Agency", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("AgencyNumber")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<string>("Language")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("SourceId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Timezone")
                        .HasColumnType("TEXT");

                    b.Property<string>("Url")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("SourceId");

                    b.ToTable("Agencies");
                });

            modelBuilder.Entity("DotNetPIS.Domain.Models.GTFS.Calendar", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("EndDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("Friday")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Monday")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Saturday")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ServiceId")
                        .HasColumnType("TEXT");

                    b.Property<int>("SourceId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("StartDate")
                        .HasColumnType("TEXT");

                    b.Property<int?>("Sunday")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Thursday")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Tuesday")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Wednesday")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("SourceId");

                    b.ToTable("Calendars");
                });

            modelBuilder.Entity("DotNetPIS.Domain.Models.GTFS.CalendarDate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Date")
                        .HasColumnType("TEXT");

                    b.Property<int?>("ExceptionType")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ServiceId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("CalendarDates");
                });

            modelBuilder.Entity("DotNetPIS.Domain.Models.GTFS.Fare", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("DestinationId")
                        .HasColumnType("TEXT");

                    b.Property<string>("FareNumber")
                        .HasColumnType("TEXT");

                    b.Property<string>("OriginId")
                        .HasColumnType("TEXT");

                    b.Property<int>("SourceId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("SourceId");

                    b.ToTable("Fares");
                });

            modelBuilder.Entity("DotNetPIS.Domain.Models.GTFS.FareAttributes", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("CurrencyType")
                        .HasColumnType("TEXT");

                    b.Property<int>("FareId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("PaymentMethod")
                        .HasColumnType("INTEGER");

                    b.Property<float>("Price")
                        .HasColumnType("REAL");

                    b.Property<string>("TransferDuration")
                        .HasColumnType("TEXT");

                    b.Property<int?>("Transfers")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("FareId");

                    b.ToTable("FareAttributesTbl");
                });

            modelBuilder.Entity("DotNetPIS.Domain.Models.GTFS.Route", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("AgencyId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Color")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("LongName")
                        .HasColumnType("TEXT");

                    b.Property<string>("RouteNumber")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ShortName")
                        .HasColumnType("TEXT");

                    b.Property<string>("TextColor")
                        .HasColumnType("TEXT");

                    b.Property<int?>("Type")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Url")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("AgencyId");

                    b.ToTable("Routes");
                });

            modelBuilder.Entity("DotNetPIS.Domain.Models.GTFS.Shape", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<float?>("DistanceTraveled")
                        .HasColumnType("REAL");

                    b.Property<int>("Sequence")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ShapeNumber")
                        .HasColumnType("INTEGER");

                    b.Property<float>("ShapePtLat")
                        .HasColumnType("REAL");

                    b.Property<float>("ShapePtLon")
                        .HasColumnType("REAL");

                    b.Property<int>("SourceId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("SourceId");

                    b.ToTable("Shapes");
                });

            modelBuilder.Entity("DotNetPIS.Domain.Models.GTFS.Source", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("FilePath")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Sources");
                });

            modelBuilder.Entity("DotNetPIS.Domain.Models.GTFS.Stop", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<float>("Latitude")
                        .HasColumnType("REAL");

                    b.Property<float>("Longitude")
                        .HasColumnType("REAL");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("SourceId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("StopNumber")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Url")
                        .HasColumnType("TEXT");

                    b.Property<string>("ZoneId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("SourceId");

                    b.ToTable("Stops");
                });

            modelBuilder.Entity("DotNetPIS.Domain.Models.GTFS.StopTime", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ArrivalTime")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("DepartureTime")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("DropoffType")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("PickupType")
                        .HasColumnType("INTEGER");

                    b.Property<int>("StopId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("StopSequence")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TripId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("StopId");

                    b.HasIndex("TripId");

                    b.ToTable("StopTimes");
                });

            modelBuilder.Entity("DotNetPIS.Domain.Models.GTFS.Trip", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("BlockId")
                        .HasColumnType("TEXT");

                    b.Property<int>("DirectionId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Headsign")
                        .HasColumnType("TEXT");

                    b.Property<string>("LongName")
                        .HasColumnType("TEXT");

                    b.Property<int>("RouteId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ServiceId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("ShapeId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ShortName")
                        .HasColumnType("TEXT");

                    b.Property<int>("SourceId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("TripNumber")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("RouteId");

                    b.HasIndex("SourceId");

                    b.ToTable("Trips");
                });

            modelBuilder.Entity("DotNetPIS.Domain.Models.GTFS.TripShape", b =>
                {
                    b.Property<int>("TripId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ShapeId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Id")
                        .HasColumnType("INTEGER");

                    b.HasKey("TripId", "ShapeId");

                    b.HasIndex("ShapeId");

                    b.ToTable("TripShapes");
                });

            modelBuilder.Entity("DotNetPIS.Domain.Models.GTFS.Agency", b =>
                {
                    b.HasOne("DotNetPIS.Domain.Models.GTFS.Source", "Source")
                        .WithMany("Agencies")
                        .HasForeignKey("SourceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Source");
                });

            modelBuilder.Entity("DotNetPIS.Domain.Models.GTFS.Calendar", b =>
                {
                    b.HasOne("DotNetPIS.Domain.Models.GTFS.Source", "Source")
                        .WithMany("Calendars")
                        .HasForeignKey("SourceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Source");
                });

            modelBuilder.Entity("DotNetPIS.Domain.Models.GTFS.Fare", b =>
                {
                    b.HasOne("DotNetPIS.Domain.Models.GTFS.Source", "Source")
                        .WithMany()
                        .HasForeignKey("SourceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Source");
                });

            modelBuilder.Entity("DotNetPIS.Domain.Models.GTFS.FareAttributes", b =>
                {
                    b.HasOne("DotNetPIS.Domain.Models.GTFS.Fare", "Fare")
                        .WithMany()
                        .HasForeignKey("FareId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Fare");
                });

            modelBuilder.Entity("DotNetPIS.Domain.Models.GTFS.Route", b =>
                {
                    b.HasOne("DotNetPIS.Domain.Models.GTFS.Agency", "Agency")
                        .WithMany("Routes")
                        .HasForeignKey("AgencyId");

                    b.Navigation("Agency");
                });

            modelBuilder.Entity("DotNetPIS.Domain.Models.GTFS.Shape", b =>
                {
                    b.HasOne("DotNetPIS.Domain.Models.GTFS.Source", "Source")
                        .WithMany()
                        .HasForeignKey("SourceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Source");
                });

            modelBuilder.Entity("DotNetPIS.Domain.Models.GTFS.Stop", b =>
                {
                    b.HasOne("DotNetPIS.Domain.Models.GTFS.Source", "Source")
                        .WithMany()
                        .HasForeignKey("SourceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Source");
                });

            modelBuilder.Entity("DotNetPIS.Domain.Models.GTFS.StopTime", b =>
                {
                    b.HasOne("DotNetPIS.Domain.Models.GTFS.Stop", "Stop")
                        .WithMany("StopTimes")
                        .HasForeignKey("StopId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DotNetPIS.Domain.Models.GTFS.Trip", "Trip")
                        .WithMany("StopTimes")
                        .HasForeignKey("TripId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Stop");

                    b.Navigation("Trip");
                });

            modelBuilder.Entity("DotNetPIS.Domain.Models.GTFS.Trip", b =>
                {
                    b.HasOne("DotNetPIS.Domain.Models.GTFS.Route", "Route")
                        .WithMany("Trips")
                        .HasForeignKey("RouteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DotNetPIS.Domain.Models.GTFS.Source", "Source")
                        .WithMany()
                        .HasForeignKey("SourceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Route");

                    b.Navigation("Source");
                });

            modelBuilder.Entity("DotNetPIS.Domain.Models.GTFS.TripShape", b =>
                {
                    b.HasOne("DotNetPIS.Domain.Models.GTFS.Shape", "Shape")
                        .WithMany()
                        .HasForeignKey("ShapeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DotNetPIS.Domain.Models.GTFS.Trip", "Trip")
                        .WithMany("TripShapes")
                        .HasForeignKey("TripId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Shape");

                    b.Navigation("Trip");
                });

            modelBuilder.Entity("DotNetPIS.Domain.Models.GTFS.Agency", b =>
                {
                    b.Navigation("Routes");
                });

            modelBuilder.Entity("DotNetPIS.Domain.Models.GTFS.Route", b =>
                {
                    b.Navigation("Trips");
                });

            modelBuilder.Entity("DotNetPIS.Domain.Models.GTFS.Source", b =>
                {
                    b.Navigation("Agencies");

                    b.Navigation("Calendars");
                });

            modelBuilder.Entity("DotNetPIS.Domain.Models.GTFS.Stop", b =>
                {
                    b.Navigation("StopTimes");
                });

            modelBuilder.Entity("DotNetPIS.Domain.Models.GTFS.Trip", b =>
                {
                    b.Navigation("StopTimes");

                    b.Navigation("TripShapes");
                });
#pragma warning restore 612, 618
        }
    }
}
