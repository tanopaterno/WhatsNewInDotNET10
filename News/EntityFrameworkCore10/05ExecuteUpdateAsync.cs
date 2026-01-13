using Microsoft.EntityFrameworkCore;

namespace WhatsNewInDotNET10.EntityFrameworkCore10.ExecuteUpdateAsync
{
	// Cosa cambia:
	// - Prima ExecuteUpdateAsync prendeva un expression tree, rendendo complicato costruire update dinamici(dove “aggiungi set” condizionali).
	//   In EF 10 puoi passare una lambda con corpo { ... } e fare if, loop, ecc.

	class Demo
	{
		public class Blog
		{
			public int Id { get; set; }
			public string? Description { get; set; }
			public int Viewers { get; set; }
		}

		public class BlogContext : DbContext
		{
			public DbSet<Blog> Blogs { get; set; } = default!;
		}

		static async void Main(bool descriptionChanged)
		{
			var context = new BlogContext();

			// Prima di EF 10:
			await context.Blogs.ExecuteUpdateAsync(s => s.SetProperty(b => b.Viewers, 8));

			if (descriptionChanged)
			{
				await context.Blogs.ExecuteUpdateAsync(s => s.SetProperty(b => b.Description, "foo"));
			}

			// Con EF 10:
			await context.Blogs.ExecuteUpdateAsync(s =>
			{
				s.SetProperty(b => b.Viewers, 8);

				if (descriptionChanged)
				{
					s.SetProperty(b => b.Description, "foo");
				}
			});

		}
	}

	// Note:
	// - Questo è molto più comodo per update “parametrici” costruiti a runtime.
}
