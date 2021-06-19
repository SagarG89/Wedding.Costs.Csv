using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.IO;
using CsvHelper;
using CsvHelper.Configuration.Attributes;
using CommandLine;

namespace Wedding.Costs.Csv
{
	public enum CostType
	{
		Location,
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

	}
}

