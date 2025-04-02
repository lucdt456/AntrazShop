using System.ComponentModel.DataAnnotations.Schema;

namespace AntrazShop.Data
{
	[Table("Carts")]
	public class Cart
	{
		[ForeignKey("User")]
		public int UserId { get; set; }
		public User User { get; set; }

		[ForeignKey("Color")]
		public int ColorId { get; set; }
		public Color Color { get; set; }

		public int Quantity { get; set; }
		public double Total { get; set; }
	}
}
