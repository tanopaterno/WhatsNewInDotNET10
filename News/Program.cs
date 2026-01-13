using Microsoft.EntityFrameworkCore;
using WhatsNewInDotNET10.CSharp14.ExtensionMembers;
using WhatsNewInDotNET10.CSharp14.FieldKeyword;
using WhatsNewInDotNET10.CSharp14.ModificatoriSuParametriLambdaSemplici;
using WhatsNewInDotNET10.CSharp14.NullConditionalAssignment;
using WhatsNewInDotNET10.CSharp14.UserDefinedCompoundAssignment;
using WhatsNewInDotNET10.EntityFrameworkCore10.ComplexTypesAndJson;
using WhatsNewInDotNET10.EntityFrameworkCore10.LINQLeftJoinAndRightJoin;

namespace News
{
	internal static class Program
	{
		static void Main()
		{
			#region What's new in C# 14 

			#region 01 - extension property + extension method

			string title = "   ";
			Console.WriteLine(title.IsBlank);               // True
			Console.WriteLine(title.OrIfBlank("Untitled")); // Untitled

			#endregion

			#region 01 - extension “statica” sul tipo

			Console.WriteLine(int.IsEven(42)); // True

			#endregion

			#region 02 - normalizzazione + null guard

			var p = new Person { Name = "  Ada  " };
			Console.WriteLine(p.Name); // "Ada"

			#endregion

			#region 03 - proprietà

			Order? o = null;
			o?.Note = Demo.Expensive(); // Expensive() NON viene chiamata se o è null

			o = new Order();
			o?.Note = Demo.Expensive(); // qui sì
			Console.WriteLine(o.Note);

			#endregion

			#region 03 - indicizzatore

			Dictionary<string, int>? counters = null;
			counters?["hits"] = 1; // nessuna eccezione, nessuna assegnazione

			counters = new();
			counters?["hits"] = 1; // counters["hits"] = 1

			#endregion

			#region 04 - una sola API su ReadOnlySpan<T>

			int[] arr = [1, 2, 3];

			// In C# 14 questo diventa più “naturale” in diversi scenari:
			Console.WriteLine(arr.StartsWith(1)); // True

			#endregion

			#region 05 - Generic nameof unbound

			Console.WriteLine(nameof(List<>));         // "List"
			Console.WriteLine(nameof(Dictionary<,>));  // "Dictionary"

			#endregion

			#region 06 - TryParse con out

			TryParse<int> i = (text, out result) => int.TryParse(text, out result);

			Console.WriteLine(i("123", out var n)); // True
			Console.WriteLine(n);                   // 123

			#endregion

			#region 06 - TryParse con ref

			Mutator<int> inc = (ref x) => x++;
			int a = 10;
			inc(ref a);

			Console.WriteLine(a); // 11

			#endregion

			// 07 - Partial per Costruttori ed Eventi

			#region 08 - tipo mutabile

			var c = new Counter();
			c += 10;                    // chiama c.operator +=(10)
			Console.WriteLine(c.Value); // 10

			#endregion

			#endregion

			#region What's new in Entity Framework Core 10

			#region 01 - VectorSearch

			//var appDbContext = new VectorSearch.AppDbContext();

			//var queryVector = appDbContext.Blogs
			//	.Where(b => b.Id == 2)
			//	.Select(b => b.Embedding)
			//	.FirstOrDefault();

			//var vectorResult = appDbContext.Blogs
			//	.OrderBy(b => EF.Functions.VectorDistance("cosine", b.Embedding, queryVector))
			//	.Take(5)
			//	.ToListAsync();

			#endregion

			#region 02 - LINQ LeftJoin e RightJoin

			var studentContext = new StudentContext();

			var queryDotNet9 = studentContext.Students
				.GroupJoin(
					studentContext.Departments,
					student => student.DepartmentID,
					department => department.ID,
					(student, departments) => new { student, departments }
				)
				.SelectMany(
					x => x.departments.DefaultIfEmpty(),
					(x, department) => new
					{
						x.student.FirstName,
						x.student.LastName,
						Department = department != null ? department.Name : "[NONE]"
					}
				)
				.Select(result => $"{result.FirstName} {result.LastName} - {result.Department}")
				.ToList();

			//Console.WriteLine(string.Join(Environment.NewLine, queryDotNet9));

			var queryDotNet10 = studentContext.Students
				.LeftJoin(
					studentContext.Departments,
					student => student.DepartmentID,
					department => department.ID,
					(student, department) => new
					{
						student.FirstName,
						student.LastName,
						Department = department.Name ?? "[NONE]"
					})
				.Select(result => $"{result.FirstName} {result.LastName} - {result.Department}")
				.ToList();

			Console.WriteLine(string.Join(Environment.NewLine, queryDotNet10));

			#endregion

			#region 04 - ComplexTypes e Json

			var blogJsonContext = new BlogJsonContext();

			var queryHighlyViewed = blogJsonContext.Blogs
				.Where(b => b.Details.Viewers > 5)
				.Select(b => $"{b.Id} - {b.Details.Description} ({b.Details.Viewers})")
				.ToList();

			Console.WriteLine(string.Join(Environment.NewLine, queryHighlyViewed));

			#endregion

			#region 06 - Bulk Update su JSON

			blogJsonContext.Blogs.ExecuteUpdateAsync(s => s.SetProperty(b => b.Details.Viewers, b => b.Details.Viewers + 1));

			#endregion

			#endregion
		}
	}
}
