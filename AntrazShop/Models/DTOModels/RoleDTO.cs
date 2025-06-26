namespace AntrazShop.Models.DTOModels
{
	public class RoleDTO
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public List<int>? PermissionIds { get; set; }
	}
}
