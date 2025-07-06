using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AntrazShop.Data
{
	[Table("OrderDetails")]
	public class OrderDetail
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[ForeignKey("Order")]
		public Guid OrderCode { get; set; }
		[JsonIgnore]
		public Order Order { get; set; }

		[Required]
		[ForeignKey("ColorCapacity")]
		public int ColorCapacityId { get; set; }
		[JsonIgnore]
		public ColorCapacity ColorCapacity { get; set; }

		[Required]
		[Range(1, int.MaxValue)]
		public int Quantity { get; set; }
	}
}
