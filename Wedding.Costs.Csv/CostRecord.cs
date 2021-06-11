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
    public class CostRecord
    {
            [Index(0)]
            public string Name { get; set; }
            [Index(1)]
            public int Cost { get; set; }
        
        private static string ParseFromUserInputLine()
        {
            var filePath = new StreamReader(@"C:\Users\sagar\source\repos\Wedding.Costs.Csv\Wedding.Costs.Csv\bin\Debug\net5.0\BookCost.csv");
            string userInput = Console.ReadLine();

            

           
        }
      
    }
    }

