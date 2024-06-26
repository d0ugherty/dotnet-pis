using DotNetPIS.Data;
using Microsoft.EntityFrameworkCore;

namespace DotNetPIS.DataImport;
internal class Program
{
    public static void Main(string[] args)
    {
        var connectionString = "Data Source=../DotNetPIS.Data/dotnetpis.db";

        var optionsBuilder = new DbContextOptionsBuilder<Context>();

        optionsBuilder.UseSqlite(connectionString);
        optionsBuilder.EnableSensitiveDataLogging();

        var options = optionsBuilder.Options;

        var importService = new DataImport(new Context(options));

        string sourceName = "";
        string transitType = "";
        if (args.Length > 0)
        {
            sourceName = args[0];
            transitType = args[1];
        }

        importService.ImportGtfsData(sourceName, transitType);

        Console.WriteLine("Data import completed successfully.");
    }
}
