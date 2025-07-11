USE [master]
GO
/****** Object:  Database [AntrazShop]    Script Date: 2025-06-27 20:33:47 ******/
CREATE DATABASE [AntrazShop]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'AntrazShop', FILENAME = N'D:\TL\MySQLsever\MSSQL16.SQLEXPRESS\MSSQL\DATA\AntrazShop.mdf' , SIZE = 73728KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'AntrazShop_log', FILENAME = N'D:\TL\MySQLsever\MSSQL16.SQLEXPRESS\MSSQL\DATA\AntrazShop_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [AntrazShop] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [AntrazShop].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [AntrazShop] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [AntrazShop] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [AntrazShop] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [AntrazShop] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [AntrazShop] SET ARITHABORT OFF 
GO
ALTER DATABASE [AntrazShop] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [AntrazShop] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [AntrazShop] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [AntrazShop] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [AntrazShop] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [AntrazShop] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [AntrazShop] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [AntrazShop] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [AntrazShop] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [AntrazShop] SET  DISABLE_BROKER 
GO
ALTER DATABASE [AntrazShop] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [AntrazShop] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [AntrazShop] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [AntrazShop] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [AntrazShop] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [AntrazShop] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [AntrazShop] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [AntrazShop] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [AntrazShop] SET  MULTI_USER 
GO
ALTER DATABASE [AntrazShop] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [AntrazShop] SET DB_CHAINING OFF 
GO
ALTER DATABASE [AntrazShop] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [AntrazShop] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [AntrazShop] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [AntrazShop] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [AntrazShop] SET QUERY_STORE = ON
GO
ALTER DATABASE [AntrazShop] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [AntrazShop]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 2025-06-27 20:33:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Brands]    Script Date: 2025-06-27 20:33:47 ******/
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
/****** Object:  Table [dbo].[Capacities]    Script Date: 2025-06-27 20:33:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Capacities](
	[Id] [int] IDENTITY(1000,1) NOT NULL,
	[Value] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Capacities] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Carts]    Script Date: 2025-06-27 20:33:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Carts](
	[UserId] [int] NOT NULL,
	[ColorCapacityId] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
	[Total] [decimal](18, 2) NOT NULL,
	[Id] [int] NOT NULL,
 CONSTRAINT [PK_Carts] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[ColorCapacityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 2025-06-27 20:33:47 ******/
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
/****** Object:  Table [dbo].[ColorCapacities]    Script Date: 2025-06-27 20:33:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ColorCapacities](
	[Id] [int] IDENTITY(100000,1) NOT NULL,
	[Stock] [int] NOT NULL,
	[Price] [decimal](18, 2) NOT NULL,
	[ColorId] [int] NOT NULL,
	[CapacityId] [int] NOT NULL,
	[Image] [nvarchar](255) NOT NULL,
	[Status] [int] NOT NULL,
	[ProductId] [int] NOT NULL,
	[SoldAmount] [int] NOT NULL,
	[StatusImage] [bit] NOT NULL,
	[CreateAt] [datetime2](7) NULL,
 CONSTRAINT [PK_ColorCapacities] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Colors]    Script Date: 2025-06-27 20:33:47 ******/
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
/****** Object:  Table [dbo].[LoginHistories]    Script Date: 2025-06-27 20:33:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LoginHistories](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[StatusLogin] [nvarchar](50) NOT NULL,
	[IPAddress] [nvarchar](100) NOT NULL,
	[Time] [datetime2](0) NOT NULL,
 CONSTRAINT [PK_LoginHistories] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderDetails]    Script Date: 2025-06-27 20:33:47 ******/
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
/****** Object:  Table [dbo].[Orders]    Script Date: 2025-06-27 20:33:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[OrderCode] [uniqueidentifier] NOT NULL,
	[UserId] [int] NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[Status] [int] NOT NULL,
 CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED 
(
	[OrderCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Permissions]    Script Date: 2025-06-27 20:33:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Permissions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](255) NOT NULL,
	[NameController] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Permissions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductImages]    Script Date: 2025-06-27 20:33:47 ******/
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
/****** Object:  Table [dbo].[Products]    Script Date: 2025-06-27 20:33:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[Id] [int] IDENTITY(1000,1) NOT NULL,
	[Name] [nvarchar](150) NOT NULL,
	[Description] [nvarchar](1000) NOT NULL,
	[ImageView] [nvarchar](255) NOT NULL,
	[BrandId] [int] NOT NULL,
	[CategoryId] [int] NOT NULL,
	[DiscountAmount] [decimal](18, 2) NOT NULL,
	[ImageFolder] [nvarchar](255) NOT NULL,
	[SaleId] [int] NULL,
	[CreateAt] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Reviews]    Script Date: 2025-06-27 20:33:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Reviews](
	[UserId] [int] NOT NULL,
	[ColorCapacityId] [int] NOT NULL,
	[Description] [nvarchar](1000) NOT NULL,
	[Rating] [float] NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[Id] [int] NOT NULL,
 CONSTRAINT [PK_Reviews] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[ColorCapacityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RolePermissions]    Script Date: 2025-06-27 20:33:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RolePermissions](
	[RoleId] [int] NOT NULL,
	[PermissionId] [int] NOT NULL,
	[Id] [int] NOT NULL,
 CONSTRAINT [PK_RolePermissions] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC,
	[PermissionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 2025-06-27 20:33:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](255) NOT NULL,
	[CreateAt] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sales]    Script Date: 2025-06-27 20:33:47 ******/
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
/****** Object:  Table [dbo].[UserAuthInfos]    Script Date: 2025-06-27 20:33:47 ******/
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
/****** Object:  Table [dbo].[UserPermissions]    Script Date: 2025-06-27 20:33:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserPermissions](
	[UserId] [int] NOT NULL,
	[PermissionId] [int] NOT NULL,
	[Id] [int] NOT NULL,
 CONSTRAINT [PK_UserPermissions] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[PermissionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserRoles]    Script Date: 2025-06-27 20:33:47 ******/
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
/****** Object:  Table [dbo].[Users]    Script Date: 2025-06-27 20:33:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1000,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
	[PasswordHash] [nvarchar](255) NOT NULL,
	[PhoneNumber] [nvarchar](20) NOT NULL,
	[Address] [nvarchar](255) NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[Gender] [nvarchar](10) NOT NULL,
	[Avatar] [nvarchar](255) NULL,
	[Birthday] [date] NOT NULL,
	[Hometown] [nvarchar](100) NULL,
	[workerAccount] [bit] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WishLists]    Script Date: 2025-06-27 20:33:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WishLists](
	[UserId] [int] NOT NULL,
	[ColorCapacityId] [int] NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[Id] [int] NOT NULL,
 CONSTRAINT [PK_WishLists] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[ColorCapacityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250309054209_CreateDatabase', N'8.0.13')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250312164405_editProduct', N'8.0.13')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250313163517_addstockproduct', N'8.0.13')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250313164011_addstockproduct2', N'8.0.13')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250317092228_phanquyen', N'8.0.13')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250317112620_genderUser', N'8.0.13')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250317201231_deleteRoleTableUser', N'8.0.13')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250318091112_updadatePhanQuyen', N'8.0.13')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250322142217_logobrandcategory', N'8.0.13')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250330161030_UpdateModelNN', N'8.0.13')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250401104012_AddAvataUser', N'8.0.13')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250401111212_DeleteControllerActionsPermissions', N'8.0.13')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250402055252_AddColorAndCapacityToProduct', N'8.0.13')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250402070620_a', N'8.0.13')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250402083256_AddNameColor', N'8.0.13')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250402092213_AddTableColorCapacity', N'8.0.13')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250402093447_ProductColorCapacity', N'8.0.13')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250402095433_UpdateCapacityTable', N'8.0.13')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250403073941_AddColorCapaciyTabeDbset', N'8.0.13')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250424125944_AddFolderChoProduct', N'8.0.13')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250516035319_BoSungUserVaProductCC', N'8.0.13')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250624105930_AddAuthAcount', N'8.0.13')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250625104359_AddSaleTable', N'8.0.13')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250626145110_EditTableCC', N'8.0.13')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250626150530_AddValidateTable', N'8.0.13')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250626195645_AddCreateAt', N'8.0.13')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250627024450_EditTableCartTotal', N'8.0.13')
GO
SET IDENTITY_INSERT [dbo].[Brands] ON 

INSERT [dbo].[Brands] ([Id], [Name], [Description], [Logo], [CreateAt]) VALUES (1, N'Apple', N'Apple Inc. là một Tập đoàn công nghệ đa quốc gia của Mỹ có trụ sở chính tại Cupertino, California, chuyên Thiết kế, phát triển và bán thiết bị điện tử tiêu dùng, phần mềm máy tính và các dịch vụ trực tuyến. Nó được coi là một trong năm công ty lớn của ngành công nghệ thông tin Hoa Kỳ ', N'', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Brands] ([Id], [Name], [Description], [Logo], [CreateAt]) VALUES (2, N'Samsung', N'Tập đoàn Samsung[5] hay Samsung (Tiếng Hàn: 삼성, Romaja: Samseong, Hanja: 三星; Hán-Việt: Tam Tinh - 3 ngôi sao) là một tập đoàn đa quốc gia của Hàn Quốc có trụ sở chính đặt tại Samsung Town, Seocho, Seoul. Tập đoàn sở hữu rất nhiều công ty con, chuỗi hệ thống bán hàng cùng các văn phòng đại diện trên toàn cầu hoạt động dưới tên thương hiệu mẹ. Đây là một trong những thương hiệu công nghệ lớn nhất thế giới.', N'', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Brands] ([Id], [Name], [Description], [Logo], [CreateAt]) VALUES (3, N'Dell', N'Dell Inc. là một công ty công nghệ có trụ sở tại Hoa Kỳ. Công ty phát triển, bán hàng, sửa chữa và hỗ trợ máy tính và các sản phẩm và dịch vụ liên quan. Dell thuộc sở hữu của công ty mẹ Dell Technologies', N'', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Brands] ([Id], [Name], [Description], [Logo], [CreateAt]) VALUES (4, N'Sony', N'Tập đoàn Sony của Nhật Bản là một trong những công ty giải trí và công nghệ hàng đầu thế giới, chuyên sản xuất điện tử tiêu dùng, PlayStation, camera và thiết bị âm thanh chất lượng cao.', NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Brands] ([Id], [Name], [Description], [Logo], [CreateAt]) VALUES (5, N'Xiaomi', N'Xiaomi là công ty công nghệ Trung Quốc chuyên sản xuất smartphone giá rẻ chất lượng cao, laptop, smart home và hệ sinh thái IoT phong phú với triết lý ''Innovation for everyone''', NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Brands] ([Id], [Name], [Description], [Logo], [CreateAt]) VALUES (6, N'Acer', N'Acer Inc. là tập đoàn công nghệ đa quốc gia của Đài Loan, chuyên sản xuất laptop, máy tính để bàn, màn hình gaming và thiết bị di động. Nổi tiếng với dòng Predator gaming, Swift mỏng nhẹ và Aspire phổ thông với chất lượng ổn định, giá cả hợp lý.', NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Brands] ([Id], [Name], [Description], [Logo], [CreateAt]) VALUES (7, N'Lenovo', N'Lenovo Group là công ty máy tính lớn nhất thế giới đến từ Trung Quốc, nổi tiếng với dòng ThinkPad doanh nghiệp, IdeaPad và Legion gaming với thiết kế bền bỉ.', NULL, CAST(N'2025-06-27T07:35:32.6810166' AS DateTime2))
INSERT [dbo].[Brands] ([Id], [Name], [Description], [Logo], [CreateAt]) VALUES (8, N'Asus', N'ASUSTeK Computer Inc. của Đài Loan là nhà sản xuất mainboard, laptop gaming và thiết bị mạng hàng đầu, nổi tiếng với dòng ROG gaming và ZenBook cao cấp.', NULL, CAST(N'2025-06-27T07:35:54.7843826' AS DateTime2))
INSERT [dbo].[Brands] ([Id], [Name], [Description], [Logo], [CreateAt]) VALUES (9, N'Logitech', N'Logitech International là công ty thiết bị ngoại vi hàng đầu từ Thụy Sĩ, chuyên sản xuất chuột máy tính, bàn phím, webcam và tai nghe chất lượng cao. Nổi tiếng với dòng chuột gaming G Series, chuột văn phòng MX và các giải pháp không dây tiên tiến.', NULL, CAST(N'2025-06-27T07:36:47.3845966' AS DateTime2))
SET IDENTITY_INSERT [dbo].[Brands] OFF
GO
SET IDENTITY_INSERT [dbo].[Capacities] ON 

INSERT [dbo].[Capacities] ([Id], [Value]) VALUES (1008, N'128GB')
INSERT [dbo].[Capacities] ([Id], [Value]) VALUES (1009, N'256GB')
INSERT [dbo].[Capacities] ([Id], [Value]) VALUES (1010, N'64GB')
INSERT [dbo].[Capacities] ([Id], [Value]) VALUES (1011, N'2222')
INSERT [dbo].[Capacities] ([Id], [Value]) VALUES (1012, N'1')
INSERT [dbo].[Capacities] ([Id], [Value]) VALUES (1013, N'100Gb')
INSERT [dbo].[Capacities] ([Id], [Value]) VALUES (1014, N'100')
INSERT [dbo].[Capacities] ([Id], [Value]) VALUES (1015, N'128GBB')
INSERT [dbo].[Capacities] ([Id], [Value]) VALUES (1016, N'string')
INSERT [dbo].[Capacities] ([Id], [Value]) VALUES (1017, N'string1')
INSERT [dbo].[Capacities] ([Id], [Value]) VALUES (1018, N'string123123')
INSERT [dbo].[Capacities] ([Id], [Value]) VALUES (1019, N'156GB')
INSERT [dbo].[Capacities] ([Id], [Value]) VALUES (1020, N'123')
INSERT [dbo].[Capacities] ([Id], [Value]) VALUES (1021, N'strin123g')
INSERT [dbo].[Capacities] ([Id], [Value]) VALUES (1022, N'123456')
INSERT [dbo].[Capacities] ([Id], [Value]) VALUES (1023, N'512GB')
INSERT [dbo].[Capacities] ([Id], [Value]) VALUES (1024, N'2')
INSERT [dbo].[Capacities] ([Id], [Value]) VALUES (1025, N'16GB/256GB')
INSERT [dbo].[Capacities] ([Id], [Value]) VALUES (1026, N'16GB/512GB')
INSERT [dbo].[Capacities] ([Id], [Value]) VALUES (1027, N'23')
INSERT [dbo].[Capacities] ([Id], [Value]) VALUES (1028, N'131231231')
INSERT [dbo].[Capacities] ([Id], [Value]) VALUES (1029, N'123123')
INSERT [dbo].[Capacities] ([Id], [Value]) VALUES (1030, N'12331231')
INSERT [dbo].[Capacities] ([Id], [Value]) VALUES (1031, N'123213')
INSERT [dbo].[Capacities] ([Id], [Value]) VALUES (1032, N'22')
INSERT [dbo].[Capacities] ([Id], [Value]) VALUES (1033, N'222')
INSERT [dbo].[Capacities] ([Id], [Value]) VALUES (1034, N'asdasdas')
INSERT [dbo].[Capacities] ([Id], [Value]) VALUES (1035, N'de1 ')
INSERT [dbo].[Capacities] ([Id], [Value]) VALUES (1036, N'd123123213')
INSERT [dbo].[Capacities] ([Id], [Value]) VALUES (1037, N'qweqwe')
SET IDENTITY_INSERT [dbo].[Capacities] OFF
GO
INSERT [dbo].[Carts] ([UserId], [ColorCapacityId], [Quantity], [Total], [Id]) VALUES (1010, 100061, 3, CAST(333333.00 AS Decimal(18, 2)), 0)
INSERT [dbo].[Carts] ([UserId], [ColorCapacityId], [Quantity], [Total], [Id]) VALUES (1010, 100067, 2, CAST(3600000.00 AS Decimal(18, 2)), 0)
GO
SET IDENTITY_INSERT [dbo].[Categories] ON 

INSERT [dbo].[Categories] ([Id], [Name], [Description], [Image], [CreateAt]) VALUES (1, N'Smart Phone', N'Điện thoại di động', N'', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Categories] ([Id], [Name], [Description], [Image], [CreateAt]) VALUES (2, N'Laptop', N'Máy tính xách tay', N'', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Categories] ([Id], [Name], [Description], [Image], [CreateAt]) VALUES (4, N'Tai nghe', N'string', NULL, CAST(N'2025-06-27T07:38:02.5878904' AS DateTime2))
INSERT [dbo].[Categories] ([Id], [Name], [Description], [Image], [CreateAt]) VALUES (5, N'Phụ kiện điện thoại', N'Test', NULL, CAST(N'2025-06-27T07:38:10.0207836' AS DateTime2))
INSERT [dbo].[Categories] ([Id], [Name], [Description], [Image], [CreateAt]) VALUES (6, N'Gaming Gear', N'Test', NULL, CAST(N'2025-06-27T07:38:31.0798492' AS DateTime2))
INSERT [dbo].[Categories] ([Id], [Name], [Description], [Image], [CreateAt]) VALUES (7, N'Âm thanh', N'Test', NULL, CAST(N'2025-06-27T07:38:40.7443436' AS DateTime2))
INSERT [dbo].[Categories] ([Id], [Name], [Description], [Image], [CreateAt]) VALUES (8, N'Sạc & Cáp', N'Test', NULL, CAST(N'2025-06-27T07:39:31.3791093' AS DateTime2))
INSERT [dbo].[Categories] ([Id], [Name], [Description], [Image], [CreateAt]) VALUES (9, N'PC & Linh kiện', N'Test', NULL, CAST(N'2025-06-27T07:40:10.9582722' AS DateTime2))
INSERT [dbo].[Categories] ([Id], [Name], [Description], [Image], [CreateAt]) VALUES (10, N'Màn hình & Phụ kiện', N'Test', NULL, CAST(N'2025-06-27T07:40:18.5357025' AS DateTime2))
SET IDENTITY_INSERT [dbo].[Categories] OFF
GO
SET IDENTITY_INSERT [dbo].[ColorCapacities] ON 

INSERT [dbo].[ColorCapacities] ([Id], [Stock], [Price], [ColorId], [CapacityId], [Image], [Status], [ProductId], [SoldAmount], [StatusImage], [CreateAt]) VALUES (100061, 12, CAST(111111.00 AS Decimal(18, 2)), 1009, 1008, N'trang_128gb.png', 1, 1069, 0, 0, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[ColorCapacities] ([Id], [Stock], [Price], [ColorId], [CapacityId], [Image], [Status], [ProductId], [SoldAmount], [StatusImage], [CreateAt]) VALUES (100062, 3, CAST(3500000.00 AS Decimal(18, 2)), 1008, 1009, N'den_256gb.png', 1, 1070, 0, 0, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[ColorCapacities] ([Id], [Stock], [Price], [ColorId], [CapacityId], [Image], [Status], [ProductId], [SoldAmount], [StatusImage], [CreateAt]) VALUES (100063, 3, CAST(123.00 AS Decimal(18, 2)), 1032, 1008, N'tims_128gb.png', 1, 1071, 0, 0, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[ColorCapacities] ([Id], [Stock], [Price], [ColorId], [CapacityId], [Image], [Status], [ProductId], [SoldAmount], [StatusImage], [CreateAt]) VALUES (100064, 4, CAST(3200000.00 AS Decimal(18, 2)), 1015, 1008, N'tim_128gb.png', 1, 1072, 0, 0, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[ColorCapacities] ([Id], [Stock], [Price], [ColorId], [CapacityId], [Image], [Status], [ProductId], [SoldAmount], [StatusImage], [CreateAt]) VALUES (100065, 3, CAST(1150000.00 AS Decimal(18, 2)), 1033, 1023, N'hong_512gb.png', 1, 1073, 0, 0, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[ColorCapacities] ([Id], [Stock], [Price], [ColorId], [CapacityId], [Image], [Status], [ProductId], [SoldAmount], [StatusImage], [CreateAt]) VALUES (100066, 4, CAST(1450000.00 AS Decimal(18, 2)), 1034, 1024, N'3_2.png', 1, 1074, 0, 0, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[ColorCapacities] ([Id], [Stock], [Price], [ColorId], [CapacityId], [Image], [Status], [ProductId], [SoldAmount], [StatusImage], [CreateAt]) VALUES (100067, 2, CAST(1800000.00 AS Decimal(18, 2)), 1008, 1025, N'den_16gb256gb.png', 1, 1075, 0, 0, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[ColorCapacities] ([Id], [Stock], [Price], [ColorId], [CapacityId], [Image], [Status], [ProductId], [SoldAmount], [StatusImage], [CreateAt]) VALUES (100068, 4, CAST(21000000.00 AS Decimal(18, 2)), 1008, 1026, N'den_16gb512gb.png', 1, 1076, 0, 0, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[ColorCapacities] ([Id], [Stock], [Price], [ColorId], [CapacityId], [Image], [Status], [ProductId], [SoldAmount], [StatusImage], [CreateAt]) VALUES (100069, 4, CAST(7500000.00 AS Decimal(18, 2)), 1008, 1009, N'den_256gb.png', 1, 1077, 0, 0, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[ColorCapacities] ([Id], [Stock], [Price], [ColorId], [CapacityId], [Image], [Status], [ProductId], [SoldAmount], [StatusImage], [CreateAt]) VALUES (100070, 111, CAST(333.00 AS Decimal(18, 2)), 1035, 1027, N'123213_23.png', 1, 1078, 0, 0, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[ColorCapacities] ([Id], [Stock], [Price], [ColorId], [CapacityId], [Image], [Status], [ProductId], [SoldAmount], [StatusImage], [CreateAt]) VALUES (100071, 111, CAST(1222.00 AS Decimal(18, 2)), 1036, 1028, N'123123_131231231.png', 1, 1075, 0, 0, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[ColorCapacities] ([Id], [Stock], [Price], [ColorId], [CapacityId], [Image], [Status], [ProductId], [SoldAmount], [StatusImage], [CreateAt]) VALUES (100072, 2, CAST(2.00 AS Decimal(18, 2)), 1037, 1024, N'2_2.png', 1, 1079, 0, 0, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[ColorCapacities] ([Id], [Stock], [Price], [ColorId], [CapacityId], [Image], [Status], [ProductId], [SoldAmount], [StatusImage], [CreateAt]) VALUES (100073, 2, CAST(1.00 AS Decimal(18, 2)), 1037, 1024, N'2_2.png', 1, 1079, 0, 0, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[ColorCapacities] ([Id], [Stock], [Price], [ColorId], [CapacityId], [Image], [Status], [ProductId], [SoldAmount], [StatusImage], [CreateAt]) VALUES (100074, 11, CAST(12212.00 AS Decimal(18, 2)), 1036, 1029, N'123123_123123.png', 1, 1080, 0, 0, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[ColorCapacities] ([Id], [Stock], [Price], [ColorId], [CapacityId], [Image], [Status], [ProductId], [SoldAmount], [StatusImage], [CreateAt]) VALUES (100075, 123, CAST(123123.00 AS Decimal(18, 2)), 1036, 1030, N'123123_12331231.png', 1, 1080, 0, 0, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[ColorCapacities] ([Id], [Stock], [Price], [ColorId], [CapacityId], [Image], [Status], [ProductId], [SoldAmount], [StatusImage], [CreateAt]) VALUES (100077, 2, CAST(3500000.00 AS Decimal(18, 2)), 1009, 1010, N'trang_64gb.png', 1, 1082, 0, 0, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[ColorCapacities] ([Id], [Stock], [Price], [ColorId], [CapacityId], [Image], [Status], [ProductId], [SoldAmount], [StatusImage], [CreateAt]) VALUES (100078, 22, CAST(3500000.00 AS Decimal(18, 2)), 1008, 1008, N'den_128gb.png', 1, 1083, 0, 0, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[ColorCapacities] ([Id], [Stock], [Price], [ColorId], [CapacityId], [Image], [Status], [ProductId], [SoldAmount], [StatusImage], [CreateAt]) VALUES (100082, 123, CAST(123.00 AS Decimal(18, 2)), 1041, 1034, N'asdasd_asdasdas.png', 1, 1086, 0, 0, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[ColorCapacities] ([Id], [Stock], [Price], [ColorId], [CapacityId], [Image], [Status], [ProductId], [SoldAmount], [StatusImage], [CreateAt]) VALUES (100085, 3123123, CAST(12312321.00 AS Decimal(18, 2)), 1043, 1036, N'asdasdas_d123123213.jpg', 1, 1091, 0, 0, CAST(N'2025-06-27T05:21:13.9925283' AS DateTime2))
INSERT [dbo].[ColorCapacities] ([Id], [Stock], [Price], [ColorId], [CapacityId], [Image], [Status], [ProductId], [SoldAmount], [StatusImage], [CreateAt]) VALUES (100086, 123, CAST(123123.00 AS Decimal(18, 2)), 1009, 1037, N'trang_qweqwe.jpg', 1, 1092, 0, 0, CAST(N'2025-06-27T05:27:54.9029100' AS DateTime2))
INSERT [dbo].[ColorCapacities] ([Id], [Stock], [Price], [ColorId], [CapacityId], [Image], [Status], [ProductId], [SoldAmount], [StatusImage], [CreateAt]) VALUES (100087, 123213, CAST(123.00 AS Decimal(18, 2)), 1009, 1008, N'trang_128gb.png', 1, 1092, 0, 0, CAST(N'2025-06-27T05:28:26.5284933' AS DateTime2))
INSERT [dbo].[ColorCapacities] ([Id], [Stock], [Price], [ColorId], [CapacityId], [Image], [Status], [ProductId], [SoldAmount], [StatusImage], [CreateAt]) VALUES (100088, 10, CAST(31500000.00 AS Decimal(18, 2)), 1013, 1023, N'gold_512gb.png', 1, 1093, 0, 0, CAST(N'2025-06-27T08:44:25.6212831' AS DateTime2))
INSERT [dbo].[ColorCapacities] ([Id], [Stock], [Price], [ColorId], [CapacityId], [Image], [Status], [ProductId], [SoldAmount], [StatusImage], [CreateAt]) VALUES (100089, 12, CAST(3500000.00 AS Decimal(18, 2)), 1008, 1008, N'den_128gb.png', 1, 1093, 0, 0, CAST(N'2025-06-27T08:52:50.1146779' AS DateTime2))
SET IDENTITY_INSERT [dbo].[ColorCapacities] OFF
GO
SET IDENTITY_INSERT [dbo].[Colors] ON 

INSERT [dbo].[Colors] ([Id], [NameColor]) VALUES (1008, N'Đen')
INSERT [dbo].[Colors] ([Id], [NameColor]) VALUES (1009, N'Trắng')
INSERT [dbo].[Colors] ([Id], [NameColor]) VALUES (1010, N'Vàng')
INSERT [dbo].[Colors] ([Id], [NameColor]) VALUES (1011, N'1')
INSERT [dbo].[Colors] ([Id], [NameColor]) VALUES (1012, N'Đỏ')
INSERT [dbo].[Colors] ([Id], [NameColor]) VALUES (1013, N'Gold')
INSERT [dbo].[Colors] ([Id], [NameColor]) VALUES (1014, N'Xanh lá')
INSERT [dbo].[Colors] ([Id], [NameColor]) VALUES (1015, N'Tím')
INSERT [dbo].[Colors] ([Id], [NameColor]) VALUES (1016, N'Trắng2')
INSERT [dbo].[Colors] ([Id], [NameColor]) VALUES (1017, N'Trắng 3')
INSERT [dbo].[Colors] ([Id], [NameColor]) VALUES (1018, N'Test 1')
INSERT [dbo].[Colors] ([Id], [NameColor]) VALUES (1019, N'string')
INSERT [dbo].[Colors] ([Id], [NameColor]) VALUES (1020, N'string1')
INSERT [dbo].[Colors] ([Id], [NameColor]) VALUES (1021, N'string1231')
INSERT [dbo].[Colors] ([Id], [NameColor]) VALUES (1022, N'Đỏ1')
INSERT [dbo].[Colors] ([Id], [NameColor]) VALUES (1023, N'Đen1')
INSERT [dbo].[Colors] ([Id], [NameColor]) VALUES (1024, N'Đen5')
INSERT [dbo].[Colors] ([Id], [NameColor]) VALUES (1025, N'Den5')
INSERT [dbo].[Colors] ([Id], [NameColor]) VALUES (1026, N'Đến123')
INSERT [dbo].[Colors] ([Id], [NameColor]) VALUES (1027, N'Đỏ123')
INSERT [dbo].[Colors] ([Id], [NameColor]) VALUES (1028, N'Đỏ1234')
INSERT [dbo].[Colors] ([Id], [NameColor]) VALUES (1029, N'123')
INSERT [dbo].[Colors] ([Id], [NameColor]) VALUES (1030, N'DDen')
INSERT [dbo].[Colors] ([Id], [NameColor]) VALUES (1031, N'Xanh ngọc')
INSERT [dbo].[Colors] ([Id], [NameColor]) VALUES (1032, N'Tims')
INSERT [dbo].[Colors] ([Id], [NameColor]) VALUES (1033, N'Hồng')
INSERT [dbo].[Colors] ([Id], [NameColor]) VALUES (1034, N'3')
INSERT [dbo].[Colors] ([Id], [NameColor]) VALUES (1035, N'123213')
INSERT [dbo].[Colors] ([Id], [NameColor]) VALUES (1036, N'123123')
INSERT [dbo].[Colors] ([Id], [NameColor]) VALUES (1037, N'2')
INSERT [dbo].[Colors] ([Id], [NameColor]) VALUES (1038, N'dd')
INSERT [dbo].[Colors] ([Id], [NameColor]) VALUES (1039, N'w2q')
INSERT [dbo].[Colors] ([Id], [NameColor]) VALUES (1040, N'11')
INSERT [dbo].[Colors] ([Id], [NameColor]) VALUES (1041, N'asdasd')
INSERT [dbo].[Colors] ([Id], [NameColor]) VALUES (1042, N'qw dasd as')
INSERT [dbo].[Colors] ([Id], [NameColor]) VALUES (1043, N'asdasdas')
SET IDENTITY_INSERT [dbo].[Colors] OFF
GO
SET IDENTITY_INSERT [dbo].[LoginHistories] ON 

INSERT [dbo].[LoginHistories] ([Id], [UserId], [StatusLogin], [IPAddress], [Time]) VALUES (1, 1000, N'Thành công, Đăng nhập thành công!', N'192.168.0.193', CAST(N'2025-06-25T13:52:44.0000000' AS DateTime2))
INSERT [dbo].[LoginHistories] ([Id], [UserId], [StatusLogin], [IPAddress], [Time]) VALUES (2, 1000, N'Thất bại, Sai mật khẩu!', N'192.168.0.193', CAST(N'2025-06-25T13:52:57.0000000' AS DateTime2))
INSERT [dbo].[LoginHistories] ([Id], [UserId], [StatusLogin], [IPAddress], [Time]) VALUES (3, 1000, N'Thất bại, Sai mật khẩu!', N'192.168.0.193', CAST(N'2025-06-25T13:52:59.0000000' AS DateTime2))
INSERT [dbo].[LoginHistories] ([Id], [UserId], [StatusLogin], [IPAddress], [Time]) VALUES (4, 1000, N'Thất bại, Sai mật khẩu!', N'192.168.0.193', CAST(N'2025-06-25T13:53:02.0000000' AS DateTime2))
INSERT [dbo].[LoginHistories] ([Id], [UserId], [StatusLogin], [IPAddress], [Time]) VALUES (5, 1000, N'Thất bại, Sai mật khẩu!', N'192.168.0.193', CAST(N'2025-06-25T13:53:03.0000000' AS DateTime2))
INSERT [dbo].[LoginHistories] ([Id], [UserId], [StatusLogin], [IPAddress], [Time]) VALUES (6, 1000, N'Thất bại, Sai mật khẩu!', N'192.168.0.193', CAST(N'2025-06-25T13:53:05.0000000' AS DateTime2))
INSERT [dbo].[LoginHistories] ([Id], [UserId], [StatusLogin], [IPAddress], [Time]) VALUES (7, 1000, N'Thất bại! Tài khoản đang bị khoá', N'192.168.0.193', CAST(N'2025-06-25T13:53:08.0000000' AS DateTime2))
INSERT [dbo].[LoginHistories] ([Id], [UserId], [StatusLogin], [IPAddress], [Time]) VALUES (8, 1000, N'Thất bại! Tài khoản đang bị khoá', N'192.168.0.193', CAST(N'2025-06-25T13:53:10.0000000' AS DateTime2))
INSERT [dbo].[LoginHistories] ([Id], [UserId], [StatusLogin], [IPAddress], [Time]) VALUES (9, 1000, N'Thành công, Đăng nhập thành công!', N'192.168.0.193', CAST(N'2025-06-25T13:54:10.0000000' AS DateTime2))
INSERT [dbo].[LoginHistories] ([Id], [UserId], [StatusLogin], [IPAddress], [Time]) VALUES (10, 1000, N'Thành công, Đăng nhập thành công!', N'192.168.0.193', CAST(N'2025-06-25T14:26:57.0000000' AS DateTime2))
INSERT [dbo].[LoginHistories] ([Id], [UserId], [StatusLogin], [IPAddress], [Time]) VALUES (11, 1000, N'Thành công, Đăng nhập thành công!', N'192.168.0.193', CAST(N'2025-06-25T14:57:04.0000000' AS DateTime2))
INSERT [dbo].[LoginHistories] ([Id], [UserId], [StatusLogin], [IPAddress], [Time]) VALUES (12, 1000, N'Thành công, Đăng nhập thành công!', N'192.168.0.193', CAST(N'2025-06-25T16:38:46.0000000' AS DateTime2))
INSERT [dbo].[LoginHistories] ([Id], [UserId], [StatusLogin], [IPAddress], [Time]) VALUES (13, 1000, N'Thất bại, Sai mật khẩu!', N'192.168.0.193', CAST(N'2025-06-25T16:51:51.0000000' AS DateTime2))
INSERT [dbo].[LoginHistories] ([Id], [UserId], [StatusLogin], [IPAddress], [Time]) VALUES (14, 1000, N'Thất bại! Tài khoản đang bị khoá', N'192.168.0.193', CAST(N'2025-06-25T16:51:52.0000000' AS DateTime2))
INSERT [dbo].[LoginHistories] ([Id], [UserId], [StatusLogin], [IPAddress], [Time]) VALUES (15, 1000, N'Thất bại! Tài khoản đang bị khoá', N'192.168.0.193', CAST(N'2025-06-25T16:51:58.0000000' AS DateTime2))
INSERT [dbo].[LoginHistories] ([Id], [UserId], [StatusLogin], [IPAddress], [Time]) VALUES (16, 1000, N'Thành công, Đăng nhập thành công!', N'192.168.0.193', CAST(N'2025-06-25T17:05:58.0000000' AS DateTime2))
INSERT [dbo].[LoginHistories] ([Id], [UserId], [StatusLogin], [IPAddress], [Time]) VALUES (17, 1000, N'Thành công, Đăng nhập thành công!', N'192.168.0.193', CAST(N'2025-06-25T18:10:17.0000000' AS DateTime2))
INSERT [dbo].[LoginHistories] ([Id], [UserId], [StatusLogin], [IPAddress], [Time]) VALUES (18, 1000, N'Thành công, Đăng nhập thành công!', N'192.168.0.193', CAST(N'2025-06-25T21:29:58.0000000' AS DateTime2))
INSERT [dbo].[LoginHistories] ([Id], [UserId], [StatusLogin], [IPAddress], [Time]) VALUES (19, 1000, N'Thành công, Đăng nhập thành công!', N'192.168.0.193', CAST(N'2025-06-25T21:29:58.0000000' AS DateTime2))
INSERT [dbo].[LoginHistories] ([Id], [UserId], [StatusLogin], [IPAddress], [Time]) VALUES (20, 1000, N'Thành công, Đăng nhập thành công!', N'192.168.0.193', CAST(N'2025-06-26T01:21:29.0000000' AS DateTime2))
INSERT [dbo].[LoginHistories] ([Id], [UserId], [StatusLogin], [IPAddress], [Time]) VALUES (21, 1000, N'Thành công, Đăng nhập thành công!', N'192.168.0.193', CAST(N'2025-06-26T21:44:04.0000000' AS DateTime2))
INSERT [dbo].[LoginHistories] ([Id], [UserId], [StatusLogin], [IPAddress], [Time]) VALUES (22, 1000, N'Thành công, Đăng nhập thành công!', N'192.168.0.193', CAST(N'2025-06-27T00:18:22.0000000' AS DateTime2))
INSERT [dbo].[LoginHistories] ([Id], [UserId], [StatusLogin], [IPAddress], [Time]) VALUES (23, 1000, N'Thành công, Đăng nhập thành công!', N'192.168.0.193', CAST(N'2025-06-27T00:52:35.0000000' AS DateTime2))
INSERT [dbo].[LoginHistories] ([Id], [UserId], [StatusLogin], [IPAddress], [Time]) VALUES (24, 1000, N'Thành công, Đăng nhập thành công!', N'192.168.0.193', CAST(N'2025-06-27T01:50:31.0000000' AS DateTime2))
INSERT [dbo].[LoginHistories] ([Id], [UserId], [StatusLogin], [IPAddress], [Time]) VALUES (25, 1000, N'Thành công, Đăng nhập thành công!', N'192.168.0.193', CAST(N'2025-06-27T02:20:51.0000000' AS DateTime2))
INSERT [dbo].[LoginHistories] ([Id], [UserId], [StatusLogin], [IPAddress], [Time]) VALUES (26, 1000, N'Thành công, Đăng nhập thành công!', N'192.168.0.193', CAST(N'2025-06-27T04:32:16.0000000' AS DateTime2))
INSERT [dbo].[LoginHistories] ([Id], [UserId], [StatusLogin], [IPAddress], [Time]) VALUES (27, 1000, N'Thành công, Đăng nhập thành công!', N'192.168.0.193', CAST(N'2025-06-27T05:02:37.0000000' AS DateTime2))
INSERT [dbo].[LoginHistories] ([Id], [UserId], [StatusLogin], [IPAddress], [Time]) VALUES (28, 1000, N'Thành công, Đăng nhập thành công!', N'192.168.0.193', CAST(N'2025-06-27T05:11:47.0000000' AS DateTime2))
INSERT [dbo].[LoginHistories] ([Id], [UserId], [StatusLogin], [IPAddress], [Time]) VALUES (29, 1000, N'Thành công, Đăng nhập thành công!', N'192.168.0.193', CAST(N'2025-06-27T05:35:49.0000000' AS DateTime2))
INSERT [dbo].[LoginHistories] ([Id], [UserId], [StatusLogin], [IPAddress], [Time]) VALUES (30, 1000, N'Thành công, Đăng nhập thành công!', N'192.168.0.193', CAST(N'2025-06-27T06:01:49.0000000' AS DateTime2))
INSERT [dbo].[LoginHistories] ([Id], [UserId], [StatusLogin], [IPAddress], [Time]) VALUES (31, 1010, N'Thành công, Đăng nhập thành công!', N'192.168.0.193', CAST(N'2025-06-27T06:01:55.0000000' AS DateTime2))
INSERT [dbo].[LoginHistories] ([Id], [UserId], [StatusLogin], [IPAddress], [Time]) VALUES (32, 1010, N'Thành công, Đăng nhập thành công!', N'192.168.0.193', CAST(N'2025-06-27T06:03:17.0000000' AS DateTime2))
INSERT [dbo].[LoginHistories] ([Id], [UserId], [StatusLogin], [IPAddress], [Time]) VALUES (33, 1010, N'Thành công, Đăng nhập thành công!', N'192.168.0.193', CAST(N'2025-06-27T06:04:30.0000000' AS DateTime2))
INSERT [dbo].[LoginHistories] ([Id], [UserId], [StatusLogin], [IPAddress], [Time]) VALUES (34, 1010, N'Thành công, Đăng nhập thành công!', N'192.168.0.193', CAST(N'2025-06-27T06:05:19.0000000' AS DateTime2))
INSERT [dbo].[LoginHistories] ([Id], [UserId], [StatusLogin], [IPAddress], [Time]) VALUES (35, 1010, N'Thành công, Đăng nhập thành công!', N'192.168.0.193', CAST(N'2025-06-27T06:07:56.0000000' AS DateTime2))
INSERT [dbo].[LoginHistories] ([Id], [UserId], [StatusLogin], [IPAddress], [Time]) VALUES (36, 1010, N'Thành công, Đăng nhập thành công!', N'192.168.0.193', CAST(N'2025-06-27T06:08:00.0000000' AS DateTime2))
INSERT [dbo].[LoginHistories] ([Id], [UserId], [StatusLogin], [IPAddress], [Time]) VALUES (37, 1010, N'Thành công, Đăng nhập thành công!', N'192.168.0.193', CAST(N'2025-06-27T06:08:16.0000000' AS DateTime2))
INSERT [dbo].[LoginHistories] ([Id], [UserId], [StatusLogin], [IPAddress], [Time]) VALUES (38, 1010, N'Thành công, Đăng nhập thành công!', N'192.168.0.193', CAST(N'2025-06-27T06:08:32.0000000' AS DateTime2))
INSERT [dbo].[LoginHistories] ([Id], [UserId], [StatusLogin], [IPAddress], [Time]) VALUES (39, 1010, N'Thành công, Đăng nhập thành công!', N'192.168.0.193', CAST(N'2025-06-27T06:08:47.0000000' AS DateTime2))
INSERT [dbo].[LoginHistories] ([Id], [UserId], [StatusLogin], [IPAddress], [Time]) VALUES (40, 1010, N'Thành công, Đăng nhập thành công!', N'192.168.0.193', CAST(N'2025-06-27T06:10:45.0000000' AS DateTime2))
INSERT [dbo].[LoginHistories] ([Id], [UserId], [StatusLogin], [IPAddress], [Time]) VALUES (41, 1010, N'Thành công, Đăng nhập thành công!', N'192.168.0.193', CAST(N'2025-06-27T06:14:17.0000000' AS DateTime2))
INSERT [dbo].[LoginHistories] ([Id], [UserId], [StatusLogin], [IPAddress], [Time]) VALUES (42, 1010, N'Thành công, Đăng nhập thành công!', N'192.168.0.193', CAST(N'2025-06-27T06:28:36.0000000' AS DateTime2))
INSERT [dbo].[LoginHistories] ([Id], [UserId], [StatusLogin], [IPAddress], [Time]) VALUES (43, 1010, N'Thành công, Đăng nhập thành công!', N'192.168.0.193', CAST(N'2025-06-27T06:36:48.0000000' AS DateTime2))
INSERT [dbo].[LoginHistories] ([Id], [UserId], [StatusLogin], [IPAddress], [Time]) VALUES (44, 1010, N'Thành công, Đăng nhập thành công!', N'192.168.0.193', CAST(N'2025-06-27T06:40:36.0000000' AS DateTime2))
INSERT [dbo].[LoginHistories] ([Id], [UserId], [StatusLogin], [IPAddress], [Time]) VALUES (45, 1010, N'Thành công, Đăng nhập thành công!', N'192.168.0.193', CAST(N'2025-06-27T06:40:40.0000000' AS DateTime2))
INSERT [dbo].[LoginHistories] ([Id], [UserId], [StatusLogin], [IPAddress], [Time]) VALUES (46, 1010, N'Thành công, Đăng nhập thành công!', N'192.168.0.193', CAST(N'2025-06-27T06:40:43.0000000' AS DateTime2))
INSERT [dbo].[LoginHistories] ([Id], [UserId], [StatusLogin], [IPAddress], [Time]) VALUES (47, 1010, N'Thành công, Đăng nhập thành công!', N'192.168.0.193', CAST(N'2025-06-27T06:41:09.0000000' AS DateTime2))
INSERT [dbo].[LoginHistories] ([Id], [UserId], [StatusLogin], [IPAddress], [Time]) VALUES (48, 1010, N'Thành công, Đăng nhập thành công!', N'192.168.0.193', CAST(N'2025-06-27T07:54:57.0000000' AS DateTime2))
INSERT [dbo].[LoginHistories] ([Id], [UserId], [StatusLogin], [IPAddress], [Time]) VALUES (49, 1000, N'Thành công, Đăng nhập thành công!', N'192.168.0.193', CAST(N'2025-06-27T07:55:03.0000000' AS DateTime2))
INSERT [dbo].[LoginHistories] ([Id], [UserId], [StatusLogin], [IPAddress], [Time]) VALUES (50, 1000, N'Thành công, Đăng nhập thành công!', N'192.168.0.193', CAST(N'2025-06-27T08:35:40.0000000' AS DateTime2))
INSERT [dbo].[LoginHistories] ([Id], [UserId], [StatusLogin], [IPAddress], [Time]) VALUES (51, 1000, N'Thành công, Đăng nhập thành công!', N'192.168.0.193', CAST(N'2025-06-27T09:19:08.0000000' AS DateTime2))
INSERT [dbo].[LoginHistories] ([Id], [UserId], [StatusLogin], [IPAddress], [Time]) VALUES (52, 1000, N'Thành công, Đăng nhập thành công!', N'192.168.0.193', CAST(N'2025-06-27T17:37:45.0000000' AS DateTime2))
INSERT [dbo].[LoginHistories] ([Id], [UserId], [StatusLogin], [IPAddress], [Time]) VALUES (53, 1000, N'Thành công, Đăng nhập thành công!', N'192.168.0.193', CAST(N'2025-06-27T18:09:51.0000000' AS DateTime2))
INSERT [dbo].[LoginHistories] ([Id], [UserId], [StatusLogin], [IPAddress], [Time]) VALUES (54, 1000, N'Thành công, Đăng nhập thành công!', N'192.168.0.193', CAST(N'2025-06-27T19:26:41.0000000' AS DateTime2))
INSERT [dbo].[LoginHistories] ([Id], [UserId], [StatusLogin], [IPAddress], [Time]) VALUES (55, 1000, N'Thành công, Đăng nhập thành công!', N'192.168.0.193', CAST(N'2025-06-27T19:29:14.0000000' AS DateTime2))
SET IDENTITY_INSERT [dbo].[LoginHistories] OFF
GO
SET IDENTITY_INSERT [dbo].[Permissions] ON 

INSERT [dbo].[Permissions] ([Id], [Name], [Description], [NameController]) VALUES (2, N'ViewProducts', N'Có thể xem được sản phẩm', N'ProductController')
INSERT [dbo].[Permissions] ([Id], [Name], [Description], [NameController]) VALUES (3, N'EditProducts', N'Có thể sửa được sản phẩm', N'ProductController')
INSERT [dbo].[Permissions] ([Id], [Name], [Description], [NameController]) VALUES (4, N'DeleteProducts', N'Xoá sản phẩm', N'ProductController')
INSERT [dbo].[Permissions] ([Id], [Name], [Description], [NameController]) VALUES (5, N'string', N'string', N'string')
INSERT [dbo].[Permissions] ([Id], [Name], [Description], [NameController]) VALUES (6, N'TestP1', N'123', N'TestController')
INSERT [dbo].[Permissions] ([Id], [Name], [Description], [NameController]) VALUES (7, N'TestP2', N'123', N'TestController')
INSERT [dbo].[Permissions] ([Id], [Name], [Description], [NameController]) VALUES (8, N'TestP3', N'123', N'TestController')
INSERT [dbo].[Permissions] ([Id], [Name], [Description], [NameController]) VALUES (9, N'TestP4', N'123', N'TestController')
INSERT [dbo].[Permissions] ([Id], [Name], [Description], [NameController]) VALUES (10, N'TestP5', N'123', N'TestController')
INSERT [dbo].[Permissions] ([Id], [Name], [Description], [NameController]) VALUES (11, N'GetUserWorkers', N'Lấy được danh sách tài khoản nhân viên', N'AccountManagerController')
SET IDENTITY_INSERT [dbo].[Permissions] OFF
GO
SET IDENTITY_INSERT [dbo].[Products] ON 

INSERT [dbo].[Products] ([Id], [Name], [Description], [ImageView], [BrandId], [CategoryId], [DiscountAmount], [ImageFolder], [SaleId], [CreateAt]) VALUES (1069, N'Iphone 8', N'asdas das asdasd', N'1.png', 1, 1, CAST(1.00 AS Decimal(18, 2)), N'iphone_8', NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Products] ([Id], [Name], [Description], [ImageView], [BrandId], [CategoryId], [DiscountAmount], [ImageFolder], [SaleId], [CreateAt]) VALUES (1070, N'Iphone XS', N'12312 3vvd d1 ', N'1.png', 1, 1, CAST(3.00 AS Decimal(18, 2)), N'iphone_xs', NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Products] ([Id], [Name], [Description], [ImageView], [BrandId], [CategoryId], [DiscountAmount], [ImageFolder], [SaleId], [CreateAt]) VALUES (1071, N'Iphone 11', N'eqwae ads dasd ', N'1.png', 1, 1, CAST(2.00 AS Decimal(18, 2)), N'iphone_11', NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Products] ([Id], [Name], [Description], [ImageView], [BrandId], [CategoryId], [DiscountAmount], [ImageFolder], [SaleId], [CreateAt]) VALUES (1072, N'Iphone 12', N'ád ád ádas dá d', N'1.png', 1, 1, CAST(3.00 AS Decimal(18, 2)), N'iphone_12', NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Products] ([Id], [Name], [Description], [ImageView], [BrandId], [CategoryId], [DiscountAmount], [ImageFolder], [SaleId], [CreateAt]) VALUES (1073, N'Iphone 13', N'á dá đâsdasdas', N'1.png', 1, 1, CAST(23.00 AS Decimal(18, 2)), N'iphone_13', NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Products] ([Id], [Name], [Description], [ImageView], [BrandId], [CategoryId], [DiscountAmount], [ImageFolder], [SaleId], [CreateAt]) VALUES (1074, N'Iphone 15', N'ád ád ádas đâs d', N'1.png', 1, 1, CAST(4.00 AS Decimal(18, 2)), N'iphone_15', NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Products] ([Id], [Name], [Description], [ImageView], [BrandId], [CategoryId], [DiscountAmount], [ImageFolder], [SaleId], [CreateAt]) VALUES (1075, N'Macbook Air 2020', N'12500000asdasd á đá', N'1.png', 1, 2, CAST(30.00 AS Decimal(18, 2)), N'macbook_air_2020', NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Products] ([Id], [Name], [Description], [ImageView], [BrandId], [CategoryId], [DiscountAmount], [ImageFolder], [SaleId], [CreateAt]) VALUES (1076, N'Dell Precision 7560 2021', N'd á56d 1as56 1d56as1d 56asd ád ', N'1.png', 3, 2, CAST(20.00 AS Decimal(18, 2)), N'dell_precision_7560_2021', NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Products] ([Id], [Name], [Description], [ImageView], [BrandId], [CategoryId], [DiscountAmount], [ImageFolder], [SaleId], [CreateAt]) VALUES (1077, N'Samsung Galaxy Z', N'đá á d1as23 d1as32d23as dá d', N'1.png', 2, 1, CAST(20.00 AS Decimal(18, 2)), N'samsung_galaxy_z', NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Products] ([Id], [Name], [Description], [ImageView], [BrandId], [CategoryId], [DiscountAmount], [ImageFolder], [SaleId], [CreateAt]) VALUES (1078, N'213213', N'asdasd asd asd', N'1.png', 1, 1, CAST(1231.00 AS Decimal(18, 2)), N'213213', NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Products] ([Id], [Name], [Description], [ImageView], [BrandId], [CategoryId], [DiscountAmount], [ImageFolder], [SaleId], [CreateAt]) VALUES (1079, N'Iphone 8ádasda', N'đá ádas d', N'1.png', 1, 1, CAST(222.00 AS Decimal(18, 2)), N'iphone_8adasda', NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Products] ([Id], [Name], [Description], [ImageView], [BrandId], [CategoryId], [DiscountAmount], [ImageFolder], [SaleId], [CreateAt]) VALUES (1080, N'qưeqwe', N'ádasdaádasd', N'1.png', 1, 2, CAST(2.00 AS Decimal(18, 2)), N'queqwe', NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Products] ([Id], [Name], [Description], [ImageView], [BrandId], [CategoryId], [DiscountAmount], [ImageFolder], [SaleId], [CreateAt]) VALUES (1082, N'zzz', N'asdasdasd', N'1.png', 1, 2, CAST(2.00 AS Decimal(18, 2)), N'zzz', NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Products] ([Id], [Name], [Description], [ImageView], [BrandId], [CategoryId], [DiscountAmount], [ImageFolder], [SaleId], [CreateAt]) VALUES (1083, N'Dta', N'aaaa', N'1.png', 2, 1, CAST(2.00 AS Decimal(18, 2)), N'dta', NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Products] ([Id], [Name], [Description], [ImageView], [BrandId], [CategoryId], [DiscountAmount], [ImageFolder], [SaleId], [CreateAt]) VALUES (1086, N'adsasdasd', N'asdasdasd asasdas das das ', N'1.png', 1, 1, CAST(123.00 AS Decimal(18, 2)), N'adsasdasd', NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Products] ([Id], [Name], [Description], [ImageView], [BrandId], [CategoryId], [DiscountAmount], [ImageFolder], [SaleId], [CreateAt]) VALUES (1091, N'Iphone 16 Pro Max', N'123123123123213213', N'1.png', 1, 1, CAST(123.00 AS Decimal(18, 2)), N'iphone_16_pro_max', NULL, CAST(N'2025-06-27T05:21:13.9411357' AS DateTime2))
INSERT [dbo].[Products] ([Id], [Name], [Description], [ImageView], [BrandId], [CategoryId], [DiscountAmount], [ImageFolder], [SaleId], [CreateAt]) VALUES (1092, N'asdasdas das da123213', N'qweqweqwe qweqweqweqwe', N'1.png', 1, 1, CAST(123.00 AS Decimal(18, 2)), N'asdasdas_das_da123213', NULL, CAST(N'2025-06-27T05:27:54.7392861' AS DateTime2))
INSERT [dbo].[Products] ([Id], [Name], [Description], [ImageView], [BrandId], [CategoryId], [DiscountAmount], [ImageFolder], [SaleId], [CreateAt]) VALUES (1093, N'Iphone 16 Pro Max 2', N'Iphone 16 Apple', N'1.png', 1, 1, CAST(2000000.00 AS Decimal(18, 2)), N'iphone_16_pro_max_2', NULL, CAST(N'2025-06-27T08:44:25.4774514' AS DateTime2))
SET IDENTITY_INSERT [dbo].[Products] OFF
GO
INSERT [dbo].[Reviews] ([UserId], [ColorCapacityId], [Description], [Rating], [CreatedAt], [Id]) VALUES (1000, 100061, N'Pin cực khỏe, sạc nhanh, thiết kế đẹp', 5, CAST(N'2025-06-27T08:24:22.1166667' AS DateTime2), 1)
INSERT [dbo].[Reviews] ([UserId], [ColorCapacityId], [Description], [Rating], [CreatedAt], [Id]) VALUES (1000, 100070, N'CPU mạnh mẽ, xử lý đa nhiệm tốt', 5, CAST(N'2025-06-27T08:24:22.1166667' AS DateTime2), 10)
INSERT [dbo].[Reviews] ([UserId], [ColorCapacityId], [Description], [Rating], [CreatedAt], [Id]) VALUES (1000, 100085, N'Pin tụt nhanh, sạc lâu', 2, CAST(N'2025-06-27T08:24:22.1166667' AS DateTime2), 19)
INSERT [dbo].[Reviews] ([UserId], [ColorCapacityId], [Description], [Rating], [CreatedAt], [Id]) VALUES (1005, 100062, N'Màn hình sắc nét, hiệu năng mượt mà', 5, CAST(N'2025-06-27T08:24:22.1166667' AS DateTime2), 2)
INSERT [dbo].[Reviews] ([UserId], [ColorCapacityId], [Description], [Rating], [CreatedAt], [Id]) VALUES (1005, 100071, N'Touch screen nhạy, phản hồi nhanh', 4, CAST(N'2025-06-27T08:24:22.1166667' AS DateTime2), 11)
INSERT [dbo].[Reviews] ([UserId], [ColorCapacityId], [Description], [Rating], [CreatedAt], [Id]) VALUES (1005, 100085, N'Camera bị mờ, chụp không rõ nét', 1, CAST(N'2025-06-27T08:24:22.1166667' AS DateTime2), 20)
INSERT [dbo].[Reviews] ([UserId], [ColorCapacityId], [Description], [Rating], [CreatedAt], [Id]) VALUES (1005, 100088, N'Pin trâu cực kỳ, dùng cả ngày không hết', 5, CAST(N'2025-06-27T09:00:43.4000000' AS DateTime2), 1)
INSERT [dbo].[Reviews] ([UserId], [ColorCapacityId], [Description], [Rating], [CreatedAt], [Id]) VALUES (1005, 100089, N'Tai nghe wireless chất lượng cao, noise cancellation tuyệt vời', 5, CAST(N'2025-06-27T09:00:43.4000000' AS DateTime2), 6)
INSERT [dbo].[Reviews] ([UserId], [ColorCapacityId], [Description], [Rating], [CreatedAt], [Id]) VALUES (1006, 100063, N'Camera chụp ảnh rất đẹp, chất lượng tuyệt vời', 5, CAST(N'2025-06-27T08:24:22.1166667' AS DateTime2), 3)
INSERT [dbo].[Reviews] ([UserId], [ColorCapacityId], [Description], [Rating], [CreatedAt], [Id]) VALUES (1006, 100072, N'Fast charging technology rất ấn tượng', 4, CAST(N'2025-06-27T08:24:22.1166667' AS DateTime2), 12)
INSERT [dbo].[Reviews] ([UserId], [ColorCapacityId], [Description], [Rating], [CreatedAt], [Id]) VALUES (1006, 100088, N'Màn hình OLED sắc nét, màu sắc đẹp', 5, CAST(N'2025-06-27T09:00:43.4000000' AS DateTime2), 2)
INSERT [dbo].[Reviews] ([UserId], [ColorCapacityId], [Description], [Rating], [CreatedAt], [Id]) VALUES (1006, 100089, N'Bass sâu, treble trong, âm thanh balanced', 5, CAST(N'2025-06-27T09:00:43.4000000' AS DateTime2), 7)
INSERT [dbo].[Reviews] ([UserId], [ColorCapacityId], [Description], [Rating], [CreatedAt], [Id]) VALUES (1007, 100064, N'Thiết kế sang trọng, cầm nắm thoải mái', 4, CAST(N'2025-06-27T08:24:22.1166667' AS DateTime2), 4)
INSERT [dbo].[Reviews] ([UserId], [ColorCapacityId], [Description], [Rating], [CreatedAt], [Id]) VALUES (1007, 100073, N'Bluetooth đôi khi bị disconnect', 3, CAST(N'2025-06-27T08:24:22.1166667' AS DateTime2), 13)
INSERT [dbo].[Reviews] ([UserId], [ColorCapacityId], [Description], [Rating], [CreatedAt], [Id]) VALUES (1007, 100088, N'Camera chụp ảnh rất đẹp, tự động lấy nét nhanh', 4, CAST(N'2025-06-27T09:00:43.4000000' AS DateTime2), 3)
INSERT [dbo].[Reviews] ([UserId], [ColorCapacityId], [Description], [Rating], [CreatedAt], [Id]) VALUES (1007, 100089, N'Kết nối Bluetooth ổn định, battery life dài', 4, CAST(N'2025-06-27T09:00:43.4000000' AS DateTime2), 8)
INSERT [dbo].[Reviews] ([UserId], [ColorCapacityId], [Description], [Rating], [CreatedAt], [Id]) VALUES (1008, 100065, N'Âm thanh cực hay, bass rất ấm', 5, CAST(N'2025-06-27T08:24:22.1166667' AS DateTime2), 5)
INSERT [dbo].[Reviews] ([UserId], [ColorCapacityId], [Description], [Rating], [CreatedAt], [Id]) VALUES (1008, 100074, N'Sạc wireless hơi chậm', 3, CAST(N'2025-06-27T08:24:22.1166667' AS DateTime2), 14)
INSERT [dbo].[Reviews] ([UserId], [ColorCapacityId], [Description], [Rating], [CreatedAt], [Id]) VALUES (1008, 100088, N'Máy hơi nóng khi chơi game lâu', 3, CAST(N'2025-06-27T09:00:43.4000000' AS DateTime2), 4)
INSERT [dbo].[Reviews] ([UserId], [ColorCapacityId], [Description], [Rating], [CreatedAt], [Id]) VALUES (1008, 100089, N'Touch control đôi khi nhầm lẫn, case hơi to', 3, CAST(N'2025-06-27T09:00:43.4000000' AS DateTime2), 9)
INSERT [dbo].[Reviews] ([UserId], [ColorCapacityId], [Description], [Rating], [CreatedAt], [Id]) VALUES (1009, 100066, N'Bluetooth kết nối nhanh, ổn định', 4, CAST(N'2025-06-27T08:24:22.1166667' AS DateTime2), 6)
INSERT [dbo].[Reviews] ([UserId], [ColorCapacityId], [Description], [Rating], [CreatedAt], [Id]) VALUES (1009, 100075, N'Gaming ok nhưng đôi khi giật lag', 3, CAST(N'2025-06-27T08:24:22.1166667' AS DateTime2), 15)
INSERT [dbo].[Reviews] ([UserId], [ColorCapacityId], [Description], [Rating], [CreatedAt], [Id]) VALUES (1009, 100088, N'Pin tụt nhanh sau 6 tháng sử dụng', 2, CAST(N'2025-06-27T09:00:43.4000000' AS DateTime2), 5)
INSERT [dbo].[Reviews] ([UserId], [ColorCapacityId], [Description], [Rating], [CreatedAt], [Id]) VALUES (1009, 100089, N'Tai nghe bên trái bị hỏng sau 3 tháng', 2, CAST(N'2025-06-27T09:00:43.4000000' AS DateTime2), 10)
INSERT [dbo].[Reviews] ([UserId], [ColorCapacityId], [Description], [Rating], [CreatedAt], [Id]) VALUES (1010, 100067, N'Wireless charging tiện lợi, sạc rất nhanh', 5, CAST(N'2025-06-27T08:24:22.1166667' AS DateTime2), 7)
INSERT [dbo].[Reviews] ([UserId], [ColorCapacityId], [Description], [Rating], [CreatedAt], [Id]) VALUES (1010, 100077, N'Touch có delay nhẹ, chưa mượt lắm', 3, CAST(N'2025-06-27T08:24:22.1166667' AS DateTime2), 16)
INSERT [dbo].[Reviews] ([UserId], [ColorCapacityId], [Description], [Rating], [CreatedAt], [Id]) VALUES (1011, 100068, N'Hiệu năng gaming mượt mà, không lag', 5, CAST(N'2025-06-27T08:24:22.1166667' AS DateTime2), 8)
INSERT [dbo].[Reviews] ([UserId], [ColorCapacityId], [Description], [Rating], [CreatedAt], [Id]) VALUES (1011, 100078, N'Port USB-C chỉ có 1 cái, hơi ít', 3, CAST(N'2025-06-27T08:24:22.1166667' AS DateTime2), 17)
INSERT [dbo].[Reviews] ([UserId], [ColorCapacityId], [Description], [Rating], [CreatedAt], [Id]) VALUES (1012, 100069, N'Bộ nhớ lớn, lưu trữ được nhiều dữ liệu', 4, CAST(N'2025-06-27T08:24:22.1166667' AS DateTime2), 9)
INSERT [dbo].[Reviews] ([UserId], [ColorCapacityId], [Description], [Rating], [CreatedAt], [Id]) VALUES (1012, 100082, N'Performance ổn nhưng chưa đạt kỳ vọng', 3, CAST(N'2025-06-27T08:24:22.1166667' AS DateTime2), 18)
GO
INSERT [dbo].[RolePermissions] ([RoleId], [PermissionId], [Id]) VALUES (1, 11, 0)
INSERT [dbo].[RolePermissions] ([RoleId], [PermissionId], [Id]) VALUES (3, 4, 0)
INSERT [dbo].[RolePermissions] ([RoleId], [PermissionId], [Id]) VALUES (4, 2, 0)
INSERT [dbo].[RolePermissions] ([RoleId], [PermissionId], [Id]) VALUES (4, 3, 0)
INSERT [dbo].[RolePermissions] ([RoleId], [PermissionId], [Id]) VALUES (4, 5, 0)
INSERT [dbo].[RolePermissions] ([RoleId], [PermissionId], [Id]) VALUES (5, 3, 0)
INSERT [dbo].[RolePermissions] ([RoleId], [PermissionId], [Id]) VALUES (5, 5, 0)
INSERT [dbo].[RolePermissions] ([RoleId], [PermissionId], [Id]) VALUES (5, 6, 0)
INSERT [dbo].[RolePermissions] ([RoleId], [PermissionId], [Id]) VALUES (5, 7, 0)
GO
SET IDENTITY_INSERT [dbo].[Roles] ON 

INSERT [dbo].[Roles] ([Id], [Name], [Description], [CreateAt]) VALUES (1, N'Admin123', N'string', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Roles] ([Id], [Name], [Description], [CreateAt]) VALUES (3, N'Test Edit', N'Test', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Roles] ([Id], [Name], [Description], [CreateAt]) VALUES (4, N'Role Test mới', N'Test', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Roles] ([Id], [Name], [Description], [CreateAt]) VALUES (5, N'123', N'123456789', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Roles] ([Id], [Name], [Description], [CreateAt]) VALUES (6, N'Khách hàng', N'Role cho khách hàng', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
SET IDENTITY_INSERT [dbo].[Roles] OFF
GO
INSERT [dbo].[UserAuthInfos] ([UserId], [IsActive], [FailedAttempts]) VALUES (1000, 1, 5)
INSERT [dbo].[UserAuthInfos] ([UserId], [IsActive], [FailedAttempts]) VALUES (1005, 1, 5)
INSERT [dbo].[UserAuthInfos] ([UserId], [IsActive], [FailedAttempts]) VALUES (1006, 1, 5)
INSERT [dbo].[UserAuthInfos] ([UserId], [IsActive], [FailedAttempts]) VALUES (1007, 1, 5)
INSERT [dbo].[UserAuthInfos] ([UserId], [IsActive], [FailedAttempts]) VALUES (1008, 1, 5)
INSERT [dbo].[UserAuthInfos] ([UserId], [IsActive], [FailedAttempts]) VALUES (1009, 1, 5)
INSERT [dbo].[UserAuthInfos] ([UserId], [IsActive], [FailedAttempts]) VALUES (1010, 1, 5)
INSERT [dbo].[UserAuthInfos] ([UserId], [IsActive], [FailedAttempts]) VALUES (1011, 1, 5)
INSERT [dbo].[UserAuthInfos] ([UserId], [IsActive], [FailedAttempts]) VALUES (1012, 1, 5)
GO
INSERT [dbo].[UserPermissions] ([UserId], [PermissionId], [Id]) VALUES (1000, 2, 0)
GO
INSERT [dbo].[UserRoles] ([UserId], [RoleId], [Id]) VALUES (1000, 1, 0)
INSERT [dbo].[UserRoles] ([UserId], [RoleId], [Id]) VALUES (1006, 1, 0)
INSERT [dbo].[UserRoles] ([UserId], [RoleId], [Id]) VALUES (1006, 3, 0)
INSERT [dbo].[UserRoles] ([UserId], [RoleId], [Id]) VALUES (1006, 5, 0)
INSERT [dbo].[UserRoles] ([UserId], [RoleId], [Id]) VALUES (1007, 1, 0)
INSERT [dbo].[UserRoles] ([UserId], [RoleId], [Id]) VALUES (1009, 1, 0)
INSERT [dbo].[UserRoles] ([UserId], [RoleId], [Id]) VALUES (1010, 6, 0)
INSERT [dbo].[UserRoles] ([UserId], [RoleId], [Id]) VALUES (1011, 6, 0)
INSERT [dbo].[UserRoles] ([UserId], [RoleId], [Id]) VALUES (1012, 6, 0)
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([Id], [Name], [Email], [PasswordHash], [PhoneNumber], [Address], [CreatedAt], [Gender], [Avatar], [Birthday], [Hometown], [workerAccount]) VALUES (1000, N'Admin', N'admin', N'AQAAAAIAAYagAAAAEMnVR6OkLTAe1vikPaddIvw+NQX+y3lmHJbRYvXo7IKNbNrn+cpLzAngEwnoGF4I9Q==', N'0368502432', N'abcdb cakjshdjk ádq ', CAST(N'2025-03-31T11:12:12.2046408' AS DateTime2), N'Nam       ', N'defaultAvt.png', CAST(N'0001-01-01' AS Date), N'Nghệ An', 1)
INSERT [dbo].[Users] ([Id], [Name], [Email], [PasswordHash], [PhoneNumber], [Address], [CreatedAt], [Gender], [Avatar], [Birthday], [Hometown], [workerAccount]) VALUES (1005, N'Dta', N'lucstudy456@gmail.com', N'AQAAAAIAAYagAAAAEGdZgB4DIP4AE6rKGztnzHv1xgg+WvuHf7Eekq0a3DWoYCdRc9B304qFcTS6rGLQiA==', N'0948789435', N'X1', CAST(N'2025-03-31T14:46:57.3728956' AS DateTime2), N'Nam       ', N'defaultAvt.png', CAST(N'0001-01-01' AS Date), NULL, 0)
INSERT [dbo].[Users] ([Id], [Name], [Email], [PasswordHash], [PhoneNumber], [Address], [CreatedAt], [Gender], [Avatar], [Birthday], [Hometown], [workerAccount]) VALUES (1006, N'Trần Lực', N'luicdt456@gmail.com', N'AQAAAAIAAYagAAAAEMLW1SBQvfZboUG3UzAPKn1QDF7fGn3iH7dvwzEWtu25lZ/YDE4db7OKXBY2d6NAGQ==', N'123456789', N'sad asd sad', CAST(N'2025-06-12T18:39:38.4577844' AS DateTime2), N'Nam       ', N'luicdt456gmailcom.jpg', CAST(N'2025-06-12' AS Date), N'asdfasdasd', 1)
INSERT [dbo].[Users] ([Id], [Name], [Email], [PasswordHash], [PhoneNumber], [Address], [CreatedAt], [Gender], [Avatar], [Birthday], [Hometown], [workerAccount]) VALUES (1007, N'Test', N'Lucdt456@gmail.com', N'AQAAAAIAAYagAAAAELPt6XMF8O0CQUaDtz28A8srqNqNHPGqHpGLgASDA9GHTaz30ob1byn/QMcCM2zM7g==', N'123456', N'ádasdsadsadsa', CAST(N'2025-06-12T18:57:43.1898353' AS DateTime2), N'Nam       ', N'lucdt456gmailcom.jpg', CAST(N'2025-06-12' AS Date), N'123456asdasds', 1)
INSERT [dbo].[Users] ([Id], [Name], [Email], [PasswordHash], [PhoneNumber], [Address], [CreatedAt], [Gender], [Avatar], [Birthday], [Hometown], [workerAccount]) VALUES (1008, N'string', N'string', N'AQAAAAIAAYagAAAAEKOOE9KvPINdyYhfFwcV44pLq7IJRMNHyeGrrTHTHANs+WS2/YCGHUVTVeeMtwTj5A==', N'string', N'string', CAST(N'2025-06-12T19:40:33.9148322' AS DateTime2), N'Nam       ', N'defaultAvt.png', CAST(N'2025-06-12' AS Date), N'string', 1)
INSERT [dbo].[Users] ([Id], [Name], [Email], [PasswordHash], [PhoneNumber], [Address], [CreatedAt], [Gender], [Avatar], [Birthday], [Hometown], [workerAccount]) VALUES (1009, N'string', N'string123', N'AQAAAAIAAYagAAAAELhKAtlkQc7NZkXNu+ewrntn6LvEd5DmcUzJQoYEIdb5eVOYAqlYWrNKjrN2G20TKw==', N'string', N'string', CAST(N'2025-06-12T19:45:54.6709540' AS DateTime2), N'string    ', N'defaultAvt.png', CAST(N'2025-06-12' AS Date), N'string', 1)
INSERT [dbo].[Users] ([Id], [Name], [Email], [PasswordHash], [PhoneNumber], [Address], [CreatedAt], [Gender], [Avatar], [Birthday], [Hometown], [workerAccount]) VALUES (1010, N'Test1', N'Test1', N'AQAAAAIAAYagAAAAED5ZTM+UQujiGug4U8l5ji4BjIChh39wIn3Ln51hvKf0UujkQD0CGhAnmje1/C2ZJQ==', N'123456789', N'123sa1c6ascasc á đâs a a a ', CAST(N'2025-06-24T20:57:28.3406591' AS DateTime2), N'Nam       ', N'defaultAvt.png', CAST(N'2002-05-23' AS Date), NULL, 0)
INSERT [dbo].[Users] ([Id], [Name], [Email], [PasswordHash], [PhoneNumber], [Address], [CreatedAt], [Gender], [Avatar], [Birthday], [Hometown], [workerAccount]) VALUES (1011, N'ádasd ád ádasd', N'test3', N'AQAAAAIAAYagAAAAEBgiwzzS1o1p3hmQlN99PfPUO/ksIl24HtvwxdT1mXvIyzsls5W6uc8ERPgKaB96HQ==', N'123456789', N'ádasd', CAST(N'2025-06-25T00:56:57.9479408' AS DateTime2), N'nam       ', N'defaultAvt.png', CAST(N'2025-08-06' AS Date), NULL, 0)
INSERT [dbo].[Users] ([Id], [Name], [Email], [PasswordHash], [PhoneNumber], [Address], [CreatedAt], [Gender], [Avatar], [Birthday], [Hometown], [workerAccount]) VALUES (1012, N'Dta', N'Test23', N'AQAAAAIAAYagAAAAEHpUmrHTR2o9CXdYP0l8LPxsDux5t0IfV4OzO8sBjKAmZaKNtrIX0T+CXekw9lTcTA==', N'0948789435', N'X1', CAST(N'2025-06-25T01:11:35.5614685' AS DateTime2), N'Nam       ', N'defaultAvt.png', CAST(N'2025-06-04' AS Date), NULL, 0)
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
/****** Object:  Index [IX_Carts_ColorCapacityId]    Script Date: 2025-06-27 20:33:47 ******/
CREATE NONCLUSTERED INDEX [IX_Carts_ColorCapacityId] ON [dbo].[Carts]
(
	[ColorCapacityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_ColorCapacities_CapacityId]    Script Date: 2025-06-27 20:33:47 ******/
CREATE NONCLUSTERED INDEX [IX_ColorCapacities_CapacityId] ON [dbo].[ColorCapacities]
(
	[CapacityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_ColorCapacities_ColorId]    Script Date: 2025-06-27 20:33:47 ******/
CREATE NONCLUSTERED INDEX [IX_ColorCapacities_ColorId] ON [dbo].[ColorCapacities]
(
	[ColorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_ColorCapacities_ProductId]    Script Date: 2025-06-27 20:33:47 ******/
CREATE NONCLUSTERED INDEX [IX_ColorCapacities_ProductId] ON [dbo].[ColorCapacities]
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_LoginHistories_UserId]    Script Date: 2025-06-27 20:33:47 ******/
CREATE NONCLUSTERED INDEX [IX_LoginHistories_UserId] ON [dbo].[LoginHistories]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_OrderDetails_ColorCapacityId]    Script Date: 2025-06-27 20:33:47 ******/
CREATE NONCLUSTERED INDEX [IX_OrderDetails_ColorCapacityId] ON [dbo].[OrderDetails]
(
	[ColorCapacityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Orders_UserId]    Script Date: 2025-06-27 20:33:47 ******/
CREATE NONCLUSTERED INDEX [IX_Orders_UserId] ON [dbo].[Orders]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_ProductImages_ProductId]    Script Date: 2025-06-27 20:33:47 ******/
CREATE NONCLUSTERED INDEX [IX_ProductImages_ProductId] ON [dbo].[ProductImages]
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Products_BrandId]    Script Date: 2025-06-27 20:33:47 ******/
CREATE NONCLUSTERED INDEX [IX_Products_BrandId] ON [dbo].[Products]
(
	[BrandId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Products_CategoryId]    Script Date: 2025-06-27 20:33:47 ******/
CREATE NONCLUSTERED INDEX [IX_Products_CategoryId] ON [dbo].[Products]
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Products_SaleId]    Script Date: 2025-06-27 20:33:47 ******/
CREATE NONCLUSTERED INDEX [IX_Products_SaleId] ON [dbo].[Products]
(
	[SaleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Reviews_ColorCapacityId]    Script Date: 2025-06-27 20:33:47 ******/
CREATE NONCLUSTERED INDEX [IX_Reviews_ColorCapacityId] ON [dbo].[Reviews]
(
	[ColorCapacityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_RolePermissions_PermissionId]    Script Date: 2025-06-27 20:33:47 ******/
CREATE NONCLUSTERED INDEX [IX_RolePermissions_PermissionId] ON [dbo].[RolePermissions]
(
	[PermissionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_UserPermissions_PermissionId]    Script Date: 2025-06-27 20:33:47 ******/
CREATE NONCLUSTERED INDEX [IX_UserPermissions_PermissionId] ON [dbo].[UserPermissions]
(
	[PermissionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_UserRoles_RoleId]    Script Date: 2025-06-27 20:33:47 ******/
CREATE NONCLUSTERED INDEX [IX_UserRoles_RoleId] ON [dbo].[UserRoles]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_WishLists_ColorCapacityId]    Script Date: 2025-06-27 20:33:47 ******/
CREATE NONCLUSTERED INDEX [IX_WishLists_ColorCapacityId] ON [dbo].[WishLists]
(
	[ColorCapacityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Brands] ADD  DEFAULT ('0001-01-01T00:00:00.0000000') FOR [CreateAt]
GO
ALTER TABLE [dbo].[Carts] ADD  DEFAULT ((0)) FOR [Id]
GO
ALTER TABLE [dbo].[Categories] ADD  DEFAULT ('0001-01-01T00:00:00.0000000') FOR [CreateAt]
GO
ALTER TABLE [dbo].[ColorCapacities] ADD  CONSTRAINT [DF__ColorCapa__Produ__13BCEBC1]  DEFAULT ((0)) FOR [ProductId]
GO
ALTER TABLE [dbo].[ColorCapacities] ADD  DEFAULT ((0)) FOR [SoldAmount]
GO
ALTER TABLE [dbo].[ColorCapacities] ADD  DEFAULT (CONVERT([bit],(0))) FOR [StatusImage]
GO
ALTER TABLE [dbo].[Orders] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[Products] ADD  CONSTRAINT [DF__Products__Discou__2739D489]  DEFAULT ((0.0)) FOR [DiscountAmount]
GO
ALTER TABLE [dbo].[Products] ADD  DEFAULT ('0001-01-01T00:00:00.0000000') FOR [CreateAt]
GO
ALTER TABLE [dbo].[Reviews] ADD  DEFAULT ((0)) FOR [Id]
GO
ALTER TABLE [dbo].[RolePermissions] ADD  DEFAULT ((0)) FOR [Id]
GO
ALTER TABLE [dbo].[Roles] ADD  DEFAULT ('0001-01-01T00:00:00.0000000') FOR [CreateAt]
GO
ALTER TABLE [dbo].[UserPermissions] ADD  DEFAULT ((0)) FOR [Id]
GO
ALTER TABLE [dbo].[UserRoles] ADD  DEFAULT ((0)) FOR [Id]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF__Users__workerAcc__46486B8E]  DEFAULT (CONVERT([bit],(0))) FOR [workerAccount]
GO
ALTER TABLE [dbo].[WishLists] ADD  DEFAULT ((0)) FOR [Id]
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
ALTER TABLE [dbo].[UserPermissions]  WITH CHECK ADD  CONSTRAINT [FK_UserPermissions_Permissions_PermissionId] FOREIGN KEY([PermissionId])
REFERENCES [dbo].[Permissions] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserPermissions] CHECK CONSTRAINT [FK_UserPermissions_Permissions_PermissionId]
GO
ALTER TABLE [dbo].[UserPermissions]  WITH CHECK ADD  CONSTRAINT [FK_UserPermissions_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserPermissions] CHECK CONSTRAINT [FK_UserPermissions_Users_UserId]
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
ALTER DATABASE [AntrazShop] SET  READ_WRITE 
GO
