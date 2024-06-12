namespace DotNetPIS.Domain.Models.GTFS;

public class CalendarDate
{
	public int Id { get; set; }
	public string? ServiceId { get; set; }
	public string? Date { get; set; }
	public int? ExceptionType { get; set; }
}