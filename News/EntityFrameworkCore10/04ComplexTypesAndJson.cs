using Microsoft.EntityFrameworkCore;

namespace WhatsNewInDotNET10.EntityFrameworkCore10.ComplexTypesAndJson
{
	// Qui ci sono due livelli importanti in EF 10:

	#region 5a) “JSON type support” su SQL Server 2025 / Azure SQL

	// EF 10 supporta il nuovo tipo json (non più JSON come nvarchar(max)), con vantaggi di efficienza e sicurezza.
	// Se usi Azure SQL o compat level >= 170, EF può usare automaticamente json

	//CREATE TABLE dbo.Blogs
	//(
	//	Id INT IDENTITY(1,1) NOT NULL
	//		CONSTRAINT PK_Blogs PRIMARY KEY,
	//    -- Colonna JSON nativa(SQL Server 2025)
	//	Details JSON NOT NULL
	//);
	//GO

	//-- Esempio insert(Description può essere null)
	//INSERT INTO dbo.Blogs(Details)
	//VALUES
	//	(CAST(N'{"Description":"Primo blog","Viewers":10}' AS json)),
	//  (CAST(N'{"Description":null,"Viewers":0}' AS json));
	//GO

	#endregion

	#region 5b) Mappare un complex type dentro una colonna JSON

	public class Blog
	{
		public int Id { get; set; }
		public required BlogDetails Details { get; set; }
	}

	public class BlogDetails
	{
		public string? Description { get; set; }
		public int Viewers { get; set; }
	}

	public class BlogJsonContext : DbContext
	{
		public DbSet<Blog> Blogs { get; set; } = default!;

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Blog>()
				.ComplexProperty(b => b.Details, bd => bd.ToJson());
		}
		protected override void OnConfiguring(DbContextOptionsBuilder options)
			=> options.UseSqlServer("Server=SLIM5-TANOP\\SQL2025;Database=WhatsNewInDotNET10;Trusted_Connection=True;Encrypt=True;TrustServerCertificate=True;");
	}

	#endregion

	// Note:
	// - EF traduce verso funzioni JSON (es. JSON_VALUE(...)) in SQL Server.
}
