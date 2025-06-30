using AntrazShop.Data;
using AntrazShop.Helper;
using AntrazShop.Interfaces.Repositories;
using AntrazShop.Interfaces.Services;
using AntrazShop.Models.DTOModels;
using AntrazShop.Models.ViewModels;
using AutoMapper;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
namespace AntrazShop.Services
{
	public class ProductService : IProductService
	{
		private readonly IProductRepository _productRepository;
		private readonly IProductColorCapacityRepository _productCCRepository;
		private readonly IMapper _mapper;

		public ProductService(IProductRepository productRepository, IProductColorCapacityRepository productCCRepository, IMapper mapper)
		{
			_productRepository = productRepository;
			_productCCRepository = productCCRepository;
			_mapper = mapper;
		}

		//public async Task<(IEnumerable<ProductVM>, Paginate)> GetProducts(int pg, int size)
		//{
		//	var totalProducts = await _productRepository.GetTotalProductCount();
		//	var pagination = new Paginate(totalProducts, pg, size);
		//	int recSkip = (pg - 1) * size;
		//	var products = await _productRepository.GetProductsWithDetails(recSkip, size);

		//	var productVMs = products.Select(p =>
		//	{
		//		var colorCapacityVMs = p.ColorCapacities.Select(cc => new ProductColorCapacityVM
		//		{
		//			Id = cc.Id,
		//			ColorName = cc.Color.NameColor,
		//			CapacityValue = cc.Capacity.Value,
		//			Image = cc.Image,
		//			Stock = cc.Stock,
		//			Price = cc.Price,
		//			Status = cc.Status,
		//			Reviews = cc.Reviews.Select(r => new ProductReviewVM
		//			{
		//				UserName = r.User.Name,
		//				Description = r.Description,
		//				Rating = r.Rating,
		//				CreatedAt = r.CreatedAt
		//			}).ToList()
		//		}).ToList();

		//		var status = colorCapacityVMs.Any() ? colorCapacityVMs.Max(cc => cc.Status) : 0;

		//		var totalStock = colorCapacityVMs.Sum(cc => cc.Stock);
		//		if (totalStock == 0) status = 2;

		//		return new ProductVM
		//		{
		//			Id = p.Id,
		//			Name = p.Name,
		//			DiscountAmount = p.DiscountAmount,
		//			Description = p.Description,
		//			ImageView = p.ImageView,
		//			FolderImage = p.ImageFolder,
		//			Brand = p.Brand.Name,
		//			Category = p.Category.Name,
		//			Rating = Math.Round(
		//				colorCapacityVMs
		//				 .SelectMany(cc => cc.Reviews)
		//				 .Select(r => r.Rating)
		//				 .DefaultIfEmpty(0)
		//				 .Average(), 1),
		//			ProductCCs = colorCapacityVMs,
		//			MinPrice = colorCapacityVMs.Any() ? colorCapacityVMs.Min(x => x.Price) : 0,
		//			MaxPrice = colorCapacityVMs.Any() ? colorCapacityVMs.Max(x => x.Price) : 0,
		//			TotalStock = totalStock,
		//			Status = status
		//		};
		//	}).ToList();

		//	return (productVMs, pagination);
		//}

		public async Task<ServiceResponse<(IEnumerable<ProductVM>, Paginate)>> GetProducts(int pg, int size)
		{
			var response = new ServiceResponse<(IEnumerable<ProductVM>, Paginate)>();
			try
			{
				var totalProducts = await _productRepository.GetTotalProductCount();
				var pagination = new Paginate(totalProducts, pg, size);
				int recSkip = (pg - 1) * size;
				recSkip = (recSkip < 0) ? 0 : recSkip;
				var products = await _productRepository.GetProductsWithDetails(recSkip, size);
				var productVMs = _mapper.Map<List<ProductVM>>(products);

				response.Data = (productVMs, pagination);
			}
			catch (Exception ex)
			{
				response.IsSuccess = false;
				response.Errors.Add(ex.Message);
			}
			return response;
		}

