using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AntrazShop.Data
{
	[Table("ColorCapacities")]
	public class ColorCapacity
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[Range(0, int.MaxValue)]
		public int Stock { get; set; }

		[Required]
		[Range(0, double.MaxValue)]
		[Column(TypeName = "decimal(18,2)")]
		public decimal Price { get; set; }

		[Required]
		[ForeignKey("Color")]
		public int ColorId { get; set; }

		[JsonIgnore]
		public Color Color { get; set; }

		[Required]
		[ForeignKey("Capacity")]
		public int CapacityId { get; set; }

		[JsonIgnore]
		public Capacity Capacity { get; set; }

		[Required]
		[ForeignKey("Product")]
		public int ProductId { get; set; }

		[JsonIgnore]
		public Product Product { get; set; }

		[Required]
		[StringLength(255)]
		public string Image { get; set; }

		[Required]
		public int Status { get; set; }

		[Required]
		public bool StatusImage { get; set; } = false;

		[Range(0, int.MaxValue)]
		public int SoldAmount { get; set; } = 0;

		public DateTime? CreateAt { get; set; }

		[JsonIgnore]
		public ICollection<Review> Reviews { get; set; } = new List<Review>();
		[JsonIgnore]
		public ICollection<WishList> WishLists { get; set; } = new List<WishList>();
		[JsonIgnore]
		public ICollection<Cart> Carts { get; set; } = new List<Cart>();
		[JsonIgnore]
		public ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
	}
}
