using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AntrazShop.Data
{
	[Table("Permissions")]
	public class Permission
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[StringLength(100)]
		public string Name { get; set; }

		[StringLength(255)]
		public string Description { get; set; }

		[Required]
		[StringLength(100)]
		public string NameController { get; set; }

		public ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
		public ICollection<UserPermission> UserPermissions { get; set; } = new List<UserPermission>();
	}
}
