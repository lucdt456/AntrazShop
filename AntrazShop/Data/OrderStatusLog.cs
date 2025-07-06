using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AntrazShop.Data
{
	[Table("OrderStatusLogs")]
	public class OrderStatusLog
	{
		[Key]
		public int Id { get; set; }
		public DateTime CreateAt { get; set; }

		[ForeignKey("Order")]
		public Guid OrderCode { get; set; }
		[JsonIgnore]
		public Order Order { get; set; }
		public string Status { get; set; }
	}
}
