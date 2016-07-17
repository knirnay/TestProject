IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'ProductCategory')
BEGIN
	MERGE INTO dbo.ProductCategory AS TARGET
	USING (
		SELECT * FROM (
			VALUES 
				  (1, 'Electronics & Computers', NULL)
				, (2, 'Books & Audible', NULL)
				, (3, 'Movies, Music & Games', NULL)
				, (4, 'Home, Garden & Tools', NULL)
				, (5, 'Beauty, Health & Grocery', NULL)
				, (6, 'Toys, Kids & Baby', NULL)
				, (7, 'Clothing, Shoes & Jewelry', NULL)
				, (8, 'Sports & Outdoors', NULL)
				, (9, 'Automotive & Industrial', NULL)
				, (10, 'Electornics', 1)
				, (11, 'Computers', 1)
				, (12, 'TV & Videos', 10)
				, (13, 'Home Audio & Theater', 10)
				, (14, 'Camera, Photo & Video', 10)
				, (15, 'Cell Phones & Accessories', 10)
				, (16, 'Headphones', 10)
				, (17, 'Video Games', 10)
				, (18, 'Bluetooth & Wireless Speakers', 10)
				, (19, 'Car Electronics', 10)
				, (20, 'Musical Instruments', 10)
				, (21, 'Wearable Technology', 10)
				, (22, 'Computers & Tablets', 11)
				, (23, 'Monitors', 11)
				, (24, 'Accessories', 11)
				, (25, 'Networking', 11)
				, (26, 'Drives & Storage', 11)
				, (27, 'Computer Parts & Components', 11)
				, (28, 'Software', 11)
				, (29, 'Printers & Ink', 11)
		) as dt (CategoryId, Name, ParentCategoryId)
	) AS SOURCE
	ON TARGET.Name = SOURCE.Name AND TARGET.CategoryId = SOURCE.CategoryId
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
			INSERT (CategoryId, Name, ParentCategoryId)
			VALUES (SOURCE.CategoryId, SOURCE.Name, SOURCE.ParentCategoryId)
	WHEN NOT MATCHED BY SOURCE 
		THEN DELETE;
END
ELSE
BEGIN;
	THROW 50001, 'ProductCategory table missing.', 1
END;
