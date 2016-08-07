CREATE PROCEDURE [dbo].[SelectProductByProductId]
	@productId INT
AS
BEGIN
	SET NOCOUNT ON;
	IF (@productId IS NULL)
	BEGIN;
		THROW 50001, '@product is null.', 1
	END

	IF NOT EXISTS (SELECT 1 FROM dbo.Product WHERE ProductId = @productId)
	BEGIN;
		THROW 50001, 'Product does not exists', 1
	END

	SELECT CategoryId, Name, ProductDescription FROM dbo.Product WHERE ProductId = @productId
END
