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

		[ForeignKey("Color")]
		public int ColorId { get; set; }
		public Color Color { get; set; }
		public int Quantity { get; set; }
	}
}
