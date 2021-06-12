using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.IO;
using CsvHelper;
using CsvHelper.Configuration.Attributes;

namespace Wedding.Costs.Csv
{
    public class CsvWriter
    {
        
        public static void WeddingCsvWriter()
        {
           
            var records = new List<CostRecord>()
            {
                
            };

            var config = new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false,
            };

            using (var stream = File.Open(@"C:\Users\sagar\source\repos\Wedding.Costs.Csv\Wedding.Costs.Csv\bin\Debug\net5.0\BookCost.csv", FileMode.Append))
            using (var writer = new StreamWriter(stream))
            using (var csv = new CsvHelper.CsvWriter(writer, config))
            {
                
                csv.WriteRecords(records);
            }
        }

        /// <summary>
        /// This will add costs to the end of the file
        /// </summary>
        public static void AppendNewCosts(string file, IEnumerable<CostRecord> newCosts)
		{
            var config = new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = !File.Exists(file), // Need header once in file only
            };

			using var stream = File.Open(file, FileMode.Append);
			using var writer = new StreamWriter(stream);
			using var csv = new CsvHelper.CsvWriter(writer, config);
			csv.WriteRecords(newCosts);
		}

        /// <summary>
        /// This will delete the costs file and replace with input
        /// This is the only way to edit/ delete in CSV file
        /// Only way that isn't complicated
        /// </summary>
        public static void SetNewCosts(string file, ICollection<CostRecord> costs)
		{
            var config = new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true
            };

            using var stream = File.Open(file, FileMode.Create);
            using var writer = new StreamWriter(stream);
            using var csv = new CsvHelper.CsvWriter(writer, config);
            csv.WriteRecords(costs);
        }
    }
}
