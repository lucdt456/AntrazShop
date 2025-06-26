using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AntrazShop.Data
{
	[Table("UserPermissions")]
	public class UserPermission
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[ForeignKey("User")]
		public int UserId { get; set; }

		public User User { get; set; }

		[Required]
		[ForeignKey("Permission")]
		public int PermissionId { get; set; }

		public Permission Permission { get; set; }
	}
}
