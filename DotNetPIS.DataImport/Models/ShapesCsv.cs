namespace DataImport.Models;

public class ShapesCsv
{
    public int shape_id { get; set; }
    public float shape_pt_lat { get; set; }
    public float shape_pt_lon { get; set; }
    public int shape_pt_sequence { get; set; }
    public float? shape_dist_traveled { get; set; }
}