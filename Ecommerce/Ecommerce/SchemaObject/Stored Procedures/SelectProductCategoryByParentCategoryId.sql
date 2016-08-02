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
			CASE WHEN EXISTS (SELECT 1 FROM dbo.ProductCategory AS P WHERE P.ParentCategoryId = PC.CategoryId) THEN 1 ELSE 0 END AS HasChild
		FROM dbo.ProductCategory AS PC WHERE PC.ParentCategoryId IS NULL
	END
	ELSE
	BEGIN
		IF NOT EXISTS (SELECT 1 FROM dbo.ProductCategory WHERE ParentCategoryId = @parentCategoryId AND ParentCategoryId IS NOT NULL)
		BEGIN;
			THROW 50001, '@parentCategoryId does not exists', 1
		END

		SELECT 
			PC.CategoryId, 
			PC.Name,
			PC.ParentCategoryId,
			CASE WHEN EXISTS (SELECT 1 FROM dbo.ProductCategory AS P WHERE P.ParentCategoryId = PC.CategoryId AND P.ParentCategoryId IS NOT NULL) THEN 1 ELSE 0 END AS HasChild
		FROM dbo.ProductCategory AS PC WHERE PC.ParentCategoryId = @parentCategoryId AND PC.ParentCategoryId IS NOT NULL
	END
END
