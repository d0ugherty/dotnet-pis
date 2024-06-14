using System.ComponentModel.DataAnnotations.Schema;

namespace DotNetPIS.Domain.Models.GTFS
{
    public class StopTime
    {
        public int Id { get; set; }

        public required string ArrivalTime { get; set; }
        public required string DepartureTime { get; set; }

        public int StopSequence { get; set; }
        public int? PickupType { get; set; }
        public int? DropoffType { get; set; }

        [ForeignKey("Stop")]
        public int StopId { get; set; }
        public required Stop Stop { get; set; }

        [ForeignKey("Trip")]
        public int TripId { get; set; }
        public required Trip Trip { get; set; }
    }
}