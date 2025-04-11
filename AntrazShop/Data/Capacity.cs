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
		public ICollection<ColorCapacity> ColorCapacities { get; set; } = new List<ColorCapacity>();
	}
}
