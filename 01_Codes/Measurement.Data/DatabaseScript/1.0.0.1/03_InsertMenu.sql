
--select newid()

INSERT [Core].[Menu] ([Code], [Name], [ParentCode], [ResourceUID], [Ordinary], [Command], [IsActive], [IsHidden])
VALUES (N'MeasuringReport', N'MeasuringReport', NULL, N'DB0249F4-ABC9-46D3-B7B8-59F6A828F6A5', 11, NULL, 1, 0)
Go
INSERT [Core].[MenuTranslate] ([Code], [LanguageCode], [Name]) VALUES (N'MeasuringReport', N'en-US', N'Report')
INSERT [Core].[MenuTranslate] ([Code], [LanguageCode], [Name]) VALUES (N'MeasuringReport', N'th-TH', N'รายงาน')
Go


INSERT [Core].[Menu] ([Code], [Name], [ParentCode], [ResourceUID], [Ordinary], [Command], [IsActive], [IsHidden], [NoteData], [CreatedByAppCode], [CreatedByUserCode], [CreatedDate])
VALUES (N'WgRecordsGroupingReport', N'WgRecordsGroupingReport', N'MeasuringReport', N'1E66E2F7-8609-452C-A589-62A304DE2329', 1, N'WgRecordsGroupingReportCommand', 1, 0, NULL, NULL, NULL, CAST(N'2020-01-01T12:00:00.6023751+07:00' AS DateTimeOffset))
Go

INSERT [Core].[MenuTranslate] ([Code], [LanguageCode], [Name], [NoteData], [CreatedByAppCode], [CreatedByUserCode], [CreatedDate])
VALUES (N'WgRecordsGroupingReport', N'en-US', N'Weight Records Grouping Report', NULL, N'Msat', N'admin', CAST(N'2020-01-01T12:00:00.6023751+07:00' AS DateTimeOffset))
INSERT [Core].[MenuTranslate] ([Code], [LanguageCode], [Name], [NoteData], [CreatedByAppCode], [CreatedByUserCode], [CreatedDate])
VALUES (N'WgRecordsGroupingReport', N'th-TH', N'รายงานการบันทึกน้ำหนักจัดกลุ่มข้อมูล', NULL, N'Msat', N'admin', CAST(N'2020-01-01T12:00:00.6023751+07:00' AS DateTimeOffset))
Go
