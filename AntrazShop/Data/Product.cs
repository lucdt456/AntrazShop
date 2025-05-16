using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AntrazShop.Data
{
	[Table("Products")]
	public class Product
	{
		[Key]
		public int Id { get; set; }
		public string Name { get; set; }

		[Column(TypeName = "decimal(18,2)")]
		public Decimal DiscountAmount { get; set; }
		public string Description { get; set; }
		public string ImageView { get; set; }
		public string ImageFolder { get; set; }

		[ForeignKey("Brand")]
		public int BrandId { get; set; }
		[JsonIgnore]
		public Brand Brand { get; set; }

		[ForeignKey("Category")]
		public int CategoryId { get; set; }
		[JsonIgnore]
		public Category Category { get; set; }
		public ICollection<ProductImage> ProductImages { get; set; } = new List<ProductImage>();
		public ICollection<ColorCapacity>? ColorCapacities { get; set; } = new List<ColorCapacity>();

	}
}
