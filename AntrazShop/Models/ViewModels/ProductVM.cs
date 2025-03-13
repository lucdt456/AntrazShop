using AntrazShop.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace AntrazShop.Models.ViewModels
{
	public class ProductVM
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public Decimal Price { get; set; }
		public Decimal DiscountAmount { get; set; }
		public string Description { get; set; }
		public string ImageView { get; set; }
		public string Brand { get; set; }
		public string Category { get; set; }
		public int status { get; set; }
		public int Stock { get; set; }
	}
}