		//public async Task<(IEnumerable<ProductVM>, Paginate)> SearchProducts(string? search, int pg, int size)
		//{
		//	search ??= "";
		//	var count = await _productRepository.GetTotalProductCountSearch(search);
		//	var panigation = new Paginate(count, pg, size);
		//	int recSkip = (pg - 1) * size;
		//	var products = await _productRepository.SearchProducts(search, recSkip, size);
		//	var productVMs = products.Select(p =>
		//	{
		//		var colorCapacityVMs = p.ColorCapacities.Select(cc => new ProductColorCapacityVM
		//		{
		//			Id = cc.Id,
		//			ColorName = cc.Color.NameColor,
		//			CapacityValue = cc.Capacity.Value,
		//			Image = cc.Image,
		//			Stock = cc.Stock,
		//			Price = cc.Price,
		//			Status = cc.Status,
		//			Reviews = cc.Reviews.Select(r => new ProductReviewVM
		//			{
		//				UserName = r.User.Name,
		//				Description = r.Description,
		//				Rating = r.Rating,
		//				CreatedAt = r.CreatedAt
		//			}).ToList()
		//		}).ToList();

		//		var status = colorCapacityVMs.Any() ? colorCapacityVMs.Max(cc => cc.Status) : 0;

		//		var totalStock = colorCapacityVMs.Sum(cc => cc.Stock);
		//		if (totalStock == 0) status = 2;

		//		return new ProductVM
		//		{
		//			Id = p.Id,
		//			Name = p.Name,
		//			DiscountAmount = p.DiscountAmount,
		//			Description = p.Description,
		//			ImageView = p.ImageView,
		//			FolderImage = p.ImageFolder,
		//			Brand = p.Brand.Name,
		//			Category = p.Category.Name,
		//			Rating = Math.Round(
		//				colorCapacityVMs
		//				 .SelectMany(cc => cc.Reviews)
		//				 .Select(r => r.Rating)
		//				 .DefaultIfEmpty(0)
		//				 .Average(), 1),
		//			ProductCCs = colorCapacityVMs,
		//			MinPrice = colorCapacityVMs.Any() ? colorCapacityVMs.Min(x => x.Price) : 0,
		//			MaxPrice = colorCapacityVMs.Any() ? colorCapacityVMs.Max(x => x.Price) : 0,
		//			TotalStock = totalStock,
		//			Status = status,
		//			SoldAmount = p.ColorCapacities.Sum(cc => cc.SoldAmount)
		//		};
		//	}).ToList();

		//	return (productVMs, panigation);
		//}

		public async Task<ServiceResponse<(IEnumerable<ProductVM>, Paginate)>> SearchProducts(string? search, int pg, int size)
		{
			var response = new ServiceResponse<(IEnumerable<ProductVM>, Paginate)>();
			try
			{
				search ??= "";
				var count = await _productRepository.GetTotalProductCountSearch(search);
				var pagination = new Paginate(count, pg, size);
				int recSkip = (pg - 1) * size;
				recSkip = (recSkip < 0) ? 0 : recSkip;
				var products = await _productRepository.SearchProducts(search, recSkip, size);
				var productVMs = _mapper.Map<List<ProductVM>>(products);

				response.Data = (productVMs, pagination);
			}
			catch (Exception ex)
			{
				response.IsSuccess = false;
				response.Errors.Add(ex.Message);
			}
			return response;
		}

		//public async Task<ProductVM> GetProduct(int id)
		//{
		//	var product = await _productRepository.GetProduct(id);

		//	var colorCapacityVMs = product.ColorCapacities.Select(cc => new ProductColorCapacityVM
		//	{
		//		Id = cc.Id,
		//		ColorName = cc.Color.NameColor,
		//		CapacityValue = cc.Capacity.Value,
		//		Image = cc.Image,
		//		Stock = cc.Stock,
		//		Price = cc.Price,
		//		Status = cc.Status,
		//		Reviews = cc.Reviews.Select(r => new ProductReviewVM
		//		{
		//			UserName = r.User.Name,
		//			Description = r.Description,
		//			Rating = r.Rating,
		//			CreatedAt = r.CreatedAt
		//		}).ToList()
		//	}).ToList();

		//	var status = colorCapacityVMs.Any() ? colorCapacityVMs.Max(cc => cc.Status) : 0;
		//	var totalStock = colorCapacityVMs.Sum(cc => cc.Stock);
		//	if (totalStock == 0) status = 2;

