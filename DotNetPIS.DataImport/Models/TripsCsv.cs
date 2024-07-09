namespace DotNetPIS.DataImport.Models;

public class TripsCsv
{
    public string route_id { get; set; } = null!;
    public string service_id { get; set; } = null!;
    public string trip_id { get; set; } = null!;
    public string trip_headsign { get; set; } = null!;
    public string? block_id { get; set; } = null!;
    public string? trip_short_name { get; set; }
    public string? shape_id { get; set; }
    public int direction_id { get; set; }
}