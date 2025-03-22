using System.ComponentModel.DataAnnotations.Schema;

namespace AntrazShop.Data
{
	[Table("UserPermissions")]
	public class UserPermission
	{
		public int UserId { get; set; }
		public User User { get; set; }

		public int PermissionId { get; set; }
		public Permission Permission { get; set; }
	}
}