		//	var rating = Math.Round(
		//		colorCapacityVMs
		//			.SelectMany(cc => cc.Reviews)
		//			.Select(r => r.Rating)
		//			.DefaultIfEmpty(0)
		//			.Average(), 1);

		//	var productVM = new ProductVM
		//	{
		//		Id = product.Id,
		//		Name = product.Name,
		//		DiscountAmount = product.DiscountAmount,
		//		Description = product.Description,
		//		ImageView = product.ImageView,
		//		FolderImage = product.ImageFolder,
		//		Brand = product.Brand.Name,
		//		Category = product.Category.Name,
		//		Rating = rating,
		//		ProductCCs = colorCapacityVMs,
		//		MinPrice = colorCapacityVMs.Any() ? colorCapacityVMs.Min(x => x.Price) : 0,
		//		MaxPrice = colorCapacityVMs.Any() ? colorCapacityVMs.Max(x => x.Price) : 0,
		//		TotalStock = totalStock,
		//		Status = status
		//	};

		//	return productVM;
		//}

		public async Task<ServiceResponse<ProductVM>> GetProduct(int id)
		{
			var response = new ServiceResponse<ProductVM>();
			try
			{
				var product = await _productRepository.GetProduct(id);
				if (product == null)
				{
					response.IsSuccess = false;
					response.Errors.Add("Không tìm thấy sản phẩm");
					return response;
				}

				response.Data = _mapper.Map<ProductVM>(product);
			}
			catch (Exception ex)
			{
				response.IsSuccess = false;
				response.Errors.Add(ex.Message);
			}
			return response;
		}

		public async Task<ServiceResponse<string>> AddProduct(ProductDTO newProduct)
		{
			var response = new ServiceResponse<string>();
			try
			{
				var checkExistName = await _productRepository.CheckProductNameExist(newProduct.Name);
				if (checkExistName)
				{
					response.IsSuccess = false;
					response.Errors.Add("Tên sản phẩm đã tồn tại");
					return response;
				}
				//Chuyển đổi tên file ảnh đã xử lý trong class FileNameHelper
				string slugProductName = FileNameHelper.ToSlug(newProduct.Name);

				// Đường dẫn thư mục (wwwroot/admin/imgs/product/productName)
				var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "admin", "imgs", "product", slugProductName);

				// Kiểm tra thư mục chưa tồn tại thì tạo mới
				if (!Directory.Exists(imagePath))
				{
					Directory.CreateDirectory(imagePath);
				}

				// tên file lưu
				string imageViewName = "1" + Path.GetExtension(newProduct.ImageView.FileName);

				//Đường dẫn lưu + tên file
				var filePath = Path.Combine(imagePath, imageViewName);

				// lưu file ảnh vào đường dẫn
				using (var stream = new FileStream(filePath, FileMode.Create))
				{
					await newProduct.ImageView.CopyToAsync(stream);
				}

				var product = new Product
				{
					Name = newProduct.Name.Trim(),
					Description = newProduct.Description,
					ImageView = imageViewName,
					ImageFolder = slugProductName,
					BrandId = newProduct.BrandId,
					CategoryId = newProduct.CategoryId,
					DiscountAmount = newProduct.DiscountAmount,
					CreateAt = DateTime.Now
				};

				var productId = await _productRepository.AddProduct(product);
				foreach (var productCCDTO in newProduct.ProductCCDTOs)
				{
					var colorId = await _productCCRepository.AddColor(productCCDTO.ColorName);
					var capacityId = await _productCCRepository.AddCapacity(productCCDTO.CapacityValue);
					string imageName = FileNameHelper.ToSlug(productCCDTO.ColorName) + '_' + FileNameHelper.ToSlug(productCCDTO.CapacityValue) + Path.GetExtension(productCCDTO.Image.FileName);

					filePath = Path.Combine(imagePath, imageName);

					using (var stream = new FileStream(filePath, FileMode.Create))
					{
						await productCCDTO.Image.CopyToAsync(stream);
					}

					var productCC = new ColorCapacity
					{
						Stock = productCCDTO.Stock,
						Price = productCCDTO.Price,
						ColorId = colorId,
						CapacityId = capacityId,
						Image = imageName,
						Status = productCCDTO.Status,
						ProductId = productId,
						CreateAt = DateTime.Now
					};

					await _productCCRepository.AddColorCapacity(productCC);
				}
			}
			catch (Exception ex)
			{
				response.IsSuccess = false;
				response.Errors.Add(ex.Message);
				return response;
			}
			return response;
		}

