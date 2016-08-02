IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'SpecificationProductCategoryXref')
BEGIN
	MERGE INTO dbo.SpecificationProductCategoryXref AS TARGET
	USING (
		SELECT * FROM (
			VALUES 
				    ((SELECT SpecId FROM dbo.Specification WHERE Name = 'Screen Size'), (SELECT CategoryId FROM dbo.ProductCategory WHERE Name = 'Electronics & Computers'))
				  , ((SELECT SpecId FROM dbo.Specification WHERE Name = 'Max Screen Resolution'), (SELECT CategoryId FROM dbo.ProductCategory WHERE Name = 'Electronics & Computers'))
				  , ((SELECT SpecId FROM dbo.Specification WHERE Name = 'Processor'), (SELECT CategoryId FROM dbo.ProductCategory WHERE Name = 'Electronics & Computers'))
				  , ((SELECT SpecId FROM dbo.Specification WHERE Name = 'RAM'), (SELECT CategoryId FROM dbo.ProductCategory WHERE Name = 'Electronics & Computers'))
				  , ((SELECT SpecId FROM dbo.Specification WHERE Name = 'Hard Drive'), (SELECT CategoryId FROM dbo.ProductCategory WHERE Name = 'Electronics & Computers'))
				  , ((SELECT SpecId FROM dbo.Specification WHERE Name = 'Graphics Coprocessor'), (SELECT CategoryId FROM dbo.ProductCategory WHERE Name = 'Electronics & Computers'))
				  , ((SELECT SpecId FROM dbo.Specification WHERE Name = 'Chipset Brand'), (SELECT CategoryId FROM dbo.ProductCategory WHERE Name = 'Electronics & Computers'))
				  , ((SELECT SpecId FROM dbo.Specification WHERE Name = 'Card Description'), (SELECT CategoryId FROM dbo.ProductCategory WHERE Name = 'Electronics & Computers'))
				  , ((SELECT SpecId FROM dbo.Specification WHERE Name = 'Wireless Type'), (SELECT CategoryId FROM dbo.ProductCategory WHERE Name = 'Electronics & Computers'))
				  , ((SELECT SpecId FROM dbo.Specification WHERE Name = 'Number of USB 3.0 Ports'), (SELECT CategoryId FROM dbo.ProductCategory WHERE Name = 'Electronics & Computers'))
				  , ((SELECT SpecId FROM dbo.Specification WHERE Name = 'Brand Name'), (SELECT CategoryId FROM dbo.ProductCategory WHERE Name = 'Electronics & Computers'))
				  , ((SELECT SpecId FROM dbo.Specification WHERE Name = 'Series'), (SELECT CategoryId FROM dbo.ProductCategory WHERE Name = 'Electronics & Computers'))
				  , ((SELECT SpecId FROM dbo.Specification WHERE Name = 'Item Model Number'), (SELECT CategoryId FROM dbo.ProductCategory WHERE Name = 'Electronics & Computers'))
				  , ((SELECT SpecId FROM dbo.Specification WHERE Name = 'Hardware Plateform'), (SELECT CategoryId FROM dbo.ProductCategory WHERE Name = 'Electronics & Computers'))
				  , ((SELECT SpecId FROM dbo.Specification WHERE Name = 'Operating System'), (SELECT CategoryId FROM dbo.ProductCategory WHERE Name = 'Electronics & Computers'))
				  , ((SELECT SpecId FROM dbo.Specification WHERE Name = 'Weight'), (SELECT CategoryId FROM dbo.ProductCategory WHERE Name = 'Electronics & Computers'))
				  , ((SELECT SpecId FROM dbo.Specification WHERE Name = 'Product Dimensions'), (SELECT CategoryId FROM dbo.ProductCategory WHERE Name = 'Electronics & Computers'))
				  , ((SELECT SpecId FROM dbo.Specification WHERE Name = 'Item Dimensions L X W X H'), (SELECT CategoryId FROM dbo.ProductCategory WHERE Name = 'Electronics & Computers'))
				  , ((SELECT SpecId FROM dbo.Specification WHERE Name = 'Color'), (SELECT CategoryId FROM dbo.ProductCategory WHERE Name = 'Electronics & Computers'))
				  , ((SELECT SpecId FROM dbo.Specification WHERE Name = 'Processor Brand'), (SELECT CategoryId FROM dbo.ProductCategory WHERE Name = 'Electronics & Computers'))
				  , ((SELECT SpecId FROM dbo.Specification WHERE Name = 'Process Count'), (SELECT CategoryId FROM dbo.ProductCategory WHERE Name = 'Electronics & Computers'))
				  , ((SELECT SpecId FROM dbo.Specification WHERE Name = 'Hard Drive Rotational Speed'), (SELECT CategoryId FROM dbo.ProductCategory WHERE Name = 'Electronics & Computers'))
				  , ((SELECT SpecId FROM dbo.Specification WHERE Name = 'Optical Drive Type'), (SELECT CategoryId FROM dbo.ProductCategory WHERE Name = 'Electronics & Computers'))
				  , ((SELECT SpecId FROM dbo.Specification WHERE Name = 'Hardware Platform'), (SELECT CategoryId FROM dbo.ProductCategory WHERE Name = 'Electronics & Computers'))
				  , ((SELECT SpecId FROM dbo.Specification WHERE Name = 'Flash Memory Size'), (SELECT CategoryId FROM dbo.ProductCategory WHERE Name = 'Electronics & Computers'))
				  , ((SELECT SpecId FROM dbo.Specification WHERE Name = 'Battery Type'), (SELECT CategoryId FROM dbo.ProductCategory WHERE Name = 'Electronics & Computers'))
				  , ((SELECT SpecId FROM dbo.Specification WHERE Name = 'Power Source'), (SELECT CategoryId FROM dbo.ProductCategory WHERE Name = 'Electronics & Computers'))
				  , ((SELECT SpecId FROM dbo.Specification WHERE Name = 'Screen Resolution'), (SELECT CategoryId FROM dbo.ProductCategory WHERE Name = 'Electronics & Computers'))
		) AS DT (SpecId, CategoryId)
	) AS SOURCE
	ON TARGET.SpecId = SOURCE.SpecId AND TARGET.CategoryId = SOURCE.CategoryId
	WHEN MATCHED THEN
		UPDATE SET TARGET.UpdateDate = GETUTCDATE()
	WHEN NOT MATCHED BY TARGET THEN
		INSERT (SpecId, CategoryId) VALUES (SpecId, CategoryId)
	WHEN NOT MATCHED BY SOURCE THEN
		DELETE;
END
ELSE
BEGIN;
	THROW 50001, 'SpecificationProductCategoryXref table is missing', 1
END