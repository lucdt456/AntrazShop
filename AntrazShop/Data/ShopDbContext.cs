using Microsoft.EntityFrameworkCore;

namespace AntrazShop.Data
{
	public class ShopDbContext : DbContext
	{
		public ShopDbContext(DbContextOptions<ShopDbContext> options) : base(options)
		{

		}
		public DbSet<Brand> Brands { get; set; }
		public DbSet<Cart> Carts { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<OrderDetail> OrderDetails { get; set; }
		public DbSet<Product> Products { get; set; }
		public DbSet<ProductImage> ProductImages { get; set; }
		public DbSet<Review> Reviews { get; set; }
		public DbSet<User> Users { get; set; }
		public DbSet<WishList> WishLists { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<WishList>().HasKey(w => new { w.UserId, w.ProductId });
			modelBuilder.Entity<Cart>().HasKey(c => new { c.UserId, c.ProductId });
			modelBuilder.Entity<OrderDetail>().HasKey(od => new { od.OrderCode, od.ProductId });

			modelBuilder.Entity<Order>().Property(o => o.CreatedAt).HasDefaultValueSql("GETDATE()");
			base.OnModelCreating(modelBuilder);
		}
	}
}
