USE [master]
GO
/****** Object:  Database [Molybdenum]    Script Date: 10/9/2015 5:35:36 PM ******/
CREATE DATABASE [Molybdenum]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Molybdenum', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\Molybdenum.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'Molybdenum_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\Molybdenum_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [Molybdenum] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Molybdenum].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Molybdenum] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Molybdenum] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Molybdenum] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Molybdenum] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Molybdenum] SET ARITHABORT OFF 
GO
ALTER DATABASE [Molybdenum] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Molybdenum] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Molybdenum] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Molybdenum] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Molybdenum] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Molybdenum] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Molybdenum] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Molybdenum] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Molybdenum] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Molybdenum] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Molybdenum] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Molybdenum] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Molybdenum] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Molybdenum] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Molybdenum] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Molybdenum] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Molybdenum] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Molybdenum] SET RECOVERY FULL 
GO
ALTER DATABASE [Molybdenum] SET  MULTI_USER 
GO
ALTER DATABASE [Molybdenum] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Molybdenum] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Molybdenum] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Molybdenum] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [Molybdenum] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'Molybdenum', N'ON'
GO
USE [Molybdenum]
GO
/****** Object:  Table [dbo].[Manufectures]    Script Date: 10/9/2015 5:35:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Manufectures](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Address] [nvarchar](50) NULL,
	[NumberOfFactories] [int] NULL,
 CONSTRAINT [PK_Manufectures] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Measures]    Script Date: 10/9/2015 5:35:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Measures](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MeasuresName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Measures] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Produced]    Script Date: 10/9/2015 5:35:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Produced](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProductId] [int] NOT NULL,
	[ManufactureId] [int] NOT NULL,
	[Amount] [int] NULL,
 CONSTRAINT [PK_Produced] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Products]    Script Date: 10/9/2015 5:35:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Formula] [nvarchar](50) NOT NULL,
	[MeasureID] [int] NOT NULL,
	[PricePerUnit] [money] NOT NULL,
	[TypeId] [int] NOT NULL,
	[DescriptionOfProduction] [text] NULL,
	[DescriptionOfStorage] [text] NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Sales]    Script Date: 10/9/2015 5:35:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sales](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TraderId] [int] NOT NULL,
	[ProductId] [int] NOT NULL,
	[Sold] [int] NULL,
 CONSTRAINT [PK_Sales] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Traders]    Script Date: 10/9/2015 5:35:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Traders](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Address] [nvarchar](50) NULL,
	[NumberOfShops] [int] NULL,
 CONSTRAINT [PK_Traders] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Types]    Script Date: 10/9/2015 5:35:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Types](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TypeName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Types] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[Manufectures] ON 

INSERT [dbo].[Manufectures] ([Id], [Name], [Address], [NumberOfFactories]) VALUES (1, N'OrgaHim', NULL, NULL)
SET IDENTITY_INSERT [dbo].[Manufectures] OFF
SET IDENTITY_INSERT [dbo].[Measures] ON 

INSERT [dbo].[Measures] ([Id], [MeasuresName]) VALUES (1, N'Liters')
SET IDENTITY_INSERT [dbo].[Measures] OFF
SET IDENTITY_INSERT [dbo].[Produced] ON 

INSERT [dbo].[Produced] ([Id], [ProductId], [ManufactureId], [Amount]) VALUES (1, 1, 1, 50)
SET IDENTITY_INSERT [dbo].[Produced] OFF
SET IDENTITY_INSERT [dbo].[Products] ON 

INSERT [dbo].[Products] ([id], [Name], [Formula], [MeasureID], [PricePerUnit], [TypeId], [DescriptionOfProduction], [DescriptionOfStorage]) VALUES (1, N'Sulfurous', N'H2SO3', 1, 20.0000, 1, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Products] OFF
SET IDENTITY_INSERT [dbo].[Sales] ON 

INSERT [dbo].[Sales] ([Id], [TraderId], [ProductId], [Sold]) VALUES (1, 1, 1, 50000)
SET IDENTITY_INSERT [dbo].[Sales] OFF
SET IDENTITY_INSERT [dbo].[Traders] ON 

INSERT [dbo].[Traders] ([Id], [Name], [Address], [NumberOfShops]) VALUES (1, N'Bila', NULL, NULL)
SET IDENTITY_INSERT [dbo].[Traders] OFF
SET IDENTITY_INSERT [dbo].[Types] ON 

INSERT [dbo].[Types] ([Id], [TypeName]) VALUES (1, N'Acid')
SET IDENTITY_INSERT [dbo].[Types] OFF
ALTER TABLE [dbo].[Produced]  WITH CHECK ADD  CONSTRAINT [FK_Produced_Manufectures] FOREIGN KEY([ManufactureId])
REFERENCES [dbo].[Manufectures] ([Id])
GO
ALTER TABLE [dbo].[Produced] CHECK CONSTRAINT [FK_Produced_Manufectures]
GO
ALTER TABLE [dbo].[Produced]  WITH CHECK ADD  CONSTRAINT [FK_Produced_Products] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([id])
GO
ALTER TABLE [dbo].[Produced] CHECK CONSTRAINT [FK_Produced_Products]
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK_Products_Measures] FOREIGN KEY([MeasureID])
REFERENCES [dbo].[Measures] ([Id])
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_Products_Measures]
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK_Products_Types] FOREIGN KEY([TypeId])
REFERENCES [dbo].[Types] ([Id])
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_Products_Types]
GO
ALTER TABLE [dbo].[Sales]  WITH CHECK ADD  CONSTRAINT [FK_Sales_Products] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([id])
GO
ALTER TABLE [dbo].[Sales] CHECK CONSTRAINT [FK_Sales_Products]
GO
ALTER TABLE [dbo].[Sales]  WITH CHECK ADD  CONSTRAINT [FK_Sales_Traders] FOREIGN KEY([TraderId])
REFERENCES [dbo].[Traders] ([Id])
GO
ALTER TABLE [dbo].[Sales] CHECK CONSTRAINT [FK_Sales_Traders]
GO
USE [master]
GO
ALTER DATABASE [Molybdenum] SET  READ_WRITE 
GO
