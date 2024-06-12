namespace DataImport.Models;

public class AgencyCsv
{
    public string agency_id { get; set; } = null!;
    public string agency_name { get; set; } = null!;
    public string? agency_url { get; set; }
    public string? agency_timezone { get; set; }
    public string? agency_lang { get; set; }
    public string? agency_email { get; set; }
}