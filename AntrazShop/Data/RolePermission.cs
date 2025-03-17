using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace AntrazShop.Data
{
	[Table("RolePermissions")]
	public class RolePermission
	{
		[ForeignKey("Role")]
		public int RoleId { get; set; }
		public Role Role { get; set; }

		[ForeignKey("Permission")]
		public int PermissionId { get; set; }
		public Permission Permission { get; set; }
	}
}
