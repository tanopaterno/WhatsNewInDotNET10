namespace DotNETConf2025Catania.FileBasedApps.DemoSimple
{
	// dotnet run --file .\01DemoSimple.cs
	// dotnet run .\01DemoSimple.cs
	// dotnet .\01DemoSimple.cs

	public class DemoSimple
	{
		public static void Main()
		{
			Console.WriteLine("Demo: Simple File-Based Application");

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
			string newContent = "Welcome to DotNetConf 2025 Catania";

			File.WriteAllText(filePath, newContent);

			Console.WriteLine($"Wrote to file '{filePath}': {newContent}");
		}
	}
}
