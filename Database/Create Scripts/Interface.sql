IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Item_ItemType]') AND parent_object_id = OBJECT_ID(N'[dbo].[Item]'))
ALTER TABLE [dbo].[Item] DROP CONSTRAINT [FK_Item_ItemType]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Shop_Customer]') AND parent_object_id = OBJECT_ID(N'[dbo].[Shop]'))
ALTER TABLE [dbo].[Shop] DROP CONSTRAINT [FK_Shop_Customer]
GO
/****** Object:  Table [dbo].[ContractFee]    Script Date: 08/20/2007 11:02:33 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ContractFee]') AND type in (N'U'))
DROP TABLE [dbo].[ContractFee]
GO
/****** Object:  Table [dbo].[YearRebate]    Script Date: 08/20/2007 11:02:33 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[YearRebate]') AND type in (N'U'))
DROP TABLE [dbo].[YearRebate]
GO
/****** Object:  Table [dbo].[PromotionActivity]    Script Date: 08/20/2007 11:02:33 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PromotionActivity]') AND type in (N'U'))
DROP TABLE [dbo].[PromotionActivity]
GO
/****** Object:  Table [dbo].[Promotion]    Script Date: 08/20/2007 11:02:33 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Promotion]') AND type in (N'U'))
DROP TABLE [dbo].[Promotion]
GO
/****** Object:  Table [dbo].[Item]    Script Date: 08/20/2007 11:02:33 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Item]') AND type in (N'U'))
DROP TABLE [dbo].[Item]
GO
/****** Object:  Table [dbo].[ItemType]    Script Date: 08/20/2007 11:02:33 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ItemType]') AND type in (N'U'))
DROP TABLE [dbo].[ItemType]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 08/20/2007 11:02:33 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Customer]') AND type in (N'U'))
DROP TABLE [dbo].[Customer]
GO
/****** Object:  Table [dbo].[Shop]    Script Date: 08/20/2007 11:02:33 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Shop]') AND type in (N'U'))
DROP TABLE [dbo].[Shop]
GO
/****** Object:  Table [dbo].[ContractFee]    Script Date: 08/20/2007 11:02:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ContractFee]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ContractFee](
	[Period] [datetime] NOT NULL,
	[CompanyNo] [varchar](50) NOT NULL,
	[CustomerNo] [varchar](50) NOT NULL,
	[ItemTypeNo] [varchar](50) NOT NULL,
	[Revenue] [numeric](18, 2) NOT NULL,
 CONSTRAINT [PK_ContractFee] PRIMARY KEY CLUSTERED 
(
	[Period] ASC,
	[CompanyNo] ASC,
	[CustomerNo] ASC,
	[ItemTypeNo] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[YearRebate]    Script Date: 08/20/2007 11:02:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[YearRebate]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[YearRebate](
	[Period] [datetime] NOT NULL,
	[CompanyNo] [varchar](50) NOT NULL,
	[CustomerNo] [varchar](50) NOT NULL,
	[ItemTypeNo] [varchar](50) NOT NULL,
	[Revenue] [numeric](18, 2) NOT NULL,
 CONSTRAINT [PK_YearRebate] PRIMARY KEY CLUSTERED 
(
	[Period] ASC,
	[CompanyNo] ASC,
	[CustomerNo] ASC,
	[ItemTypeNo] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PromotionActivity]    Script Date: 08/20/2007 11:02:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PromotionActivity]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[PromotionActivity](
	[Period] [datetime] NOT NULL,
	[CompanyNo] [varchar](50) NOT NULL,
	[CustomerNo] [varchar](50) NOT NULL,
	[ItemTypeNo] [varchar](50) NOT NULL,
	[Revenue] [numeric](18, 2) NOT NULL,
 CONSTRAINT [PK_PromotionActivity] PRIMARY KEY CLUSTERED 
(
	[Period] ASC,
	[CompanyNo] ASC,
	[CustomerNo] ASC,
	[ItemTypeNo] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Promotion]    Script Date: 08/20/2007 11:02:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Promotion]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Promotion](
	[Period] [datetime] NOT NULL,
	[CompanyNo] [varchar](50) NOT NULL,
	[CustomerNo] [varchar](50) NOT NULL,
	[ShopNo] [varchar](50) NOT NULL,
	[ItemTypeNo] [varchar](50) NOT NULL,
	[Quantity] [int] NOT NULL,
 CONSTRAINT [PK_Promotion] PRIMARY KEY CLUSTERED 
(
	[Period] ASC,
	[CompanyNo] ASC,
	[CustomerNo] ASC,
	[ShopNo] ASC,
	[ItemTypeNo] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Item]    Script Date: 08/20/2007 11:02:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Item]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Item](
	[ItemNo] [varchar](50) NOT NULL,
	[ItemTypeNo] [varchar](50) NOT NULL,
	[ItemName] [varchar](100) NOT NULL,
	[IsActive] [bit] NOT NULL CONSTRAINT [DF_Item_IsActive]  DEFAULT ((1)),
 CONSTRAINT [PK_Item] PRIMARY KEY CLUSTERED 
(
	[ItemNo] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ItemType]    Script Date: 08/20/2007 11:02:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ItemType]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ItemType](
	[ItemTypeNo] [varchar](50) NOT NULL,
	[ItemTypeName] [varchar](100) NOT NULL,
	[IsActive] [bit] NOT NULL CONSTRAINT [DF_ItemType_IsActive]  DEFAULT ((1)),
 CONSTRAINT [PK_ItemType] PRIMARY KEY CLUSTERED 
(
	[ItemTypeNo] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 08/20/2007 11:02:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Customer]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Customer](
	[CustomerNo] [varchar](50) NOT NULL,
	[CustomerName] [varchar](100) NOT NULL,
	[CustomerShortName] [varchar](50) NOT NULL,
	[CompanyNo] [varchar](50) NOT NULL,
	[Rebate] [numeric](18, 2) NOT NULL,
	[IsActive] [bit] NOT NULL CONSTRAINT [DF_Customer_IsActive]  DEFAULT ((1)),
 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
(
	[CustomerNo] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Shop]    Script Date: 08/20/2007 11:02:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Shop]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Shop](
	[CustomerNo] [varchar](50) NULL,
	[ShopNo] [varchar](50) NOT NULL,
	[ShopName] [varchar](100) NULL,
	[IsActive] [bit] NOT NULL CONSTRAINT [DF_Shop_IsActive]  DEFAULT ((1)),
 CONSTRAINT [PK_Shop] PRIMARY KEY CLUSTERED 
(
	[ShopNo] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Item_ItemType]') AND parent_object_id = OBJECT_ID(N'[dbo].[Item]'))
ALTER TABLE [dbo].[Item]  WITH CHECK ADD  CONSTRAINT [FK_Item_ItemType] FOREIGN KEY([ItemTypeNo])
REFERENCES [dbo].[ItemType] ([ItemTypeNo])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Shop_Customer]') AND parent_object_id = OBJECT_ID(N'[dbo].[Shop]'))
ALTER TABLE [dbo].[Shop]  WITH CHECK ADD  CONSTRAINT [FK_Shop_Customer] FOREIGN KEY([CustomerNo])
REFERENCES [dbo].[Customer] ([CustomerNo])
GO
