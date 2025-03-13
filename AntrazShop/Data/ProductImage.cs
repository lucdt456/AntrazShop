using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AntrazShop.Data
{
	[Table("ProductImages")]
	public class ProductImage
	{
		[Key]
		public int Id { get; set; }
		public string ImageUrl { get; set; }
		public int ProductId { get; set; }
		public Product Product { get; set; }

	}
}
