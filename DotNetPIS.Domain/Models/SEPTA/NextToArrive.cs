namespace DotNetPIS.Domain.Models.SEPTA;

public class NextToArrive
{
    public string OriginTrain { get; set; } = null!;
    public string OriginLine { get; set; } = null!;
    public string OriginDepartureTime { get; set; } = null!;
    public string OriginArrivalTime { get; set; } = null!;
    public string OriginDelay { get; set; } = null!;
    public string? TerminatingTrain { get; set; }
    public string? TerminatingLine { get; set; }
    public string? TerminatingDepartureTime { get; set; }
    public string? TerminatingArrivalTime { get; set; }
    public string? Connection { get; set; }
    public string? TerminatingDelay { get; set; }
    public bool IsDirect { get; set; }
}