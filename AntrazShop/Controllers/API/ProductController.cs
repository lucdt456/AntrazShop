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
			var response = await _productService.GetProducts(page, size);
			if (!response.IsSuccess)
			{
				return BadRequest(new { errors = response.Errors });
			}

			var (products, pagination) = response.Data;
			return Ok(new
			{
				Products = products,
				Pagination = pagination
			});
		}

		//Tìm kiếm sản phẩm
		[HttpGet("search")]
		public async Task<IActionResult> SearchProducts(string? search, int page = 1, int size = 10)
		{
			var response = await _productService.SearchProducts(search, page, size);
			if (!response.IsSuccess)
			{
				return BadRequest(new { errors = response.Errors });
			}

			var (products, pagination) = response.Data;
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
			var response = await _productService.GetProduct(id);
			if (!response.IsSuccess)
			{
				return BadRequest(new { errors = response.Errors });
			}
			return Ok(response.Data);
		}

		//Tạo sản phẩm
		[Authorize(Policy = "CanAddProduct")]
		[HttpPost("create")]
		public async Task<IActionResult> AddProduct([FromForm] ProductDTO newProduct)
		{
			var response = await _productService.AddProduct(newProduct);
			if (!response.IsSuccess)
			{
				return BadRequest(new { errors = response.Errors });
			}
			else return Ok(response.Data);
		}

		//Edit sản phẩm
		[Authorize(Policy = "CanUpdateProduct")]
		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateProduct(int id, [FromForm] ProductDTO productUpdate)
		{
			var response = await _productService.UpdateProduct(id, productUpdate);
			if (!response.IsSuccess)
			{
				return BadRequest(new { errors = response.Errors });
			}
			else return Ok(response.Data);
		}

		[Authorize(Policy = "CanDeleteProduct")]
		//Xoá sản phẩm
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteProduct(int id)
		{
			var response = await _productService.DeleteProduct(id);
			if (!response.IsSuccess)
			{
				return BadRequest(new { errors = response.Errors });
			}
			else return Ok("Xoá thành công!");
		}

		//Chỉnh sửa phân loại sản phẩm
		[Authorize(Policy = "CanEditProductCC")]
		[HttpPut("{productFolder}/{idCC}")]
		public async Task<IActionResult> EditProductCC(string productFolder, int idCC, [FromForm] ProductColorCapacityDTO dTO)
		{
			var response = await _productCCService.EditColorCapacity(productFolder, idCC, dTO);
			if (!response.IsSuccess)
			{
				return BadRequest(new { errors = response.Errors });
			}
			else return Ok(response.Data);
		}

		//Tạo phân loại sản phẩm mới
		[Authorize(Policy = "CanCreateProductCC")]
		[HttpPost("{idProduct}/{productFolder}")]
		public async Task<IActionResult> CreateProductCC(int idProduct, string productFolder, [FromForm] ProductColorCapacityDTO dTO)
		{
			var response = await _productCCService.CreateColorCapacity(idProduct, productFolder, dTO);
			if (!response.IsSuccess)
			{
				return BadRequest(new { errors = response.Errors });
			}
			else return Ok(response.Data);
		}

		//Xoá phân loại sản phẩm
		[Authorize(Policy = "CanDeleteProductCC")]
		[HttpDelete("{id}/{productFolder}")]
		public async Task<IActionResult> DeleteProductCC(int id, string productFolder)
		{
			var response = await _productCCService.DeleteColorCapacity(id, productFolder);
			if (!response.IsSuccess)
			{
				return BadRequest(new { errors = response.Errors });
			}
			else return Ok("Xoá thành công!");
		}
	}
}
