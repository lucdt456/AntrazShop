using AntrazShop.Data;
using AntrazShop.Helper;
using AntrazShop.Interfaces.Repositories;
using AntrazShop.Interfaces.Services;
using AntrazShop.Models.DTOModels;
using AntrazShop.Models.ViewModels;
using AntrazShop.Repositories;
using AutoMapper;

namespace AntrazShop.Services
{
	public class CategoryService : ICategoryService
	{
		private readonly ICategoryRepository _categoryRepository;
		private readonly IMapper _mapper;

		public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
		{
			_categoryRepository = categoryRepository;
			_mapper = mapper;
		}

		public async Task<ServiceResponse<CategoryVM>> CreateCategory(CategoryDTO dto)
		{
			var response = new ServiceResponse<CategoryVM>();
			try
			{
				var category = _mapper.Map<Category>(dto);
				category.CreateAt = DateTime.Now;
				category = await _categoryRepository.CreateCategory(category);
				response.Data = _mapper.Map<CategoryVM>(category);
			}
			catch (Exception ex)
			{
				response.IsSuccess = false;
				response.Errors.Add(ex.Message);
			}
			return response;
		}

		public async Task<ServiceResponse<string>> DeleteCategory(int id)
		{
			var response = new ServiceResponse<string>();
			try
			{
				await _categoryRepository.DeletecCategory(id);
				response.Data = "Xoá danh mục thành công!";
			}
			catch (Exception ex)
			{
				response.IsSuccess = false;
				response.Errors.Add(ex.Message);
			}
			return response;
		}


		public async Task<ServiceResponse<IEnumerable<CategoryVM>>> GetCategories()
		{
			var response = new ServiceResponse<IEnumerable<CategoryVM>>();
			try
			{
				var categories = await _categoryRepository.GetCategories();
				response.Data = _mapper.Map<List<CategoryVM>>(categories);
			}
			catch (Exception ex)
			{
				response.IsSuccess = false;
				response.Errors.Add(ex.Message);
			}
			return response;
		}

		public async Task<ServiceResponse<CategoryVM>> GetCategory(int id)
		{
			var response = new ServiceResponse<CategoryVM>();
			try
			{
				var category = await _categoryRepository.GetCategory(id);
				response.Data = _mapper.Map<CategoryVM>(category);
			}
			catch (Exception ex)
			{
				response.IsSuccess = false;
				response.Errors.Add(ex.Message);
			}
			return response;
		}

		public async Task<ServiceResponse<CategoryVM>> UpdateCategory(int id, CategoryDTO dto)
		{
			var response = new ServiceResponse<CategoryVM>();
			try
			{
				var category = _mapper.Map<Category>(dto);
				category.Id = id;
				category = await _categoryRepository.UpdateCategory(category);
				response.Data = _mapper.Map<CategoryVM>(category);
			}
			catch (Exception ex)
			{
				response.IsSuccess = false;
				response.Errors.Add(ex.Message);
			}
			return response;
		}
	}
}
