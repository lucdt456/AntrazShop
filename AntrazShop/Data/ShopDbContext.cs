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
		public DbSet<Role> Roles { get; set; }
		public DbSet<UserRole> UserRoles { get; set; }
		public DbSet<Permission> Permissions { get; set; }
		public DbSet<RolePermission> RolePermissions { get; set; }
		public DbSet<UserPermission> UserPermissions { get; set; }
		public DbSet<Color> Colors { get; set; }
		public DbSet<Capacity> Capacities { get; set; }
		public DbSet<ColorCapacity> ColorCapacities { get; set; }
		public DbSet<UserAuthInfo> UserAuthInfos { get; set; }
		public DbSet<LoginHistory> LoginHistories { get; set; }
		public DbSet<Sale> Sales { get; set; }


		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<WishList>()
						.HasKey(w => new { w.UserId, w.ColorCapacityId });

			modelBuilder.Entity<Cart>()
						.HasKey(c => new { c.UserId, c.ColorCapacityId });

			modelBuilder.Entity<UserRole>()
						.HasKey(u => new { u.UserId, u.RoleId });

			modelBuilder.Entity<RolePermission>()
						.HasKey(r => new { r.RoleId, r.PermissionId });

			modelBuilder.Entity<OrderDetail>()
						.HasKey(od => new { od.OrderCode, od.ColorCapacityId });

			modelBuilder.Entity<Review>()
						.HasKey(rv => new { rv.UserId, rv.ColorCapacityId });

			modelBuilder.Entity<UserPermission>().HasKey(up => new { up.UserId, up.PermissionId });

			modelBuilder.Entity<Product>()
						.HasOne(p => p.Sale)
						.WithMany(s => s.Products)
						.HasForeignKey(p => p.SaleId)
						.OnDelete(DeleteBehavior.SetNull);
		
			modelBuilder.Entity<Order>().Property(o => o.CreatedAt).HasDefaultValueSql("GETDATE()");

			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<User>()
					.HasOne(u => u.UserAuthInfo)
					.WithOne(a => a.User)
					.HasForeignKey<UserAuthInfo>(a => a.UserId)
					.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<LoginHistory>()
						.HasOne(l => l.UserAuthInfo)
						.WithMany(u => u.LoginHistories)
						.HasForeignKey(l => l.UserId)
						.OnDelete(DeleteBehavior.Cascade);
		}
	}
}
