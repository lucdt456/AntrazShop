using AntrazShop.Data;

namespace AntrazShop.Models.ViewModels
{
	public class RoleVM
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public int CountUser { get; set; }
		public ICollection<PermissionVM> Permissions { get; set; }
		public ICollection<string> UserNames { get; set; }
	}
}
