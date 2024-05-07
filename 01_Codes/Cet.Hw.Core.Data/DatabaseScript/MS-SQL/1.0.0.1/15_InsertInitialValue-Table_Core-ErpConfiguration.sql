
INSERT INTO [Core].[ConfigurationCategory] ([Id], [Code], [Name], [IsAccessWrite])
	VALUES('4a9dc41e-96e9-4034-b436-41445d6fdd34', 'email', 'Email', 1);
	
INSERT INTO [Core].[Configuration] ([Id], [ConfigCategoryId], [Code], [Name], [Description], [AllowClientAccess])
	values
	('98357a87-23c0-469b-aec8-33ea74755231', '4a9dc41e-96e9-4034-b436-41445d6fdd34', 'MailServer', 'Mail Server', 'Your Mail Server for send to contact person (smtp.gmail.com)', 1),
	('41559b90-e000-43b4-951b-55ecd60921a6', '4a9dc41e-96e9-4034-b436-41445d6fdd34', 'MailPort', 'Mail Port', 'Your Mail Port for send to contact person(587)', 1),
	('3ee78fcd-cf26-4f0a-9df0-e2f0518853fb', '4a9dc41e-96e9-4034-b436-41445d6fdd34', 'MailEnabledSSL', 'Mail EnabledSSL', 'If your need EnabledSSL set [1] value (1/0)', 1),
	('9109c219-0b7b-47c1-b092-bc6737a98ade', '4a9dc41e-96e9-4034-b436-41445d6fdd34', 'MailTimeout', 'Mail Timeout', 'Time out limit for send email [sec.]', 1),
	('b0c3b468-cdd3-467d-b53e-d2bffc0c81c6', '4a9dc41e-96e9-4034-b436-41445d6fdd34', 'MailLogin', 'Mail Login', 'Login Email', 1),
	('45818b5c-5e83-495b-8144-b9868ebf4d47', '4a9dc41e-96e9-4034-b436-41445d6fdd34', 'MailPassword', 'Mail Password', 'Password Email', 1),
	('a5f6085f-cc11-4b90-908a-0cb24150d274', '4a9dc41e-96e9-4034-b436-41445d6fdd34', 'MailName', 'Mail Name ', 'Email Address Name', 1),
	('fc735bcb-0528-41fb-be12-08a7d02c9db4', '4a9dc41e-96e9-4034-b436-41445d6fdd34', 'MailIsWithCredentials', 'Mail Is With Credentials', 'Mail send with credentials or security (user/password)', 1)
	
;

INSERT INTO [Core].[ConfigurationCategory] ([Id], [Code], [Name], [IsAccessWrite])
	VALUES('a34a341c-7377-48b5-9e55-935cb21dbb02', 'application', 'Application', 1);

INSERT INTO [Core].[Configuration] ([Id], [ConfigCategoryId], [Code], [Name], [Description], [AllowClientAccess])
	values
	('e4bc171b-ffd7-43d3-90c3-8d4d0a6e097f', 'a34a341c-7377-48b5-9e55-935cb21dbb02', 'SvrPathUrl', 'Server Path URL', 'Server Path URL for prefix notify or comfirmation link', 0)
;





