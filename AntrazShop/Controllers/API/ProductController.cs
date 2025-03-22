using AntrazShop.Data;
using AntrazShop.Models.DTOModels;
using AntrazShop.Models.ViewModels;
using AntrazShop.Services;
using AntrazShop.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AntrazShop.Controllers.API
{
	[Route("api/[controller]")]
	[ApiController]

	public class ProductController : ControllerBase
	{
		private readonly IProductService _productService;
		public ProductController(IProductService productService)
		{
			_productService = productService;
		}
		
		[HttpGet]
		public async Task<IEnumerable<ProductVM>> GetProducts()
		{
			return await _productService.GetProducts();
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetProduct(int id)
		{
			var product = await _productService.GetProduct(id);
			if (product != null)
			{
				return Ok(product);
			}
			else return NotFound(new { message = "Không tìm thấy sản phẩm" });
		}
		
		[HttpPost]
		public async Task<IActionResult> AddProduct([FromBody] ProductDTO newProduct)
		{
			return Ok(await _productService.AddProduct(newProduct));
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductDTO productUpdate)
		{
			var productNew = await _productService.UpdateProduct(id, productUpdate);
			if (productNew != null)
			{
				return Ok(productNew);
			}
			else return NotFound(new { message = "Không tìm thấy sản phẩm" });
		}

		[HttpDelete("{id}")]

		public async Task<IActionResult> DeleteProduct(int id)
		{
			bool isDeleted = await _productService.DeleteProduct(id);
			if (isDeleted)
			{
				return Ok(new { message = "Xóa thành công" });
			}
			return NotFound(new { message = "Không tìm thấy sản phẩm" });
		}
	}
}
