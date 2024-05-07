
INSERT INTO [Core].[ConfigurationCategory] ([Id], [Code], [Name], [IsAccessWrite])
	VALUES('2D4DA189-E253-4B33-B5E7-6674CA1DAB01', 'measuring', 'Measuring', 1)
;
	
	

INSERT INTO [Core].[Configuration] ([Id], [ConfigCategoryId], [Code], [Name], [Description], [AllowClientAccess], [ConfigurationValue])
	values
	('046D5666-D4C4-44B3-9677-2EC58AE54F3F', '2D4DA189-E253-4B33-B5E7-6674CA1DAB01', 'FileImportPath', 'FileImportPath', 'Path of process sheet local path for import data', 0,'D:\2020_Works\2020-02-01_MSAT-WeightRecord\trunk\02_Implement')
;
