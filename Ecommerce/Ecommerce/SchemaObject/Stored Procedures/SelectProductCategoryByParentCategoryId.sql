CREATE PROCEDURE [dbo].[SelectProductCategoryByParentCategoryId]
	@parentCategoryId int = NULL
AS
BEGIN
	SET NOCOUNT ON;
	IF (@parentCategoryId IS NULL)
	BEGIN
		SELECT CategoryId, Name FROM dbo.ProductCategory WHERE ParentCategoryId IS NULL
	END
	ELSE
	BEGIN
		IF NOT EXISTS (SELECT 1 FROM dbo.ProductCategory WHERE ParentCategoryId = @parentCategoryId)
		BEGIN;
			THROW 50001, '@parentCategoryId does not exists', 1
		END

		SELECT CategoryId, Name FROM dbo.ProductCategory WHERE ParentCategoryId = @parentCategoryId
	END
END
