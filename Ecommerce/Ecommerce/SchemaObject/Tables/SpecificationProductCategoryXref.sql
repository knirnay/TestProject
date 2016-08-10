CREATE TABLE [dbo].[SpecificationProductCategoryXref]
(
	SpecId INT NOT NULL,
	CategoryId	INT NOT NULL,
	InsertDate DATETIME CONSTRAINT [DF_InsertDate_SpecificationProductCategoryXref] DEFAULT (GETDATE()) NOT NULL,
	UpdateDate DATETIME NULL,
	CONSTRAINT [PK_SpecificationProductCategoryXref_SpecId_CategoryId] PRIMARY KEY CLUSTERED (SpecId ASC, CategoryId ASC),
	CONSTRAINT [FK_SpecificationProductCategoryXref_Specification_SpecId] FOREIGN KEY (SpecId) REFERENCES dbo.Specification(SpecId) ON UPDATE CASCADE ON DELETE CASCADE,
	CONSTRAINT [FK_SpecificationProductCategoryXref_Specification_CategoryId] FOREIGN KEY (CategoryId) REFERENCES dbo.ProductCategory(CategoryId) ON UPDATE CASCADE ON DELETE CASCADE
)
