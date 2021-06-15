using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wedding.Costs.Csv
{
	[Verb("view")]
	class ViewCostsCommand
	{
		[Option('c',Separator = ',')]
		public IEnumerable<string> CostNames { get; set; }
		
		[Option('t')]
		public CostType? Type { get; set; }
		// TODO: Add Ids option
		public int Id { get; set; }
		// TODO: Add CostsAbove option
		public decimal CostsAbove { get; set; }
	}

	[Verb("add")]
	class AddCostsCommand
	{
		[Value(0)]
		public CostType Type { get; set; }
		[Value(1)]
		public string Name { get; set; }
		[Value(2)]
		public decimal Cost { get; set; }
	}

	[Verb("edit")]
	class EditCostsCommand
	{
		[Value(0)]
		public int Id { get; set; }
		[Option('t')]
		public CostType? NewType { get; set; }
		[Option('n')]
		public string NewName { get; set; }
		[Option('c')]
		public decimal? NewCost { get; set; }
	}

	[Verb("rand")]
	class GenerateRandomCosts
	{
		[Option('n')]
		public int? Count { get; set; }
	}

	[Verb("del")]
	class DeleteCostsCommand
	{
		[Value(0)]
		public int Id { get; set; }
	}

	[Verb("interactive")]
	class InteractiveCommand
	{

	}
}
