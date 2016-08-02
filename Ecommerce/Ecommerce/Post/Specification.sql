IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'Specification')
BEGIN
	MERGE INTO dbo.Specification AS TARGET
	USING (
		SELECT * FROM (
			VALUES
				  ('Screen Size')
				, ('Max Screen Resolution')
				, ('Processor')
				, ('RAM')
				, ('Hard Drive')
				, ('Graphics Coprocessor')
				, ('Chipset Brand')
				, ('Card Description')
				, ('Wireless Type')
				, ('Number of USB 3.0 Ports')
				, ('Brand Name')
				, ('Series')
				, ('Item Model Number')
				, ('Hardware Plateform')
				, ('Operating System')
				, ('Weight')
				, ('Product Dimensions')
				, ('Item Dimensions L X W X H')
				, ('Color')
				, ('Processor Brand')
				, ('Process Count')
				, ('Hard Drive Rotational Speed')
				, ('Optical Drive Type')
				, ('Hardware Platform')
				, ('Flash Memory Size')
				, ('Battery Type')
				, ('Power Source')
				, ('Screen Resolution')
			) AS DT (Name)
		) AS SOURCE
		ON TARGET.Name = SOURCE.Name 
		WHEN MATCHED THEN
			UPDATE SET TARGET.UpdateDate = GETUTCDATE()
		WHEN NOT MATCHED BY TARGET THEN
			INSERT (Name) VALUES (SOURCE.Name)
		WHEN NOT MATCHED BY SOURCE THEN
			DELETE;
END
ELSE
BEGIN;
	THROW 50001, 'Specification table is missing', 1
END