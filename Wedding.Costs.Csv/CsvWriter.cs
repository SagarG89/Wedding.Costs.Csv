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
    }
}
