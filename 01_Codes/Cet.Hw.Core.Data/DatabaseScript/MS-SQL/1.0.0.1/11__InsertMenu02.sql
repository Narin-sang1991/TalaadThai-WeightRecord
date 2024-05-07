
DECLARE
@ParentCode varchar(255),
@Ordinary int, 
@DateSave DATETIMEOFFSET,
@NewID varchar(255),
@NewCommand varchar(255)


SET @ParentCode = 'FileGroup'
SET @NewID = 'E80DD6BC-707A-4218-827B-6271A474B02E'
SET @NewCommand = 'HomePageCommand'
SET @Ordinary = 0
SET @DateSave = (SELECT SYSDATETIMEOFFSET())

INSERT INTO [Core].Menu
	(Code,Name,ParentCode,ResourceUID,Ordinary,Command,IsActive,IsHidden,CreatedDate)
VALUES('HomePage','Home Page',@ParentCode,@NewID,@Ordinary,@NewCommand,1,0,@DateSave);

INSERT INTO [Core].MenuTranslate
	(Code,LanguageCode,Name,CreatedDate,CreatedByAppCode,CreatedByUserCode)
VALUES('HomePage','en-US','Home Page', @DateSave, 'PartTransform','ninja');

INSERT INTO [Core].MenuTranslate
	(Code,LanguageCode,Name,CreatedDate,CreatedByAppCode,CreatedByUserCode)
VALUES('HomePage','th-TH','หน้าแรก',@DateSave,'PartTransform','ninja');
Go


INSERT [Core].[Menu] ([Code], [Name], [ParentCode], [ResourceUID], [Ordinary], [Command], [IsActive], [IsHidden])
VALUES (N'UserManager', N'User Manager', N'FileGroup', N'b6b486bb-9c65-4f09-ae2f-b20277f74766', 2, N'UserManager', 1, 0)
Go

INSERT [Core].[MenuTranslate] ([Code], [LanguageCode], [Name]) VALUES (N'UserManager', N'en-US', N'User Manager')
INSERT [Core].[MenuTranslate] ([Code], [LanguageCode], [Name]) VALUES (N'UserManager', N'th-TH', N'จัดการข้อมูลผู้ใช้งาน')
Go


