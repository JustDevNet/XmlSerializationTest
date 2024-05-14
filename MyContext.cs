namespace Projekty;
using Microsoft.EntityFrameworkCore;


public class MyContext : DbContext
{
	public static readonly string ConnectionString = "Server=localhost;Database=XmlTest;Trusted_Connection=True;Integrated Security=true; Encrypt=false";

	public DbSet<Doc> Docs { get; set; }

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.UseSqlServer(ConnectionString);
	}
}
