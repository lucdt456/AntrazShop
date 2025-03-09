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
		public string Description { get; set; }
		public string ImageView { get; set; }

		[ForeignKey("Brand")]
		public int BrandId { get; set; }
		public virtual Brand Brand { get; set; }

		[ForeignKey("Category")]
		public int CategoryId { get; set; }
		public virtual Category Category { get; set; }
		public List<ProductImage> ProductImages { get; set; } = new List<ProductImage>();
		public List<Review> Reviews { get; set; } = new List<Review>();
	}
} 
