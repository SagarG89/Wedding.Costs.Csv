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
    class Program
    {
        static void Main(string[] args)
        {
            CsvReader.WeddingCsvReader();
            CsvWriter.WeddingCsvWriter();

            
        }
    }
}
