using System.ComponentModel.DataAnnotations.Schema;

namespace AntrazShop.Data
{
	[Table("Carts")]
	public class Cart
	{
		[ForeignKey("User")]
		public int UserId { get; set; }
		public User User { get; set; }

		[ForeignKey("ColorCapacity")]
		public int ColorCapacityId { get; set; }
		public ColorCapacity ColorCapacity { get; set; }

		public int Quantity { get; set; }
		public double Total { get; set; }
	}
}
