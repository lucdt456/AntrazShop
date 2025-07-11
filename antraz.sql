
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Brands]    Script Date: 2025-07-04 20:30:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Brands](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](500) NOT NULL,
	[Logo] [nvarchar](255) NULL,
	[CreateAt] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Brands] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Capacities]    Script Date: 2025-07-04 20:30:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Capacities](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Value] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Capacities] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Carts]    Script Date: 2025-07-04 20:30:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Carts](
	[UserId] [int] NOT NULL,
	[ColorCapacityId] [int] NOT NULL,
	[Id] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
	[Total] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_Carts] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[ColorCapacityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 2025-07-04 20:30:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](500) NOT NULL,
	[Image] [nvarchar](255) NULL,
	[CreateAt] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ColorCapacities]    Script Date: 2025-07-04 20:30:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ColorCapacities](
	[Id] [int] IDENTITY(1000,1) NOT NULL,
	[Stock] [int] NOT NULL,
	[Price] [decimal](18, 2) NOT NULL,
	[ColorId] [int] NOT NULL,
	[CapacityId] [int] NOT NULL,
	[ProductId] [int] NOT NULL,
	[Image] [nvarchar](255) NOT NULL,
	[Status] [int] NOT NULL,
	[StatusImage] [bit] NOT NULL,
	[SoldAmount] [int] NOT NULL,
	[CreateAt] [datetime2](7) NULL,
 CONSTRAINT [PK_ColorCapacities] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Colors]    Script Date: 2025-07-04 20:30:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Colors](
	[Id] [int] IDENTITY(1000,1) NOT NULL,
	[NameColor] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Colors] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EmailCodes]    Script Date: 2025-07-04 20:30:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmailCodes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[Code] [nvarchar](max) NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[ExpiresAt] [datetime2](7) NOT NULL,
	[IsUsed] [bit] NOT NULL,
 CONSTRAINT [PK_EmailCodes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LoginHistories]    Script Date: 2025-07-04 20:30:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LoginHistories](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[StatusLogin] [nvarchar](50) NOT NULL,
	[IPAddress] [nvarchar](100) NOT NULL,
	[Time] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_LoginHistories] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderDetails]    Script Date: 2025-07-04 20:30:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderDetails](
	[OrderCode] [uniqueidentifier] NOT NULL,
	[ColorCapacityId] [int] NOT NULL,
	[Id] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
 CONSTRAINT [PK_OrderDetails] PRIMARY KEY CLUSTERED 
(
	[OrderCode] ASC,
	[ColorCapacityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Orders]    Script Date: 2025-07-04 20:30:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[OrderCode] [uniqueidentifier] NOT NULL,
	[UserId] [int] NOT NULL,
	[CreatedAt] [datetime2](0) NOT NULL,
	[Status] [nvarchar](max) NOT NULL,
	[Adress] [nvarchar](max) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[PhoneNumber] [nvarchar](max) NOT NULL,
	[Total] [decimal](18, 2) NOT NULL,
	[DeliveryDate] [datetime2](7) NULL,
 CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED 
(
	[OrderCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderStatusLogs]    Script Date: 2025-07-04 20:30:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderStatusLogs](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CreateAt] [datetime2](0) NOT NULL,
	[OrderCode] [uniqueidentifier] NOT NULL,
	[Status] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_OrderStatusLogs] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PermissionGroups]    Script Date: 2025-07-04 20:30:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PermissionGroups](
	[Id] [int] IDENTITY(1000,1) NOT NULL,
	[GroupName] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_PermissionGroups] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Permissions]    Script Date: 2025-07-04 20:30:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Permissions](
	[Id] [int] IDENTITY(10000,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](255) NOT NULL,
	[NameController] [nvarchar](100) NOT NULL,
	[PermissionGroupId] [int] NULL,
 CONSTRAINT [PK_Permissions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductImages]    Script Date: 2025-07-04 20:30:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductImages](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ImageUrl] [nvarchar](255) NOT NULL,
	[ProductId] [int] NOT NULL,
 CONSTRAINT [PK_ProductImages] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 2025-07-04 20:30:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[Id] [int] IDENTITY(10000,1) NOT NULL,
	[Name] [nvarchar](150) NOT NULL,
	[DiscountAmount] [decimal](18, 2) NOT NULL,
	[Description] [nvarchar](1000) NOT NULL,
	[ImageView] [nvarchar](255) NOT NULL,
	[ImageFolder] [nvarchar](255) NOT NULL,
	[BrandId] [int] NOT NULL,
	[CategoryId] [int] NOT NULL,
	[SaleId] [int] NULL,
	[CreateAt] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Reviews]    Script Date: 2025-07-04 20:30:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Reviews](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[ColorCapacityId] [int] NOT NULL,
	[Description] [nvarchar](1000) NOT NULL,
	[Rating] [float] NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Reviews] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RolePermissions]    Script Date: 2025-07-04 20:30:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RolePermissions](
	[RoleId] [int] NOT NULL,
	[PermissionId] [int] NOT NULL,
 CONSTRAINT [PK_RolePermissions] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC,
	[PermissionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 2025-07-04 20:30:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[Id] [int] IDENTITY(10000,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](255) NOT NULL,
	[CreateAt] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sales]    Script Date: 2025-07-04 20:30:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sales](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](500) NOT NULL,
	[StartDate] [datetime2](7) NOT NULL,
	[EndDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Sales] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserAuthInfos]    Script Date: 2025-07-04 20:30:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserAuthInfos](
	[UserId] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[FailedAttempts] [int] NOT NULL,
 CONSTRAINT [PK_UserAuthInfos] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserPermissions]    Script Date: 2025-07-04 20:30:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserPermissions](
	[UserId] [int] NOT NULL,
	[PermissionId] [int] NOT NULL,
 CONSTRAINT [PK_UserPermissions] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[PermissionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserRoles]    Script Date: 2025-07-04 20:30:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRoles](
	[UserId] [int] NOT NULL,
	[RoleId] [int] NOT NULL,
	[Id] [int] NOT NULL,
 CONSTRAINT [PK_UserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 2025-07-04 20:30:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(10000,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Gender] [nvarchar](10) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
	[PasswordHash] [nvarchar](255) NOT NULL,
	[PhoneNumber] [nvarchar](20) NOT NULL,
	[Address] [nvarchar](255) NULL,
	[Avatar] [nvarchar](255) NULL,
	[Birthday] [date] NOT NULL,
	[Hometown] [nvarchar](100) NULL,
	[workerAccount] [bit] NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WishLists]    Script Date: 2025-07-04 20:30:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WishLists](
	[UserId] [int] NOT NULL,
	[ColorCapacityId] [int] NOT NULL,
	[Id] [int] NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_WishLists] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[ColorCapacityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250701095624_Create-Database', N'8.0.13')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250701101254_RemoveUserPermissionId', N'8.0.13')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250701102803_AddTablePermissionGroup', N'8.0.13')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250703093440_EditTableOrder', N'8.0.13')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250703094924_EditTableOrder2', N'8.0.13')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250703100120_EditTableOrder3', N'8.0.13')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250703110512_EditTableOrder4', N'8.0.13')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250704082623_CreateOrderStatusLogTable', N'8.0.13')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250704102047_EditRolePermission', N'8.0.13')
GO
SET IDENTITY_INSERT [dbo].[Brands] ON 

