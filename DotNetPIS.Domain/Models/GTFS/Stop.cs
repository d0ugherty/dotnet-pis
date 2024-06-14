using System.ComponentModel.DataAnnotations.Schema;

namespace DotNetPIS.Domain.Models.GTFS;

public class Stop
{
	public int Id { get; set; }

	public required string StopNumber { get; set; }

	public string Name { get; set; } = null!;

	public string? Description { get; set; }

	public float Latitude { get; set; }

	public float Longitude { get; set; }

	public string? ZoneId { get; set; }

	public string? Url { get; set; }

	public List<StopTime> StopTimes { get; set; } = new List<StopTime>();

	[ForeignKey("Source")] 
	public int SourceId { get; set; }

	public required Source Source { get; set; }
}