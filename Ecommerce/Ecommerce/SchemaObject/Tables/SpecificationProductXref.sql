CREATE TABLE [dbo].[SpecificationProductXref]
(
	SpecId INT NOT NULL,
	ProductId INT NOT NULL,
	SpecValue VARCHAR(100) NOT NULL,
	InsertDate DATETIME CONSTRAINT [DF_ProductSpecificationXref_InsertDate] DEFAULT (GETUTCDATE()) NOT NULL,
	UpdateDate DATETIME NULL,
	CONSTRAINT [PK_ProductSpecificationXref_SpecId_ProductId] PRIMARY KEY CLUSTERED (SpecId ASC, ProductId ASC),
	CONSTRAINT [FK_ProductSpecificationXref_Specification_SpecId] FOREIGN KEY (SpecId) REFERENCES dbo.Specification(SpecId) ON UPDATE CASCADE ON DELETE CASCADE,
	CONSTRAINT [FK_ProductSpecificationXref_Product_ProductId] FOREIGN KEY (ProductId) REFERENCES dbo.Product(ProductId) ON UPDATE CASCADE ON DELETE CASCADE
)
