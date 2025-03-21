using System.ComponentModel.DataAnnotations.Schema;

namespace AntrazShop.Data
{
	[Table("UserRoles")]
	public class UserRole
	{
		[ForeignKey("User")]
		public int UserId { get; set; }
		public User User { get; set; }

		[ForeignKey("Role")]
		public int RoleId { get; set; }
		public Role Role { get; set; }
	}
}
