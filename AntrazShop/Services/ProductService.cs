using AntrazShop.Data;
using AntrazShop.Helper;
using AntrazShop.Interfaces.Repositories;
using AntrazShop.Interfaces.Services;
using AntrazShop.Models.DTOModels;
using AntrazShop.Models.ViewModels;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
namespace AntrazShop.Services
{
	public class ProductService : IProductService
	{
		private readonly IProductRepository _productRepository;
		private readonly IProductColorCapacityRepository _productCCRepository;
		List<string> ListErrors = new();

		public ProductService(IProductRepository productRepository, IProductColorCapacityRepository productCCRepository)
		{
			_productRepository = productRepository;
			_productCCRepository = productCCRepository;
		}

		public async Task<(IEnumerable<ProductVM>, Paginate)> GetProducts(int pg, int size)
		{
			var totalProducts = await _productRepository.GetTotalProductCount();
			var pagination = new Paginate(totalProducts, pg, size);
			int recSkip = (pg - 1) * size;
			var products = await _productRepository.GetProductsWithDetails(recSkip, size);

			var productVMs = products.Select(p =>
			{
				var colorCapacityVMs = p.ColorCapacities.Select(cc => new ProductColorCapacityVM
				{
					Id = cc.Id,
					ColorName = cc.Color.NameColor,
					CapacityValue = cc.Capacity.Value,
					Image = cc.Image,
					Stock = cc.Stock,
					Price = cc.Price,
					Status = cc.Status,
					Reviews = cc.Reviews.Select(r => new ProductReviewVM
					{
						UserName = r.User.Name,
						Description = r.Description,
						Rating = r.Rating,
						CreatedAt = r.CreatedAt
					}).ToList()
				}).ToList();

				var status = colorCapacityVMs.Any() ? colorCapacityVMs.Max(cc => cc.Status) : 0;

				var totalStock = colorCapacityVMs.Sum(cc => cc.Stock);
				if (totalStock == 0) status = 2;

				return new ProductVM
				{
					Id = p.Id,
					Name = p.Name,
					DiscountAmount = p.DiscountAmount,
					Description = p.Description,
					ImageView = p.ImageView,
					FolderImage = p.ImageFolder,
					Brand = p.Brand.Name,
					Category = p.Category.Name,
					Rating = Math.Round(
						colorCapacityVMs
						 .SelectMany(cc => cc.Reviews)
						 .Select(r => r.Rating)
						 .DefaultIfEmpty(0)
						 .Average(), 1),
					ProductCCs = colorCapacityVMs,
					MinPrice = colorCapacityVMs.Any() ? colorCapacityVMs.Min(x => x.Price) : 0,
					MaxPrice = colorCapacityVMs.Any() ? colorCapacityVMs.Max(x => x.Price) : 0,
					TotalStock = totalStock,
					Status = status
				};
			}).ToList();

			return (productVMs, pagination);
		}

		/// <summary>
		/// Tìm kiếm sản phẩm
		/// </summary>
		/// <param name="search"></param>
		/// <param name="pg"></param>
		/// <param name="size"></param>
		/// <returns></returns>
		public async Task<(IEnumerable<ProductVM>, Paginate)> SearchProducts(string? search, int pg, int size)
		{
			search ??= "";
			var count = await _productRepository.GetTotalProductCountSearch(search);
			var panigation = new Paginate(count, pg, size);
			int recSkip = (pg - 1) * size;
			var products = await _productRepository.SearchProducts(search, recSkip, size);
			var productVMs = products.Select(p =>
			{
				var colorCapacityVMs = p.ColorCapacities.Select(cc => new ProductColorCapacityVM
				{
					Id = cc.Id,
					ColorName = cc.Color.NameColor,
					CapacityValue = cc.Capacity.Value,
					Image = cc.Image,
					Stock = cc.Stock,
					Price = cc.Price,
					Status = cc.Status,
					Reviews = cc.Reviews.Select(r => new ProductReviewVM
					{
						UserName = r.User.Name,
						Description = r.Description,
						Rating = r.Rating,
						CreatedAt = r.CreatedAt
					}).ToList()
				}).ToList();

				var status = colorCapacityVMs.Any() ? colorCapacityVMs.Max(cc => cc.Status) : 0;

				var totalStock = colorCapacityVMs.Sum(cc => cc.Stock);
				if (totalStock == 0) status = 2;

				return new ProductVM
				{
					Id = p.Id,
					Name = p.Name,
					DiscountAmount = p.DiscountAmount,
					Description = p.Description,
					ImageView = p.ImageView,
					FolderImage = p.ImageFolder,
					Brand = p.Brand.Name,
					Category = p.Category.Name,
					Rating = Math.Round(
						colorCapacityVMs
						 .SelectMany(cc => cc.Reviews)
						 .Select(r => r.Rating)
						 .DefaultIfEmpty(0)
						 .Average(), 1),
					ProductCCs = colorCapacityVMs,
					MinPrice = colorCapacityVMs.Any() ? colorCapacityVMs.Min(x => x.Price) : 0,
					MaxPrice = colorCapacityVMs.Any() ? colorCapacityVMs.Max(x => x.Price) : 0,
					TotalStock = totalStock,
					Status = status
				};
			}).ToList();

			return (productVMs, panigation);
		}

