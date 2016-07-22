﻿CREATE TABLE [dbo].[Specification]
(
	SpecId INT IDENTITY(1, 1) NOT NULL,
	Name VARCHAR(100) NOT NULL,
	InsertDate DATETIME CONSTRAINT [DF_Specification_InsertDate] DEFAULT (GETUTCDATE()) NOT NULL,
	UpdateDate DATETIME NULL,
	CONSTRAINT [PK_Specification_SpecId] PRIMARY KEY CLUSTERED (SpecId)
)
GO
CREATE UNIQUE NONCLUSTERED INDEX [UIX_Specification_Name] ON dbo.Specification(Name ASC)