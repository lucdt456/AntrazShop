using System.ComponentModel.DataAnnotations.Schema;

namespace AntrazShop.Data
{
	[Table("ControllerActionsPermissions")]
	public class ControllerActionsPermission
	{
		public int Id { get; set; }
		public string ControllerName { get; set; }
		public string ActionName { get; set; }

		public int PermissionId { get; set; }
		public Permission Permission { get; set; }
	}
}
