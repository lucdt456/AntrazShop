using AntrazShop.Data;
using AntrazShop.Models.ViewModels;
using AntrazShop.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AntrazShop.Controllers.API
{
	[Route("api/[controller]")]
	[ApiController]
	public class BrandController : ControllerBase
	{
		private readonly IBrandRepository _brandRepository;
		public BrandController(IBrandRepository brandRepository)
		{
			_brandRepository = brandRepository;
		}

		[HttpGet]
		public async Task<IEnumerable<BrandVM>> GetBrands()
		{
			return await _brandRepository.GetBrands();
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetBrand(int id)
		{
			var brand = await _brandRepository.GetBrand(id);
			if (brand != null)
			{
				return Ok(brand);
			}
			else return NotFound();
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateBrand(int id, [FromBody] Brand newBrand)
		{
			var brand = await _brandRepository.UpdateBrand(id, newBrand);
			if (brand != null)
			{
				return Ok();
			}
			else return NotFound();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteBrand(int id)
		{
			var isDelete = await _brandRepository.DeleteBrand(id);
			if (isDelete == true)
			{
				return Ok();
			}
			else return NotFound();
		}
	}
}
