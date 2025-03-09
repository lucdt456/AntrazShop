using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AntrazShop.Data
{
	[Table("Orders")]
	public class Order
	{
		[Key]
		public Guid OrderCode { get; set; } = Guid.NewGuid();

		[ForeignKey("User")]
		public int UserId { get; set; }
		public virtual User User { get; set; }
		public DateTime CreatedAt { get; set; } = DateTime.Now;
		public int Status { get; set; }
	}
}
