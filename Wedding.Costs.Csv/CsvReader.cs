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
    }
}
