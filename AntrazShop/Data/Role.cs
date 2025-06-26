using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AntrazShop.Data
{
	[Table("Roles")]
	public class Role
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[StringLength(100)]
		public string Name { get; set; }

		[StringLength(255)]
		public string Description { get; set; }

		public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
		public ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
	}
}
