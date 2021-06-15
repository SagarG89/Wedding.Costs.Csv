using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.IO;
using CsvHelper;
using CsvHelper.Configuration.Attributes;
using System.Reflection;
using CommandLine;

namespace Wedding.Costs.Csv
{
    class Program
    {
		public const string CsvFile = @"C:\Users\sagar\source\repos\Wedding.Costs.Csv\Wedding.Costs.Csv\bin\Debug\net5.0\BookCost.csv";
        static void Main(string[] args)
        {
			// Use commandline parser package to read user commands
			// Any unrecognised command will return help text that the package generates automatically
			// For local development, right click thie csproj file > Properties > Debug > Application arguments, put 'interactive'
			// So while debugging, will default the interactive command below
			// You can also use the IsDefault from commandline parser Verb attribute
            var commands = Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => t.GetCustomAttribute<VerbAttribute>() != null).ToArray();
            Parser.Default.ParseArguments(args, commands)
                .WithParsed(Run);
            
        }

		private static void Run(object obj)
		{
			try
			{
				switch (obj)
				{
					case GenerateRandomCosts gc:
						GenerateRandomCosts(gc);
						break;
					case ViewCostsCommand vc:
						ViewCosts(vc);
						break;
					case EditCostsCommand ec:
						EditCosts(ec);
						break;
					case InteractiveCommand i:
						Console.WriteLine("Beginning interactive session:");
						while (true)
						{
							// TODO: Use Console.Write and a loop of Console.ReadKey (break on Enter), so the prompt and input is same line
							Console.Write("Enter command:");
							var userInput = Console.ReadLine();
						
							if (userInput.Equals("view", StringComparison.InvariantCultureIgnoreCase))
                            {
								var displayView = new ViewCostsCommand();
								Console.WriteLine(displayView);
                            }
							if (userInput.Equals("add", StringComparison.InvariantCultureIgnoreCase))
                            {
								
								var displayAdd = new AddCostsCommand();
								Console.WriteLine(displayAdd);
                            }
							if (userInput.Equals("quit", StringComparison.InvariantCultureIgnoreCase)
								|| userInput.Equals("exit", StringComparison.InvariantCultureIgnoreCase))
								break;
							Main(userInput.Split(' '));
						}
						Console.WriteLine("Ending interactive session:");
						break;
					case AddCostsCommand ac:
						AddCosts(ac);
							break;
					default:
						throw new InvalidOperationException($"Unexpected command {obj?.GetType().FullName}");
				}
			}
			catch (Exception e)
			{
				Console.Error.WriteLine($"Error in user command:");
				Console.Error.WriteLine(e.ToString());
			}
		}
		//Have I gone about this the right way?
		private static void AddCosts(AddCostsCommand ac)
        {
            var records = CsvReader.ReadFile(CsvFile);
			var toAddName = records.SingleOrDefault(x => x.Name == ac.Name);
			var toAddCost = records.SingleOrDefault(x => x.Cost == ac.Cost);
			var toAddCostType = records.SingleOrDefault(x => x.Type == ac.Type);
			if (toAddCost == null || toAddName == null || toAddCostType == null)
				throw new InvalidOperationException($"Unable to add cost: {toAddName}");
			if (ac.Name.Length > 0)
				toAddName.Name = ac.Name;
			if (ac.Cost > 0)
				toAddCost.Cost = ac.Cost;
			if (ac.Type > 0)
				toAddCostType.Type = ac.Type;

			CsvWriter.AppendNewCosts(CsvFile, records);
        }
		private static void EditCosts(EditCostsCommand ec)
		{
			// This will raise error if no file/ corrupt file
			var records = CsvReader.ReadFile(CsvFile);
			var toEdit = records.SingleOrDefault(x => x.Id == ec.Id);
			if (toEdit == null)
				throw new InvalidOperationException($"No cost record found with ID: {ec.Id}");
			if (ec.NewCost.HasValue)
				toEdit.Cost = ec.NewCost.Value;
			// TODO: Other properties
			if (ec.NewName.Length > 0)
				toEdit.Name = ec.NewName;
			if (ec.NewType.HasValue)
				toEdit.Type = ec.NewType.Value;

			// The edited record is in the records list, so saving the whole list will make the csv file edited
			CsvWriter.SetNewCosts(CsvFile, records);
		}

		private static void GenerateRandomCosts(GenerateRandomCosts gc)
		{
			var rand = new Random();
			var costType = (CostType[])Enum.GetValues(typeof(CostType));
			var costFrom = 100.0M;
			var costTo = 1000.0M;
			var count = gc.Count ?? 100;
			var records = new List<CostRecord>(count);
			var id = 0;
			if (File.Exists(CsvFile))
				id = CsvReader.ReadFile(CsvFile).Max(x => x.Id) + 1;
			
			for (int i = 0; i < count; i++)
			{
				var newRecord = new CostRecord
				{
					Id = id++,
					Type = costType[rand.Next(costType.Length)],
					Name = Guid.NewGuid().ToString(),
					Cost = Math.Round(costFrom + (costTo - costFrom) * (decimal)rand.NextDouble(),2)
				};
				records.Add(newRecord);
			}
			CsvWriter.AppendNewCosts(CsvFile, records);
		}

		private static void ViewCosts(ViewCostsCommand vc)
		{
			if (!File.Exists(CsvFile))
			{
				Console.WriteLine($"No file exists: {Path.GetFullPath(CsvFile)}");
				return;
			}
			var query = CsvReader.ReadFile(CsvFile).AsQueryable();
			var names = vc.CostNames?.ToList();
			if (names?.Count > 0)
				query = query.Where(x => names.Contains(x.Name));
			if (vc.Type.HasValue)
				query = query.Where(x => x.Type == vc.Type);

			var results = query.ToList();
			Console.WriteLine($"Got {results.Count:N0} costs matching user query:");
			Console.WriteLine();
			// This nuget package is the first one I found and I use it at work, there is probably better ones
			var asTable = ConsoleTables.ConsoleTable.From(results).ToMarkDownString();
			Console.WriteLine(asTable);
			Console.WriteLine();
		}
	}
}
