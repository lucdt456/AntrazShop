using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AntrazShop.Data
{
	// Ví dụ Entity User
	[Table("Users")]
	public class User
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[StringLength(100)]
		public string Name { get; set; }

		[Required]
		[StringLength(10)]
		public string Gender { get; set; }

		[Required]
		[EmailAddress]
		[StringLength(100)]
		public string Email { get; set; }

		[Required]
		[StringLength(255)]
		public string PasswordHash { get; set; }

		[Required]
		[Phone]
		[StringLength(20)]
		public string PhoneNumber { get; set; }

		[StringLength(255)]
		public string? Address { get; set; }

		[StringLength(255)]
		public string? Avatar { get; set; }

		[Required]
		public DateOnly Birthday { get; set; }

		[StringLength(100)]
		public string? Hometown { get; set; }

		[Required]
		public bool workerAccount { get; set; } = false;

		[Required]
		public DateTime CreatedAt { get; set; } = DateTime.Now;

		public ICollection<WishList> WishLists { get; set; } = new List<WishList>();
		public ICollection<Cart> Carts { get; set; } = new List<Cart>();
		public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
		public ICollection<UserPermission> UserPermissions { get; set; } = new List<UserPermission>();
		public ICollection<Review> Reviews { get; set; } = new List<Review>();

		public UserAuthInfo UserAuthInfo { get; set; }
	}
}
