using AntrazShop.Interfaces.Services;
using AntrazShop.Models.DTOModels;
using Microsoft.AspNetCore.Authorization;
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
		public async Task<IActionResult> GetBrands()
		{
			var response = await _brandService.GetBrands();

			if (!response.IsSuccess)
			{
				return BadRequest(new { errors = response.Errors });
			}
			return Ok(response.Data);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetBrand(int id)
		{
			var response = await _brandService.GetBrand(id);

			if (!response.IsSuccess)
			{
				return BadRequest(new { errors = response.Errors });
			}
			return Ok(response.Data);
		}

		[Authorize(Policy = "CanCreateBrand")]
		[HttpPost]
		public async Task<IActionResult> CreateBrand([FromBody] BrandDTO dto)
		{
			var response = await _brandService.CreateBrand(dto);

			if (!response.IsSuccess)
			{
				return BadRequest(new { errors = response.Errors });
			}
			return Ok(response.Data);
		}

		[Authorize(Policy = "CanUpdateBrand")]
		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateBrand(int id, [FromBody] BrandDTO dto)
		{
			var response = await _brandService.UpdateBrand(id, dto);

			if (!response.IsSuccess)
			{
				return BadRequest(new { errors = response.Errors });
			}
			return Ok(response.Data);
		}

		[Authorize(Policy = "CanDeleteBrand")]
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteBrand(int id)
		{
			var response = await _brandService.DeleteBrand(id);

			if (!response.IsSuccess)
			{
				return BadRequest(new { errors = response.Errors });
			}
			return Ok(response.Data);
		}
	}
}
