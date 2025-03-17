using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AntrazShop.Data
{
	[Table("Roles")]
	public class Role
	{
		[Key]
		public int  Id { get; set; }
		public string  Name { get; set; }
		public string Description { get; set; }
	}
}
