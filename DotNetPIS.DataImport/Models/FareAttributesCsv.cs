namespace DotNetPIS.DataImport.Models;

public class FareAttributesCsv
{
    public string fare_id { get; set; } = null!;
    public float price { get; set; }
    public string? currency_type { get; set; }
    public int? payment_method { get; set; }
    public int? transfers { get; set; }
    public string? transfer_duration { get; set; }
}