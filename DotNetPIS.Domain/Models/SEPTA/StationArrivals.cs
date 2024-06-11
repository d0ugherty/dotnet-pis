using System.Collections;

namespace DotNetPIS.Domain.Models.SEPTA;

public class StationArrivals : IEnumerable
{
    public string StationName { get; set; }
    public DateTime Time { get; set; }
    public List<Arrival> Northbound { get; set; }
    public List<Arrival> Southbound { get; set; }
    public IEnumerator GetEnumerator()
    {
        throw new NotImplementedException();
    }
}