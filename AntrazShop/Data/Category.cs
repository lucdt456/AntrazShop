using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AntrazShop.Data
{
	[Table("Categories")]
	public class Category
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[StringLength(100)]
		public string Name { get; set; }

		[StringLength(500)]
		public string Description { get; set; }

		public ICollection<Product> Products { get; set; } = new List<Product>();

		[StringLength(255)]
		public string? Image { get; set; }
	}
}
