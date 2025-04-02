using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;

namespace AntrazShop.Data
{
	[Table("Capacities")]
	public class Capacity
	{
		[Key]
		public int Id { get; set; }
		public string Value { get; set; }
		public string Description { get; set; }

		[ForeignKey("Product")]
		public int ProductId { get; set; }
		public Product Product { get; set; }
		public ICollection<Color> Colors { get; set; } = new List<Color>();
	}
}
