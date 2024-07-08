namespace DataImport.Models;

public class StopsCsv
{
    public string stop_id { get; set; } = null!;
    public string stop_name { get; set; } = null!;
    public string? stop_desc { get; set; }
    public float? stop_lat { get; set; }
    public float? stop_lon { get; set; }
    public string? zone_id { get; set; }
    public string? stop_url { get; set; }
}