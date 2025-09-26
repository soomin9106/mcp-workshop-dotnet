
using MyMonkeyApp;
using System.Threading.Tasks;

// MCP 서버 URL (예시)
const string mcpServerUrl = "https://your-mcp-server/api/monkeys";

var asciiArts = new[]
{
	@"   (o.o)  //\n  //| |\\\n   /   \",
	@"  (o.o)\n  (  (  )  )\n   \\   //",
	@"   //\\\n  (o.o)\n  ( : )",
	@"   (o.o)\n  /|   |\\\n   /   \",
	@"   (o.o)\n  (  (  )  )\n   ( : )"
};

void ShowMenu()
{
	Console.WriteLine("============================");
	Console.WriteLine(" Monkey Console Application ");
	Console.WriteLine("============================");
	Console.WriteLine("1. List all monkeys");
	Console.WriteLine("2. Get details for a specific monkey by name");
	Console.WriteLine("3. Get a random monkey");
	Console.WriteLine("4. Exit app");
	Console.Write("Select menu: ");
}

void PrintMonkeyTable(List<Monkey> monkeys)
{
	Console.WriteLine("\n| Name                | Location               | Population |");
	Console.WriteLine("-----------------------------------------------------------");
	foreach (var m in monkeys)
		Console.WriteLine($"| {m.Name,-18} | {m.Location,-20} | {m.Population,10} |");
	Console.WriteLine();
}

void PrintMonkeyDetail(Monkey monkey)
{
	Console.WriteLine($"\nName: {monkey.Name}");
	Console.WriteLine($"Location: {monkey.Location}");
	Console.WriteLine($"Population: {monkey.Population}");
	Console.WriteLine("ASCII Art:");
	Console.WriteLine(monkey.AsciiArt);
}

void PrintRandomAsciiArt()
{
	var rand = new Random();
	var art = asciiArts[rand.Next(asciiArts.Length)];
	Console.WriteLine("\nFunny Monkey Art:");
	Console.WriteLine(art);
	Console.WriteLine();
}

await RunAppAsync();

async Task RunAppAsync()
{
	try
	{
		// MCP 서버에서 데이터 로드
		Console.WriteLine("Loading monkey data from MCP server...");
		await MonkeyHelper.LoadMonkeysFromServerAsync(mcpServerUrl);
		Console.WriteLine("Data loaded!\n");
	}
	catch (Exception ex)
	{
		Console.WriteLine($"데이터 로드 실패: {ex.Message}");
		return;
	}

	while (true)
	{
		ShowMenu();
		var input = Console.ReadLine();
		Console.WriteLine();
		switch (input)
		{
			case "1":
				PrintMonkeyTable(MonkeyHelper.GetMonkeys());
				PrintRandomAsciiArt();
				break;
			case "2":
				Console.Write("Enter monkey name: ");
				var name = Console.ReadLine();
				var monkey = MonkeyHelper.GetMonkeyByName(name ?? "");
				if (monkey != null)
					PrintMonkeyDetail(monkey);
				else
					Console.WriteLine("No monkey found with that name.");
				PrintRandomAsciiArt();
				break;
			case "3":
				var randomMonkey = MonkeyHelper.GetRandomMonkey();
				PrintMonkeyDetail(randomMonkey);
				Console.WriteLine($"(Random picked count: {MonkeyHelper.GetRandomAccessCount()})");
				PrintRandomAsciiArt();
				break;
			case "4":
				Console.WriteLine("Exiting app. Bye!");
				return;
			default:
				Console.WriteLine("Invalid input. Please select again.");
				break;
		}
		Console.WriteLine();
	}
}
