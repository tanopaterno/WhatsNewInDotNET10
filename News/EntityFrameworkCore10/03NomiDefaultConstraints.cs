using Microsoft.EntityFrameworkCore;

namespace WhatsNewInDotNET10.EntityFrameworkCore10.NomiDefaultConstraints
{
	// Cosa cambia:
	// - Su SQL Server, EF 10 permette di impostare esplicitamente il nome del vincolo di default (invece di farlo generare al DB),
	//   oppure attivare la nominazione automatica per tutti.	

	public class PostContext : DbContext
	{
		public class Post
		{
			public int ID { get; set; }
			public string Title { get; set; } = default!;
			public DateTime CreatedAt { get; set; }
		}

		public DbSet<Post> Posts { get; set; } = default!;

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Post>()
				.Property(p => p.CreatedAt)
				.HasDefaultValueSql("GETDATE()", "DF_Post_CreatedAt");

			modelBuilder.UseNamedDefaultConstraints();
		}
	}

	// Perché è utile:

	// ✅ 1. Migrazioni affidabili e prevedibili

	// - Se non dai un nome al vincolo, SQL Server ne genera uno casuale: DF__Post__Creat__7A672E12
	//   Quando in futuro devi:
	//   - cambiare il default
	//   - rimuovere la colonna
	//   - modificare il tipo
	//  …EF(o tu) non può sapere quel nome.

	// Risultato tipico:
	// - ALTER TABLE Post DROP CONSTRAINT DF__Post__Creat__7A672E12
	// -- 💥 fallisce perché il nome cambia tra ambienti

	// Con un nome esplicito:
	// - ALTER TABLE Post DROP CONSTRAINT DF_Post_CreatedAt
	// -- ✅ funziona sempre
	// -- ✔️ Migrazioni deterministiche
	// -- ✔️ Niente SQL “difensivo” con lookup su sys.default_constraints

	// ✅ 2. Refactoring più semplice

	// Esempi reali:
	// - CreatedAt → InsertedAt
	// - GETDATE() → SYSUTCDATETIME()
	// - cambiare default per multi-tenant

	// Con vincolo nominato:
	// ALTER TABLE Post DROP CONSTRAINT DF_Post_CreatedAt;
	// ALTER TABLE Post ADD CONSTRAINT DF_Post_InsertedAt DEFAULT SYSUTCDATETIME() FOR InsertedAt;

	// ✅ 3. Allineamento con standard DBA / enterprise

	// In molti ambienti:
	// - naming convention obbligatorie
	// - review automatizzate
	// - script condivisi tra team

	// Esempio comune:
	// - DF_<Table>_<Column>

	// Se lasci nomi random:
	// - gli script DBA diventano fragili
	// - i deploy cross-env sono più rischiosi

	// ✅ 4. Coerenza con altri vincoli (PK, FK, CK, IX)

	// Nessuno lascerebbe:
	// - una FK
	// - una PK
	// - un CHECK
	// senza nome.

	// Il DEFAULT è l’unico che storicamente SQL Server ha “nascosto”, ma:
	// - è un oggetto schema a tutti gli effetti
	// - va versionato come gli altri
}
