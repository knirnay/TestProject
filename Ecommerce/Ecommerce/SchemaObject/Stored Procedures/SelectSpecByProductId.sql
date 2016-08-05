CREATE PROCEDURE [dbo].[SelectSpecByProductId]
	@productId INT
AS
BEGIN
	SET NOCOUNT ON;
	IF (@productId IS NULL)
	BEGIN;
		THROW 50001, '@productId is null', 1
	END

	IF NOT EXISTS (SELECT 1 FROM dbo.ProductSpecificationXref WHERE ProductId = @productId)
	BEGIN;
		THROW 50001, 'Product does not exist', 1
	END

	SELECT S.Name, SPX.SpecValue FROM dbo.ProductSpecificationXref AS SPX INNER JOIN dbo.Specification AS S
	ON SPX.SpecId = S.SpecId
	WHERE ProductId = @productId
END
