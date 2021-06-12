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
    public class CsvReader
    {

        public static void WeddingCsvReader()
        {
            var config = new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture)
            {
                NewLine = Environment.NewLine,
            };
            using (var stream = new MemoryStream())
            {
                using (var reader = new StreamReader(@"C:\Users\sagar\source\repos\Wedding.Costs.Csv\Wedding.Costs.Csv\bin\Debug\net5.0\BookCost.csv"))
                using (var csv = new CsvHelper.CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    var records = csv.GetRecords<CostRecord>();
                }
            }
        }


        public static List<CostRecord> ReadFile(string file)
		{
            // No exception handling, any read error needs user to fix the file
            using var sr = new StreamReader(file);
            using var csv = new CsvHelper.CsvReader(sr, CultureInfo.InvariantCulture);
            // IDeally from external data source, you would return IEnumerable, but for files, you will get ObjectDisposedException because of the above using statements
            // It is advanced topic for later...
            return csv.GetRecords<CostRecord>().ToList();
		}
    }
}
