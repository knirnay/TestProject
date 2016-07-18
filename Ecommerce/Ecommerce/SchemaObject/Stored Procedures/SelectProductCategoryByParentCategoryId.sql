CREATE PROCEDURE [dbo].[SelectProductCategoryByParentCategoryId]
	@parentCategoryId int
AS
BEGIN
	SET NOCOUNT ON;
	IF NOT EXISTS (SELECT 1 FROM dbo.ProductCategory WHERE ParentCategoryId = @parentCategoryId OR @parentCategoryId IS NULL)
	BEGIN;
		THROW 50001, '@parentCategoryId does not exists', 1
	END

	SELECT CategoryId, Name FROM dbo.ProductCategory WHERE ParentCategoryId = @parentCategoryId
END
