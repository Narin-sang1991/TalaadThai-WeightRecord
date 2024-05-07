

INSERT [Core].[Menu] ([Code], [Name], [ParentCode], [ResourceUID], [Ordinary], [Command], [IsActive], [IsHidden])
VALUES (N'PSS_Receive', N'MSAT_Measuring', NULL, N'44724423-af4e-4da2-8e23-e61c9c3a09e6', 10, NULL, 1, 0)
Go
INSERT [Core].[MenuTranslate] ([Code], [LanguageCode], [Name]) VALUES (N'PSS_Receive', N'en-US', N'Measuring')
INSERT [Core].[MenuTranslate] ([Code], [LanguageCode], [Name]) VALUES (N'PSS_Receive', N'th-TH', N'รายการบันทึกน้ำหนัก')
Go

INSERT [Core].[Menu] ([Code], [Name], [ParentCode], [ResourceUID], [Ordinary], [Command], [IsActive], [IsHidden], [NoteData], [CreatedByAppCode], [CreatedByUserCode], [CreatedDate], [UpdatedByAppCode], [UpdatedByUserCode], [UpdatedDate])
VALUES (N'PartReceiveSearch', N'PartReceiveSearch', N'PSS_Receive', N'303bfcc4-bd03-4cc8-b527-625ca3c4048e', 1, N'ReceiveSearchCommand', 1, 0, NULL, NULL, NULL, CAST(N'2017-08-19T13:52:30.6023751+07:00' AS DateTimeOffset), NULL, NULL, NULL)
Go

INSERT [Core].[MenuTranslate] ([Code], [LanguageCode], [Name], [NoteData], [CreatedByAppCode], [CreatedByUserCode], [CreatedDate], [UpdatedByAppCode], [UpdatedByUserCode], [UpdatedDate])
VALUES (N'PartReceiveSearch', N'en-US', N'Search Weight Document', NULL, N'PSS', N'ninja', CAST(N'2017-08-19T13:52:30.6023751+07:00' AS DateTimeOffset), NULL, NULL, NULL)
INSERT [Core].[MenuTranslate] ([Code], [LanguageCode], [Name], [NoteData], [CreatedByAppCode], [CreatedByUserCode], [CreatedDate], [UpdatedByAppCode], [UpdatedByUserCode], [UpdatedDate])
VALUES (N'PartReceiveSearch', N'th-TH', N'ค้นหาเอกสาร', NULL, N'PSS', N'ninja', CAST(N'2017-08-19T13:52:30.6023751+07:00' AS DateTimeOffset), NULL, NULL, NULL)
Go

INSERT [Core].[Menu] ([Code], [Name], [ParentCode], [ResourceUID], [Ordinary], [Command], [IsActive], [IsHidden], [NoteData], [CreatedByAppCode], [CreatedByUserCode], [CreatedDate], [UpdatedByAppCode], [UpdatedByUserCode], [UpdatedDate])
VALUES (N'PartTransfer_Receive', N'PartWeight', N'PSS_Receive', N'0a819709-2b35-4d11-a323-45c4fbf9701a', 2, N'PartTransferReceiveCommand', 1, 0, NULL, NULL, NULL, CAST(N'2017-08-19T13:52:39.0107899+07:00' AS DateTimeOffset), NULL, NULL, NULL)
Go

INSERT [Core].[MenuTranslate] ([Code], [LanguageCode], [Name], [NoteData], [CreatedByAppCode], [CreatedByUserCode], [CreatedDate], [UpdatedByAppCode], [UpdatedByUserCode], [UpdatedDate])
VALUES (N'PartTransfer_Receive', N'th-TH', N'บันทึกน้ำหนัก', NULL, N'PSS', N'ninja', CAST(N'2017-08-19T13:52:39.0107899+07:00' AS DateTimeOffset), NULL, NULL, NULL)
INSERT [Core].[MenuTranslate] ([Code], [LanguageCode], [Name], [NoteData], [CreatedByAppCode], [CreatedByUserCode], [CreatedDate], [UpdatedByAppCode], [UpdatedByUserCode], [UpdatedDate])
VALUES (N'PartTransfer_Receive', N'en-US', N'Create Weight Document', NULL, N'PSS', N'ninja', CAST(N'2017-08-19T13:53:00.9288284+07:00' AS DateTimeOffset), NULL, NULL, NULL)
Go