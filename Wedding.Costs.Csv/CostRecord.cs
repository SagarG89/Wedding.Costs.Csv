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
	public enum CostType
	{
		Unspecified,
		Alcohol,
		Other
	}
	public class CostRecord
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public decimal Cost { get; set; }
		public CostType Type { get; set; }

		public static CostRecord ParseFromUserInputLine(string userInput)
		{
			throw new NotImplementedException($"Make this");
		}

	}
}

