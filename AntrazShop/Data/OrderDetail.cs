using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AntrazShop.Data
{
	[Table("OrderDetails")]
	public class OrderDetail
	{
		public int Id { get; set; }
		[ForeignKey("Order")]
		public Guid OrderCode { get; set; }
		public Order Order { get; set; }

		[ForeignKey("ColorCapacity")]
		public int ColorCapacityId { get; set; }
		public ColorCapacity ColorCapacity { get; set; }
		public int Quantity { get; set; }
	}
}
