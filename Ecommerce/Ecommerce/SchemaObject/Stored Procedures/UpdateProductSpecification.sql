CREATE PROCEDURE [dbo].[UpdateProductSpecification]
	@productId INT,
	@categoryId INT,
	@name VARCHAR(100),
	@productDescription VARCHAR(256),
	@specification AS dbo.Specification READONLY
AS
BEGIN
	SET NOCOUNT ON;
	IF (@productId IS NULL)
	BEGIN;
		THROW 50001, '@productId is null.', 1
	END

	IF (@categoryId IS NULL)
	BEGIN;
		THROW 50001, '@categoryId is null.', 1
	END

	IF (@name IS NULL)
	BEGIN;
		THROW 50001, '@name is null.', 1
	END

	IF NOT EXISTS (SELECT 1 FROM dbo.Product WHERE ProductId = @productId) OR NOT EXISTS (SELECT 1 FROM dbo.ProductSpecificationXref WHERE ProductId = @productId)
	BEGIN;
		THROW 50001, 'Product does not exists', 1
	END

	IF (@productDescription IS NULL)
	BEGIN;
		THROW 50001, '@productDescription is null.', 1
	END

	BEGIN TRY
		BEGIN TRAN
			UPDATE dbo.Product 
			SET Name = @name,
				CategoryId = @categoryId,
				ProductDescription = @productDescription,
				UpdateDate = GETUTCDATE()
			WHERE 
				ProductId = @productId

			UPDATE PSX 
			SET SpecValue = SP.SpecValue,
				UpdateDate = GETUTCDATE()
			FROM
				dbo.ProductSpecificationXref AS PSX INNER JOIN dbo.Specification AS S
				ON PSX.SpecId = S.SpecId INNER JOIN  @specification AS SP
				ON S.Name = SP.Name
			WHERE 
				ProductId = @productId
		COMMIT
	END TRY
	BEGIN CATCH
		
		IF @@TRANCOUNT > 0
		BEGIN
			ROLLBACK TRAN
		END

		DECLARE @message		NVARCHAR(1000)		= ERROR_MESSAGE()
		DECLARE @currentTime	DATETIME			= GETDATE()
		DECLARE @proc			NVARCHAR(128)		= ERROR_PROCEDURE()
		DECLARE @lineNumber		INT					= ERROR_LINE()
		DECLARE @errorSeverity	INT					= ERROR_SEVERITY()
		DECLARE @state			INT					= ERROR_STATE()
		DECLARE @error			INT					= ERROR_NUMBER()

		DECLARE @errorMessage	NVARCHAR(4000)
		
		SELECT @errorMessage = 'Module: ' + @proc + ', Line: ' + RTRIM(LTRIM(STR(@lineNumber))) + ', ' + @message + ', Current time: ' + CONVERT(NVARCHAR(27), @currentTime, 121);
		
		RAISERROR(@errorMessage, @errorSeverity, @state) WITH LOG;
		
		THROW @error, @errorMessage, @state;
	END CATCH
END
