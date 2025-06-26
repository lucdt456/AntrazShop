using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AntrazShop.Data
{
	[Table("Orders")]
	public class Order
	{
		[Key]
		public Guid OrderCode { get; set; } = Guid.NewGuid();

		[Required]
		[ForeignKey("User")]
		public int UserId { get; set; }

		public User User { get; set; }

		[Required]
		public DateTime CreatedAt { get; set; } = DateTime.Now;

		[Required]
		public int Status { get; set; }

		public ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
	}
}
