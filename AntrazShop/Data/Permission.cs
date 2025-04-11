using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AntrazShop.Data
{
	[Table("Permissions")]
	public class Permission
	{
		[Key]
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string NameController { get; set; }
		public ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
		public ICollection<UserPermission> UserPermissions { get; set; } = new List<UserPermission>();
	}
}
