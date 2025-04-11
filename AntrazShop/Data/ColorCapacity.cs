using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AntrazShop.Data
{
	[Table("ColorCapacities")]
	public class ColorCapacity
	{
		[Key]
		public int Id { get; set; }
		public int Stock { get; set; }
		[Column(TypeName = "decimal(18,2)")]
		public decimal Price { get; set; }

		[ForeignKey("Color")]
		public int ColorId { get; set; }
		public Color Color { get; set; }

		[ForeignKey("Capacity")]
		public int CapacityId { get; set; }
		public Capacity Capacity { get; set; }

		[ForeignKey("Product")]
		public int ProductId { get; set; }
		public Product Product { get; set; }

		public string Image { get; set; }
		public int Status { get; set; }

		public ICollection<Review> Reviews { get; set; } = new List<Review>();
		public ICollection<WishList> WishLists { get; set; } = new List<WishList>();
		public ICollection<Cart> Carts { get; set; } = new List<Cart>();
		public ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
	}
}
	