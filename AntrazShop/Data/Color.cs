using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AntrazShop.Data
{
	[Table("Colors")]
	public class Color
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[StringLength(50)]
		public string NameColor { get; set; }

		public ICollection<ColorCapacity> ColorCapacities { get; set; } = new List<ColorCapacity>();
	}
}
