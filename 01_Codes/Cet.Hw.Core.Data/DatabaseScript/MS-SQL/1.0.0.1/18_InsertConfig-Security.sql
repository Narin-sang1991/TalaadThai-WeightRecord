




INSERT INTO [Core].[ConfigurationCategory] ([Id], [Code], [Name], [IsAccessWrite])
	VALUES('7ca2b4b3-b80b-4e72-b201-c67d72ad6942', 'security', 'Security', 1)
;
	
	
	
INSERT INTO [Core].[Configuration] ([Id], [ConfigCategoryId], [Code], [Name], [Description], [AllowClientAccess])
	values
	('defe82e4-5eff-4da5-a660-55ad3b3a4419', '7ca2b4b3-b80b-4e72-b201-c67d72ad6942', 'RequireUpperChar', 'Require upper charactor of password', 'false', 0),
	('88dd898e-5a44-4aeb-8c5b-15fbe5f8bf96', '7ca2b4b3-b80b-4e72-b201-c67d72ad6942', 'RequireLowerChar', 'Require lower charactor of password', 'true', 0),
	('b49e1a5e-37e2-4873-b606-de67a520cafd', '7ca2b4b3-b80b-4e72-b201-c67d72ad6942', 'RequireNumeric', 'Require numeric of password', 'true', 0),
	('acc07d8e-e029-4dc4-a375-df49f68cba78', '7ca2b4b3-b80b-4e72-b201-c67d72ad6942', 'RequireSpecialChar', 'Require special charactor of password', 'false', 0),
	('9cc816b4-b494-4b84-8583-88c9ee29ffb9', '7ca2b4b3-b80b-4e72-b201-c67d72ad6942', 'PasswordLength', 'Length of password', '8', 0)
;

INSERT INTO [Core].[Configuration] ([Id], [ConfigCategoryId], [Code], [Name], [Description], [AllowClientAccess], [ConfigurationValue])
	values
	('d1c0a36b-5916-4b49-8c6d-713780e3edd2', '4a9dc41e-96e9-4034-b436-41445d6fdd34', 'EmailConfirmationTime', 'EmailConfirmationTime', 'Time of emailconfirmation after register [Default=1440/1day] minute', 0,'1440'),
	('64b52e91-3d94-493d-988d-afba2c9870ad', 'a34a341c-7377-48b5-9e55-935cb21dbb02', 'InactiveTime', 'InactiveTime', 'Inactive Time of access permission [Default=30] minute', 0,'30')
;
