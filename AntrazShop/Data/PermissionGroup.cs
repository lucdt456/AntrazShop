namespace AntrazShop.Data
{
	public class PermissionGroup
	{
		public int Id { get; set; }
		public string GroupName { get; set; }
		public ICollection<Permission> Permissions { get; set; } = new List<Permission>();
	}
}
