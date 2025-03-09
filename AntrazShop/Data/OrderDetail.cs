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
		public virtual Order Order { get; set; }
		[ForeignKey("Product")]
		public int ProductId { get; set; }
		public Product Product { get; set; }
		public int Quantity { get; set; }
	}
}
