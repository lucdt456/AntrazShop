using AntrazShop.Interfaces.Services;
using AntrazShop.Models.DTOModels;
using Microsoft.AspNetCore.Mvc;

namespace AntrazShop.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]

    public class ShopController : ControllerBase
    {
		private readonly IShopService _shopService;
		public ShopController(IShopService shopService)
		{
			_shopService = shopService;
		}

		[HttpPost("Products")]
		public async Task<IActionResult> GetProducts([FromBody] ProductFilter filters,  int page = 1, int size = 12)
		{
			var response = await _shopService.GetProducts(filters, page, size);
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

		[HttpGet("TopRating")]
		public async Task<IActionResult> GetProductsTopRating()
		{
			var response = await _shopService.GetProductsTopRating();

			if (!response.IsSuccess)
			{
				return BadRequest(new { errors = response.Errors });
			}

			return Ok(response.Data);

		}

		[HttpGet("TopSold")]
		public async Task<IActionResult> GetProductsTopSell()
		{
			var response = await _shopService.GetProductsTopSell();

			if (!response.IsSuccess)
			{
				return BadRequest(new { errors = response.Errors });
			}

			return Ok(response.Data);

		}

		[HttpGet("TopSaling")]
		public async Task<IActionResult> GetProductsTopSale()
		{
			var response = await _shopService.GetProductsTopSale();

			if (!response.IsSuccess)
			{
				return BadRequest(new { errors = response.Errors });
			}

			return Ok(response.Data);

		}
	}
}
