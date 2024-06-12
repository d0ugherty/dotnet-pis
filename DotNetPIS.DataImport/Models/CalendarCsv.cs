namespace DataImport.Models;

public class CalendarCsv
{
    public string service_id { get; set; } = null!;
	
    public int monday { get; set; }
    public int tuesday { get; set; }
    public int wednesday { get; set; }
    public int thursday { get; set; }
    public int friday { get; set; }
    public int? saturday { get; set; }
    public int? sunday { get; set; }
	
    public string? start_date { get; set; }
    public string? end_date { get; set; }
}