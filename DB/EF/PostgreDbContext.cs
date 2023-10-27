using Microsoft.EntityFrameworkCore;
using Shate.DAL;
using Shate.DAL.Entities;

namespace Shate.DB.EF;

public class PostgreDbContext : DbContext
{
	public DbSet<User> Users { get; set; }
	public DbSet<Item> Items { get; set; }
	public DbSet<Computer> Computers { get; set; }
	public DbSet<Category> Categories { get; set; }
	public DbSet<Accessory> Accessories { get; set; }
	public PostgreDbContext()
	{
		//Database.EnsureDeleted();
		Database.EnsureCreated();
	}
	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=TestShop;Username=postgres;Password=root");
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<BaseEntity>().HasKey(e => e.Id);
		modelBuilder.Entity<BaseEntity>().UseTpcMappingStrategy();
	}
}