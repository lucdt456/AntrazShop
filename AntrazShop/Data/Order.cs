using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AntrazShop.Data
{
	[Table("Orders")]
	public class Order
	{
		[Key]
		public Guid OrderCode { get; set; } = Guid.NewGuid();

		[Required]
		[ForeignKey("User")]
		public int UserId { get; set; }
		[JsonIgnore]
		public User User { get; set; }
		public string Name { get; set; }
		public string Adress { get; set; }
		public string PhoneNumber { get; set; }
		public decimal Total { get; set; }

		[Required]
		public DateTime CreatedAt { get; set; } = DateTime.Now;

		[Required]
		public string Status { get; set; }
		[JsonIgnore]
		public ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
		public ICollection<OrderStatusLog> OrderStatusLogs { get; set; } = new List<OrderStatusLog>();
		public DateTime? DeliveryDate{ get; set; }
	}
}
