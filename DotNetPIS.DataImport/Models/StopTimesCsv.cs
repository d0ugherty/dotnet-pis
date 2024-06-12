namespace DataImport.Models;

public class StopTimesCsv
{
    public string trip_id { get; set; } = null!;

    public string arrival_time { get; set; } = null!;
    public string departure_time { get; set; } = null!;

    public string stop_id { get; set; } = null!;
    public int stop_sequence { get; set; }
    public int? pickup_type { get; set; }
    public int? drop_off_type { get; set; }
}