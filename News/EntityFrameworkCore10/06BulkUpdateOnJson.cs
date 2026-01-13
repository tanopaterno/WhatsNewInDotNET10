using Microsoft.EntityFrameworkCore;

namespace WhatsNewInDotNET10.EntityFrameworkCore10.BulkUpdateOnJson
{
	// Cosa cambia:
	// - EF 10 abilita anche ExecuteUpdateAsync su proprietà dentro colonne JSON (se mappate come complex types), così fai bulk update senza SaveChanges() entity-by-entity.

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

	public class BlogContext : DbContext
	{
		public DbSet<Blog> Blogs { get; set; } = default!;
	}
}
