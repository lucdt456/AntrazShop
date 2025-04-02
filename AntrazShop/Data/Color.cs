using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AntrazShop.Data
{
	[Table("Colors")]
	public class Color
	{
		[Key]
		public int Id { get; set; }
		public int Stock { get; set; }

		[Column(TypeName = "decimal(18,2)")]
		public decimal Price { get; set; }

		[ForeignKey("Capacity")]
		public int CapacityId { get; set; }
		public Capacity Capacity { get; set; }
		public ICollection<Review> Reviews { get; set; } = new List<Review>();
		public int Status { get; set; }
		public string Image { get; set; }
		public ICollection<WishList> WishLists { get; set; } = new List<WishList>();
		public ICollection<Cart> Carts { get; set; } = new List<Cart>();
		public ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
	}
}
