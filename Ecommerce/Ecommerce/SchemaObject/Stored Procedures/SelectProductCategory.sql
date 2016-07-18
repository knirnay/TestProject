CREATE PROCEDURE [dbo].[SelectProductCategory]
AS
BEGIN
	SET NOCOUNT ON;
	SELECT CategoryId, Name FROM dbo.ProductCategory
END
