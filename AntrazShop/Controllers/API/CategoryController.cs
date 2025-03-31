using AntrazShop.Data;
using AntrazShop.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AntrazShop.Controllers.API
{
	[Route("api/[controller]")]
	[ApiController]
	public class CategoryController : ControllerBase
	{
		private readonly ICategoryService _categoryService;
		public CategoryController(ICategoryService categoryService)
		{
			_categoryService = categoryService;
		}

		[HttpGet]
		public async Task<IActionResult> GetCategories(int page = 1, int size = 10)
		{
			var (categories, pagination) = await _categoryService.GetCategories(page, size);
			return Ok(new
			{
				Categories = categories,
				Pagination = pagination
			});
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetCategory(int id)
		{
			var brand = await _categoryService.GetCategory(id);
			if (brand != null)
			{
				return Ok(brand);
			}
			else return NotFound();
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateCategory(int id, [FromBody] Category newCategory)
		{
			var category = await _categoryService.UpdateCategory(id, newCategory);
			if (category != null)
			{
				return Ok();
			}
			else return NotFound();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteCategory(int id)
		{
			var isDelete = await _categoryService.DeleteCategory(id);
			if (isDelete == true)
			{
				return Ok();
			}
			else return NotFound();
		}
	}
}
