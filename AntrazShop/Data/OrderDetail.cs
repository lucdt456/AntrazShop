using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AntrazShop.Data
{
	[Table("OrderDetails")]
	public class OrderDetail
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[ForeignKey("Order")]
		public Guid OrderCode { get; set; }

		public Order Order { get; set; }

		[Required]
		[ForeignKey("ColorCapacity")]
		public int ColorCapacityId { get; set; }

		public ColorCapacity ColorCapacity { get; set; }

		[Required]
		[Range(1, int.MaxValue)]
		public int Quantity { get; set; }
	}
}
