namespace DotNetPIS.DataImport.Models;

public class ShapesCsv
{
    public string shape_id { get; set; } = null!;
    public float shape_pt_lat { get; set; }
    public float shape_pt_lon { get; set; }
    public int shape_pt_sequence { get; set; }
    public float? shape_dist_traveled { get; set; }
}