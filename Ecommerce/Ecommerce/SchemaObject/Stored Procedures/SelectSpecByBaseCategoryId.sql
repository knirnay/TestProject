CREATE PROCEDURE [dbo].[SelectSpecByBaseCategoryId]
	@baseCategoryId INT
AS 
BEGIN
	SET NOCOUNT ON;

	IF (@baseCategoryId IS NULL)
	BEGIN;
		THROW 50001, '@baseCategoryId is null', 1
	END

	IF NOT EXISTS (SELECT 1 FROM SpecificationProductCategoryXref WHERE CategoryId = @baseCategoryId)
	BEGIN;
		THROW 50001, 'Base category id is missing.', 1
	END

	SELECT S.Name FROM Specification AS S INNER JOIN SpecificationProductCategoryXref AS SPCX 
	ON S.SpecId = SPCX.SpecId
	WHERE SPCX.CategoryId = @baseCategoryId
END
