using DotNetPIS.Data;
using Microsoft.EntityFrameworkCore;

namespace DotNetPIS.DataImport;
internal class Program
    {
        public static void Main(string[] args)
        {
            var connectionString = "Data Source=../DotNetPIS.Data/gtfs.db";
		    
            var optionsBuilder = new DbContextOptionsBuilder<Context>();
		    
            optionsBuilder.UseSqlite(connectionString);
            optionsBuilder.EnableSensitiveDataLogging();
		    
            var options = optionsBuilder.Options;

            var importService = new DataImport(new Context(options));

            string sourceName = "";
            
            if (args.Length > 0)
            {
                sourceName = args[0];
            }

            importService.ImportGtfsData(sourceName);
		    
            Console.WriteLine("Data import completed successfully.");
        }
    }
