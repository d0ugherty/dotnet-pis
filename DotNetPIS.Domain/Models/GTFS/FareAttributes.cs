using System.ComponentModel.DataAnnotations.Schema;

namespace DotNetPIS.Domain.Models.GTFS;

public class FareAttributes
{
	public int Id { get; set; }
    
	public float Price { get; set; }
	public string? CurrencyType { get; set; }
	public int? PaymentMethod { get; set; }
    
	public int? Transfers { get; set; }
	public string? TransferDuration { get; set; }
    
	[ForeignKey("Fare")] 
	public int FareId { get; set; }
	public required Fare Fare { get; set; }
}