/*
USE [DatabaseName]
Go
*/


INSERT [Core].[Menu] ([Code], [Name], [ParentCode], [ResourceUID], [Ordinary], [Command], [IsActive], [IsHidden]) 
	VALUES (N'HelpGroup', N'Help', NULL, N'816f2ce3-e591-40b0-97e1-5ccd730276f1', 99, NULL, 1, 0)
Go
INSERT [Core].[MenuTranslate] ([Code], [LanguageCode], [Name]) 
	VALUES (N'HelpGroup', N'en-US', N'Help')
INSERT [Core].[MenuTranslate] ([Code], [LanguageCode], [Name]) 
	VALUES (N'HelpGroup', N'th-TH', N'ช่วยเหลือ')
Go

INSERT [Core].[Menu] ([Code], [Name], [ParentCode], [ResourceUID], [Ordinary], [Command], [IsActive], [IsHidden]) 
	VALUES (N'About', N'About', N'HelpGroup', N'56872c89-cd3d-40be-98f5-e48a17c48aa9', 1, N'About', 1, 0)
Go
INSERT [Core].[MenuTranslate] ([Code], [LanguageCode], [Name]) 
	VALUES (N'About', N'en-US', N'About')
INSERT [Core].[MenuTranslate] ([Code], [LanguageCode], [Name]) 
	VALUES (N'About', N'th-TH', N'เกี่ยวกับ')
Go


INSERT [Core].[Menu] ([Code], [Name], [ParentCode], [ResourceUID], [Ordinary], [Command], [IsActive], [IsHidden]) 
	VALUES (N'FileGroup', N'File', NULL, N'ede84469-46d2-4b91-9abb-d5d86554b9dc', 1, NULL, 1, 0)
INSERT [Core].[MenuTranslate] ([Code], [LanguageCode], [Name])
	VALUES (N'FileGroup', N'en-US', N'File')
INSERT [Core].[MenuTranslate] ([Code], [LanguageCode], [Name]) 
	VALUES (N'FileGroup', N'th-TH', N'ไฟล์')
Go


INSERT [Core].[Menu] ([Code], [Name], [ParentCode], [ResourceUID], [Ordinary], [Command], [IsActive], [IsHidden]) 
	VALUES (N'Language', N'Language', 'FileGroup', N'06D4E712-4352-4722-82B8-49CBB68DF569', 99, NULL, 1, 0)
Go
INSERT [Core].[MenuTranslate] ([Code], [LanguageCode], [Name]) 
	VALUES (N'Language', N'en-US', N'Language')
INSERT [Core].[MenuTranslate] ([Code], [LanguageCode], [Name]) 
	VALUES (N'Language', N'th-TH', N'ภาษา')
Go

INSERT [Core].[Menu] ([Code], [Name], [ParentCode], [ResourceUID], [Ordinary], [Command], [IsActive], [IsHidden]) 
	VALUES (N'ChangeLanguageEN', N'EN', N'Language', N'53dd6bd8-be55-4cc2-b1d5-3fa7a08017d5', 1, N'LanguageENCommand', 1, 0)
INSERT [Core].[Menu] ([Code], [Name], [ParentCode], [ResourceUID], [Ordinary], [Command], [IsActive], [IsHidden])
	VALUES (N'ChangeLanguageTH', N'TH', N'Language', N'e7954b72-fc42-450e-9128-62e7e48b514b', 1, N'LanguageTHCommand', 1, 0)
Go
INSERT [Core].[MenuTranslate] ([Code], [LanguageCode], [Name]) 
	VALUES (N'ChangeLanguageEN', N'en-US', N'English')
INSERT [Core].[MenuTranslate] ([Code], [LanguageCode], [Name]) 
	VALUES (N'ChangeLanguageEN', N'th-TH', N'English')
INSERT [Core].[MenuTranslate] ([Code], [LanguageCode], [Name]) 
	VALUES (N'ChangeLanguageTH', N'en-US', N'ภาษาไทย')
INSERT [Core].[MenuTranslate] ([Code], [LanguageCode], [Name]) 
	VALUES (N'ChangeLanguageTH', N'th-TH', N'ภาษาไทย')
Go

INSERT [Core].[Menu] ([Code], [Name], [ParentCode], [ResourceUID], [Ordinary], [Command], [IsActive], [IsHidden]) 
	VALUES (N'Logout', N'Log out', 'FileGroup', N'A968D8F0-9365-4028-8A24-B91758A8EA69', 99, 'Logout', 1, 0)
Go
INSERT [Core].[MenuTranslate] ([Code], [LanguageCode], [Name]) 
	VALUES (N'Logout', N'en-US', N'Log out')
INSERT [Core].[MenuTranslate] ([Code], [LanguageCode], [Name]) 
	VALUES (N'Logout', N'th-TH', N'ออกจากระบบ')
Go


