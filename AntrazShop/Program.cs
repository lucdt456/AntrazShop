using AntrazShop.Data;
using AntrazShop.Services;
using Microsoft.EntityFrameworkCore;
using AntrazShop.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using AntrazShop.Authorization;
using Microsoft.AspNetCore.Authorization;
using AntrazShop.Interfaces.Repositories;
using AntrazShop.Interfaces.Services;
using System.Text.Json.Serialization;
using AntrazShop.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
	c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
	{
		Title = "AntrazShop API",
		Version = "v1"
	});

	// Thêm hỗ trợ JWT
	c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
	{
		Description = "Nhập token vào đây: Bearer {your JWT token}",
		Name = "Authorization",
		In = Microsoft.OpenApi.Models.ParameterLocation.Header,
		Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
		Scheme = "Bearer"
	});

	c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
	{
		{
			new Microsoft.OpenApi.Models.OpenApiSecurityScheme
			{
				Reference = new Microsoft.OpenApi.Models.OpenApiReference
				{
					Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
					Id = "Bearer"
				}
			},
			new string[] {}
		}
	});
});

builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));

builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowAll",
		policy => policy.AllowAnyOrigin()
						.AllowAnyMethod()
						.AllowAnyHeader());
});


builder.Services.AddDbContext<ShopDbContext>(options =>
{
	options.UseSqlServer(builder.Configuration.GetConnectionString("AntrazShop"));
});

