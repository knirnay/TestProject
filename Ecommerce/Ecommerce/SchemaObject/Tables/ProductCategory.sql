CREATE TABLE dbo.ProductCategory
(
	CategoryId INT NOT NULL,
	Name VARCHAR(100) NOT NULL,
	ParentCategoryId INT NULL,
	InsertDate DATETIME CONSTRAINT [DF_ProductCategory_InsertDate] DEFAULT (GETUTCDATE()) NOT NULL,
	UpdateDate DATETIME NULL,
	CONSTRAINT [PK_ProductCategory_CategoryId] PRIMARY KEY CLUSTERED (CategoryId),
	CONSTRAINT [FK_ProductCategory_ProductCategory_CategoryId] FOREIGN KEY (ParentCategoryId) REFERENCES dbo.ProductCategory(CategoryId)
)
GO
CREATE UNIQUE NONCLUSTERED INDEX [UIX_ProductCategory_Name] ON dbo.ProductCategory(Name ASC) 