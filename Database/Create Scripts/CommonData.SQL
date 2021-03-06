IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_AlterFeeSubType_AlterFeeType]') AND parent_object_id = OBJECT_ID(N'[dbo].[AlterFeeSubType]'))
ALTER TABLE [dbo].[AlterFeeSubType] DROP CONSTRAINT [FK_AlterFeeSubType_AlterFeeType]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ExchangeRate_Currency]') AND parent_object_id = OBJECT_ID(N'[dbo].[ExchangeRate]'))
ALTER TABLE [dbo].[ExchangeRate] DROP CONSTRAINT [FK_ExchangeRate_Currency]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_HighSeasonParameter_HighSeasonParameter]') AND parent_object_id = OBJECT_ID(N'[dbo].[HighSeasonParameter]'))
ALTER TABLE [dbo].[HighSeasonParameter] DROP CONSTRAINT [FK_HighSeasonParameter_HighSeasonParameter]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_LowSeasonParameter_SalesChannelType]') AND parent_object_id = OBJECT_ID(N'[dbo].[LowSeasonParameter]'))
ALTER TABLE [dbo].[LowSeasonParameter] DROP CONSTRAINT [FK_LowSeasonParameter_SalesChannelType]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TripFeeStandard_TripFeeClass]') AND parent_object_id = OBJECT_ID(N'[dbo].[TripFeeStandard]'))
ALTER TABLE [dbo].[TripFeeStandard] DROP CONSTRAINT [FK_TripFeeStandard_TripFeeClass]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TripFeeStandard_TripFeeType]') AND parent_object_id = OBJECT_ID(N'[dbo].[TripFeeStandard]'))
ALTER TABLE [dbo].[TripFeeStandard] DROP CONSTRAINT [FK_TripFeeStandard_TripFeeType]
GO
/****** Object:  Table [dbo].[AlterFeeSubType]    Script Date: 08/20/2007 15:44:57 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AlterFeeSubType]') AND type in (N'U'))
DROP TABLE [dbo].[AlterFeeSubType]
GO
/****** Object:  Table [dbo].[AlterFeeType]    Script Date: 08/20/2007 15:44:57 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AlterFeeType]') AND type in (N'U'))
DROP TABLE [dbo].[AlterFeeType]
GO
/****** Object:  Table [dbo].[Campaign]    Script Date: 08/20/2007 15:44:57 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Campaign]') AND type in (N'U'))
DROP TABLE [dbo].[Campaign]
GO
/****** Object:  Table [dbo].[Currency]    Script Date: 08/20/2007 15:44:58 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Currency]') AND type in (N'U'))
DROP TABLE [dbo].[Currency]
GO
/****** Object:  Table [dbo].[ExchangeRate]    Script Date: 08/20/2007 15:44:58 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ExchangeRate]') AND type in (N'U'))
DROP TABLE [dbo].[ExchangeRate]
GO
/****** Object:  Table [dbo].[FeeParameter]    Script Date: 08/20/2007 15:44:58 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FeeParameter]') AND type in (N'U'))
DROP TABLE [dbo].[FeeParameter]
GO
/****** Object:  Table [dbo].[FixedFeeType]    Script Date: 08/20/2007 15:44:58 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FixedFeeType]') AND type in (N'U'))
DROP TABLE [dbo].[FixedFeeType]
GO
/****** Object:  Table [dbo].[HighSeasonParameter]    Script Date: 08/20/2007 15:44:58 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[HighSeasonParameter]') AND type in (N'U'))
DROP TABLE [dbo].[HighSeasonParameter]
GO
/****** Object:  Table [dbo].[LowSeasonParameter]    Script Date: 08/20/2007 15:44:58 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LowSeasonParameter]') AND type in (N'U'))
DROP TABLE [dbo].[LowSeasonParameter]
GO
/****** Object:  Table [dbo].[RejectReason]    Script Date: 08/20/2007 15:44:58 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RejectReason]') AND type in (N'U'))
DROP TABLE [dbo].[RejectReason]
GO
/****** Object:  Table [dbo].[SalesChannelType]    Script Date: 08/20/2007 15:44:58 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SalesChannelType]') AND type in (N'U'))
DROP TABLE [dbo].[SalesChannelType]
GO
/****** Object:  Table [dbo].[SocietyReason]    Script Date: 08/20/2007 15:44:58 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocietyReason]') AND type in (N'U'))
DROP TABLE [dbo].[SocietyReason]
GO
/****** Object:  Table [dbo].[TripFeeClass]    Script Date: 08/20/2007 15:44:58 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TripFeeClass]') AND type in (N'U'))
DROP TABLE [dbo].[TripFeeClass]
GO
/****** Object:  Table [dbo].[TripFeeStandard]    Script Date: 08/20/2007 15:44:58 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TripFeeStandard]') AND type in (N'U'))
DROP TABLE [dbo].[TripFeeStandard]
GO
/****** Object:  Table [dbo].[TripFeeType]    Script Date: 08/20/2007 15:44:58 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TripFeeType]') AND type in (N'U'))
DROP TABLE [dbo].[TripFeeType]
GO
/****** Object:  Table [dbo].[AlterFeeSubType]    Script Date: 08/20/2007 15:44:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AlterFeeSubType]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AlterFeeSubType](
	[AlterFeeSubTypeId] [int] IDENTITY(1,1) NOT NULL,
	[AlterFeeSubTypeName] [nvarchar](100) NULL,
	[AlterFeeSubTypeEngName] [nvarchar](100) NULL,
	[IsBudget] [bit] NOT NULL,
	[AccountSubjectCode] [nvarchar](50) NOT NULL,
	[AlterFeeTypeId] [int] NOT NULL,
	[IsActive] [bit] NOT NULL CONSTRAINT [DF_AlterFeeSubType_IsActive]  DEFAULT ((1)),
 CONSTRAINT [PK_AlterFeeSubType] PRIMARY KEY CLUSTERED 
(
	[AlterFeeSubTypeId] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[AlterFeeType]    Script Date: 08/20/2007 15:44:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AlterFeeType]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AlterFeeType](
	[AlterFeeTypeId] [int] IDENTITY(1,1) NOT NULL,
	[AlterFeeTypeName] [nvarchar](100) NULL,
	[AlterFeeTypeEngName] [nvarchar](100) NULL,
	[IsBudget] [bit] NOT NULL,
	[IsActive] [bit] NOT NULL CONSTRAINT [DF_AlterFeeType_IsActive]  DEFAULT ((1)),
 CONSTRAINT [PK_AlterFeeType] PRIMARY KEY CLUSTERED 
(
	[AlterFeeTypeId] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Campaign]    Script Date: 08/20/2007 15:44:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Campaign]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Campaign](
	[CampaignId] [int] IDENTITY(1,1) NOT NULL,
	[CustomerNo] [nvarchar](50) NOT NULL,
	[CustomerName] [nvarchar](100) NOT NULL,
	[ShopNo] [nvarchar](50) NOT NULL,
	[ShopName] [nvarchar](100) NOT NULL,
	[ItemNo] [nvarchar](50) NOT NULL,
	[ItemName] [nvarchar](100) NOT NULL,
	[ItemTypeNo] [nchar](10) NOT NULL,
	[ItemTypeName] [nvarchar](100) NOT NULL,
	[ItemModel] [nvarchar](100) NOT NULL,
	[StartDate] [datetime] NOT NULL,
	[EndDate] [datetime] NOT NULL,
	[Fee] [money] NOT NULL,
 CONSTRAINT [PK_Campaign] PRIMARY KEY CLUSTERED 
(
	[CampaignId] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Currency]    Script Date: 08/20/2007 15:44:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Currency]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Currency](
	[CurrId] [int] IDENTITY(1,1) NOT NULL,
	[CurrShortName] [nvarchar](50) NOT NULL,
	[CurrFullName] [nvarchar](50) NOT NULL,
	[CurrSymbol] [nchar](10) NULL,
	[Memo] [nvarchar](100) NULL,
 CONSTRAINT [PK_Currency] PRIMARY KEY CLUSTERED 
(
	[CurrId] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[ExchangeRate]    Script Date: 08/20/2007 15:44:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ExchangeRate]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ExchangeRate](
	[ExchangeRateId] [int] IDENTITY(1,1) NOT NULL,
	[ExchangeRateValue] [numeric](5, 2) NOT NULL,
	[AppBegin] [datetime] NOT NULL,
	[AppEnd] [datetime] NOT NULL,
	[CurrId] [int] NOT NULL,
 CONSTRAINT [PK_ExchangeRate] PRIMARY KEY CLUSTERED 
(
	[ExchangeRateId] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[FeeParameter]    Script Date: 08/20/2007 15:44:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FeeParameter]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[FeeParameter](
	[FeeParameterId] [int] NOT NULL,
	[FeeType] [int] IDENTITY(1,1) NOT NULL,
	[FeeTypeName] [nvarchar](50) NULL,
	[Parameter] [float] NOT NULL,
 CONSTRAINT [PK_FeeParameter] PRIMARY KEY CLUSTERED 
(
	[FeeParameterId] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[FixedFeeType]    Script Date: 08/20/2007 15:44:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FixedFeeType]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[FixedFeeType](
	[FixedFeeTypeId] [int] IDENTITY(1,1) NOT NULL,
	[FixedFeeTypeName] [nvarchar](100) NULL,
	[FixedFeeTypeEngName] [nvarchar](100) NULL,
	[IsBudget] [bit] NOT NULL,
	[AccountSubjectCode] [nvarchar](50) NOT NULL,
	[IsActive] [bit] NOT NULL CONSTRAINT [DF_FixedFeeType_IsActive]  DEFAULT ((1)),
 CONSTRAINT [PK_FixedFeeType] PRIMARY KEY CLUSTERED 
(
	[FixedFeeTypeId] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[HighSeasonParameter]    Script Date: 08/20/2007 15:44:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[HighSeasonParameter]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[HighSeasonParameter](
	[HighSeasonParameterId] [int] IDENTITY(1,1) NOT NULL,
	[SalesChannelTypeId] [int] NOT NULL,
	[Parameter] [float] NOT NULL,
 CONSTRAINT [PK_HighSeasonParameter] PRIMARY KEY CLUSTERED 
(
	[HighSeasonParameterId] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IX_HighSeasonParameter] UNIQUE NONCLUSTERED 
(
	[SalesChannelTypeId] ASC,
	[Parameter] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[LowSeasonParameter]    Script Date: 08/20/2007 15:44:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LowSeasonParameter]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[LowSeasonParameter](
	[LowSeasonParameterId] [int] IDENTITY(1,1) NOT NULL,
	[SalesChannelTypeId] [int] NOT NULL,
	[Parameter] [float] NOT NULL,
 CONSTRAINT [PK_LowSeasonParameter] PRIMARY KEY CLUSTERED 
(
	[LowSeasonParameterId] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IX_LowSeasonParameter] UNIQUE NONCLUSTERED 
(
	[SalesChannelTypeId] ASC,
	[Parameter] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[RejectReason]    Script Date: 08/20/2007 15:44:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RejectReason]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[RejectReason](
	[RejectReasonId] [int] IDENTITY(1,1) NOT NULL,
	[RejectReasonIndex] [int] NOT NULL,
	[RejectReasonContent] [nvarchar](500) NOT NULL,
 CONSTRAINT [PK_RejectReason] PRIMARY KEY CLUSTERED 
(
	[RejectReasonId] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[SalesChannelType]    Script Date: 08/20/2007 15:44:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SalesChannelType]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[SalesChannelType](
	[SalesChannelTypeId] [int] IDENTITY(1,1) NOT NULL,
	[SalesChannelTypeName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_SalesChannelType] PRIMARY KEY CLUSTERED 
(
	[SalesChannelTypeId] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[SocietyReason]    Script Date: 08/20/2007 15:44:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocietyReason]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[SocietyReason](
	[SocietyReasonId] [int] IDENTITY(1,1) NOT NULL,
	[SocietyReasonIndex] [int] NOT NULL,
	[SocietyReasonName] [nvarchar](1000) NOT NULL,
	[Memo] [nvarchar](100) NULL,
 CONSTRAINT [PK_SocietyReason] PRIMARY KEY CLUSTERED 
(
	[SocietyReasonId] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[TripFeeClass]    Script Date: 08/20/2007 15:44:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TripFeeClass]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[TripFeeClass](
	[TripFeeClassId] [int] IDENTITY(1,1) NOT NULL,
	[TripFeeClassName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_TripFeeClass] PRIMARY KEY CLUSTERED 
(
	[TripFeeClassId] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[TripFeeStandard]    Script Date: 08/20/2007 15:44:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TripFeeStandard]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[TripFeeStandard](
	[TripFeeTypeId] [int] NOT NULL,
	[TripFeeClassId] [int] NOT NULL,
	[Standard] [nvarchar](800) NOT NULL,
 CONSTRAINT [PK_TripFeeStandard] PRIMARY KEY CLUSTERED 
(
	[TripFeeTypeId] ASC,
	[TripFeeClassId] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[TripFeeType]    Script Date: 08/20/2007 15:44:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TripFeeType]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[TripFeeType](
	[TripFeeTypeId] [int] IDENTITY(1,1) NOT NULL,
	[TripFeeTypeName] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_TripFeeType] PRIMARY KEY CLUSTERED 
(
	[TripFeeTypeId] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_AlterFeeSubType_AlterFeeType]') AND parent_object_id = OBJECT_ID(N'[dbo].[AlterFeeSubType]'))
ALTER TABLE [dbo].[AlterFeeSubType]  WITH CHECK ADD  CONSTRAINT [FK_AlterFeeSubType_AlterFeeType] FOREIGN KEY([AlterFeeTypeId])
REFERENCES [dbo].[AlterFeeType] ([AlterFeeTypeId])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ExchangeRate_Currency]') AND parent_object_id = OBJECT_ID(N'[dbo].[ExchangeRate]'))
ALTER TABLE [dbo].[ExchangeRate]  WITH CHECK ADD  CONSTRAINT [FK_ExchangeRate_Currency] FOREIGN KEY([CurrId])
REFERENCES [dbo].[Currency] ([CurrId])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_HighSeasonParameter_HighSeasonParameter]') AND parent_object_id = OBJECT_ID(N'[dbo].[HighSeasonParameter]'))
ALTER TABLE [dbo].[HighSeasonParameter]  WITH CHECK ADD  CONSTRAINT [FK_HighSeasonParameter_HighSeasonParameter] FOREIGN KEY([SalesChannelTypeId])
REFERENCES [dbo].[SalesChannelType] ([SalesChannelTypeId])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_LowSeasonParameter_SalesChannelType]') AND parent_object_id = OBJECT_ID(N'[dbo].[LowSeasonParameter]'))
ALTER TABLE [dbo].[LowSeasonParameter]  WITH CHECK ADD  CONSTRAINT [FK_LowSeasonParameter_SalesChannelType] FOREIGN KEY([SalesChannelTypeId])
REFERENCES [dbo].[SalesChannelType] ([SalesChannelTypeId])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TripFeeStandard_TripFeeClass]') AND parent_object_id = OBJECT_ID(N'[dbo].[TripFeeStandard]'))
ALTER TABLE [dbo].[TripFeeStandard]  WITH CHECK ADD  CONSTRAINT [FK_TripFeeStandard_TripFeeClass] FOREIGN KEY([TripFeeClassId])
REFERENCES [dbo].[TripFeeClass] ([TripFeeClassId])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TripFeeStandard_TripFeeType]') AND parent_object_id = OBJECT_ID(N'[dbo].[TripFeeStandard]'))
ALTER TABLE [dbo].[TripFeeStandard]  WITH CHECK ADD  CONSTRAINT [FK_TripFeeStandard_TripFeeType] FOREIGN KEY([TripFeeTypeId])
REFERENCES [dbo].[TripFeeType] ([TripFeeTypeId])
GO
