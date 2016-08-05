CREATE PROCEDURE [dbo].[InsertNewProductWithSpec]
	@categoryId INT,
	@name VARCHAR(100),
	@description VARCHAR(256),
	@specification AS dbo.Specification READONLY
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY
		IF (@categoryId IS NULL)
		BEGIN;
			THROW 50001, '@categoryId is null', 1
		END

		IF (@name IS NULL)
		BEGIN;
			THROW 50001, '@name is null', 1
		END

		IF (@description IS NULL)
		BEGIN;
			THROW 50001, '@description is null', 1
		END

		IF NOT EXISTS (SELECT 1 FROM dbo.ProductCategory WHERE CategoryId = @categoryId)
		BEGIN;
			THROW 50001, 'Category does not exists', 1
		END

		IF EXISTS (SELECT 1 FROM dbo.Product WHERE Name = @name)
		BEGIN;
			THROW 50001, 'Product name already exists', 1
		END

		IF NOT EXISTS (SELECT 1 FROM dbo.Specification AS SPEC WHERE EXISTS (SELECT 1 FROM @specification AS S WHERE S.Name = SPEC.Name))
		BEGIN;
			THROW 50001, 'Specification does not exists', 1
		END

		DECLARE @Product TABLE (
			ProductId INT NOT NULL
		)

		BEGIN TRAN
			INSERT dbo.Product(Name, CategoryId, ProductDescription)
			OUTPUT INSERTED.ProductId INTO @Product(ProductId)
			VALUES (@name, @categoryId, @description)

			INSERT INTO dbo.ProductSpecificationXref(SpecId, ProductId, SpecValue)
			SELECT SPEC.SpecId, P.ProductId, S.SpecValue FROM @specification AS S INNER JOIN dbo.Specification AS SPEC ON S.Name = SPEC.Name CROSS APPLY @Product AS P
			WHERE S.SpecValue IS NOT NULL
		COMMIT

		SELECT ProductId FROM @Product;
	END TRY
	BEGIN CATCH
	
		IF @@TRANCOUNT > 0
		BEGIN
			ROLLBACK TRAN
		END

		DECLARE @MESSAGE           NVARCHAR(1000)	= ERROR_MESSAGE()
		DECLARE @CURRENT_TIME      DATETIME			= GETDATE()
		DECLARE @PROC              NVARCHAR(128)	= ERROR_PROCEDURE()
		DECLARE @LINE_NUMBER       INT				= ERROR_LINE()
		DECLARE @ERROR_SEVERITY    INT				= ERROR_SEVERITY()
		DECLARE @STATE             INT				= ERROR_STATE()
		DECLARE @ERROR             INT				= ERROR_NUMBER()

		DECLARE @ERROR_MESSAGE    NVARCHAR(4000)

		SELECT  @ERROR_MESSAGE = 'Module: ' + @PROC + ', Line: ' + RTRIM(LTRIM(STR(@LINE_NUMBER))) + ', ' + @MESSAGE + ', Current time: ' + CONVERT(NVARCHAR(27),@CURRENT_TIME,121);
		
		RAISERROR(@ERROR_MESSAGE, @ERROR_SEVERITY, @STATE) WITH LOG;

		THROW @ERROR, @ERROR_MESSAGE, @STATE;

	END CATCH
END