CREATE PROCEDURE [dbo].[SelectProductCategoryByCategoryId]
	@categoryId int
AS
BEGIN
	SET NOCOUNT ON;
	IF (@categoryId IS NULL)
	BEGIN;
		THROW 50001, '@categoryId is null', 1
	END

	IF NOT EXISTS (SELECT 1 FROM dbo.ProductCategory WHERE CategoryId = @CategoryId)
	BEGIN;
		THROW 50001, 'CategoryId does not exists', 1
	END
	
	SELECT 
		CategoryId, 
		Name, 
		ParentCategoryId, 
		CASE WHEN EXISTS (SELECT 1 FROM dbo.ProductCategory AS P WHERE ISNULL(P.ParentCategoryId, -1) = PC.CategoryId) THEN 1 ELSE 0 END AS HasChild
	FROM dbo.ProductCategory AS PC
	WHERE 
		PC.CategoryId = @categoryId
END