builder.Services.AddAuthentication(options =>
{
	options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(options =>
{
	options.RequireHttpsMetadata = false;
	options.SaveToken = true;
	options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
	{
		ValidIssuer = builder.Configuration["JwtConfig:Issuer"],
		ValidAudience = builder.Configuration["JwtConfig:Audience"],
		IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtConfig:Key"]!)),
		ValidateIssuer = true,
		ValidateAudience = true,
		ValidateLifetime = true,
		ValidateIssuerSigningKey = true
	};
});

builder.Services.ConfigureHttpJsonOptions(options =>
{
	options.SerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
});

builder.Services.AddAuthorization(options =>
{
	options.AddPolicy("CanGetUserWorkers", policy => policy.Requirements.Add(new PermissionRequirement("GetUserWorkers")));
	options.AddPolicy("CanGetUserCustomer", policy => policy.Requirements.Add(new PermissionRequirement("GetUserCustomer")));
	options.AddPolicy("CanCreateWorkerAccount", policy => policy.Requirements.Add(new PermissionRequirement("CreateWorkerAccount")));
	options.AddPolicy("CanGetLoginHistories", policy => policy.Requirements.Add(new PermissionRequirement("GetLoginHistories")));
	options.AddPolicy("CanEditUserRoles", policy => policy.Requirements.Add(new PermissionRequirement("EditUserRoles")));
	options.AddPolicy("CanEditAuthUser", policy => policy.Requirements.Add(new PermissionRequirement("EditAuthUser")));
	options.AddPolicy("CanDeleteAccount", policy => policy.Requirements.Add(new PermissionRequirement("DeleteAccount")));
	options.AddPolicy("CanCreateBrand", policy => policy.Requirements.Add(new PermissionRequirement("CreateBrand")));
	options.AddPolicy("CanUpdateBrand", policy => policy.Requirements.Add(new PermissionRequirement("UpdateBrand")));
	options.AddPolicy("CanDeleteBrand", policy => policy.Requirements.Add(new PermissionRequirement("DeleteBrand")));
	options.AddPolicy("CanAddToCart", policy => policy.Requirements.Add(new PermissionRequirement("AddToCart")));
	options.AddPolicy("CanGetCart", policy => policy.Requirements.Add(new PermissionRequirement("CanGetCart")));
	options.AddPolicy("CanUpdateCartItem", policy => policy.Requirements.Add(new PermissionRequirement("UpdateCartItem")));
	options.AddPolicy("CanRemoveFromCart", policy => policy.Requirements.Add(new PermissionRequirement("RemoveFromCart")));
	options.AddPolicy("CanCheckOut", policy => policy.Requirements.Add(new PermissionRequirement("CheckOut")));
	options.AddPolicy("CanGetRoles", policy => policy.Requirements.Add(new PermissionRequirement("GetRoles")));
	options.AddPolicy("CanCreateCategory", policy => policy.Requirements.Add(new PermissionRequirement("CreateCategory")));
	options.AddPolicy("CanUpdateCategory", policy => policy.Requirements.Add(new PermissionRequirement("UpdateCategory")));
	options.AddPolicy("CanDeleteCategory", policy => policy.Requirements.Add(new PermissionRequirement("DeleteCategory")));
	options.AddPolicy("CanGetOrders", policy => policy.Requirements.Add(new PermissionRequirement("GetOrders")));
	options.AddPolicy("CanUpdateOrderStatus", policy => policy.Requirements.Add(new PermissionRequirement("UpdateOrderStatus")));
	options.AddPolicy("CanGetPermissions", policy => policy.Requirements.Add(new PermissionRequirement("GetPermissions")));
	options.AddPolicy("CanGetPermissionGroups", policy => policy.Requirements.Add(new PermissionRequirement("GetPermissionGroups")));
	options.AddPolicy("CanCreatePermissions", policy => policy.Requirements.Add(new PermissionRequirement("CreatePermissions")));
	options.AddPolicy("CanAddProduct", policy => policy.Requirements.Add(new PermissionRequirement("AddProduct")));
	options.AddPolicy("CanUpdateProduct", policy => policy.Requirements.Add(new PermissionRequirement("UpdateProduct")));
	options.AddPolicy("CanDeleteProduct", policy => policy.Requirements.Add(new PermissionRequirement("DeleteProduct")));
	options.AddPolicy("CanEditProductCC", policy => policy.Requirements.Add(new PermissionRequirement("EditProductCC")));
	options.AddPolicy("CanCreateProductCC", policy => policy.Requirements.Add(new PermissionRequirement("CreateProductCC")));
	options.AddPolicy("CanDeleteProductCC", policy => policy.Requirements.Add(new PermissionRequirement("DeleteProductCC")));
	options.AddPolicy("CanDeleteRole", policy => policy.Requirements.Add(new PermissionRequirement("DeleteRole")));
	options.AddPolicy("CanEditRolePermission", policy => policy.Requirements.Add(new PermissionRequirement("EditRolePermission")));
	options.AddPolicy("CanEditRole", policy => policy.Requirements.Add(new PermissionRequirement("EditRole")));
});


builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IAccountService, AccountService>();

builder.Services.AddScoped<IBrandRepository, BrandRepository>();
builder.Services.AddScoped<IBrandService, BrandService>();

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

builder.Services.AddScoped<IPermissionRepository, PermissionRepository>();
builder.Services.AddScoped<IPermissionService, PermissionService>();

builder.Services.AddScoped<IAuthorizationHandler, PermissionHandler>();

builder.Services.AddScoped<IAccountManagerRepository, AccountManagerRepository>();
builder.Services.AddScoped<IAccountManagerService, AccountManagerService>();

builder.Services.AddScoped<IProductColorCapacityRepository, ProductColorCapacityRepository>();
builder.Services.AddScoped<IProductCCService, ProductCCService>();

builder.Services.AddScoped <IRoleResponsive, RoleRepository>();
builder.Services.AddScoped<IRoleService, RoleService>();

builder.Services.AddScoped<IShopRepository, ShopRepository>();
builder.Services.AddScoped<IShopService, ShopService>();

builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<ICartService, CartService>();

builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IEmailRepository, EmailRepository>();

builder.Services.AddScoped<IOrderManagerService, OrderManagerService>();
builder.Services.AddScoped<IOrderManagerRepository, OrderManagerRepository>();

builder.Services.AddSingleton<IEmailSender, EmailSender>(); 

builder.Services.AddAutoMapper(typeof(Program));



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseCors("AllowAll");

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
	name: "Areas",
	pattern: "{area:exists}/{controller=dashboard}/{action=index}/{id?}");

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllers();
app.Run();
