CREATE PROCEDURE [dbo].[SelectProductCategoryByCategoryId]
	@categoryId int
AS
BEGIN
	SET NOCOUNT ON;
	IF (@categoryId IS NULL)
	BEGIN;
		THROW 50001, '@categoryId is null', 1
	END

	DECLARE @name VARCHAR(100)
	SELECT @name = Name FROM dbo.ProductCategory WHERE CategoryId = @categoryId;
	IF (@name IS NULL)
	BEGIN;
		THROW 50001, 'CategoryId does not exists', 1
	END

	SELECT @name AS Name;
END
