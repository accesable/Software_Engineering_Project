USE [master]
GO
/****** Object:  Database [MobilePhoneDistributor]    Script Date: 4/24/2023 10:03:26 AM ******/
CREATE DATABASE [MobilePhoneDistributor]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'MobilePhoneDistributor', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\MobilePhoneDistributor.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'MobilePhoneDistributor_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\MobilePhoneDistributor_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [MobilePhoneDistributor] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [MobilePhoneDistributor].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [MobilePhoneDistributor] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [MobilePhoneDistributor] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [MobilePhoneDistributor] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [MobilePhoneDistributor] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [MobilePhoneDistributor] SET ARITHABORT OFF 
GO
ALTER DATABASE [MobilePhoneDistributor] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [MobilePhoneDistributor] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [MobilePhoneDistributor] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [MobilePhoneDistributor] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [MobilePhoneDistributor] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [MobilePhoneDistributor] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [MobilePhoneDistributor] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [MobilePhoneDistributor] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [MobilePhoneDistributor] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [MobilePhoneDistributor] SET  ENABLE_BROKER 
GO
ALTER DATABASE [MobilePhoneDistributor] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [MobilePhoneDistributor] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [MobilePhoneDistributor] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [MobilePhoneDistributor] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [MobilePhoneDistributor] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [MobilePhoneDistributor] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [MobilePhoneDistributor] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [MobilePhoneDistributor] SET RECOVERY FULL 
GO
ALTER DATABASE [MobilePhoneDistributor] SET  MULTI_USER 
GO
ALTER DATABASE [MobilePhoneDistributor] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [MobilePhoneDistributor] SET DB_CHAINING OFF 
GO
ALTER DATABASE [MobilePhoneDistributor] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [MobilePhoneDistributor] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [MobilePhoneDistributor] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [MobilePhoneDistributor] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'MobilePhoneDistributor', N'ON'
GO
ALTER DATABASE [MobilePhoneDistributor] SET QUERY_STORE = OFF
GO
USE [MobilePhoneDistributor]
GO
/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 4/24/2023 10:03:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__MigrationHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ContextKey] [nvarchar](300) NOT NULL,
	[Model] [varbinary](max) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK_dbo.__MigrationHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC,
	[ContextKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PhoneModels]    Script Date: 4/24/2023 10:03:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PhoneModels](
	[PhoneId] [nvarchar](128) NOT NULL,
	[PhoneName] [nvarchar](100) NOT NULL,
	[PhoneBrand] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_dbo.PhoneModels] PRIMARY KEY CLUSTERED 
(
	[PhoneId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ReceiptDetails]    Script Date: 4/24/2023 10:03:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ReceiptDetails](
	[ReceiptDetailId] [int] IDENTITY(1,1) NOT NULL,
	[ReceiptId] [nvarchar](128) NOT NULL,
	[Quantity] [int] NOT NULL,
	[PhoneModelId] [nvarchar](128) NOT NULL,
	[UnitAmmount] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_dbo.ReceiptDetails] PRIMARY KEY CLUSTERED 
(
	[ReceiptDetailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Receipts]    Script Date: 4/24/2023 10:03:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Receipts](
	[ReceiptId] [nvarchar](128) NOT NULL,
	[ReceiptDate] [datetime] NOT NULL,
	[StaffId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.Receipts] PRIMARY KEY CLUSTERED 
(
	[ReceiptId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Staffs]    Script Date: 4/24/2023 10:03:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Staffs](
	[StaffId] [nvarchar](128) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[Username] [nvarchar](100) NOT NULL,
	[Password] [nvarchar](200) NOT NULL,
	[PasswordSalt] [nvarchar](100) NULL,
	[Email] [nvarchar](max) NOT NULL,
	[PhoneNumber] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_dbo.Staffs] PRIMARY KEY CLUSTERED 
(
	[StaffId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_PhoneModelId]    Script Date: 4/24/2023 10:03:26 AM ******/
CREATE NONCLUSTERED INDEX [IX_PhoneModelId] ON [dbo].[ReceiptDetails]
(
	[PhoneModelId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_ReceiptId]    Script Date: 4/24/2023 10:03:26 AM ******/
CREATE NONCLUSTERED INDEX [IX_ReceiptId] ON [dbo].[ReceiptDetails]
(
	[ReceiptId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_StaffId]    Script Date: 4/24/2023 10:03:26 AM ******/
CREATE NONCLUSTERED INDEX [IX_StaffId] ON [dbo].[Receipts]
(
	[StaffId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Username]    Script Date: 4/24/2023 10:03:26 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Username] ON [dbo].[Staffs]
(
	[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ReceiptDetails]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ReceiptDetails_dbo.PhoneModels_PhoneModelId] FOREIGN KEY([PhoneModelId])
REFERENCES [dbo].[PhoneModels] ([PhoneId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ReceiptDetails] CHECK CONSTRAINT [FK_dbo.ReceiptDetails_dbo.PhoneModels_PhoneModelId]
GO
ALTER TABLE [dbo].[ReceiptDetails]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ReceiptDetails_dbo.Receipts_ReceiptId] FOREIGN KEY([ReceiptId])
REFERENCES [dbo].[Receipts] ([ReceiptId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ReceiptDetails] CHECK CONSTRAINT [FK_dbo.ReceiptDetails_dbo.Receipts_ReceiptId]
GO
ALTER TABLE [dbo].[Receipts]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Receipts_dbo.Staffs_StaffId] FOREIGN KEY([StaffId])
REFERENCES [dbo].[Staffs] ([StaffId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Receipts] CHECK CONSTRAINT [FK_dbo.Receipts_dbo.Staffs_StaffId]
GO
USE [master]
GO
ALTER DATABASE [MobilePhoneDistributor] SET  READ_WRITE 
GO
