namespace AntrazShop.Models.ViewModels
{
	public class PermissionGroupVM
	{
		public int  IdGroups { get; set; }
		public string GroupName { get; set; }
		public List<PermissionVM> Permissions { get; set; } = new List<PermissionVM>();
	}
}
