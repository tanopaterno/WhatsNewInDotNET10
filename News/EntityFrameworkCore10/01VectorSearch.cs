using Microsoft.Data.SqlTypes;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace WhatsNewInDotNET10.EntityFrameworkCore10.VectorSearch
{
	// Cosa cambia:
	// - EF Core 10 aggiunge supporto nativo al tipo SQL vector(n) e alla funzione VECTOR_DISTANCE() (SQL Server 2025 / Azure SQL).
	//   In pratica puoi salvare embedding(array di float) direttamente in tabella e fare query di similarità da LINQ.

	// Quando serve:
	// - semantic search (“trova contenuti simili a questa frase”)
	// - RAG(retrieval-augmented generation)
	// - deduplica / clustering per similarità

	[Table("Blog")]
	public class Blog
	{
		public int Id { get; set; }
		public string Name { get; set; } = default!;

		// 1536 è una dimensione tipica (es. embedding di alcuni modelli), ma dipende da te
		[Column(TypeName = "vector(1536)")]
		public SqlVector<float> Embedding { get; set; }
	}

	public class AppDbContext : DbContext
	{
		public DbSet<Blog> Blogs => Set<Blog>();

		protected override void OnConfiguring(DbContextOptionsBuilder options)
			=> options.UseSqlServer("Server=SLIM5-TANOP\\SQL2025;Database=WhatsNewInDotNET10;Trusted_Connection=True;Encrypt=True;TrustServerCertificate=True;");
	}

	// Note:
	// - la distanza più bassa = più simile (dipende dalla metrica) → spesso ordini per distanza crescente come sopra.
	// - serve un DB che supporti davvero vector (SQL Server 2025 / Azure SQL).

	//-- Crea tabella di test
	// CREATE TABLE Blog(Id INT IDENTITY(1,1) PRIMARY KEY, Name NVARCHAR(255) NOT NULL, Embedding VECTOR(1536) NOT NULL );

	//-- Genera 100 righe come dati di test
	//; WITH Blogs AS
	//(
	//	SELECT TOP (100)

	//		ROW_NUMBER() OVER(ORDER BY (SELECT NULL)) AS BlogNumber

	//	FROM sys.objects a
	//	CROSS JOIN sys.objects b
	//),
	//Dims AS
	//(
	//	SELECT TOP (1536)

	//		ROW_NUMBER() OVER(ORDER BY (SELECT NULL)) AS Dim

	//	FROM sys.objects o1
	//	CROSS JOIN sys.objects o2
	//)
	//INSERT INTO dbo.Blog(Name, Embedding)
	//SELECT

	//	CONCAT(N'Blog ', b.BlogNumber) AS Name,
	//	CAST(v.JsonArray AS VECTOR(1536)) AS Embedding
	//FROM Blogs b
	//CROSS APPLY
	//(
	//	SELECT
	//		JsonArray =
	//			CONVERT(NVARCHAR(MAX), N'[') +
	//			STRING_AGG(CONVERT(NVARCHAR(MAX), x.ValStr), N',')

	//				WITHIN GROUP (ORDER BY x.Dim) +
	//            CONVERT(NVARCHAR(MAX), N']')

	//	FROM
	//	(
	//		SELECT
	//			d.Dim,
	//            -- valore deterministico in [-1, 1] circa, diverso per (BlogNumber, Dim)
	//			ValStr = CONVERT(NVARCHAR(40),
	//					 CAST(((ABS(CHECKSUM(CONCAT(b.BlogNumber, N':', d.Dim))) % 2001) - 1000) / 1000.0 AS FLOAT))

	//		FROM Dims d

	//	) x
	//) v;

	//-- Verifica dati di test:
	//-- Conta righe
	//SELECT COUNT(*) AS Righe
	//FROM dbo.Blog;

	//-- Verifica dimensione del vettore(conta elementi dell'array JSON)
	//SELECT TOP (5)

	//	b.Id,
	//    b.Name,
	//    v.Dimensions
	//FROM dbo.Blog b
	//CROSS APPLY(
	//	SELECT COUNT(*) AS Dimensions
	//	FROM OPENJSON(CONVERT(NVARCHAR(MAX), b.Embedding))
	//) v;
}