		public async Task<ServiceResponse<Product>> UpdateProduct(int id, ProductDTO productUpdate)
		{
			var response = new ServiceResponse<Product>();
			try
			{
				var product = await _productRepository.GetProduct(id);
				if (product.Name != productUpdate.Name)
				{
					var checkExistName = await _productRepository.CheckProductNameExist(productUpdate.Name);
					if (checkExistName)
					{
						response.IsSuccess = false;
						response.Errors.Add("Tên sản phẩm đã tồn tại");
						return response;
					}
				}

				string newProductFolder = FileNameHelper.ToSlug(productUpdate.Name);
				string newProductUrl = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "admin", "imgs", "product", newProductFolder);
				string oldProductUrl = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "admin", "imgs", "product", product.ImageFolder);

				// Xử lý đổi tên thư mục nếu cần
				if (newProductFolder != product.ImageFolder)
				{
					if (Directory.Exists(oldProductUrl))
					{
						if (Directory.Exists(newProductUrl))
						{
							Directory.Delete(newProductUrl, true);
						}
						Directory.Move(oldProductUrl, newProductUrl);
					}
					else
					{
						Directory.CreateDirectory(newProductUrl);
					}
				}

				string imageViewName = product.ImageView;

				// Xử lý cập nhật ảnh nếu có
				if (productUpdate.ImageView != null)
				{
					imageViewName = "1" + Path.GetExtension(productUpdate.ImageView.FileName);
					// Fix: Đường dẫn chính xác
					var oldImagePath = Path.Combine(newProductUrl, product.ImageView);
					var newImagePath = Path.Combine(newProductUrl, imageViewName);

					// Xóa ảnh cũ nếu tồn tại
					if (File.Exists(oldImagePath))
					{
						File.Delete(oldImagePath);
					}

					// Lưu ảnh mới
					using (var stream = new FileStream(newImagePath, FileMode.Create))
					{
						await productUpdate.ImageView.CopyToAsync(stream);
					}
				}

				// Cập nhật sản phẩm
				var newProduct = new Product
				{
					Name = productUpdate.Name,
					DiscountAmount = productUpdate.DiscountAmount,
					Description = productUpdate.Description,
					ImageView = imageViewName,
					BrandId = productUpdate.BrandId,
					CategoryId = productUpdate.CategoryId,
					ImageFolder = newProductFolder
				};

				response.Data = await _productRepository.UpdateProduct(id, newProduct);
			}
			catch (Exception ex)
			{
				response.IsSuccess = false;
				response.Errors.Add(ex.Message);
				return response;
			}
			return response;
		}
		public async Task<ServiceResponse<bool>> DeleteProduct(int id)
		{
			var response = new ServiceResponse<bool>();
			var product = await _productRepository.GetProduct(id);

			try
			{
				string urlFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "admin", "imgs", "product", product.ImageFolder);
				try
				{
					Directory.Delete(urlFolder, true);
				}
				catch
				{
					response.IsSuccess = false;
					response.Errors.Add("Lỗi khi xoá folder ảnh");
				}

				await _productRepository.DeleteProduct(id);
			}
			catch (Exception ex)
			{
				response.IsSuccess = false;
				response.Errors.Add(ex.Message);
			}
			return response;
		}

		public async Task<ServiceResponse<(IEnumerable<ProductVM>, Paginate)>> GetProductShop(ProductFilter filter, int pg, int size)
		{
			var response = new ServiceResponse<(IEnumerable<ProductVM>, Paginate)>();
			try
			{
				var totalProducts = await _productRepository.GetTotalProductCount();
				var pagination = new Paginate(totalProducts, pg, size);
				int recSkip = (pg - 1) * size;
				recSkip = (recSkip < 0) ? 0 : recSkip;

				var products = await _productRepository.GetProductsWithDetails(recSkip, size);
				var productVMs = _mapper.Map<List<ProductVM>>(products);

				response.Data = (productVMs, pagination);
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
