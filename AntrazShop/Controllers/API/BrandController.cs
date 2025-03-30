using AntrazShop.Data;
using AntrazShop.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AntrazShop.Controllers.API
{
	[Route("api/[controller]")]
	[ApiController]
	public class BrandController : ControllerBase
	{
		private readonly IBrandService _brandService;
		public BrandController(IBrandService brandService)
		{
			_brandService = brandService;
		}

		[HttpGet]
		public async Task<IActionResult> GetBrands(int page = 1, int size = 10)
		{
			var (brands, pagination) = await _brandService.GetBrands(page, size);

			return Ok(new
			{
				Brands = brands,
				Pagination = pagination
			});
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetBrand(int id)
		{
			var brand = await _brandService.GetBrand(id);
			if (brand != null)
			{
				return Ok(brand);
			}
			else return NotFound();
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateBrand(int id, [FromBody] Brand newBrand)
		{
			var brand = await _brandService.UpdateBrand(id, newBrand);
			if (brand != null)
			{
				return Ok();
			}
			else return NotFound();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteBrand(int id)
		{
			var isDelete = await _brandService.DeleteBrand(id);
			if (isDelete == true)
			{
				return Ok();
			}
			else return NotFound();
		}
	}
}
