namespace DotNETConf2025Catania.FileBasedApps.DemoWithArgs
{
	// dotnet run --file .\02DemoWithArgs.cs -- 'Welcome to DotNetConf 2025 Catania'

	public class DemoWithArgs
	{
		public static void Main(string[] args)
		{
			Console.WriteLine("Demo: File-Based Application with args");

			// Example of reading from a file
			string filePath = "example.txt";

			if (File.Exists(filePath))
			{
				string content = File.ReadAllText(filePath);
				Console.WriteLine("File Content:");
				Console.WriteLine(content);
			}
			else
			{
				Console.WriteLine($"File '{filePath}' does not exist.");
			}

			// Example of writing to a file
			string newContent = args[0];

			File.WriteAllText(filePath, newContent);

			Console.WriteLine($"Wrote to file '{filePath}': {newContent}");
		}
	}
}
