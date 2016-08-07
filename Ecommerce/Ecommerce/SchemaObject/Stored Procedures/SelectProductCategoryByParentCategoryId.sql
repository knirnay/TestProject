CREATE PROCEDURE [dbo].[SelectProductCategoryByParentCategoryId]
	@parentCategoryId int = NULL
AS
BEGIN
	SET NOCOUNT ON;
	IF (@parentCategoryId IS NULL)
	BEGIN
		SELECT 
			PC.CategoryId, 
			PC.Name,
			PC.ParentCategoryId, 
			CASE WHEN EXISTS (SELECT 1 FROM dbo.ProductCategory AS P WHERE ISNULL(P.ParentCategoryId, -1) = PC.CategoryId) THEN 1 ELSE 0 END AS HasChild
		FROM dbo.ProductCategory AS PC WHERE PC.ParentCategoryId IS NULL
	END
	ELSE
	BEGIN
		IF NOT EXISTS (SELECT 1 FROM dbo.ProductCategory WHERE ISNULL(ParentCategoryId, -1) = @parentCategoryId)
		BEGIN;
			THROW 50001, '@parentCategoryId does not exists', 1
		END

		SELECT 
			PC.CategoryId, 
			PC.Name,
			PC.ParentCategoryId,
			CASE WHEN EXISTS (SELECT 1 FROM dbo.ProductCategory AS P WHERE ISNULL(P.ParentCategoryId, -1) = PC.CategoryId AND P.ParentCategoryId IS NOT NULL) THEN 1 ELSE 0 END AS HasChild
		FROM dbo.ProductCategory AS PC WHERE ISNULL(PC.ParentCategoryId, -1) = @parentCategoryId
	END
END
