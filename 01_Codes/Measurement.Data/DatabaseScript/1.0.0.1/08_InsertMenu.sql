

INSERT [Core].[Menu] ([Code], [Name], [ParentCode], [ResourceUID], [Ordinary], [Command], [IsActive], [IsHidden], [NoteData], [CreatedByAppCode], [CreatedByUserCode], [CreatedDate])
VALUES (N'ProcessPlanSearch', N'ProcessPlanSearch', N'PSS_Receive', N'30679BD5-4D8B-4559-A296-0019B08A2840', 3, N'ProcessPlanSearchCommand', 1, 0, NULL, NULL, NULL, CAST(N'2020-01-01T12:00:00.6023751+07:00' AS DateTimeOffset))
Go

INSERT [Core].[MenuTranslate] ([Code], [LanguageCode], [Name], [NoteData], [CreatedByAppCode], [CreatedByUserCode], [CreatedDate])
VALUES (N'ProcessPlanSearch', N'en-US', N'Process plan search', NULL, N'Msat', N'admin', CAST(N'2020-01-01T12:00:00.6023751+07:00' AS DateTimeOffset))
INSERT [Core].[MenuTranslate] ([Code], [LanguageCode], [Name], [NoteData], [CreatedByAppCode], [CreatedByUserCode], [CreatedDate])
VALUES (N'ProcessPlanSearch', N'th-TH', N'ค้นหาข้อมูลแผนประมวลผล', NULL, N'Msat', N'admin', CAST(N'2020-01-01T12:00:00.6023751+07:00' AS DateTimeOffset))
Go



INSERT [Core].[Menu] ([Code], [Name], [ParentCode], [ResourceUID], [Ordinary], [Command], [IsActive], [IsHidden], [NoteData], [CreatedByAppCode], [CreatedByUserCode], [CreatedDate])
VALUES (N'ProcessPlanImported', N'ProcessPlanImported', N'PSS_Receive', N'67A17982-C9BD-460B-86F7-4A10CF0561A9', 4, N'ProcessPlanImportedCommand', 1, 0, NULL, NULL, NULL, CAST(N'2020-01-01T12:00:00.6023751+07:00' AS DateTimeOffset))
Go

INSERT [Core].[MenuTranslate] ([Code], [LanguageCode], [Name], [NoteData], [CreatedByAppCode], [CreatedByUserCode], [CreatedDate])
VALUES (N'ProcessPlanImported', N'en-US', N'Process plan imported', NULL, N'Msat', N'admin', CAST(N'2020-01-01T12:00:00.6023751+07:00' AS DateTimeOffset))
INSERT [Core].[MenuTranslate] ([Code], [LanguageCode], [Name], [NoteData], [CreatedByAppCode], [CreatedByUserCode], [CreatedDate])
VALUES (N'ProcessPlanImported', N'th-TH', N'ข้อมูลแผนประมวลผล', NULL, N'Msat', N'admin', CAST(N'2020-01-01T12:00:00.6023751+07:00' AS DateTimeOffset))
Go
