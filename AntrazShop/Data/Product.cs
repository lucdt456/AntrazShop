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

		[Required]
		[StringLength(150)]
		public string Name { get; set; }

		[Required]
		[Range(0, double.MaxValue)]
		[Column(TypeName = "decimal(18,2)")]
		public Decimal DiscountAmount { get; set; }

		[StringLength(1000)]
		public string Description { get; set; }

		[Required]
		[StringLength(255)]
		public string ImageView { get; set; }

		[Required]
		[StringLength(255)]
		public string ImageFolder { get; set; }

		[Required]
		[ForeignKey("Brand")]
		public int BrandId { get; set; }

		[JsonIgnore]
		public Brand Brand { get; set; }

		[Required]
		[ForeignKey("Category")]
		public int CategoryId { get; set; }

		[JsonIgnore]
		public Category Category { get; set; }

		[ForeignKey("Sale")]
		public int? SaleId { get; set; }

		[JsonIgnore]
		public Sale? Sale { get; set; }

		public DateTime CreateAt { get; set; }

		public ICollection<ProductImage> ProductImages { get; set; } = new List<ProductImage>();
		public ICollection<ColorCapacity>? ColorCapacities { get; set; } = new List<ColorCapacity>();
	}
}
