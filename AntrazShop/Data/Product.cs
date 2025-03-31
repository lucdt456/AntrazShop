using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AntrazShop.Data
{
	[Table("Products")]
	public class Product
	{
		[Key]
		public int Id { get; set; }
		public string Name { get; set; }

		[Column(TypeName = "decimal(18,2)")]
		public Decimal Price { get; set; }

		[Column(TypeName = "decimal(18,2)")]
		public Decimal DiscountAmount { get; set; }
		public int Stock { get; set; }
		public string Description { get; set; }
		public string ImageView { get; set; }

		[ForeignKey("Brand")]
		public int BrandId { get; set; }
		public Brand Brand { get; set; }

		[ForeignKey("Category")]
		public int CategoryId { get; set; }
		public Category Category { get; set; }
		public ICollection<ProductImage> ProductImages { get; set; } = new List<ProductImage>();
		public ICollection<Review> Reviews { get; set; } = new List<Review>();
		public int status { get; set; }

		public ICollection<WishList> WishLists { get; set; } = new List<WishList>();
		public ICollection<Cart> Carts { get; set; } = new List<Cart>();
		public ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

	}
}