		public async Task<ProductVM> GetProduct(int id)
		{
			var product = await _productRepository.GetProduct(id);
			if (product != null)
			{
				var colorCapacityVMs = product.ColorCapacities.Select(cc => new ProductColorCapacityVM
				{
					Id = cc.Id,
					ColorName = cc.Color.NameColor,
					CapacityValue = cc.Capacity.Value,
					Image = cc.Image,
					Stock = cc.Stock,
					Price = cc.Price,
					Status = cc.Status,
					Reviews = cc.Reviews.Select(r => new ProductReviewVM
					{
						UserName = r.User.Name,
						Description = r.Description,
						Rating = r.Rating,
						CreatedAt = r.CreatedAt
					}).ToList()
				}).ToList();

				var status = colorCapacityVMs.Any() ? colorCapacityVMs.Max(cc => cc.Status) : 0;
				var totalStock = colorCapacityVMs.Sum(cc => cc.Stock);
				if (totalStock == 0) status = 2;

				var rating = Math.Round(
					colorCapacityVMs
						.SelectMany(cc => cc.Reviews)
						.Select(r => r.Rating)
						.DefaultIfEmpty(0)
						.Average(), 1);

				var productVM = new ProductVM
				{
					Id = product.Id,
					Name = product.Name,
					DiscountAmount = product.DiscountAmount,
					Description = product.Description,
					ImageView = product.ImageView,
					FolderImage = product.ImageFolder,
					Brand = product.Brand.Name,
					Category = product.Category.Name,
					Rating = rating,
					ProductCCs = colorCapacityVMs,
					MinPrice = colorCapacityVMs.Any() ? colorCapacityVMs.Min(x => x.Price) : 0,
					MaxPrice = colorCapacityVMs.Any() ? colorCapacityVMs.Max(x => x.Price) : 0,
					TotalStock = totalStock,
					Status = status
				};

				return productVM;
			}
			return null;
		}

		public async Task<List<string>> AddProduct(ProductDTO newProduct)
		{
			try
			{
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
					DiscountAmount = newProduct.DiscountAmount
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
						ProductId = productId
					};

					await _productCCRepository.AddColorCapacity(productCC);
				}
				return ListErrors;
			}
			catch
			{
				ListErrors.Add("Lỗi khi tạo sản phẩm");
				return ListErrors;
			}
		}
		public async Task<ServiceResponse<Product>> UpdateProduct(int id, ProductDTO productUpdate)
		{
			var response = new ServiceResponse<Product>();
			try
			{
				var product = await _productRepository.GetProduct(id);
				string newProductFolder = FileNameHelper.ToSlug(productUpdate.Name);
				string newProductUrl = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "admin", "imgs", "product", newProductFolder);
				string oldProductUrl = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "admin", "imgs", "product", product.ImageFolder);
				if (newProductFolder != product.ImageFolder)
				{
					Directory.Move(oldProductUrl, newProductUrl);
				}

				if (productUpdate.ImageView != null)
				{
					var imageViewName = "1" + Path.GetExtension(productUpdate.ImageView.FileName);
					try
					{
						var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), newProductUrl, product.ImageView);

						var newImagePath = Path.Combine(Directory.GetCurrentDirectory(), newProductUrl, imageViewName);

						File.Delete(oldImagePath);
						using (var stream = new FileStream(newImagePath, FileMode.Create))
						{
							await productUpdate.ImageView.CopyToAsync(stream);
						}
					}
					catch
					{
						response.IsSuccess = false;
						response.Errors.Add("Không thể cập nhật hình ảnh hiển thị sản phẩm!");
						return response;
					}

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
				else
				{
					var newProduct = new Product
					{
						Name = productUpdate.Name,
						DiscountAmount = productUpdate.DiscountAmount,
						Description = productUpdate.Description,
						ImageView = product.ImageView,
						BrandId = productUpdate.BrandId,
						CategoryId = productUpdate.CategoryId,
						ImageFolder = newProductFolder
					};

					response.Data = await _productRepository.UpdateProduct(id, newProduct);
				}

			}
			catch (Exception ex)
			{
				response.IsSuccess = false;
				response.Errors.Add("Lỗi: " + ex.Message);
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
				response.Errors.Add("Lỗi: " + ex.Message);
			}
			return response;
		}
	}
}
