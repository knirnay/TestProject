CREATE TABLE [dbo].[ProductSpecificationXref]
(
	ProductId INT NOT NULL,
	SpecId INT NOT NULL,
	SpecValue VARCHAR(100) NULL,
	InsertDate DATETIME CONSTRAINT [DF_ProductSpecificationXref_InsertDate] DEFAULT (GETUTCDATE()) NOT NULL,
	UpdateDate DATETIME NULL,
	CONSTRAINT [PK_ProductSpecificationXref_ProductId_SpecId] PRIMARY KEY CLUSTERED (ProductId ASC, SpecId ASC),
	CONSTRAINT [FK_ProductSpecificationXref_Specification_SpecId] FOREIGN KEY (SpecId) REFERENCES dbo.Specification(SpecId) ON UPDATE CASCADE ON DELETE CASCADE,
	CONSTRAINT [FK_ProductSpecificationXref_Product_ProductId] FOREIGN KEY (ProductId) REFERENCES dbo.Product(ProductId) ON UPDATE CASCADE ON DELETE CASCADE
)
