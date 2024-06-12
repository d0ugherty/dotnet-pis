namespace DataImport.Models;

public class CalendarDatesCsv
{
    public string service_id { get; set; } = null!;
    public string date { get; set; } = null!;
    public int exception_type { get; set; }
}