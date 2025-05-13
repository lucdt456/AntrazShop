using AntrazShop.Interfaces.Services;
using AntrazShop.Models.DTOModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AntrazShop.Controllers.API
{
	[Route("api/[controller]")]
	[ApiController]

	public class ProductController : ControllerBase
	{
		private readonly IProductService _productService;
		private readonly IProductCCService _productCCService;
		public ProductController(IProductService productService, IProductCCService productCCService)
		{
			_productService = productService;
			_productCCService = productCCService;
		}

		//[Authorize(Policy = "CanViewProducts")]
		//Lấy sản phẩm
		[HttpGet]
		public async Task<IActionResult> GetProducts(int page = 1, int size = 10)
		{
			var (products, pagination) = await _productService.GetProducts(page, size);

			return Ok(new
			{
				Products = products,
				Pagination = pagination
			});
		}

		//[Authorize]
		//Tìm kiếm sản phẩm
		[HttpGet("search")]
		public async Task<IActionResult> SearchProducts(string search, int page = 1, int size = 10)
		{
			var (products, pagination) = await _productService.SearchProducts(search, page, size);
			return Ok(new
			{
				Products = products,
				Pagination = pagination
			});
		}

		//Lấy 1 sản phẩm
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

		//Tạo sản phẩm
		[HttpPost("create")]
		public async Task<IActionResult> AddProduct([FromForm] ProductDTO newProduct)
		{
			return Ok(await _productService.AddProduct(newProduct));
		}

		//Edit sản phẩm
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

		//Xoá sản phẩm
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

		//Chỉnh sửa phân loại sản phẩm

		[HttpPut("{productFolder}/{idCC}")]
		public async Task<IActionResult> EditProductCC(string productFolder, int idCC, [FromForm] ProductColorCapacityDTO dTO)
		{
			var response = await _productCCService.EditColorCapacity(productFolder, idCC, dTO);
			if (!response.IsSuccess)
			{
				return BadRequest(new { errors = response.Errors });
			}
			return Ok(response.Data);
		}


	}
}
