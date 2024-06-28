namespace DotNetPIS.Domain.Models.SEPTA;

public class Detour
{
    public string LocationStart { get; set; }
    public string LocationEnd { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public string Message { get; set; }
    public string Reason { get; set; }
}