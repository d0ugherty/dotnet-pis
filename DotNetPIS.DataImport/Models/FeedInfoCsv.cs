namespace DataImport.Models;

public class FeedInfoCsv
{
    public string feed_publisher_name { get; set; } = null!;
    public string feed_publisher_url { get; set; } = null!;
    public string feed_lang { get; set; } = null!;
    public string feed_start_date { get; set; } = null!; 
    public string feed_end_date { get; set; } = null!;
    public string feed_version { get; set; } = null!;
}