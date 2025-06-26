using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AntrazShop.Data
{
	[Table("Capacities")]
	public class Capacity
	{
		[Key]
		public int Id { get; set; }

		[Required(ErrorMessage = "Giá trị dung lượng là bắt buộc")]
		[StringLength(50, ErrorMessage = "Giá trị dung lượng không được vượt quá 50 ký tự")]
		public string Value { get; set; }

		public ICollection<ColorCapacity> ColorCapacities { get; set; } = new List<ColorCapacity>();
	}
}
