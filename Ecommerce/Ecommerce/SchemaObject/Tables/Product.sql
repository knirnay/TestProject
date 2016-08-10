﻿CREATE TABLE dbo.Product
(
	ProductId INT IDENTITY(1, 1) NOT NULL,
	CategoryId INT NOT NULL,
	Name VARCHAR(100) NOT NULL,
	ProductDescription VARCHAR(256) NOT NULL,
	InsertDate DATETIME CONSTRAINT [DF_Product_InsertDate] DEFAULT (GETUTCDATE()) NOT NULL,
	UpdateDate DATETIME NULL,
	CONSTRAINT [PK_Product_ProductId] PRIMARY KEY CLUSTERED (ProductId),
	CONSTRAINT [FK_Product_ProductCategory_CategoryId] FOREIGN KEY (CategoryId) REFERENCES dbo.ProductCategory(CategoryId) ON UPDATE CASCADE ON DELETE CASCADE
)
GO
CREATE NONCLUSTERED INDEX [UIX_Product_Name] ON dbo.Product(Name ASC)
