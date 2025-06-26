using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AntrazShop.Data
{
	[Table("ProductImages")]
	public class ProductImage
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[StringLength(255)]
		public string ImageUrl { get; set; }

		[Required]
		[ForeignKey("Product")]
		public int ProductId { get; set; }

		public Product Product { get; set; }
	}
}