INSERT [dbo].[Brands] ([Id], [Name], [Description], [Logo], [CreateAt]) VALUES (1, N'Apple', N'string', NULL, CAST(N'2025-07-02T11:17:39.1805570' AS DateTime2))
INSERT [dbo].[Brands] ([Id], [Name], [Description], [Logo], [CreateAt]) VALUES (2, N'Dell', N'Đã sửa', NULL, CAST(N'2025-07-02T11:17:47.8690183' AS DateTime2))
INSERT [dbo].[Brands] ([Id], [Name], [Description], [Logo], [CreateAt]) VALUES (3, N'Samsung', N'string', NULL, CAST(N'2025-07-02T11:17:55.7798379' AS DateTime2))
INSERT [dbo].[Brands] ([Id], [Name], [Description], [Logo], [CreateAt]) VALUES (4, N'Acer', N'ádkjlhasd klj', NULL, CAST(N'2025-07-02T14:00:33.4406046' AS DateTime2))
INSERT [dbo].[Brands] ([Id], [Name], [Description], [Logo], [CreateAt]) VALUES (5, N'Sony ', N'Test sửa', NULL, CAST(N'2025-07-02T14:01:35.0753652' AS DateTime2))
INSERT [dbo].[Brands] ([Id], [Name], [Description], [Logo], [CreateAt]) VALUES (6, N'HP', N'sadadfjkashdj kas
', NULL, CAST(N'2025-07-02T14:02:51.2212731' AS DateTime2))
INSERT [dbo].[Brands] ([Id], [Name], [Description], [Logo], [CreateAt]) VALUES (7, N'Huawei', N'sadadfjkashdj kas
a', NULL, CAST(N'2025-07-02T14:03:01.9425717' AS DateTime2))
INSERT [dbo].[Brands] ([Id], [Name], [Description], [Logo], [CreateAt]) VALUES (13, N'Asus', N'1as5641d65as1d
', NULL, CAST(N'2025-07-02T14:18:05.1541449' AS DateTime2))
INSERT [dbo].[Brands] ([Id], [Name], [Description], [Logo], [CreateAt]) VALUES (14, N'Xiaomi', N'sadadsdsad', NULL, CAST(N'2025-07-02T14:18:24.9011479' AS DateTime2))
INSERT [dbo].[Brands] ([Id], [Name], [Description], [Logo], [CreateAt]) VALUES (16, N'MSI', N'asdsadad', NULL, CAST(N'2025-07-04T20:18:34.9411231' AS DateTime2))
SET IDENTITY_INSERT [dbo].[Brands] OFF
GO
SET IDENTITY_INSERT [dbo].[Capacities] ON 

INSERT [dbo].[Capacities] ([Id], [Value]) VALUES (1, N'128GB')
INSERT [dbo].[Capacities] ([Id], [Value]) VALUES (2, N'64GB')
INSERT [dbo].[Capacities] ([Id], [Value]) VALUES (3, N'256GB')
INSERT [dbo].[Capacities] ([Id], [Value]) VALUES (4, N'512GB')
INSERT [dbo].[Capacities] ([Id], [Value]) VALUES (5, N'1TB')
INSERT [dbo].[Capacities] ([Id], [Value]) VALUES (6, N'8GB RAM 256GB SSD')
INSERT [dbo].[Capacities] ([Id], [Value]) VALUES (7, N'320g')
INSERT [dbo].[Capacities] ([Id], [Value]) VALUES (8, N'12GB GDDR6')
SET IDENTITY_INSERT [dbo].[Capacities] OFF
GO
INSERT [dbo].[Carts] ([UserId], [ColorCapacityId], [Id], [Quantity], [Total]) VALUES (10000, 1008, 0, 1, CAST(22500000.00 AS Decimal(18, 2)))
INSERT [dbo].[Carts] ([UserId], [ColorCapacityId], [Id], [Quantity], [Total]) VALUES (10000, 1009, 0, 2, CAST(73580000.00 AS Decimal(18, 2)))
INSERT [dbo].[Carts] ([UserId], [ColorCapacityId], [Id], [Quantity], [Total]) VALUES (10004, 1005, 0, 1, CAST(6200000.00 AS Decimal(18, 2)))
GO
SET IDENTITY_INSERT [dbo].[Categories] ON 

INSERT [dbo].[Categories] ([Id], [Name], [Description], [Image], [CreateAt]) VALUES (1, N'Điện thoại', N'string', NULL, CAST(N'2025-07-02T11:44:33.0611109' AS DateTime2))
INSERT [dbo].[Categories] ([Id], [Name], [Description], [Image], [CreateAt]) VALUES (2, N'Laptop', N'string', NULL, CAST(N'2025-07-02T11:44:37.5455442' AS DateTime2))
INSERT [dbo].[Categories] ([Id], [Name], [Description], [Image], [CreateAt]) VALUES (3, N'PC', N'ạidhjaskd
', NULL, CAST(N'2025-07-02T14:25:51.6057177' AS DateTime2))
INSERT [dbo].[Categories] ([Id], [Name], [Description], [Image], [CreateAt]) VALUES (4, N'Tai nghe', N'ádknbaskd ád á', NULL, CAST(N'2025-07-02T14:26:05.2727411' AS DateTime2))
INSERT [dbo].[Categories] ([Id], [Name], [Description], [Image], [CreateAt]) VALUES (5, N'Card đồ hoạ', N'ấhdkjlasd ', NULL, CAST(N'2025-07-03T18:13:42.9218872' AS DateTime2))
SET IDENTITY_INSERT [dbo].[Categories] OFF
GO
SET IDENTITY_INSERT [dbo].[ColorCapacities] ON 

INSERT [dbo].[ColorCapacities] ([Id], [Stock], [Price], [ColorId], [CapacityId], [ProductId], [Image], [Status], [StatusImage], [SoldAmount], [CreateAt]) VALUES (1000, 5, CAST(2500000.00 AS Decimal(18, 2)), 1000, 1, 10000, N'trang_128gb.png', 1, 0, 0, CAST(N'2025-07-02T15:00:28.9837509' AS DateTime2))
INSERT [dbo].[ColorCapacities] ([Id], [Stock], [Price], [ColorId], [CapacityId], [ProductId], [Image], [Status], [StatusImage], [SoldAmount], [CreateAt]) VALUES (1001, 4, CAST(2250000.00 AS Decimal(18, 2)), 1001, 2, 10000, N'den_64gb.png', 1, 0, 0, CAST(N'2025-07-02T15:00:29.0210867' AS DateTime2))
INSERT [dbo].[ColorCapacities] ([Id], [Stock], [Price], [ColorId], [CapacityId], [ProductId], [Image], [Status], [StatusImage], [SoldAmount], [CreateAt]) VALUES (1002, 5, CAST(4200000.00 AS Decimal(18, 2)), 1000, 3, 10001, N'trang_256gb.png', 1, 0, 0, CAST(N'2025-07-02T15:03:06.3713021' AS DateTime2))
INSERT [dbo].[ColorCapacities] ([Id], [Stock], [Price], [ColorId], [CapacityId], [ProductId], [Image], [Status], [StatusImage], [SoldAmount], [CreateAt]) VALUES (1003, 5, CAST(2450000.00 AS Decimal(18, 2)), 1002, 1, 10000, N'do20250702150332_128gb.png', 1, 0, 0, CAST(N'2025-07-02T15:03:32.8459396' AS DateTime2))
INSERT [dbo].[ColorCapacities] ([Id], [Stock], [Price], [ColorId], [CapacityId], [ProductId], [Image], [Status], [StatusImage], [SoldAmount], [CreateAt]) VALUES (1004, 4, CAST(5200000.00 AS Decimal(18, 2)), 1003, 3, 10002, N'tim_256gb.png', 1, 0, 0, CAST(N'2025-07-02T15:04:24.4222608' AS DateTime2))
INSERT [dbo].[ColorCapacities] ([Id], [Stock], [Price], [ColorId], [CapacityId], [ProductId], [Image], [Status], [StatusImage], [SoldAmount], [CreateAt]) VALUES (1005, 5, CAST(6200000.00 AS Decimal(18, 2)), 1004, 3, 10003, N'xanh_256gb.png', 1, 0, 0, CAST(N'2025-07-02T15:05:08.9919016' AS DateTime2))
INSERT [dbo].[ColorCapacities] ([Id], [Stock], [Price], [ColorId], [CapacityId], [ProductId], [Image], [Status], [StatusImage], [SoldAmount], [CreateAt]) VALUES (1006, 6, CAST(9800000.00 AS Decimal(18, 2)), 1005, 4, 10004, N'hong_512gb.png', 1, 0, 0, CAST(N'2025-07-02T15:05:49.6802600' AS DateTime2))
INSERT [dbo].[ColorCapacities] ([Id], [Stock], [Price], [ColorId], [CapacityId], [ProductId], [Image], [Status], [StatusImage], [SoldAmount], [CreateAt]) VALUES (1007, 12, CAST(18500000.00 AS Decimal(18, 2)), 1000, 4, 10005, N'trang_512gb.png', 1, 0, 0, CAST(N'2025-07-02T15:06:48.9752899' AS DateTime2))
INSERT [dbo].[ColorCapacities] ([Id], [Stock], [Price], [ColorId], [CapacityId], [ProductId], [Image], [Status], [StatusImage], [SoldAmount], [CreateAt]) VALUES (1008, 4, CAST(22500000.00 AS Decimal(18, 2)), 1006, 5, 10006, N'xanh_ngoc_1tb.png', 1, 0, 0, CAST(N'2025-07-02T15:07:53.4521814' AS DateTime2))
INSERT [dbo].[ColorCapacities] ([Id], [Stock], [Price], [ColorId], [CapacityId], [ProductId], [Image], [Status], [StatusImage], [SoldAmount], [CreateAt]) VALUES (1009, 5, CAST(36790000.00 AS Decimal(18, 2)), 1004, 5, 10007, N'xanh_1tb.png', 1, 0, 0, CAST(N'2025-07-02T15:08:36.9725107' AS DateTime2))
INSERT [dbo].[ColorCapacities] ([Id], [Stock], [Price], [ColorId], [CapacityId], [ProductId], [Image], [Status], [StatusImage], [SoldAmount], [CreateAt]) VALUES (1010, 2, CAST(13290000.00 AS Decimal(18, 2)), 1000, 6, 10008, N'trang_8gb_ram_256gb_ssd.png', 1, 0, 0, CAST(N'2025-07-04T20:12:36.5870131' AS DateTime2))
INSERT [dbo].[ColorCapacities] ([Id], [Stock], [Price], [ColorId], [CapacityId], [ProductId], [Image], [Status], [StatusImage], [SoldAmount], [CreateAt]) VALUES (1011, 10, CAST(1450000.00 AS Decimal(18, 2)), 1002, 7, 10009, N'do_320g.png', 1, 0, 0, CAST(N'2025-07-04T20:15:06.4512349' AS DateTime2))
INSERT [dbo].[ColorCapacities] ([Id], [Stock], [Price], [ColorId], [CapacityId], [ProductId], [Image], [Status], [StatusImage], [SoldAmount], [CreateAt]) VALUES (1012, 5, CAST(7500000.00 AS Decimal(18, 2)), 1001, 8, 10010, N'den_12gb_gddr6.png', 1, 0, 0, CAST(N'2025-07-04T20:19:53.6003939' AS DateTime2))
SET IDENTITY_INSERT [dbo].[ColorCapacities] OFF
GO
SET IDENTITY_INSERT [dbo].[Colors] ON 

INSERT [dbo].[Colors] ([Id], [NameColor]) VALUES (1000, N'Trắng')
INSERT [dbo].[Colors] ([Id], [NameColor]) VALUES (1001, N'Đen')
INSERT [dbo].[Colors] ([Id], [NameColor]) VALUES (1002, N'Đỏ')
INSERT [dbo].[Colors] ([Id], [NameColor]) VALUES (1003, N'Tím')
INSERT [dbo].[Colors] ([Id], [NameColor]) VALUES (1004, N'Xanh')
INSERT [dbo].[Colors] ([Id], [NameColor]) VALUES (1005, N'Hồng')
INSERT [dbo].[Colors] ([Id], [NameColor]) VALUES (1006, N'Xanh ngọc')
SET IDENTITY_INSERT [dbo].[Colors] OFF
GO
SET IDENTITY_INSERT [dbo].[EmailCodes] ON 

INSERT [dbo].[EmailCodes] ([Id], [UserId], [Code], [CreatedAt], [ExpiresAt], [IsUsed]) VALUES (1, 10002, N'436960', CAST(N'2025-07-01T22:59:15.8896933' AS DateTime2), CAST(N'2025-07-01T23:14:15.8897142' AS DateTime2), 0)
INSERT [dbo].[EmailCodes] ([Id], [UserId], [Code], [CreatedAt], [ExpiresAt], [IsUsed]) VALUES (2, 10002, N'878651', CAST(N'2025-07-01T23:18:38.9395056' AS DateTime2), CAST(N'2025-07-01T23:33:38.9395323' AS DateTime2), 0)
INSERT [dbo].[EmailCodes] ([Id], [UserId], [Code], [CreatedAt], [ExpiresAt], [IsUsed]) VALUES (3, 10002, N'213730', CAST(N'2025-07-01T23:19:13.0897614' AS DateTime2), CAST(N'2025-07-01T23:34:13.0897617' AS DateTime2), 0)
INSERT [dbo].[EmailCodes] ([Id], [UserId], [Code], [CreatedAt], [ExpiresAt], [IsUsed]) VALUES (4, 10002, N'626973', CAST(N'2025-07-01T23:19:16.5060396' AS DateTime2), CAST(N'2025-07-01T23:34:16.5060397' AS DateTime2), 0)
INSERT [dbo].[EmailCodes] ([Id], [UserId], [Code], [CreatedAt], [ExpiresAt], [IsUsed]) VALUES (5, 10002, N'799601', CAST(N'2025-07-01T23:19:40.1743366' AS DateTime2), CAST(N'2025-07-01T23:34:40.1743584' AS DateTime2), 0)
INSERT [dbo].[EmailCodes] ([Id], [UserId], [Code], [CreatedAt], [ExpiresAt], [IsUsed]) VALUES (6, 10002, N'118324', CAST(N'2025-07-01T23:22:09.3391433' AS DateTime2), CAST(N'2025-07-01T23:37:09.3391670' AS DateTime2), 0)
INSERT [dbo].[EmailCodes] ([Id], [UserId], [Code], [CreatedAt], [ExpiresAt], [IsUsed]) VALUES (7, 10002, N'575350', CAST(N'2025-07-02T00:02:35.2724404' AS DateTime2), CAST(N'2025-07-02T00:17:35.2724601' AS DateTime2), 0)
INSERT [dbo].[EmailCodes] ([Id], [UserId], [Code], [CreatedAt], [ExpiresAt], [IsUsed]) VALUES (8, 10002, N'820845', CAST(N'2025-07-02T00:03:05.6716139' AS DateTime2), CAST(N'2025-07-02T00:18:05.6716348' AS DateTime2), 0)
INSERT [dbo].[EmailCodes] ([Id], [UserId], [Code], [CreatedAt], [ExpiresAt], [IsUsed]) VALUES (9, 10002, N'889882', CAST(N'2025-07-02T00:07:51.4138902' AS DateTime2), CAST(N'2025-07-02T00:22:51.4139111' AS DateTime2), 0)
INSERT [dbo].[EmailCodes] ([Id], [UserId], [Code], [CreatedAt], [ExpiresAt], [IsUsed]) VALUES (10, 10002, N'671228', CAST(N'2025-07-02T00:19:46.9593348' AS DateTime2), CAST(N'2025-07-02T00:34:46.9593554' AS DateTime2), 0)
INSERT [dbo].[EmailCodes] ([Id], [UserId], [Code], [CreatedAt], [ExpiresAt], [IsUsed]) VALUES (11, 10002, N'968759', CAST(N'2025-07-02T09:37:32.8020656' AS DateTime2), CAST(N'2025-07-02T09:52:32.8021074' AS DateTime2), 0)
INSERT [dbo].[EmailCodes] ([Id], [UserId], [Code], [CreatedAt], [ExpiresAt], [IsUsed]) VALUES (12, 10003, N'896905', CAST(N'2025-07-02T10:27:42.9381549' AS DateTime2), CAST(N'2025-07-02T10:42:42.9383073' AS DateTime2), 0)
INSERT [dbo].[EmailCodes] ([Id], [UserId], [Code], [CreatedAt], [ExpiresAt], [IsUsed]) VALUES (13, 10003, N'325921', CAST(N'2025-07-02T10:46:25.5678393' AS DateTime2), CAST(N'2025-07-02T11:01:25.5678583' AS DateTime2), 0)
INSERT [dbo].[EmailCodes] ([Id], [UserId], [Code], [CreatedAt], [ExpiresAt], [IsUsed]) VALUES (14, 10003, N'177366', CAST(N'2025-07-02T11:05:23.1853757' AS DateTime2), CAST(N'2025-07-02T11:20:23.1853963' AS DateTime2), 0)
INSERT [dbo].[EmailCodes] ([Id], [UserId], [Code], [CreatedAt], [ExpiresAt], [IsUsed]) VALUES (15, 10003, N'787100', CAST(N'2025-07-02T11:13:52.8128804' AS DateTime2), CAST(N'2025-07-02T11:28:52.8128807' AS DateTime2), 0)
INSERT [dbo].[EmailCodes] ([Id], [UserId], [Code], [CreatedAt], [ExpiresAt], [IsUsed]) VALUES (16, 10003, N'176632', CAST(N'2025-07-02T11:14:55.6002668' AS DateTime2), CAST(N'2025-07-02T11:29:55.6002671' AS DateTime2), 0)
INSERT [dbo].[EmailCodes] ([Id], [UserId], [Code], [CreatedAt], [ExpiresAt], [IsUsed]) VALUES (17, 10003, N'956438', CAST(N'2025-07-02T11:45:54.8358872' AS DateTime2), CAST(N'2025-07-02T12:00:54.8360818' AS DateTime2), 0)
INSERT [dbo].[EmailCodes] ([Id], [UserId], [Code], [CreatedAt], [ExpiresAt], [IsUsed]) VALUES (18, 10004, N'867267', CAST(N'2025-07-02T15:12:33.3631288' AS DateTime2), CAST(N'2025-07-02T15:27:33.3633673' AS DateTime2), 0)
INSERT [dbo].[EmailCodes] ([Id], [UserId], [Code], [CreatedAt], [ExpiresAt], [IsUsed]) VALUES (19, 10010, N'785739', CAST(N'2025-07-03T15:38:29.5196032' AS DateTime2), CAST(N'2025-07-03T15:53:29.5196609' AS DateTime2), 0)
INSERT [dbo].[EmailCodes] ([Id], [UserId], [Code], [CreatedAt], [ExpiresAt], [IsUsed]) VALUES (20, 10010, N'864795', CAST(N'2025-07-03T15:38:31.0024000' AS DateTime2), CAST(N'2025-07-03T15:53:31.0024002' AS DateTime2), 0)
INSERT [dbo].[EmailCodes] ([Id], [UserId], [Code], [CreatedAt], [ExpiresAt], [IsUsed]) VALUES (21, 10008, N'214428', CAST(N'2025-07-03T22:49:54.5028408' AS DateTime2), CAST(N'2025-07-03T23:04:54.5030754' AS DateTime2), 0)
SET IDENTITY_INSERT [dbo].[EmailCodes] OFF
GO
SET IDENTITY_INSERT [dbo].[LoginHistories] ON 

INSERT [dbo].[LoginHistories] ([Id], [UserId], [StatusLogin], [IPAddress], [Time]) VALUES (1, 10000, N'Thành công, Đăng nhập thành công!', N'192.168.28.232', CAST(N'2025-07-01T17:09:40.6143099' AS DateTime2))
INSERT [dbo].[LoginHistories] ([Id], [UserId], [StatusLogin], [IPAddress], [Time]) VALUES (2, 10000, N'Thành công, Đăng nhập thành công!', N'192.168.28.232', CAST(N'2025-07-01T17:13:34.9384542' AS DateTime2))
INSERT [dbo].[LoginHistories] ([Id], [UserId], [StatusLogin], [IPAddress], [Time]) VALUES (4, 10000, N'Thành công, Đăng nhập thành công!', N'192.168.28.232', CAST(N'2025-07-01T17:41:32.6233788' AS DateTime2))
INSERT [dbo].[LoginHistories] ([Id], [UserId], [StatusLogin], [IPAddress], [Time]) VALUES (5, 10002, N'Thành công, Đăng nhập thành công!', N'192.168.28.232', CAST(N'2025-07-01T17:42:30.3105421' AS DateTime2))
INSERT [dbo].[LoginHistories] ([Id], [UserId], [StatusLogin], [IPAddress], [Time]) VALUES (6, 10002, N'Thành công, Đăng nhập thành công!', N'192.168.0.193', CAST(N'2025-07-01T22:11:00.8959449' AS DateTime2))
INSERT [dbo].[LoginHistories] ([Id], [UserId], [StatusLogin], [IPAddress], [Time]) VALUES (7, 10002, N'Thành công, Đăng nhập thành công!', N'192.168.28.66', CAST(N'2025-07-02T09:34:50.7479060' AS DateTime2))
INSERT [dbo].[LoginHistories] ([Id], [UserId], [StatusLogin], [IPAddress], [Time]) VALUES (8, 10000, N'Thành công, Đăng nhập thành công!', N'192.168.28.66', CAST(N'2025-07-02T09:35:07.3215399' AS DateTime2))
INSERT [dbo].[LoginHistories] ([Id], [UserId], [StatusLogin], [IPAddress], [Time]) VALUES (9, 10000, N'Thành công, Đăng nhập thành công!', N'192.168.28.66', CAST(N'2025-07-02T10:25:23.7803368' AS DateTime2))
INSERT [dbo].[LoginHistories] ([Id], [UserId], [StatusLogin], [IPAddress], [Time]) VALUES (10, 10003, N'Thất bại, Sai mật khẩu!', N'192.168.28.66', CAST(N'2025-07-02T10:40:11.8002449' AS DateTime2))
INSERT [dbo].[LoginHistories] ([Id], [UserId], [StatusLogin], [IPAddress], [Time]) VALUES (11, 10004, N'Thành công, Đăng nhập thành công!', N'192.168.28.66', CAST(N'2025-07-02T10:51:16.6154052' AS DateTime2))
INSERT [dbo].[LoginHistories] ([Id], [UserId], [StatusLogin], [IPAddress], [Time]) VALUES (12, 10003, N'Thành công, Đăng nhập thành công!', N'192.168.28.66', CAST(N'2025-07-02T11:15:22.6651688' AS DateTime2))
INSERT [dbo].[LoginHistories] ([Id], [UserId], [StatusLogin], [IPAddress], [Time]) VALUES (13, 10003, N'Thất bại, Sai mật khẩu!', N'192.168.28.66', CAST(N'2025-07-02T11:48:27.1798495' AS DateTime2))
INSERT [dbo].[LoginHistories] ([Id], [UserId], [StatusLogin], [IPAddress], [Time]) VALUES (14, 10003, N'Thành công, Đăng nhập thành công!', N'192.168.28.66', CAST(N'2025-07-02T11:48:31.4466321' AS DateTime2))
INSERT [dbo].[LoginHistories] ([Id], [UserId], [StatusLogin], [IPAddress], [Time]) VALUES (15, 10003, N'Thành công, Đăng nhập thành công!', N'192.168.28.66', CAST(N'2025-07-02T12:25:59.8623684' AS DateTime2))
INSERT [dbo].[LoginHistories] ([Id], [UserId], [StatusLogin], [IPAddress], [Time]) VALUES (16, 10003, N'Thành công, Đăng nhập thành công!', N'192.168.28.66', CAST(N'2025-07-02T13:18:18.8242023' AS DateTime2))
INSERT [dbo].[LoginHistories] ([Id], [UserId], [StatusLogin], [IPAddress], [Time]) VALUES (17, 10003, N'Thành công, Đăng nhập thành công!', N'192.168.28.66', CAST(N'2025-07-02T13:38:07.3605769' AS DateTime2))
INSERT [dbo].[LoginHistories] ([Id], [UserId], [StatusLogin], [IPAddress], [Time]) VALUES (18, 10003, N'Thành công, Đăng nhập thành công!', N'192.168.28.66', CAST(N'2025-07-02T13:38:52.1071144' AS DateTime2))
INSERT [dbo].[LoginHistories] ([Id], [UserId], [StatusLogin], [IPAddress], [Time]) VALUES (19, 10003, N'Thành công, Đăng nhập thành công!', N'192.168.28.66', CAST(N'2025-07-02T13:39:09.0679543' AS DateTime2))
INSERT [dbo].[LoginHistories] ([Id], [UserId], [StatusLogin], [IPAddress], [Time]) VALUES (20, 10003, N'Thành công, Đăng nhập thành công!', N'192.168.28.66', CAST(N'2025-07-02T14:09:49.4247320' AS DateTime2))
INSERT [dbo].[LoginHistories] ([Id], [UserId], [StatusLogin], [IPAddress], [Time]) VALUES (21, 10003, N'Thành công, Đăng nhập thành công!', N'192.168.28.66', CAST(N'2025-07-02T14:39:58.8976555' AS DateTime2))
INSERT [dbo].[LoginHistories] ([Id], [UserId], [StatusLogin], [IPAddress], [Time]) VALUES (22, 10003, N'Thất bại, Sai mật khẩu!', N'192.168.28.66', CAST(N'2025-07-02T15:13:02.0910789' AS DateTime2))
INSERT [dbo].[LoginHistories] ([Id], [UserId], [StatusLogin], [IPAddress], [Time]) VALUES (23, 10003, N'Thất bại, Sai mật khẩu!', N'192.168.28.66', CAST(N'2025-07-02T15:13:09.4999446' AS DateTime2))
INSERT [dbo].[LoginHistories] ([Id], [UserId], [StatusLogin], [IPAddress], [Time]) VALUES (24, 10004, N'Thành công, Đăng nhập thành công!', N'192.168.28.66', CAST(N'2025-07-02T15:13:15.4664501' AS DateTime2))
INSERT [dbo].[LoginHistories] ([Id], [UserId], [StatusLogin], [IPAddress], [Time]) VALUES (25, 10004, N'Thành công, Đăng nhập thành công!', N'192.168.28.66', CAST(N'2025-07-02T15:53:24.6439891' AS DateTime2))
INSERT [dbo].[LoginHistories] ([Id], [UserId], [StatusLogin], [IPAddress], [Time]) VALUES (26, 10008, N'Thành công, Đăng nhập thành công!', N'192.168.28.66', CAST(N'2025-07-02T15:55:26.6235552' AS DateTime2))
INSERT [dbo].[LoginHistories] ([Id], [UserId], [StatusLogin], [IPAddress], [Time]) VALUES (27, 10000, N'Thành công, Đăng nhập thành công!', N'192.168.28.66', CAST(N'2025-07-02T15:55:38.2335538' AS DateTime2))
INSERT [dbo].[LoginHistories] ([Id], [UserId], [StatusLogin], [IPAddress], [Time]) VALUES (28, 10010, N'Thành công, Đăng nhập thành công!', N'192.168.28.66', CAST(N'2025-07-02T16:00:14.4037649' AS DateTime2))
INSERT [dbo].[LoginHistories] ([Id], [UserId], [StatusLogin], [IPAddress], [Time]) VALUES (29, 10010, N'Thành công, Đăng nhập thành công!', N'192.168.28.66', CAST(N'2025-07-02T16:12:33.7743676' AS DateTime2))
INSERT [dbo].[LoginHistories] ([Id], [UserId], [StatusLogin], [IPAddress], [Time]) VALUES (30, 10010, N'Thành công, Đăng nhập thành công!', N'192.168.28.66', CAST(N'2025-07-02T16:35:35.5416367' AS DateTime2))
INSERT [dbo].[LoginHistories] ([Id], [UserId], [StatusLogin], [IPAddress], [Time]) VALUES (31, 10010, N'Thành công, Đăng nhập thành công!', N'192.168.28.66', CAST(N'2025-07-02T17:14:48.2121369' AS DateTime2))
INSERT [dbo].[LoginHistories] ([Id], [UserId], [StatusLogin], [IPAddress], [Time]) VALUES (32, 10010, N'Thành công, Đăng nhập thành công!', N'192.168.28.66', CAST(N'2025-07-02T18:05:24.1472597' AS DateTime2))
INSERT [dbo].[LoginHistories] ([Id], [UserId], [StatusLogin], [IPAddress], [Time]) VALUES (33, 10010, N'Thành công, Đăng nhập thành công!', N'192.168.28.66', CAST(N'2025-07-02T18:15:30.1106280' AS DateTime2))
INSERT [dbo].[LoginHistories] ([Id], [UserId], [StatusLogin], [IPAddress], [Time]) VALUES (34, 10010, N'Thành công, Đăng nhập thành công!', N'192.168.0.195', CAST(N'2025-07-03T15:29:33.8434441' AS DateTime2))
INSERT [dbo].[LoginHistories] ([Id], [UserId], [StatusLogin], [IPAddress], [Time]) VALUES (35, 10010, N'Thành công, Đăng nhập thành công!', N'192.168.0.195', CAST(N'2025-07-03T15:38:37.6525068' AS DateTime2))
INSERT [dbo].[LoginHistories] ([Id], [UserId], [StatusLogin], [IPAddress], [Time]) VALUES (36, 10010, N'Thành công, Đăng nhập thành công!', N'192.168.0.195', CAST(N'2025-07-03T16:13:37.0012242' AS DateTime2))
INSERT [dbo].[LoginHistories] ([Id], [UserId], [StatusLogin], [IPAddress], [Time]) VALUES (37, 10010, N'Thành công, Đăng nhập thành công!', N'192.168.0.195', CAST(N'2025-07-03T16:46:50.4059169' AS DateTime2))
INSERT [dbo].[LoginHistories] ([Id], [UserId], [StatusLogin], [IPAddress], [Time]) VALUES (38, 10010, N'Thành công, Đăng nhập thành công!', N'192.168.0.195', CAST(N'2025-07-03T17:39:52.9792748' AS DateTime2))
INSERT [dbo].[LoginHistories] ([Id], [UserId], [StatusLogin], [IPAddress], [Time]) VALUES (39, 10010, N'Thành công, Đăng nhập thành công!', N'192.168.0.195', CAST(N'2025-07-03T18:03:17.7442592' AS DateTime2))
INSERT [dbo].[LoginHistories] ([Id], [UserId], [StatusLogin], [IPAddress], [Time]) VALUES (40, 10000, N'Thành công, Đăng nhập thành công!', N'192.168.0.195', CAST(N'2025-07-03T18:06:11.2590100' AS DateTime2))
INSERT [dbo].[LoginHistories] ([Id], [UserId], [StatusLogin], [IPAddress], [Time]) VALUES (41, 10000, N'Thành công, Đăng nhập thành công!', N'192.168.0.195', CAST(N'2025-07-03T18:13:16.8530067' AS DateTime2))
INSERT [dbo].[LoginHistories] ([Id], [UserId], [StatusLogin], [IPAddress], [Time]) VALUES (42, 10000, N'Thành công, Đăng nhập thành công!', N'192.168.0.195', CAST(N'2025-07-03T20:05:40.9105004' AS DateTime2))
INSERT [dbo].[LoginHistories] ([Id], [UserId], [StatusLogin], [IPAddress], [Time]) VALUES (43, 10000, N'Thành công, Đăng nhập thành công!', N'192.168.0.195', CAST(N'2025-07-03T20:54:58.0399372' AS DateTime2))
INSERT [dbo].[LoginHistories] ([Id], [UserId], [StatusLogin], [IPAddress], [Time]) VALUES (44, 10010, N'Thành công, Đăng nhập thành công!', N'192.168.0.195', CAST(N'2025-07-03T20:55:07.0114303' AS DateTime2))
INSERT [dbo].[LoginHistories] ([Id], [UserId], [StatusLogin], [IPAddress], [Time]) VALUES (45, 10010, N'Thành công, Đăng nhập thành công!', N'192.168.0.195', CAST(N'2025-07-03T21:06:29.5558078' AS DateTime2))
INSERT [dbo].[LoginHistories] ([Id], [UserId], [StatusLogin], [IPAddress], [Time]) VALUES (46, 10000, N'Thành công, Đăng nhập thành công!', N'192.168.0.195', CAST(N'2025-07-03T21:06:36.9576246' AS DateTime2))
INSERT [dbo].[LoginHistories] ([Id], [UserId], [StatusLogin], [IPAddress], [Time]) VALUES (47, 10008, N'Thành công, Đăng nhập thành công!', N'192.168.0.195', CAST(N'2025-07-03T21:07:28.7477364' AS DateTime2))
INSERT [dbo].[LoginHistories] ([Id], [UserId], [StatusLogin], [IPAddress], [Time]) VALUES (48, 10000, N'Thành công, Đăng nhập thành công!', N'192.168.0.195', CAST(N'2025-07-03T21:08:39.6681508' AS DateTime2))
INSERT [dbo].[LoginHistories] ([Id], [UserId], [StatusLogin], [IPAddress], [Time]) VALUES (49, 10000, N'Thành công, Đăng nhập thành công!', N'192.168.0.195', CAST(N'2025-07-03T21:38:56.0404218' AS DateTime2))
INSERT [dbo].[LoginHistories] ([Id], [UserId], [StatusLogin], [IPAddress], [Time]) VALUES (50, 10000, N'Thành công, Đăng nhập thành công!', N'192.168.0.195', CAST(N'2025-07-03T22:09:01.4113670' AS DateTime2))
INSERT [dbo].[LoginHistories] ([Id], [UserId], [StatusLogin], [IPAddress], [Time]) VALUES (51, 10000, N'Thành công, Đăng nhập thành công!', N'192.168.0.195', CAST(N'2025-07-03T22:40:24.5649938' AS DateTime2))
INSERT [dbo].[LoginHistories] ([Id], [UserId], [StatusLogin], [IPAddress], [Time]) VALUES (52, 10008, N'Thành công, Đăng nhập thành công!', N'192.168.0.195', CAST(N'2025-07-03T22:51:59.2397643' AS DateTime2))
INSERT [dbo].[LoginHistories] ([Id], [UserId], [StatusLogin], [IPAddress], [Time]) VALUES (53, 10000, N'Thành công, Đăng nhập thành công!', N'192.168.0.195', CAST(N'2025-07-03T23:01:55.7648735' AS DateTime2))
INSERT [dbo].[LoginHistories] ([Id], [UserId], [StatusLogin], [IPAddress], [Time]) VALUES (54, 10000, N'Thành công, Đăng nhập thành công!', N'192.168.0.195', CAST(N'2025-07-03T23:32:00.0920035' AS DateTime2))
INSERT [dbo].[LoginHistories] ([Id], [UserId], [StatusLogin], [IPAddress], [Time]) VALUES (55, 10000, N'Thành công, Đăng nhập thành công!', N'192.168.0.195', CAST(N'2025-07-04T00:02:46.3498319' AS DateTime2))
INSERT [dbo].[LoginHistories] ([Id], [UserId], [StatusLogin], [IPAddress], [Time]) VALUES (56, 10000, N'Thành công, Đăng nhập thành công!', N'192.168.0.195', CAST(N'2025-07-04T00:34:08.8876092' AS DateTime2))
INSERT [dbo].[LoginHistories] ([Id], [UserId], [StatusLogin], [IPAddress], [Time]) VALUES (57, 10000, N'Thành công, Đăng nhập thành công!', N'192.168.0.195', CAST(N'2025-07-04T01:10:42.3081443' AS DateTime2))
INSERT [dbo].[LoginHistories] ([Id], [UserId], [StatusLogin], [IPAddress], [Time]) VALUES (58, 10008, N'Thành công, Đăng nhập thành công!', N'192.168.0.195', CAST(N'2025-07-04T01:10:58.8575978' AS DateTime2))
INSERT [dbo].[LoginHistories] ([Id], [UserId], [StatusLogin], [IPAddress], [Time]) VALUES (59, 10000, N'Thành công, Đăng nhập thành công!', N'192.168.0.195', CAST(N'2025-07-04T01:14:41.5165246' AS DateTime2))
INSERT [dbo].[LoginHistories] ([Id], [UserId], [StatusLogin], [IPAddress], [Time]) VALUES (60, 10008, N'Thành công, Đăng nhập thành công!', N'192.168.0.195', CAST(N'2025-07-04T01:27:13.9723565' AS DateTime2))
INSERT [dbo].[LoginHistories] ([Id], [UserId], [StatusLogin], [IPAddress], [Time]) VALUES (61, 10000, N'Thành công, Đăng nhập thành công!', N'192.168.0.195', CAST(N'2025-07-04T01:29:12.2501525' AS DateTime2))
INSERT [dbo].[LoginHistories] ([Id], [UserId], [StatusLogin], [IPAddress], [Time]) VALUES (62, 10000, N'Thành công, Đăng nhập thành công!', N'192.168.0.195', CAST(N'2025-07-04T02:02:47.8705957' AS DateTime2))
INSERT [dbo].[LoginHistories] ([Id], [UserId], [StatusLogin], [IPAddress], [Time]) VALUES (63, 10000, N'Thành công, Đăng nhập thành công!', N'192.168.0.195', CAST(N'2025-07-04T02:32:53.0943751' AS DateTime2))
INSERT [dbo].[LoginHistories] ([Id], [UserId], [StatusLogin], [IPAddress], [Time]) VALUES (64, 10010, N'Thành công, Đăng nhập thành công!', N'192.168.0.195', CAST(N'2025-07-04T02:45:36.0632377' AS DateTime2))
INSERT [dbo].[LoginHistories] ([Id], [UserId], [StatusLogin], [IPAddress], [Time]) VALUES (65, 10000, N'Thành công, Đăng nhập thành công!', N'192.168.0.195', CAST(N'2025-07-04T02:46:32.6482879' AS DateTime2))
INSERT [dbo].[LoginHistories] ([Id], [UserId], [StatusLogin], [IPAddress], [Time]) VALUES (66, 10010, N'Thành công, Đăng nhập thành công!', N'192.168.0.195', CAST(N'2025-07-04T02:48:39.6784466' AS DateTime2))
INSERT [dbo].[LoginHistories] ([Id], [UserId], [StatusLogin], [IPAddress], [Time]) VALUES (67, 10000, N'Thành công, Đăng nhập thành công!', N'192.168.0.195', CAST(N'2025-07-04T14:40:43.7020087' AS DateTime2))
INSERT [dbo].[LoginHistories] ([Id], [UserId], [StatusLogin], [IPAddress], [Time]) VALUES (68, 10000, N'Thành công, Đăng nhập thành công!', N'192.168.0.195', CAST(N'2025-07-04T15:54:41.1552125' AS DateTime2))
INSERT [dbo].[LoginHistories] ([Id], [UserId], [StatusLogin], [IPAddress], [Time]) VALUES (69, 10000, N'Thành công, Đăng nhập thành công!', N'192.168.0.195', CAST(N'2025-07-04T16:24:49.7262445' AS DateTime2))
INSERT [dbo].[LoginHistories] ([Id], [UserId], [StatusLogin], [IPAddress], [Time]) VALUES (70, 10008, N'Thành công, Đăng nhập thành công!', N'192.168.0.195', CAST(N'2025-07-04T16:26:31.6675437' AS DateTime2))
INSERT [dbo].[LoginHistories] ([Id], [UserId], [StatusLogin], [IPAddress], [Time]) VALUES (71, 10000, N'Thành công, Đăng nhập thành công!', N'192.168.0.195', CAST(N'2025-07-04T17:11:13.0449942' AS DateTime2))
INSERT [dbo].[LoginHistories] ([Id], [UserId], [StatusLogin], [IPAddress], [Time]) VALUES (72, 10000, N'Thành công, Đăng nhập thành công!', N'192.168.0.195', CAST(N'2025-07-04T17:16:57.9334148' AS DateTime2))
INSERT [dbo].[LoginHistories] ([Id], [UserId], [StatusLogin], [IPAddress], [Time]) VALUES (73, 10000, N'Thành công, Đăng nhập thành công!', N'192.168.0.195', CAST(N'2025-07-04T17:21:16.7301781' AS DateTime2))
INSERT [dbo].[LoginHistories] ([Id], [UserId], [StatusLogin], [IPAddress], [Time]) VALUES (74, 10000, N'Thành công, Đăng nhập thành công!', N'192.168.0.195', CAST(N'2025-07-04T17:26:40.4690873' AS DateTime2))
INSERT [dbo].[LoginHistories] ([Id], [UserId], [StatusLogin], [IPAddress], [Time]) VALUES (75, 10003, N'Thành công, Đăng nhập thành công!', N'192.168.0.195', CAST(N'2025-07-04T17:26:58.9518510' AS DateTime2))
INSERT [dbo].[LoginHistories] ([Id], [UserId], [StatusLogin], [IPAddress], [Time]) VALUES (76, 10000, N'Thành công, Đăng nhập thành công!', N'192.168.0.195', CAST(N'2025-07-04T17:27:11.3386437' AS DateTime2))
INSERT [dbo].[LoginHistories] ([Id], [UserId], [StatusLogin], [IPAddress], [Time]) VALUES (77, 10000, N'Thành công, Đăng nhập thành công!', N'192.168.0.195', CAST(N'2025-07-04T17:31:32.1664827' AS DateTime2))
INSERT [dbo].[LoginHistories] ([Id], [UserId], [StatusLogin], [IPAddress], [Time]) VALUES (78, 10000, N'Thành công, Đăng nhập thành công!', N'192.168.0.195', CAST(N'2025-07-04T17:39:08.0203817' AS DateTime2))
INSERT [dbo].[LoginHistories] ([Id], [UserId], [StatusLogin], [IPAddress], [Time]) VALUES (79, 10000, N'Thành công, Đăng nhập thành công!', N'192.168.0.195', CAST(N'2025-07-04T17:58:10.8950044' AS DateTime2))
INSERT [dbo].[LoginHistories] ([Id], [UserId], [StatusLogin], [IPAddress], [Time]) VALUES (80, 10000, N'Thành công, Đăng nhập thành công!', N'192.168.0.195', CAST(N'2025-07-04T18:03:16.1985463' AS DateTime2))
INSERT [dbo].[LoginHistories] ([Id], [UserId], [StatusLogin], [IPAddress], [Time]) VALUES (81, 10000, N'Thành công, Đăng nhập thành công!', N'192.168.0.195', CAST(N'2025-07-04T18:04:58.6588254' AS DateTime2))
INSERT [dbo].[LoginHistories] ([Id], [UserId], [StatusLogin], [IPAddress], [Time]) VALUES (82, 10000, N'Thành công, Đăng nhập thành công!', N'192.168.0.195', CAST(N'2025-07-04T18:22:49.0389704' AS DateTime2))
INSERT [dbo].[LoginHistories] ([Id], [UserId], [StatusLogin], [IPAddress], [Time]) VALUES (83, 10000, N'Thành công, Đăng nhập thành công!', N'192.168.0.195', CAST(N'2025-07-04T18:53:44.9973052' AS DateTime2))
INSERT [dbo].[LoginHistories] ([Id], [UserId], [StatusLogin], [IPAddress], [Time]) VALUES (84, 10000, N'Thành công, Đăng nhập thành công!', N'192.168.0.195', CAST(N'2025-07-04T19:23:51.7570926' AS DateTime2))
INSERT [dbo].[LoginHistories] ([Id], [UserId], [StatusLogin], [IPAddress], [Time]) VALUES (85, 10000, N'Thành công, Đăng nhập thành công!', N'192.168.0.195', CAST(N'2025-07-04T19:54:24.7289602' AS DateTime2))
INSERT [dbo].[LoginHistories] ([Id], [UserId], [StatusLogin], [IPAddress], [Time]) VALUES (86, 10000, N'Thành công, Đăng nhập thành công!', N'192.168.0.195', CAST(N'2025-07-04T20:10:57.8819765' AS DateTime2))
SET IDENTITY_INSERT [dbo].[LoginHistories] OFF
GO
INSERT [dbo].[OrderDetails] ([OrderCode], [ColorCapacityId], [Id], [Quantity]) VALUES (N'71dbd034-9cdb-4920-9dd2-2c8b6e005fe6', 1008, 0, 1)
INSERT [dbo].[OrderDetails] ([OrderCode], [ColorCapacityId], [Id], [Quantity]) VALUES (N'71dbd034-9cdb-4920-9dd2-2c8b6e005fe6', 1009, 0, 1)
INSERT [dbo].[OrderDetails] ([OrderCode], [ColorCapacityId], [Id], [Quantity]) VALUES (N'31ebd7df-9d20-4a89-a23a-8deba6ddd827', 1009, 0, 1)
INSERT [dbo].[OrderDetails] ([OrderCode], [ColorCapacityId], [Id], [Quantity]) VALUES (N'd64a9304-6754-4bf4-af85-95c5d9bc1dc7', 1008, 0, 1)
INSERT [dbo].[OrderDetails] ([OrderCode], [ColorCapacityId], [Id], [Quantity]) VALUES (N'81c7fa32-afce-4c5d-8284-add590450d34', 1008, 0, 2)
GO
INSERT [dbo].[Orders] ([OrderCode], [UserId], [CreatedAt], [Status], [Adress], [Name], [PhoneNumber], [Total], [DeliveryDate]) VALUES (N'71dbd034-9cdb-4920-9dd2-2c8b6e005fe6', 10010, CAST(N'2025-07-04T02:48:56.0000000' AS DateTime2), N'Đã giao', N'Hà Nội', N'Trần Lực', N'0368502432', CAST(58290000.00 AS Decimal(18, 2)), NULL)
INSERT [dbo].[Orders] ([OrderCode], [UserId], [CreatedAt], [Status], [Adress], [Name], [PhoneNumber], [Total], [DeliveryDate]) VALUES (N'31ebd7df-9d20-4a89-a23a-8deba6ddd827', 10000, CAST(N'2025-07-04T01:27:04.0000000' AS DateTime2), N'Chờ xử lý', N'Hà Nội', N'Trần Lực', N'0368502432', CAST(36290000.00 AS Decimal(18, 2)), NULL)
INSERT [dbo].[Orders] ([OrderCode], [UserId], [CreatedAt], [Status], [Adress], [Name], [PhoneNumber], [Total], [DeliveryDate]) VALUES (N'd64a9304-6754-4bf4-af85-95c5d9bc1dc7', 10008, CAST(N'2025-07-04T01:27:45.0000000' AS DateTime2), N'Chờ xử lý', N'Hà Nội', N'Trần Lực', N'0368502432', CAST(22000000.00 AS Decimal(18, 2)), NULL)
INSERT [dbo].[Orders] ([OrderCode], [UserId], [CreatedAt], [Status], [Adress], [Name], [PhoneNumber], [Total], [DeliveryDate]) VALUES (N'81c7fa32-afce-4c5d-8284-add590450d34', 10010, CAST(N'2025-07-04T02:46:17.0000000' AS DateTime2), N'Đang giao', N'Hà Nội', N'Trần Lực', N'0368502432', CAST(44000000.00 AS Decimal(18, 2)), NULL)
GO
SET IDENTITY_INSERT [dbo].[OrderStatusLogs] ON 

INSERT [dbo].[OrderStatusLogs] ([Id], [CreateAt], [OrderCode], [Status]) VALUES (1, CAST(N'2025-07-04T15:55:16.0000000' AS DateTime2), N'71dbd034-9cdb-4920-9dd2-2c8b6e005fe6', N'Đơn hàng đã được chuyển sang trạng thái Đang giao')
INSERT [dbo].[OrderStatusLogs] ([Id], [CreateAt], [OrderCode], [Status]) VALUES (2, CAST(N'2025-07-04T16:18:52.0000000' AS DateTime2), N'71dbd034-9cdb-4920-9dd2-2c8b6e005fe6', N'Đơn hàng đã được chuyển sang trạng thái Đã giao')
INSERT [dbo].[OrderStatusLogs] ([Id], [CreateAt], [OrderCode], [Status]) VALUES (3, CAST(N'2025-07-04T16:26:05.0000000' AS DateTime2), N'81c7fa32-afce-4c5d-8284-add590450d34', N'Đơn hàng đã được chuyển sang trạng thái Đang giao')
SET IDENTITY_INSERT [dbo].[OrderStatusLogs] OFF
GO
SET IDENTITY_INSERT [dbo].[PermissionGroups] ON 

INSERT [dbo].[PermissionGroups] ([Id], [GroupName]) VALUES (1000, N'Quản lý tài khoản')
INSERT [dbo].[PermissionGroups] ([Id], [GroupName]) VALUES (1001, N'Quản lý vai trò')
INSERT [dbo].[PermissionGroups] ([Id], [GroupName]) VALUES (1002, N'Quản lý thương hiệu')
INSERT [dbo].[PermissionGroups] ([Id], [GroupName]) VALUES (1003, N'Quản lý giỏ hàng')
INSERT [dbo].[PermissionGroups] ([Id], [GroupName]) VALUES (1004, N'Quản lý danh mục')
INSERT [dbo].[PermissionGroups] ([Id], [GroupName]) VALUES (1005, N'Quản lý đơn hàng')
INSERT [dbo].[PermissionGroups] ([Id], [GroupName]) VALUES (1006, N'ERROR')
INSERT [dbo].[PermissionGroups] ([Id], [GroupName]) VALUES (1007, N'Quản lý sản phẩm')
SET IDENTITY_INSERT [dbo].[PermissionGroups] OFF
GO
SET IDENTITY_INSERT [dbo].[Permissions] ON 

INSERT [dbo].[Permissions] ([Id], [Name], [Description], [NameController], [PermissionGroupId]) VALUES (10000, N'GetUserWorkers', N'Lấy danh sách tài khoản nhân sự', N'Lấy danh sách nhân viên', 1000)
INSERT [dbo].[Permissions] ([Id], [Name], [Description], [NameController], [PermissionGroupId]) VALUES (10005, N'GetUserCustomer', N'Lấy danh sách tài khoản khách hàng', N'Lấy danh sách khách hàng', 1000)
INSERT [dbo].[Permissions] ([Id], [Name], [Description], [NameController], [PermissionGroupId]) VALUES (10006, N'CreateWorkerAccount', N'Tạo tài khoản nhân viên', N'Tạo tài khoản nhân viên', 1000)
INSERT [dbo].[Permissions] ([Id], [Name], [Description], [NameController], [PermissionGroupId]) VALUES (10007, N'GetLoginHistories', N'Lấy lịch sử đăng nhập tài khoản', N'Lấy lịch sử đăng nhập', 1000)
INSERT [dbo].[Permissions] ([Id], [Name], [Description], [NameController], [PermissionGroupId]) VALUES (10008, N'EditUserRoles', N'Chỉnh vai trò cho tài khoản', N'Gán vai trò', 1001)
INSERT [dbo].[Permissions] ([Id], [Name], [Description], [NameController], [PermissionGroupId]) VALUES (10009, N'EditAuthUser', N'Kích hoạt/Khoá tài khoản', N'Khoá/Mở tài khoản', 1000)
INSERT [dbo].[Permissions] ([Id], [Name], [Description], [NameController], [PermissionGroupId]) VALUES (10010, N'DeleteAccount', N'Xoá tài khoản', N'Xoá tài khoản', 1000)
INSERT [dbo].[Permissions] ([Id], [Name], [Description], [NameController], [PermissionGroupId]) VALUES (10011, N'CreateBrand', N'Tạo thương hiệu', N'Tạo thương hiệu', 1002)
INSERT [dbo].[Permissions] ([Id], [Name], [Description], [NameController], [PermissionGroupId]) VALUES (10012, N'UpdateBrand', N'Cập nhật thương hiệu', N'Cập nhật thương hiệu', 1002)
INSERT [dbo].[Permissions] ([Id], [Name], [Description], [NameController], [PermissionGroupId]) VALUES (10013, N'DeleteBrand', N'Xoá thương hiệu', N'Xoá thương hiệu', 1002)
INSERT [dbo].[Permissions] ([Id], [Name], [Description], [NameController], [PermissionGroupId]) VALUES (10015, N'AddToCart', N'Thêm sản phẩm vào giỏ hàng', N'Thêm sản phẩm vào giỏ hàng', 1003)
INSERT [dbo].[Permissions] ([Id], [Name], [Description], [NameController], [PermissionGroupId]) VALUES (10016, N'GetCart', N'Xem giỏ hàng', N'Xem giỏ hàng', 1003)
INSERT [dbo].[Permissions] ([Id], [Name], [Description], [NameController], [PermissionGroupId]) VALUES (10017, N'UpdateCartItem', N'Cập nhật giỏ hàng', N'Cập nhật giỏ hàng', 1003)
INSERT [dbo].[Permissions] ([Id], [Name], [Description], [NameController], [PermissionGroupId]) VALUES (10018, N'RemoveFromCart', N'Xoá sản phẩm trong giỏ', N'Xoá sản phẩm trong giỏ', 1003)
INSERT [dbo].[Permissions] ([Id], [Name], [Description], [NameController], [PermissionGroupId]) VALUES (10019, N'CheckOut', N'Tạo đơn mua hàng', N'Tạo đơn mua hàng', 1003)
INSERT [dbo].[Permissions] ([Id], [Name], [Description], [NameController], [PermissionGroupId]) VALUES (10020, N'CreateCategory', N'Tạo danh mục mới', N'Tạo mới danh mục', 1004)
INSERT [dbo].[Permissions] ([Id], [Name], [Description], [NameController], [PermissionGroupId]) VALUES (10021, N'UpdateCategory', N'Cập nhật danh mục', N'Cập nhật danh mục', 1004)
INSERT [dbo].[Permissions] ([Id], [Name], [Description], [NameController], [PermissionGroupId]) VALUES (10022, N'DeleteCategory', N'Xoá danh mục', N'Xoá danh mục', 1004)
INSERT [dbo].[Permissions] ([Id], [Name], [Description], [NameController], [PermissionGroupId]) VALUES (10023, N'GetOrders', N'Lấy danh sách đơn hàng', N'Lấy danh sách', 1005)
INSERT [dbo].[Permissions] ([Id], [Name], [Description], [NameController], [PermissionGroupId]) VALUES (10024, N'UpdateOrderStatus', N'Cập nhật trạng thái đơn hàng', N'Cập nhật trạng thái', 1005)
INSERT [dbo].[Permissions] ([Id], [Name], [Description], [NameController], [PermissionGroupId]) VALUES (10025, N'GetPermissions', N'Lấy danh sách quyền hạn ', N'Lấy danh sách quyền hạn ', 1001)
INSERT [dbo].[Permissions] ([Id], [Name], [Description], [NameController], [PermissionGroupId]) VALUES (10026, N'GetPermissionGroups', N'Lấy danh sách nhóm quyền hạn', N'Lấy danh sách nhóm quyền hạn', 1001)
INSERT [dbo].[Permissions] ([Id], [Name], [Description], [NameController], [PermissionGroupId]) VALUES (10027, N'CreatePermissions', N'Tạo quyền hạn mới chỉ dành cho nhà phát triển', N'Tạo quyền hạn mới', 1006)
INSERT [dbo].[Permissions] ([Id], [Name], [Description], [NameController], [PermissionGroupId]) VALUES (10028, N'AddProduct', N'Tạo sản phẩm mới', N'Tạo sản phẩm', 1007)
INSERT [dbo].[Permissions] ([Id], [Name], [Description], [NameController], [PermissionGroupId]) VALUES (10029, N'UpdateProduct', N'Cập nhật sản phẩm', N'Cập nhật sản phẩm', 1007)
INSERT [dbo].[Permissions] ([Id], [Name], [Description], [NameController], [PermissionGroupId]) VALUES (10030, N'DeleteProduct', N'Xoá sản phẩm', N'Xoá sản phẩm', 1007)
INSERT [dbo].[Permissions] ([Id], [Name], [Description], [NameController], [PermissionGroupId]) VALUES (10031, N'EditProductCC', N'Chỉnh sửa phân loại sản phẩm', N'Chỉnh sửa phân loại', 1007)
INSERT [dbo].[Permissions] ([Id], [Name], [Description], [NameController], [PermissionGroupId]) VALUES (10032, N'CreateProductCC', N'Tạo phân loại sản phẩm mới', N'Tạo phân loại', 1007)
INSERT [dbo].[Permissions] ([Id], [Name], [Description], [NameController], [PermissionGroupId]) VALUES (10033, N'DeleteProductCC', N'Xoá phân loại sản phẩm', N'Xoá phân loại', 1007)
INSERT [dbo].[Permissions] ([Id], [Name], [Description], [NameController], [PermissionGroupId]) VALUES (10034, N'DeleteRole', N'Xoá vai trò', N'Xoá vai trò', 1001)
INSERT [dbo].[Permissions] ([Id], [Name], [Description], [NameController], [PermissionGroupId]) VALUES (10035, N'EditRolePermission', N'Chỉnh quyền hạn vai trò', N'Chỉnh quyền', 1001)
INSERT [dbo].[Permissions] ([Id], [Name], [Description], [NameController], [PermissionGroupId]) VALUES (10037, N'EditRole', N'Chỉ sửa thông tin vai trò không sửa quyền hạn', N'Chỉnh vai trò', 1001)
SET IDENTITY_INSERT [dbo].[Permissions] OFF
GO
SET IDENTITY_INSERT [dbo].[Products] ON 

INSERT [dbo].[Products] ([Id], [Name], [DiscountAmount], [Description], [ImageView], [ImageFolder], [BrandId], [CategoryId], [SaleId], [CreateAt]) VALUES (10000, N'Iphone 8', CAST(20000.00 AS Decimal(18, 2)), N'Iphone 8 của Apple', N'1.png', N'iphone_8', 1, 1, NULL, CAST(N'2025-07-02T15:00:28.8761643' AS DateTime2))
INSERT [dbo].[Products] ([Id], [Name], [DiscountAmount], [Description], [ImageView], [ImageFolder], [BrandId], [CategoryId], [SaleId], [CreateAt]) VALUES (10001, N'IphoneX', CAST(300000.00 AS Decimal(18, 2)), N'ads adas d asd as', N'1.png', N'iphonex', 1, 1, NULL, CAST(N'2025-07-02T15:03:06.3531450' AS DateTime2))
INSERT [dbo].[Products] ([Id], [Name], [DiscountAmount], [Description], [ImageView], [ImageFolder], [BrandId], [CategoryId], [SaleId], [CreateAt]) VALUES (10002, N'Iphone 11', CAST(300000.00 AS Decimal(18, 2)), N'dsfdsf', N'1.png', N'iphone_11', 1, 1, NULL, CAST(N'2025-07-02T15:04:24.4130839' AS DateTime2))
INSERT [dbo].[Products] ([Id], [Name], [DiscountAmount], [Description], [ImageView], [ImageFolder], [BrandId], [CategoryId], [SaleId], [CreateAt]) VALUES (10003, N'Iphone12', CAST(450000.00 AS Decimal(18, 2)), N'sda sda sdas', N'1.png', N'iphone12', 1, 1, NULL, CAST(N'2025-07-02T15:05:08.9782546' AS DateTime2))
INSERT [dbo].[Products] ([Id], [Name], [DiscountAmount], [Description], [ImageView], [ImageFolder], [BrandId], [CategoryId], [SaleId], [CreateAt]) VALUES (10004, N'Iphone 13', CAST(500000.00 AS Decimal(18, 2)), N'ádas dá đâsd', N'1.png', N'iphone_13', 1, 1, NULL, CAST(N'2025-07-02T15:05:49.6696464' AS DateTime2))
INSERT [dbo].[Products] ([Id], [Name], [DiscountAmount], [Description], [ImageView], [ImageFolder], [BrandId], [CategoryId], [SaleId], [CreateAt]) VALUES (10005, N'Iphone 14', CAST(500000.00 AS Decimal(18, 2)), N'ád á dá dá', N'1.png', N'iphone_14', 1, 1, NULL, CAST(N'2025-07-02T15:06:48.9637640' AS DateTime2))
INSERT [dbo].[Products] ([Id], [Name], [DiscountAmount], [Description], [ImageView], [ImageFolder], [BrandId], [CategoryId], [SaleId], [CreateAt]) VALUES (10006, N'Iphone 15', CAST(500000.00 AS Decimal(18, 2)), N'ád ádasdsad', N'1.png', N'iphone_15', 1, 1, NULL, CAST(N'2025-07-02T15:07:53.4402276' AS DateTime2))
INSERT [dbo].[Products] ([Id], [Name], [DiscountAmount], [Description], [ImageView], [ImageFolder], [BrandId], [CategoryId], [SaleId], [CreateAt]) VALUES (10007, N'Iphone 16', CAST(500000.00 AS Decimal(18, 2)), N'sada s dá đá', N'1.png', N'iphone_16', 1, 1, NULL, CAST(N'2025-07-02T15:08:36.9610719' AS DateTime2))
INSERT [dbo].[Products] ([Id], [Name], [DiscountAmount], [Description], [ImageView], [ImageFolder], [BrandId], [CategoryId], [SaleId], [CreateAt]) VALUES (10008, N'MacBook Air 2020', CAST(500000.00 AS Decimal(18, 2)), N'13 inch M1 8GB RAM 256GB SSD', N'1.png', N'macbook_air_2020', 1, 2, NULL, CAST(N'2025-07-04T20:12:36.5133862' AS DateTime2))
INSERT [dbo].[Products] ([Id], [Name], [DiscountAmount], [Description], [ImageView], [ImageFolder], [BrandId], [CategoryId], [SaleId], [CreateAt]) VALUES (10009, N'Tai nghe Gaming Hyperx Cloud II', CAST(200000.00 AS Decimal(18, 2)), N'Ra mắt từ năm 2015, nhưng đến nay HyperX Cloud II vẫn là lựa chọn hàng đầu của nhiều game thủ chuyên nghiệp lẫn casual. Lý do rất đơn giản: bền – nhẹ – đeo êm – nghe rõ – không phải nghĩ nhiều.', N'1.png', N'tai_nghe_gaming_hyperx_cloud_ii', 4, 4, NULL, CAST(N'2025-07-04T20:15:06.4370875' AS DateTime2))
INSERT [dbo].[Products] ([Id], [Name], [DiscountAmount], [Description], [ImageView], [ImageFolder], [BrandId], [CategoryId], [SaleId], [CreateAt]) VALUES (10010, N'Card màn hình MSI RTX 3060', CAST(500000.00 AS Decimal(18, 2)), N'Hiệu năng của Card màn hình MSI RTX 3060 VENTUS 2X OC 12 GB
Được trang bị nhân đồ họa Nvidia RTX 3060, Card màn hình MSI RTX 3060 VENTUS 2X OC 12 GB có hiệu năng tốt hơn RTX 2060 và chơi tốt tất cả những game khủng hiện nay ở độ phân giải 2K và setting đặt ở mức cao. 
Đặc điểm nổi bật
Xung nhịp GPU: 1777 Mhz
12GB GDDR6 VRAM
Backplate phay xước
TORX Fan 3.0
MSI Dragon Center', N'1.png', N'card_man_hinh_msi_rtx_3060', 16, 5, NULL, CAST(N'2025-07-04T20:19:53.5883858' AS DateTime2))
SET IDENTITY_INSERT [dbo].[Products] OFF
GO
INSERT [dbo].[RolePermissions] ([RoleId], [PermissionId]) VALUES (10001, 10000)
INSERT [dbo].[RolePermissions] ([RoleId], [PermissionId]) VALUES (10001, 10005)
INSERT [dbo].[RolePermissions] ([RoleId], [PermissionId]) VALUES (10004, 10005)
INSERT [dbo].[RolePermissions] ([RoleId], [PermissionId]) VALUES (10001, 10006)
INSERT [dbo].[RolePermissions] ([RoleId], [PermissionId]) VALUES (10001, 10007)
INSERT [dbo].[RolePermissions] ([RoleId], [PermissionId]) VALUES (10004, 10007)
INSERT [dbo].[RolePermissions] ([RoleId], [PermissionId]) VALUES (10001, 10008)
INSERT [dbo].[RolePermissions] ([RoleId], [PermissionId]) VALUES (10001, 10009)
INSERT [dbo].[RolePermissions] ([RoleId], [PermissionId]) VALUES (10001, 10010)
INSERT [dbo].[RolePermissions] ([RoleId], [PermissionId]) VALUES (10001, 10011)
INSERT [dbo].[RolePermissions] ([RoleId], [PermissionId]) VALUES (10001, 10012)
INSERT [dbo].[RolePermissions] ([RoleId], [PermissionId]) VALUES (10002, 10015)
INSERT [dbo].[RolePermissions] ([RoleId], [PermissionId]) VALUES (10002, 10016)
INSERT [dbo].[RolePermissions] ([RoleId], [PermissionId]) VALUES (10002, 10017)
INSERT [dbo].[RolePermissions] ([RoleId], [PermissionId]) VALUES (10002, 10018)
INSERT [dbo].[RolePermissions] ([RoleId], [PermissionId]) VALUES (10002, 10019)
INSERT [dbo].[RolePermissions] ([RoleId], [PermissionId]) VALUES (10001, 10020)
INSERT [dbo].[RolePermissions] ([RoleId], [PermissionId]) VALUES (10001, 10021)
INSERT [dbo].[RolePermissions] ([RoleId], [PermissionId]) VALUES (10001, 10022)
INSERT [dbo].[RolePermissions] ([RoleId], [PermissionId]) VALUES (10001, 10023)
INSERT [dbo].[RolePermissions] ([RoleId], [PermissionId]) VALUES (10004, 10023)
INSERT [dbo].[RolePermissions] ([RoleId], [PermissionId]) VALUES (10001, 10024)
INSERT [dbo].[RolePermissions] ([RoleId], [PermissionId]) VALUES (10004, 10024)
INSERT [dbo].[RolePermissions] ([RoleId], [PermissionId]) VALUES (10001, 10025)
INSERT [dbo].[RolePermissions] ([RoleId], [PermissionId]) VALUES (10001, 10026)
INSERT [dbo].[RolePermissions] ([RoleId], [PermissionId]) VALUES (10001, 10028)
INSERT [dbo].[RolePermissions] ([RoleId], [PermissionId]) VALUES (10001, 10029)
INSERT [dbo].[RolePermissions] ([RoleId], [PermissionId]) VALUES (10001, 10030)
INSERT [dbo].[RolePermissions] ([RoleId], [PermissionId]) VALUES (10001, 10031)
INSERT [dbo].[RolePermissions] ([RoleId], [PermissionId]) VALUES (10001, 10032)
INSERT [dbo].[RolePermissions] ([RoleId], [PermissionId]) VALUES (10001, 10033)
INSERT [dbo].[RolePermissions] ([RoleId], [PermissionId]) VALUES (10001, 10034)
INSERT [dbo].[RolePermissions] ([RoleId], [PermissionId]) VALUES (10001, 10035)
INSERT [dbo].[RolePermissions] ([RoleId], [PermissionId]) VALUES (10001, 10037)
GO
SET IDENTITY_INSERT [dbo].[Roles] ON 

INSERT [dbo].[Roles] ([Id], [Name], [Description], [CreateAt]) VALUES (10001, N'Admin', N'Admin', CAST(N'2025-07-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Roles] ([Id], [Name], [Description], [CreateAt]) VALUES (10002, N'Khách hàng', N'string', CAST(N'2025-07-02T15:54:59.9985461' AS DateTime2))
INSERT [dbo].[Roles] ([Id], [Name], [Description], [CreateAt]) VALUES (10003, N'string', N'string', CAST(N'2025-07-04T17:40:23.5921253' AS DateTime2))
INSERT [dbo].[Roles] ([Id], [Name], [Description], [CreateAt]) VALUES (10004, N'Nhân viên', N'Nhân viên hỗ trợ đơn hàng', CAST(N'2025-07-04T19:36:47.2811046' AS DateTime2))
SET IDENTITY_INSERT [dbo].[Roles] OFF
GO
INSERT [dbo].[UserAuthInfos] ([UserId], [IsActive], [FailedAttempts]) VALUES (10000, 1, 5)
INSERT [dbo].[UserAuthInfos] ([UserId], [IsActive], [FailedAttempts]) VALUES (10002, 1, 5)
INSERT [dbo].[UserAuthInfos] ([UserId], [IsActive], [FailedAttempts]) VALUES (10003, 1, 5)
INSERT [dbo].[UserAuthInfos] ([UserId], [IsActive], [FailedAttempts]) VALUES (10004, 1, 5)
INSERT [dbo].[UserAuthInfos] ([UserId], [IsActive], [FailedAttempts]) VALUES (10008, 1, 5)
INSERT [dbo].[UserAuthInfos] ([UserId], [IsActive], [FailedAttempts]) VALUES (10010, 1, 5)
INSERT [dbo].[UserAuthInfos] ([UserId], [IsActive], [FailedAttempts]) VALUES (10011, 1, 5)
INSERT [dbo].[UserAuthInfos] ([UserId], [IsActive], [FailedAttempts]) VALUES (10012, 1, 5)
GO
INSERT [dbo].[UserRoles] ([UserId], [RoleId], [Id]) VALUES (10000, 10001, 0)
INSERT [dbo].[UserRoles] ([UserId], [RoleId], [Id]) VALUES (10000, 10002, 0)
INSERT [dbo].[UserRoles] ([UserId], [RoleId], [Id]) VALUES (10002, 10001, 0)
INSERT [dbo].[UserRoles] ([UserId], [RoleId], [Id]) VALUES (10008, 10002, 0)
INSERT [dbo].[UserRoles] ([UserId], [RoleId], [Id]) VALUES (10010, 10002, 0)
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([Id], [Name], [Gender], [Email], [PasswordHash], [PhoneNumber], [Address], [Avatar], [Birthday], [Hometown], [workerAccount], [CreatedAt]) VALUES (10000, N'Admin', N'Nam', N'Admin', N'AQAAAAIAAYagAAAAEJSyAnl3BFDWZNlD8BVV/tf9AnKiIiwA66truiqkBpDmhcX+X7gDlvO17mBtc4XD+Q==', N'123456789', N'Hà Nội', N'admin.png', CAST(N'2002-05-23' AS Date), N'Nghệ An', 1, CAST(N'2025-07-01T17:05:39.3316584' AS DateTime2))
INSERT [dbo].[Users] ([Id], [Name], [Gender], [Email], [PasswordHash], [PhoneNumber], [Address], [Avatar], [Birthday], [Hometown], [workerAccount], [CreatedAt]) VALUES (10002, N'Trần Thế Lực', N'Nam', N'lucdt456@gmail.com', N'AQAAAAIAAYagAAAAEE4ciOuDTg16fKgEFhH1kZzLL3hKTMmjw0m7N+e8KAV0UpP7Uq9uRc09LqkAsRNDrg==', N'0368502432', N'1234566', N'lucdt456gmailcom.png', CAST(N'2002-05-23' AS Date), N'Hà Nội', 1, CAST(N'2025-07-01T17:42:19.1753525' AS DateTime2))
INSERT [dbo].[Users] ([Id], [Name], [Gender], [Email], [PasswordHash], [PhoneNumber], [Address], [Avatar], [Birthday], [Hometown], [workerAccount], [CreatedAt]) VALUES (10003, N'Worker 1', N'Nam', N'worker1@antraz.store', N'AQAAAAIAAYagAAAAEDWVSocJMuAZ1PfKallm6/Mq69A0G0rMVFu3cmcgNoenMN/9R5M1QGUHtSEqt8Ufhg==', N'123456', N'123123', N'worker1antrazstore.png', CAST(N'2025-07-18' AS Date), N'123123123123', 1, CAST(N'2025-07-02T10:26:16.9598690' AS DateTime2))
INSERT [dbo].[Users] ([Id], [Name], [Gender], [Email], [PasswordHash], [PhoneNumber], [Address], [Avatar], [Birthday], [Hometown], [workerAccount], [CreatedAt]) VALUES (10004, N'Worker2', N'Nam', N'worker2@antraz.store', N'AQAAAAIAAYagAAAAEKVXg8olAYGAAAVxnJiVQn8SDw+HUVg6r1VWfArfOvazb/fgHbke6ia6bK8mn7AF4Q==', N'123546456', N'2312312356', N'worker2antrazstore.png', CAST(N'2025-07-31' AS Date), N'4123123123123156', 1, CAST(N'2025-07-02T10:27:04.3445337' AS DateTime2))
INSERT [dbo].[Users] ([Id], [Name], [Gender], [Email], [PasswordHash], [PhoneNumber], [Address], [Avatar], [Birthday], [Hometown], [workerAccount], [CreatedAt]) VALUES (10008, N'customer2', N'Nam', N'customer2@antraz.store', N'AQAAAAIAAYagAAAAEKrVjaWbGSSlNhQ8h6IbUSevHHnUPLMneCfjLof7Pyj3wDAFuww9UP3M/Q3QSl7hBw==', N'123456', N'123456', N'defaultAvt.png', CAST(N'2025-07-24' AS Date), NULL, 0, CAST(N'2025-07-02T15:55:21.4324204' AS DateTime2))
INSERT [dbo].[Users] ([Id], [Name], [Gender], [Email], [PasswordHash], [PhoneNumber], [Address], [Avatar], [Birthday], [Hometown], [workerAccount], [CreatedAt]) VALUES (10010, N'Customer1', N'Nam', N'customer1@antraz.store', N'AQAAAAIAAYagAAAAENZcE5cJCHFYNOIDBG+RLW/TnH6J2o1hyffrAghbZQU20mWhivuCqKpIMs6ndGyHCw==', N'123123156', N'5464556', N'defaultAvt.png', CAST(N'2025-07-09' AS Date), NULL, 0, CAST(N'2025-07-02T16:00:09.6421965' AS DateTime2))
INSERT [dbo].[Users] ([Id], [Name], [Gender], [Email], [PasswordHash], [PhoneNumber], [Address], [Avatar], [Birthday], [Hometown], [workerAccount], [CreatedAt]) VALUES (10011, N'test', N'Nam', N'lasjo@gmail.com', N'AQAAAAIAAYagAAAAEBzfPNpV1J29Dp5HaptNn+W8qbtmIqDPXy0Z2NinMxbyB+TH/2c+d0dlwRiYFDeNMQ==', N'123456', N'123456', N'lasjogmailcom.png', CAST(N'2025-07-25' AS Date), N'asdasd', 1, CAST(N'2025-07-04T17:37:23.2166641' AS DateTime2))
INSERT [dbo].[Users] ([Id], [Name], [Gender], [Email], [PasswordHash], [PhoneNumber], [Address], [Avatar], [Birthday], [Hometown], [workerAccount], [CreatedAt]) VALUES (10012, N'Test 2', N'Nữ', N'Test@gmail.com', N'AQAAAAIAAYagAAAAELxYHayhLTcxsVctMo3e1UPD3X4tDxMup7QRitIgqDGaXnbaIhROg0JQC+6ZuUXkTw==', N'0948789435', N'asdsad', N'defaultAvt.png', CAST(N'2025-08-01' AS Date), N'Cinh', 1, CAST(N'2025-07-04T17:38:03.3883999' AS DateTime2))
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
/****** Object:  Index [IX_Carts_ColorCapacityId]    Script Date: 2025-07-04 20:30:21 ******/
CREATE NONCLUSTERED INDEX [IX_Carts_ColorCapacityId] ON [dbo].[Carts]
(
	[ColorCapacityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_ColorCapacities_CapacityId]    Script Date: 2025-07-04 20:30:21 ******/
CREATE NONCLUSTERED INDEX [IX_ColorCapacities_CapacityId] ON [dbo].[ColorCapacities]
(
	[CapacityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_ColorCapacities_ColorId]    Script Date: 2025-07-04 20:30:21 ******/
CREATE NONCLUSTERED INDEX [IX_ColorCapacities_ColorId] ON [dbo].[ColorCapacities]
(
	[ColorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_ColorCapacities_ProductId]    Script Date: 2025-07-04 20:30:21 ******/
CREATE NONCLUSTERED INDEX [IX_ColorCapacities_ProductId] ON [dbo].[ColorCapacities]
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_EmailCodes_UserId]    Script Date: 2025-07-04 20:30:21 ******/
CREATE NONCLUSTERED INDEX [IX_EmailCodes_UserId] ON [dbo].[EmailCodes]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_LoginHistories_UserId]    Script Date: 2025-07-04 20:30:21 ******/
CREATE NONCLUSTERED INDEX [IX_LoginHistories_UserId] ON [dbo].[LoginHistories]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_OrderDetails_ColorCapacityId]    Script Date: 2025-07-04 20:30:21 ******/
CREATE NONCLUSTERED INDEX [IX_OrderDetails_ColorCapacityId] ON [dbo].[OrderDetails]
(
	[ColorCapacityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Orders_UserId]    Script Date: 2025-07-04 20:30:21 ******/
CREATE NONCLUSTERED INDEX [IX_Orders_UserId] ON [dbo].[Orders]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_OrderStatusLogs_OrderCode]    Script Date: 2025-07-04 20:30:21 ******/
CREATE NONCLUSTERED INDEX [IX_OrderStatusLogs_OrderCode] ON [dbo].[OrderStatusLogs]
(
	[OrderCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_ProductImages_ProductId]    Script Date: 2025-07-04 20:30:21 ******/
CREATE NONCLUSTERED INDEX [IX_ProductImages_ProductId] ON [dbo].[ProductImages]
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Products_BrandId]    Script Date: 2025-07-04 20:30:21 ******/
CREATE NONCLUSTERED INDEX [IX_Products_BrandId] ON [dbo].[Products]
(
	[BrandId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Products_CategoryId]    Script Date: 2025-07-04 20:30:21 ******/
CREATE NONCLUSTERED INDEX [IX_Products_CategoryId] ON [dbo].[Products]
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Products_SaleId]    Script Date: 2025-07-04 20:30:21 ******/
CREATE NONCLUSTERED INDEX [IX_Products_SaleId] ON [dbo].[Products]
(
	[SaleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Reviews_ColorCapacityId]    Script Date: 2025-07-04 20:30:21 ******/
CREATE NONCLUSTERED INDEX [IX_Reviews_ColorCapacityId] ON [dbo].[Reviews]
(
	[ColorCapacityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Reviews_UserId]    Script Date: 2025-07-04 20:30:21 ******/
CREATE NONCLUSTERED INDEX [IX_Reviews_UserId] ON [dbo].[Reviews]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_RolePermissions_PermissionId]    Script Date: 2025-07-04 20:30:21 ******/
CREATE NONCLUSTERED INDEX [IX_RolePermissions_PermissionId] ON [dbo].[RolePermissions]
(
	[PermissionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_UserPermissions_PermissionId]    Script Date: 2025-07-04 20:30:21 ******/
CREATE NONCLUSTERED INDEX [IX_UserPermissions_PermissionId] ON [dbo].[UserPermissions]
(
	[PermissionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_UserRoles_RoleId]    Script Date: 2025-07-04 20:30:21 ******/
CREATE NONCLUSTERED INDEX [IX_UserRoles_RoleId] ON [dbo].[UserRoles]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_WishLists_ColorCapacityId]    Script Date: 2025-07-04 20:30:21 ******/
CREATE NONCLUSTERED INDEX [IX_WishLists_ColorCapacityId] ON [dbo].[WishLists]
(
	[ColorCapacityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Orders] ADD  CONSTRAINT [DF__Orders__CreatedA__66603565]  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[Orders] ADD  CONSTRAINT [DF__Orders__Adress__607251E5]  DEFAULT (N'') FOR [Adress]
GO
ALTER TABLE [dbo].[Orders] ADD  CONSTRAINT [DF__Orders__Name__6166761E]  DEFAULT (N'') FOR [Name]
GO
ALTER TABLE [dbo].[Orders] ADD  CONSTRAINT [DF__Orders__PhoneNum__625A9A57]  DEFAULT (N'') FOR [PhoneNumber]
GO
ALTER TABLE [dbo].[Orders] ADD  CONSTRAINT [DF__Orders__Total__634EBE90]  DEFAULT ((0.0)) FOR [Total]
GO
ALTER TABLE [dbo].[Carts]  WITH CHECK ADD  CONSTRAINT [FK_Carts_ColorCapacities_ColorCapacityId] FOREIGN KEY([ColorCapacityId])
REFERENCES [dbo].[ColorCapacities] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Carts] CHECK CONSTRAINT [FK_Carts_ColorCapacities_ColorCapacityId]
GO
ALTER TABLE [dbo].[Carts]  WITH CHECK ADD  CONSTRAINT [FK_Carts_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Carts] CHECK CONSTRAINT [FK_Carts_Users_UserId]
GO
ALTER TABLE [dbo].[ColorCapacities]  WITH CHECK ADD  CONSTRAINT [FK_ColorCapacities_Capacities_CapacityId] FOREIGN KEY([CapacityId])
REFERENCES [dbo].[Capacities] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ColorCapacities] CHECK CONSTRAINT [FK_ColorCapacities_Capacities_CapacityId]
GO
ALTER TABLE [dbo].[ColorCapacities]  WITH CHECK ADD  CONSTRAINT [FK_ColorCapacities_Colors_ColorId] FOREIGN KEY([ColorId])
REFERENCES [dbo].[Colors] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ColorCapacities] CHECK CONSTRAINT [FK_ColorCapacities_Colors_ColorId]
GO
ALTER TABLE [dbo].[ColorCapacities]  WITH CHECK ADD  CONSTRAINT [FK_ColorCapacities_Products_ProductId] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ColorCapacities] CHECK CONSTRAINT [FK_ColorCapacities_Products_ProductId]
GO
ALTER TABLE [dbo].[EmailCodes]  WITH CHECK ADD  CONSTRAINT [FK_EmailCodes_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[EmailCodes] CHECK CONSTRAINT [FK_EmailCodes_Users_UserId]
GO
ALTER TABLE [dbo].[LoginHistories]  WITH CHECK ADD  CONSTRAINT [FK_LoginHistories_UserAuthInfos_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[UserAuthInfos] ([UserId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[LoginHistories] CHECK CONSTRAINT [FK_LoginHistories_UserAuthInfos_UserId]
GO
ALTER TABLE [dbo].[OrderDetails]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetails_ColorCapacities_ColorCapacityId] FOREIGN KEY([ColorCapacityId])
REFERENCES [dbo].[ColorCapacities] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[OrderDetails] CHECK CONSTRAINT [FK_OrderDetails_ColorCapacities_ColorCapacityId]
GO
ALTER TABLE [dbo].[OrderDetails]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetails_Orders_OrderCode] FOREIGN KEY([OrderCode])
REFERENCES [dbo].[Orders] ([OrderCode])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[OrderDetails] CHECK CONSTRAINT [FK_OrderDetails_Orders_OrderCode]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_Orders_Users_UserId]
GO
ALTER TABLE [dbo].[OrderStatusLogs]  WITH CHECK ADD  CONSTRAINT [FK_OrderStatusLogs_Orders_OrderCode] FOREIGN KEY([OrderCode])
REFERENCES [dbo].[Orders] ([OrderCode])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[OrderStatusLogs] CHECK CONSTRAINT [FK_OrderStatusLogs_Orders_OrderCode]
GO
ALTER TABLE [dbo].[Permissions]  WITH CHECK ADD  CONSTRAINT [FK_Permissions_PermissionGroups_PermissionGroupId] FOREIGN KEY([PermissionGroupId])
REFERENCES [dbo].[PermissionGroups] ([Id])
GO
ALTER TABLE [dbo].[Permissions] CHECK CONSTRAINT [FK_Permissions_PermissionGroups_PermissionGroupId]
GO
ALTER TABLE [dbo].[ProductImages]  WITH CHECK ADD  CONSTRAINT [FK_ProductImages_Products_ProductId] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProductImages] CHECK CONSTRAINT [FK_ProductImages_Products_ProductId]
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK_Products_Brands_BrandId] FOREIGN KEY([BrandId])
REFERENCES [dbo].[Brands] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_Products_Brands_BrandId]
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK_Products_Categories_CategoryId] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Categories] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_Products_Categories_CategoryId]
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK_Products_Sales_SaleId] FOREIGN KEY([SaleId])
REFERENCES [dbo].[Sales] ([Id])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_Products_Sales_SaleId]
GO
ALTER TABLE [dbo].[Reviews]  WITH CHECK ADD  CONSTRAINT [FK_Reviews_ColorCapacities_ColorCapacityId] FOREIGN KEY([ColorCapacityId])
REFERENCES [dbo].[ColorCapacities] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Reviews] CHECK CONSTRAINT [FK_Reviews_ColorCapacities_ColorCapacityId]
GO
ALTER TABLE [dbo].[Reviews]  WITH CHECK ADD  CONSTRAINT [FK_Reviews_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Reviews] CHECK CONSTRAINT [FK_Reviews_Users_UserId]
GO
ALTER TABLE [dbo].[RolePermissions]  WITH CHECK ADD  CONSTRAINT [FK_RolePermissions_Permissions_PermissionId] FOREIGN KEY([PermissionId])
REFERENCES [dbo].[Permissions] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[RolePermissions] CHECK CONSTRAINT [FK_RolePermissions_Permissions_PermissionId]
GO
ALTER TABLE [dbo].[RolePermissions]  WITH CHECK ADD  CONSTRAINT [FK_RolePermissions_Roles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[RolePermissions] CHECK CONSTRAINT [FK_RolePermissions_Roles_RoleId]
GO
ALTER TABLE [dbo].[UserAuthInfos]  WITH CHECK ADD  CONSTRAINT [FK_UserAuthInfos_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[UserAuthInfos] CHECK CONSTRAINT [FK_UserAuthInfos_Users_UserId]
GO
ALTER TABLE [dbo].[UserRoles]  WITH CHECK ADD  CONSTRAINT [FK_UserRoles_Roles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserRoles] CHECK CONSTRAINT [FK_UserRoles_Roles_RoleId]
GO
ALTER TABLE [dbo].[UserRoles]  WITH CHECK ADD  CONSTRAINT [FK_UserRoles_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserRoles] CHECK CONSTRAINT [FK_UserRoles_Users_UserId]
GO
ALTER TABLE [dbo].[WishLists]  WITH CHECK ADD  CONSTRAINT [FK_WishLists_ColorCapacities_ColorCapacityId] FOREIGN KEY([ColorCapacityId])
REFERENCES [dbo].[ColorCapacities] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[WishLists] CHECK CONSTRAINT [FK_WishLists_ColorCapacities_ColorCapacityId]
GO
ALTER TABLE [dbo].[WishLists]  WITH CHECK ADD  CONSTRAINT [FK_WishLists_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[WishLists] CHECK CONSTRAINT [FK_WishLists_Users_UserId]
GO
USE [master]
GO
ALTER DATABASE [antraz_store_db2] SET  READ_WRITE 
GO
