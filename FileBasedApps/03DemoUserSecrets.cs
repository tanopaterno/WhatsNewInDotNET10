#:package Microsoft.Extensions.Configuration.UserSecrets@10.0.1

using Microsoft.Extensions.Configuration;

namespace DotNETConf2025Catania.FileBasedApps.DemoUserSecrets
{
	// dotnet user-secrets set "API_KEY" "your-secret-value-12345" -f 03DemoUserSecrets.cs

	class DemoUserSecrets
	{
		public static void Main()
		{
			Console.WriteLine("Demo: Using User-Secrets on File-Based Application");

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
			var configuration = new ConfigurationBuilder()
				.AddUserSecrets<DemoUserSecrets>()
				.Build();

			string newContent = configuration["API_KEY"];

			File.WriteAllText(filePath, newContent);

			Console.WriteLine($"Wrote to file '{filePath}': {newContent}");
		}
	}

	// dotnet user-secrets list --file 03DemoUserSecrets.cs
}
