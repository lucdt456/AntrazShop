using AntrazShop.Data;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

[Table("Brands")]
public class Brand
{
	[Key]
	public int Id { get; set; }

	[Required(ErrorMessage = "Tên thương hiệu là bắt buộc")]
	[StringLength(100, ErrorMessage = "Tên thương hiệu không được vượt quá 100 ký tự")]
	public string Name { get; set; }

	[StringLength(500)]
	public string Description { get; set; }

	public ICollection<Product> Products { get; set; } = new List<Product>();

	[StringLength(255)]
	public string? Logo { get; set; }
}
