CREATE TABLE dbo.ProductCategory
(
	CategoryId INT IDENTITY(1, 1) NOT NULL,
	Name VARCHAR(100) NOT NULL,
	ParentCategoryId INT NULL,
	InsertDate DATETIME DEFAULT [DF_ProductCategory_InsertDate] (GETUTCDATE()) NOT NULL,
	UpdateDate DATETIME NULL,
	CONSTRAINT [PK_ProductCategory_CategoryId] PRIMARY KEY CLUSTERED (CategoryId),
	CONSTRAINT [FK_ProductCategory_ProductCategory_CategoryId] FOREIGN KEY (ParentCategoryId) REFERENCES dbo.ProductCategory(CategoryId)
)