using AntrazShop.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace AntrazShop.Models.ViewModels
{
	public class ProductVM
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public decimal MinPrice { get; set; }
		public decimal MaxPrice { get; set; }
		public decimal DiscountAmount { get; set; }
		public int TotalStock { get; set; }
		public int Status { get; set; }
		public string Description { get; set; }
		public string ImageView { get; set; }
		public string Brand { get; set; }
		public string Category { get; set; }
		public double Rating { get; set; }
		public List<ProductColorCapacityVM> ColorCapacityVMs { get; set; } = new();
	}
}	
