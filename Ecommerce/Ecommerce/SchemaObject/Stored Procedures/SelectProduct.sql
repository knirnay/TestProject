CREATE PROCEDURE [dbo].[SelectProduct]
AS
BEGIN
	SET NOCOUNT ON;
	SELECT ProductId, CategoryId, Name, ProductDescription FROM dbo.Product
END
