IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE NAME = N'ProductCategory')
BEGIN
	MERGE INTO dbo.ProductCategory AS TARGET
	USING (
		SELECT * FROM (
			VALUES 
				  ('Electronics & Computers', NULL)
				, ('Books & Audible', NULL)
				, ('Movies, Music & Games', NULL)
				, ('Home, Garden & Tools', NULL)
				, ('Beauty, Health & Grocery', NULL)
				, ('Toys, Kids & Baby', NULL)
				, ('Clothing, Shoes & Jewelry', NULL)
				, ('Sports & Outdoors', NULL)
				, ('Automotive & Industrial', NULL)
				, ('Electornics', (SELECT CategoryId FROM dbo.ProductCategory WHERE Name = 'Electronics & Computers'))
				, ('Computers', (SELECT CategoryId FROM dbo.ProductCategory WHERE Name = 'Electornics & Computers'))
				, ('TV & Videos', (SELECT CategoryId FROM dbo.ProductCategory WHERE Name = 'Electornics'))
				, ('Home Audio & Theater', (SELECT CategoryId FROM dbo.ProductCategory WHERE Name = 'Electronics'))
				, ('Camera, Photo & Video', (SELECT CategoryId FROM dbo.ProductCategory WHERE Name = 'Electronics'))
				, ('Cell Phones & Accessories', (SELECT CategoryId FROM dbo.ProductCategory WHERE Name = 'Electronics'))
				, ('Headphones', (SELECT CategoryId FROM dbo.ProductCategory WHERE Name = 'Electronics'))
				, ('Video Games', (SELECT CategoryId FROM dbo.ProductCategory WHERE Name = 'Electronics'))
				, ('Bluetooth & Wireless Speakers', (SELECT CategoryId FROM dbo.ProductCategory WHERE Name = 'Electronics'))
				, ('Car Electronics', (SELECT CategoryId FROM dbo.ProductCategory WHERE Name = 'Electronics'))
				, ('Musical Instruments', (SELECT CategoryId FROM dbo.ProductCategory WHERE Name = 'Electronics'))
				, ('Wearable Technology', (SELECT CategoryId FROM dbo.ProductCategory WHERE Name = 'Electronics'))
				, ('Computers & Tablets', (SELECT CategoryId FROM dbo.ProductCategory WHERE Name = 'Computers'))
				, ('Monitors', (SELECT CategoryId FROM dbo.ProductCategory WHERE Name = 'Computers'))
				, ('Accessories', (SELECT CategoryId FROM dbo.ProductCategory WHERE Name = 'Computers'))
				, ('Networking', (SELECT CategoryId FROM dbo.ProductCategory WHERE Name = 'Computers'))
				, ('Drives & Storage', (SELECT CategoryId FROM dbo.ProductCategory WHERE Name = 'Computers'))
				, ('Computer Parts & Components', (SELECT CategoryId FROM dbo.ProductCategory WHERE Name = 'Computers'))
				, ('Software', (SELECT CategoryId FROM dbo.ProductCategory WHERE Name = 'Computers'))
				, ('Printers & Ink', (SELECT CategoryId FROM dbo.ProductCategory WHERE Name = 'Computers'))
		) as dt (Name, ParentCategoryId)
	) AS SOURCE
	ON TARGET.Name = SOURCE.Name
	WHEN MATCHED AND (
						(TARGET.ParentCategoryId IS NOT NULL AND SOURCE.ParentCategoryId IS NOT NULL AND TARGET.ParentCategoryId != SOURCE.ParentCategoryId)
						OR
						(TARGET.ParentCategoryId IS NOT NULL AND SOURCE.ParentCategoryId IS NULL)
						OR 
						(TARGET.ParentCategoryId IS NULL AND SOURCE.ParentCategoryId IS NOT NULL)
					) 
		THEN UPDATE SET TARGET.ParentCategoryId = SOURCE.ParentCategoryId, UpdateDate = GETUTCDATE()
	WHEN NOT MATCHED BY TARGET 
		THEN
			INSERT (Name, ParentCategoryId)
			VALUES (SOURCE.Name, SOURCE.ParentCategoryId)
	WHEN NOT MATCHED BY SOURCE 
		THEN DELETE;
END
