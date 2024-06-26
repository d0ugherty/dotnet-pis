using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using DataImport.Models;
using DotNetPIS.Data;
using DotNetPIS.Domain.Models.GTFS;
using Calendar = DotNetPIS.Domain.Models.GTFS.Calendar;

namespace DotNetPIS.DataImport;

public class DataImport
{
    private readonly Context _context;

    public DataImport(Context context)
    {
        _context = context;
    }

    private void ImportSources(string filePath)
    {
        var records = ReadCsv<SourceCsv>(filePath);

        try
        {
            int row = 1;
            foreach (var record in records)
            {
                Console.Write($"{new string(' ', 20)}Importing row {row}\r");

                _context.Sources.Add(new Source
                {
                    Name = record.name.Trim(),
                    FilePath = "../data/" + record.name
                });
                row++;
            }

            Console.WriteLine("Saving changes.");
            _context.SaveChanges();
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"Invalid operation occurred during importing of \n " +
                              $"{filePath} \n " +
                              $"{ex}");
            throw;
        }
    }

    private void ImportAgencies(string filePath, Source source)
    {
        var records = ReadCsv<AgencyCsv>(filePath);
        try
        {
            int row = 1;
            foreach (var record in records)
            {
                Console.Write($"{new string(' ', 20)}Importing row {row}\r");

                var agency = new Agency
                {
                    AgencyNumber = record.agency_id.Trim(),
                    Name = record.agency_name.Trim(),
                    Url = record.agency_url!.Trim(),
                    Timezone = record.agency_timezone!.Trim(),
                    Language = record.agency_lang!.Trim(),
                    Email = record.agency_email?.Trim() ?? string.Empty,
                    SourceId = source.Id,
                    Source = source
                };

                _context.Agencies.Add(agency);

                source.Agencies.Add(agency);

                row++;
            }
            Console.WriteLine("Saving changes.\n");

            _context.SaveChanges();
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"Invalid operation occurred during importing of \n " +
                              $"{filePath} \n " +
                              $"{ex}");
            throw;
        }
    }

    private void ImportRoutes(string filePath, Source source)
    {
        var records = ReadCsv<RoutesCsv>(filePath);
        try
        {
            int row = 1;

            foreach (var record in records)
            {
                Console.Write($"{new string(' ', 20)}Importing row {row}\r");

                var agency = _context.Agencies
                    .FirstOrDefault(a => a.AgencyNumber.Equals(record.agency_id) && a.SourceId == source.Id);

                var route = new Route
                {
                    RouteNumber = record.route_id,
                    ShortName = record.route_short_name,
                    LongName = record.route_long_name,
                    Description = record.route_desc,
                    Type = record.route_type,
                    Color = record.route_color,
                    TextColor = record.route_text_color,
                    Url = record.route_url,
                    AgencyId = agency?.Id,
                    Agency = agency
                };

                _context.Routes.Add(route);

                agency?.Routes.Add(route);

                row++;
            }
            Console.WriteLine("Saving changes.");

            _context.SaveChanges();
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"Invalid operation occurred during importing of \n " +
                              $"{filePath} \n " +
                              $"{ex}");
            throw;
        }
    }

    private void ImportCalendars(string filePath, Source source)
    {
        var records = ReadCsv<CalendarCsv>(filePath);

        try
        {
            int row = 1;

            foreach (var record in records)
            {
                Console.Write($"{new string(' ', 20)}Importing row {row}\r");

                var calendar = new Calendar
                {
                    ServiceId = record.service_id,
                    Monday = record.monday,
                    Tuesday = record.tuesday,
                    Wednesday = record.wednesday,
                    Thursday = record.thursday,
                    Friday = record.friday,
                    Saturday = record.saturday,
                    Sunday = record.sunday,
                    StartDate = record.start_date,
                    EndDate = record.end_date,
                    SourceId = source.Id,
                    Source = source
                };

                _context.Calendars.Add(calendar);

                source.Calendars.Add(calendar);

                row++;
            }
            Console.WriteLine("Saving changes.");

            _context.SaveChanges();
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"Invalid operation occurred during importing of \n " +
                              $"{filePath} \n " +
                              $"{ex}");
            throw;
        }
    }

    private void ImportCalendarDates(string filePath)
    {
        var records = ReadCsv<CalendarDatesCsv>(filePath);

        try
        {
            int row = 1;

            foreach (var record in records)
            {
                Console.Write($"{new string(' ', 20)}Importing row {row}\r");

                _context.CalendarDates.Add(new CalendarDate
                {
                    ServiceId = record.service_id,
                    Date = record.date,
                    ExceptionType = record.exception_type
                });

                row++;
            }
            Console.WriteLine("Saving changes.");

            _context.SaveChanges();
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"Invalid operation occurred during importing of \n " +
                              $"{filePath} \n " +
                              $"{ex}");
            throw;
        }
    }

    private void ImportFares(string filePath, Source source)
    {
        var records = ReadCsv<FaresCsv>(filePath);

        try
        {
            int row = 1;

            foreach (var record in records)
            {
                Console.Write($"{new string(' ', 20)}Importing row {row}\r");

                _context.Fares.Add(new Fare
                {
                    FareNumber = record.fare_id,
                    OriginId = record.origin_id,
                    DestinationId = record.destination_id,
                    SourceId = source.Id,
                    Source = source
                });

                row++;
            }
            Console.WriteLine("Saving changes.");

            _context.SaveChanges();
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"Invalid operation occurred during importing of \n " +
                              $"{filePath} \n " +
                              $"{ex}");
            throw;
        }
    }

    private void ImportFareAttributes(string filePath, Source source)
    {
        var records = ReadCsv<FareAttributesCsv>(filePath);

        try
        {
            int row = 1;

            foreach (var record in records)
            {
                Console.Write($"{new string(' ', 20)}Importing row {row}\r");

                var fare = _context.Fares
                    .FirstOrDefault(f => f.FareNumber!.Equals(record.fare_id) && f.SourceId == source.Id);

                var fareAttributes = new FareAttributes
                {
                    Price = record.price,
                    CurrencyType = record.currency_type,
                    PaymentMethod = record.payment_method,
                    Transfers = record.transfers,
                    TransferDuration = record.transfer_duration,
                    FareId = fare!.Id,
                    Fare = fare
                };

                _context.FareAttributesTbl.Add(fareAttributes);

                // fare.FareAttributesId = fareAttributes.Id;
                //   fare.FareAttributes = fareAttributes;
                _context.Update(fare);

                row++;
            }
            Console.WriteLine("Saving changes.");

            _context.SaveChanges();
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"Invalid operation occurred during importing of \n " +
                              $"{filePath} \n " +
                              $"{ex}");
            throw;
        }
    }

    private void ImportShapes(string filePath, Source source)
    {
        var records = ReadCsv<ShapesCsv>(filePath);

        try
        {
            int row = 1;
            foreach (var record in records)
            {
                Console.Write($"{new string(' ', 20)}Importing row {row}\r");

                var shape = new Shape
                {
                    ShapeNumber = record.shape_id,
                    ShapePtLat = record.shape_pt_lat,
                    ShapePtLon = record.shape_pt_lon,
                    Sequence = record.shape_pt_sequence,
                    DistanceTraveled = record.shape_dist_traveled,
                    SourceId = source.Id,
                    Source = source
                };

                _context.Shapes.Add(shape);
                row++;
            }
            Console.WriteLine("Saving changes.");

            _context.SaveChanges();
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"Invalid operation occurred during importing of \n " +
                              $"{filePath} \n " +
                              $"{ex}");
            throw;
        }
    }

    private void ImportStops(string filePath, Source source)
    {
        var records = ReadCsv<StopsCsv>(filePath);

        try
        {
            int row = 1;

            foreach (var record in records)
            {
                Console.Write($"{new string(' ', 20)}Importing row {row}\r");

                _context.Stops.Add(new Stop
                {
                    Name = record.stop_name,
                    StopNumber = record.stop_id,
                    Description = record.stop_desc,
                    Latitude = record.stop_lat,
                    Longitude = record.stop_lon,
                    ZoneId = record.zone_id,
                    Url = record.stop_url,
                    SourceId = source.Id,
                    Source = source
                });

                row++;
            }
            Console.WriteLine("Saving changes.");

            _context.SaveChanges();
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"Invalid operation occurred during importing of \n " +
                              $"{filePath} \n " +
                              $"{ex}");
            throw;
        }
    }

    private void ImportStopTimes(string filePath, Source source)
    {
        var records = ReadCsv<StopTimesCsv>(filePath);

        try
        {
            int row = 1;

            var stops = _context.Stops
                .Where(s => s.SourceId == source.Id)
                .ToList();

            var trips = _context.Trips
                .Where(t => t.Route.Agency!.SourceId == source.Id)
                .ToList();

            foreach (var record in records)
            {
                Console.Write($"{new string(' ', 20)}Importing row {row}\r");

                var stop = stops.FirstOrDefault(s => s.StopNumber.Equals(record.stop_id));
                var trip = trips.FirstOrDefault(t => t.TripNumber.Equals(record.trip_id));

                var stopTime = new StopTime
                {
                    ArrivalTime = record.arrival_time,
                    DepartureTime = record.departure_time,
                    StopSequence = record.stop_sequence,
                    PickupType = record.pickup_type,
                    DropoffType = record.drop_off_type,
                    StopId = stop!.Id,
                    Stop = stop,
                    TripId = trip!.Id,
                    Trip = trip
                };

                _context.StopTimes.Add(stopTime);

                stop.StopTimes.Add(stopTime);
                trip.StopTimes!.Add(stopTime);

                row++;
            }
            Console.WriteLine("Saving changes.");

            _context.SaveChanges();
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"Invalid operation occurred during importing of \n " +
                              $"{filePath} \n " +
                              $"{ex}");
            throw;
        }
    }

    private void ImportTrips(string filePath, Source source)
    {
        var records = ReadCsv<TripsCsv>(filePath);

        try
        {
            int row = 1;

            var routes = _context.Routes
                .Where(r => r.Agency!.SourceId == source.Id)
                .ToList();

            foreach (var record in records)
            {
                Console.Write($"{new string(' ', 20)}Importing row {row}\r");

                var route = routes.Find(r => r.RouteNumber.Equals(record.route_id));

                var trip = new Trip
                {
                    ServiceId = record.service_id,
                    TripNumber = record.trip_id,
                    Headsign = record.trip_headsign,
                    BlockId = record.block_id,
                    ShortName = record.trip_short_name,
                    DirectionId = record.direction_id,
                    RouteId = route!.Id,
                    Route = route,
                    ShapeId = record.shape_id,
                    SourceId = source.Id,
                    Source = source
                };

                _context.Trips.Add(trip);
                route.Trips.Add(trip);
                
                row++;
            }
            Console.WriteLine("Saving changes.");

            _context.SaveChanges();
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"Invalid operation occurred during importing of \n " +
                              $"{filePath} \n " +
                              $"{ex}");
            throw;
        }
    }

    private void AddTripShapes(Source source)
    {
        Console.WriteLine($"Adding Trip-Shapes");
        List<Shape> shapes = _context.Shapes.Where(shape => shape.SourceId == source.Id).ToList();

        int row = 1;
        foreach (var shape in shapes)
        {
            Console.Write($"{new string(' ', 20)}Importing row {row}\r");
            
            List<Trip> trips = _context.Trips.Where(t => t.ShapeId == shape.ShapeNumber).ToList();

            foreach (var trip in trips)
            {
                var tripShape = new TripShape
                {
                    Trip = trip,
                    TripId = trip.Id,
                    Shape = shape,
                    ShapeId = shape.Id
                };

                _context.TripShapes.Add(tripShape);
                trip.TripShapes.Add(tripShape);
            }

            row++;
        }
        
        Console.WriteLine($"Saving changes...");
        _context.SaveChanges();
    }

    private IEnumerable<T> ReadCsv<T>(string filePath)
    {
        using (var reader = new StreamReader(filePath))
        {
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                MissingFieldFound = null
            }))
            {
                csv.Read();
                csv.ReadHeader();

                return csv.GetRecords<T>().ToList();
            }
        }
    }

    private void ImportTry(string filePath, Action<string> import)
    {
        if (File.Exists(filePath))
        {
            try
            {
                Console.WriteLine($"Importing {filePath}");
                import(filePath);
                Console.WriteLine($"{filePath} \n successfully imported.");
            }
            catch (ReaderException ex)
            {
                Console.WriteLine($"{ex} \n Reader exception occurred, moving on to next import");
            }
        }
        else
        {
            Console.WriteLine($"{filePath} does not exist. \n Moving on to next import.");
        }
    }

    private void ImportDataFromFiles(Source source, string transitType="")
    {
        if (transitType.Equals(""))
        {
            List<string> transitTypes = ["rail", "bus"];

            foreach (var type in transitTypes)
            {
                ImportTry($"{source.FilePath}/{type}/agency.csv", filePath => ImportAgencies(filePath, source));

                ImportTry($"{source.FilePath}/{type}/calendar.csv", filePath => ImportCalendars(filePath, source));

                ImportTry($"{source.FilePath}/{type}/calendar_dates.csv", ImportCalendarDates);

                ImportTry($"{source.FilePath}/{type}/fare_rules.csv", filePath => ImportFares(filePath, source));

                ImportTry($"{source.FilePath}/{type}/fare_attributes.csv",
                    filePath => ImportFareAttributes(filePath, source));

                ImportTry($"{source.FilePath}/{type}/routes.csv", filePath => ImportRoutes(filePath, source));

                ImportTry($"{source.FilePath}/{type}/trips.csv", filePath => ImportTrips(filePath, source));

                ImportTry($"{source.FilePath}/{type}/shapes.csv", filePath => ImportShapes(filePath, source));

                ImportTry($"{source.FilePath}/{type}/stops.csv", filePath => ImportStops(filePath, source));

                ImportTry($"{source.FilePath}/{type}/stop_times.csv", filePath => ImportStopTimes(filePath, source));
            }
        }
        else
        {
            ImportTry($"{source.FilePath}/{transitType}/agency.csv", filePath => ImportAgencies(filePath, source));

            ImportTry($"{source.FilePath}/{transitType}/calendar.csv", filePath => ImportCalendars(filePath, source));

            ImportTry($"{source.FilePath}/{transitType}/calendar_dates.csv", ImportCalendarDates);

            ImportTry($"{source.FilePath}/{transitType}/fare_rules.csv", filePath => ImportFares(filePath, source));

            ImportTry($"{source.FilePath}/{transitType}/fare_attributes.csv",
                filePath => ImportFareAttributes(filePath, source));

            ImportTry($"{source.FilePath}/{transitType}/routes.csv", filePath => ImportRoutes(filePath, source));

            ImportTry($"{source.FilePath}/{transitType}/trips.csv", filePath => ImportTrips(filePath, source));

            ImportTry($"{source.FilePath}/{transitType}/shapes.csv", filePath => ImportShapes(filePath, source));

            ImportTry($"{source.FilePath}/{transitType}/stops.csv", filePath => ImportStops(filePath, source));

            ImportTry($"{source.FilePath}/{transitType}/stop_times.csv", filePath => ImportStopTimes(filePath, source));
        }
    }

    private void CreateNewSource(string sourceName)
    {
        _context.Sources.Add(new Source
        {
            Name = sourceName.Trim(),
            FilePath = "../data/" + sourceName
        });

        _context.SaveChanges();
    }

    public void ImportGtfsData(string dataSourceName = "", string type = "")
    {
        if (dataSourceName.Equals(""))
        {
            ImportTry($"../data/sources.csv", ImportSources);

            var sources = _context.Sources.ToList();

            foreach (var source in sources)
            {
                ImportDataFromFiles(source);
                AddTripShapes(source);
            }
        }
        else
        {
            CreateNewSource(dataSourceName);

            Source source = _context.Sources.FirstOrDefault(s => s.Name.Equals(dataSourceName))!;

            ImportDataFromFiles(source, type);
            AddTripShapes(source);
        }

        Console.WriteLine("Done.");
    }
}