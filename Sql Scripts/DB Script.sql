USE [ManagerIO]
GO
/****** Object:  Table [dbo].[Accounts]    Script Date: 5/30/2020 7:35:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Accounts](
	[AccountID] [int] IDENTITY(1,1) NOT NULL,
	[AccountKey] [varchar](255) NULL,
	[AccountName] [varchar](255) NULL,
	[AccountType] [int] NOT NULL,
	[BusinessID] [decimal](30, 0) NULL,
	[BusinessKey] [varchar](255) NULL,
	[IsActive] [bit] NOT NULL,
	[CreditLimit] [decimal](30, 0) NULL,
	[Code] [varchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[AccountID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Attachment]    Script Date: 5/30/2020 7:35:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Attachment](
	[AttachmentID] [int] IDENTITY(1,1) NOT NULL,
	[AttachmentKey] [varchar](255) NULL,
	[AttachmentDate] [date] NULL,
	[Name] [varchar](255) NULL,
	[ContentType] [varchar](255) NULL,
	[Size] [decimal](30, 0) NULL,
	[InnerObjectKey] [varchar](255) NULL,
	[BusinessID] [decimal](30, 0) NULL,
	[BusinessKey] [varchar](255) NULL,
	[IsActive] [bit] NOT NULL,
	[AttachmentUrl] [varchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[AttachmentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Business]    Script Date: 5/30/2020 7:35:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Business](
	[BusinessID] [decimal](30, 0) IDENTITY(1,1) NOT NULL,
	[BusinessKey] [varchar](255) NULL,
	[BusinessName] [varchar](255) NULL,
	[IsActive] [bit] NOT NULL,
	[BaseApiUrl] [varchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[BusinessID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[BusinessDetail]    Script Date: 5/30/2020 7:35:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[BusinessDetail](
	[BusinessDetailID] [int] IDENTITY(1,1) NOT NULL,
	[BusinessDetailKey] [varchar](255) NULL,
	[Name] [varchar](255) NULL,
	[Address] [varchar](max) NULL,
	[BusinessID] [decimal](30, 0) NULL,
	[BusinessKey] [varchar](255) NULL,
	[IsActive] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[BusinessDetailID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[BusinessLogo]    Script Date: 5/30/2020 7:35:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[BusinessLogo](
	[BusinessLogoID] [int] IDENTITY(1,1) NOT NULL,
	[BusinessLogoKey] [varchar](255) NULL,
	[ContentType] [varchar](255) NULL,
	[Content] [varchar](max) NULL,
	[BusinessID] [decimal](30, 0) NULL,
	[BusinessKey] [varchar](255) NULL,
	[IsActive] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[BusinessLogoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ChartofAccounts]    Script Date: 5/30/2020 7:35:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ChartofAccounts](
	[AccountID] [int] IDENTITY(1,1) NOT NULL,
	[AccountKey] [varchar](255) NULL,
	[AccountName] [varchar](255) NULL,
	[ParentAccountID] [int] NULL,
	[ParentAccountKey] [varchar](255) NULL,
	[BusinessID] [decimal](30, 0) NULL,
	[BusinessKey] [varchar](255) NULL,
	[IsActive] [bit] NOT NULL,
	[Position] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[AccountID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Component]    Script Date: 5/30/2020 7:35:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Component](
	[ComponentID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](255) NULL,
	[Rate] [varchar](255) NULL,
	[Account] [varchar](255) NULL,
	[BusinessID] [decimal](30, 0) NULL,
	[BusinessKey] [varchar](255) NULL,
	[IsActive] [bit] NOT NULL,
	[TaxCodeID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ComponentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Customers]    Script Date: 5/30/2020 7:35:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Customers](
	[CustomerID] [int] IDENTITY(1,1) NOT NULL,
	[CustomerKey] [varchar](255) NULL,
	[CustomerName] [varchar](255) NULL,
	[CustomerGSTID] [varchar](255) NULL,
	[CustomerAddress] [varchar](max) NULL,
	[BusinessID] [decimal](30, 0) NULL,
	[BusinessKey] [varchar](255) NULL,
	[IsActive] [bit] NOT NULL,
	[Email] [varchar](255) NULL,
	[Code] [varchar](255) NULL,
	[CreditLimit] [decimal](30, 0) NULL,
PRIMARY KEY CLUSTERED 
(
	[CustomerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Description]    Script Date: 5/30/2020 7:35:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Description](
	[DescriptionID] [int] IDENTITY(1,1) NOT NULL,
	[Description] [varchar](255) NULL,
	[Qty] [varchar](255) NULL,
	[Item] [varchar](255) NULL,
	[Amount] [decimal](30, 5) NULL,
	[Account] [varchar](255) NULL,
	[TaxCode] [varchar](255) NULL,
	[IsActive] [bit] NOT NULL,
	[Discount] [decimal](15, 0) NULL,
	[DiscountType] [int] NULL,
	[EntityID] [int] NULL,
	[EntityTypeID] [int] NULL,
	[InvoiceKey] [varchar](255) NULL,
	[Sequence] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[DescriptionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Folders]    Script Date: 5/30/2020 7:35:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Folders](
	[FolderID] [int] IDENTITY(1,1) NOT NULL,
	[FolderKey] [varchar](255) NULL,
	[FolderName] [varchar](255) NULL,
	[BusinessID] [decimal](30, 0) NULL,
	[BusinessKey] [varchar](255) NULL,
	[IsActive] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[FolderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[InnerObject]    Script Date: 5/30/2020 7:35:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[InnerObject](
	[InnerObjectID] [int] IDENTITY(1,1) NOT NULL,
	[InnerObjectKey] [varchar](255) NULL,
	[InnerObjectName] [varchar](255) NULL,
	[ParentObjectID] [int] NULL,
	[ParentObjectKey] [varchar](255) NULL,
	[BusinessID] [decimal](30, 0) NULL,
	[BusinessKey] [varchar](255) NULL,
	[IsActive] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[InnerObjectID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[InventoryItem]    Script Date: 5/30/2020 7:35:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[InventoryItem](
	[InventoryID] [int] IDENTITY(1,1) NOT NULL,
	[InventoryKey] [varchar](255) NULL,
	[InventoryName] [varchar](255) NULL,
	[Description] [varchar](255) NULL,
	[BusinessID] [decimal](30, 0) NULL,
	[BusinessKey] [varchar](255) NULL,
	[IsActive] [bit] NOT NULL,
	[Code] [varchar](255) NULL,
	[UnitName] [varchar](255) NULL,
	[PurchasePrice] [decimal](15, 0) NULL,
	[SalePrice] [decimal](15, 0) NULL,
	[TaxCodeKey] [varchar](255) NULL,
	[IncomeAccountKey] [varchar](255) NULL,
	[IsInactiveInApi] [bit] NULL,
	[ExpenseAccountKey] [varchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[InventoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Invoice]    Script Date: 5/30/2020 7:35:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Invoice](
	[InvoiceID] [int] IDENTITY(1,1) NOT NULL,
	[InvoiceKey] [varchar](255) NULL,
	[InvoiceBillNo] [varchar](255) NULL,
	[IssueDate] [date] NULL,
	[PartyKey] [varchar](255) NULL,
	[InvoiceSummary] [varchar](255) NULL,
	[RoundingMethod] [int] NULL,
	[IsRounding] [bit] NULL,
	[InvoiceType] [int] NULL,
	[PartyType] [int] NULL,
	[BusinessID] [decimal](30, 0) NULL,
	[BusinessKey] [varchar](255) NULL,
	[IsActive] [bit] NOT NULL,
	[IsDiscount] [bit] NULL,
	[DiscountType] [int] NULL,
	[IsAmountsIncludeTax] [bit] NULL,
	[PurchaseOrderNo] [varchar](255) NULL,
	[SalesQuoteNo] [varchar](255) NULL,
	[Address] [varchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[InvoiceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Object]    Script Date: 5/30/2020 7:35:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Object](
	[ObjectID] [int] IDENTITY(1,1) NOT NULL,
	[ObjectKey] [varchar](255) NULL,
	[ObjectName] [varchar](255) NULL,
	[IsActive] [bit] NOT NULL,
	[OrderBy] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ObjectID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ReceiptsPayments]    Script Date: 5/30/2020 7:35:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ReceiptsPayments](
	[ReceiptPaymentID] [int] IDENTITY(1,1) NOT NULL,
	[ReceiptPaymentKey] [varchar](255) NULL,
	[ReceiptPaymentDate] [date] NULL,
	[AccountKey] [varchar](255) NULL,
	[Name] [varchar](255) NULL,
	[Payee] [varchar](255) NULL,
	[TransectionType] [int] NULL,
	[BusinessID] [decimal](30, 0) NULL,
	[BusinessKey] [varchar](255) NULL,
	[IsActive] [bit] NOT NULL,
	[IsAmountsIncludeTax] [bit] NULL,
	[Reference] [varchar](255) NULL,
	[IsBalanceClear] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[ReceiptPaymentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Suppliers]    Script Date: 5/30/2020 7:35:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Suppliers](
	[SupplierID] [int] IDENTITY(1,1) NOT NULL,
	[SupplierKey] [varchar](255) NULL,
	[Name] [varchar](255) NULL,
	[Email] [varchar](255) NULL,
	[Address] [varchar](max) NULL,
	[BusinessID] [decimal](30, 0) NULL,
	[BusinessKey] [varchar](255) NULL,
	[IsActive] [bit] NOT NULL,
	[Code] [varchar](255) NULL,
	[CreditLimit] [decimal](30, 0) NULL,
PRIMARY KEY CLUSTERED 
(
	[SupplierID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TaxCodes]    Script Date: 5/30/2020 7:35:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TaxCodes](
	[TaxCodeID] [int] IDENTITY(1,1) NOT NULL,
	[ComponentKey] [varchar](255) NULL,
	[TaxCodeName] [varchar](255) NULL,
	[TaxRate] [varchar](255) NULL,
	[TaxRateType] [varchar](255) NULL,
	[Account] [varchar](255) NULL,
	[SalesInvoiceTitle] [varchar](255) NULL,
	[CustomSalesInvoiceTitle] [bit] NULL,
	[BusinessID] [decimal](30, 0) NULL,
	[BusinessKey] [varchar](255) NULL,
	[IsActive] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[TaxCodeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Accounts]  WITH CHECK ADD  CONSTRAINT [fk_accounts_business] FOREIGN KEY([BusinessID])
REFERENCES [dbo].[Business] ([BusinessID])
GO
ALTER TABLE [dbo].[Accounts] CHECK CONSTRAINT [fk_accounts_business]
GO
ALTER TABLE [dbo].[Attachment]  WITH CHECK ADD  CONSTRAINT [fk_attachment_business] FOREIGN KEY([BusinessID])
REFERENCES [dbo].[Business] ([BusinessID])
GO
ALTER TABLE [dbo].[Attachment] CHECK CONSTRAINT [fk_attachment_business]
GO
ALTER TABLE [dbo].[BusinessDetail]  WITH CHECK ADD  CONSTRAINT [fk_businessdetail_business] FOREIGN KEY([BusinessID])
REFERENCES [dbo].[Business] ([BusinessID])
GO
ALTER TABLE [dbo].[BusinessDetail] CHECK CONSTRAINT [fk_businessdetail_business]
GO
ALTER TABLE [dbo].[BusinessLogo]  WITH CHECK ADD  CONSTRAINT [fk_businesslogo_business] FOREIGN KEY([BusinessID])
REFERENCES [dbo].[Business] ([BusinessID])
GO
ALTER TABLE [dbo].[BusinessLogo] CHECK CONSTRAINT [fk_businesslogo_business]
GO
ALTER TABLE [dbo].[ChartofAccounts]  WITH CHECK ADD  CONSTRAINT [fk_chartacc_business] FOREIGN KEY([BusinessID])
REFERENCES [dbo].[Business] ([BusinessID])
GO
ALTER TABLE [dbo].[ChartofAccounts] CHECK CONSTRAINT [fk_chartacc_business]
GO
ALTER TABLE [dbo].[Component]  WITH CHECK ADD  CONSTRAINT [fk_component_business] FOREIGN KEY([BusinessID])
REFERENCES [dbo].[Business] ([BusinessID])
GO
ALTER TABLE [dbo].[Component] CHECK CONSTRAINT [fk_component_business]
GO
ALTER TABLE [dbo].[Component]  WITH CHECK ADD  CONSTRAINT [fk_taxcode_component] FOREIGN KEY([TaxCodeID])
REFERENCES [dbo].[TaxCodes] ([TaxCodeID])
GO
ALTER TABLE [dbo].[Component] CHECK CONSTRAINT [fk_taxcode_component]
GO
ALTER TABLE [dbo].[Customers]  WITH CHECK ADD  CONSTRAINT [fk_customer_business] FOREIGN KEY([BusinessID])
REFERENCES [dbo].[Business] ([BusinessID])
GO
ALTER TABLE [dbo].[Customers] CHECK CONSTRAINT [fk_customer_business]
GO
ALTER TABLE [dbo].[Folders]  WITH CHECK ADD  CONSTRAINT [fk_folder_business] FOREIGN KEY([BusinessID])
REFERENCES [dbo].[Business] ([BusinessID])
GO
ALTER TABLE [dbo].[Folders] CHECK CONSTRAINT [fk_folder_business]
GO
ALTER TABLE [dbo].[InnerObject]  WITH CHECK ADD  CONSTRAINT [fk_innerobject_business] FOREIGN KEY([BusinessID])
REFERENCES [dbo].[Business] ([BusinessID])
GO
ALTER TABLE [dbo].[InnerObject] CHECK CONSTRAINT [fk_innerobject_business]
GO
ALTER TABLE [dbo].[InnerObject]  WITH CHECK ADD  CONSTRAINT [fk_innerobject_object] FOREIGN KEY([ParentObjectID])
REFERENCES [dbo].[Object] ([ObjectID])
GO
ALTER TABLE [dbo].[InnerObject] CHECK CONSTRAINT [fk_innerobject_object]
GO
ALTER TABLE [dbo].[InventoryItem]  WITH CHECK ADD  CONSTRAINT [fk_invItem_business] FOREIGN KEY([BusinessID])
REFERENCES [dbo].[Business] ([BusinessID])
GO
ALTER TABLE [dbo].[InventoryItem] CHECK CONSTRAINT [fk_invItem_business]
GO
ALTER TABLE [dbo].[Invoice]  WITH CHECK ADD  CONSTRAINT [fk_inv_business] FOREIGN KEY([BusinessID])
REFERENCES [dbo].[Business] ([BusinessID])
GO
ALTER TABLE [dbo].[Invoice] CHECK CONSTRAINT [fk_inv_business]
GO
ALTER TABLE [dbo].[ReceiptsPayments]  WITH CHECK ADD  CONSTRAINT [fk_receiptandpayment_business] FOREIGN KEY([BusinessID])
REFERENCES [dbo].[Business] ([BusinessID])
GO
ALTER TABLE [dbo].[ReceiptsPayments] CHECK CONSTRAINT [fk_receiptandpayment_business]
GO
ALTER TABLE [dbo].[Suppliers]  WITH CHECK ADD  CONSTRAINT [fk_supplier_business] FOREIGN KEY([BusinessID])
REFERENCES [dbo].[Business] ([BusinessID])
GO
ALTER TABLE [dbo].[Suppliers] CHECK CONSTRAINT [fk_supplier_business]
GO
ALTER TABLE [dbo].[TaxCodes]  WITH CHECK ADD  CONSTRAINT [fk_taxcode_business] FOREIGN KEY([BusinessID])
REFERENCES [dbo].[Business] ([BusinessID])
GO
ALTER TABLE [dbo].[TaxCodes] CHECK CONSTRAINT [fk_taxcode_business]
GO
