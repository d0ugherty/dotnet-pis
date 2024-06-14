namespace DotNetPIS.Domain.Models.GTFS;

public class Source
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string FilePath { get; set; }

    public List<Agency> Agencies { get; set; }
    public List<Calendar> Calendars { get; set; }

    public Source()
    {
        Agencies = new List<Agency>();
        Calendars = new List<Calendar>();
    }

}