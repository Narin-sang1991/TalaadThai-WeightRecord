/*
USE [DatabaseName]
Go
*/
/****** Object:  Table [Core].[Menu]    Script Date: 12/11/2560 18:11:13 ******/


CREATE SCHEMA [Core]
Go


SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [Core].[Language](
	[Code] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[IsActive] [bit] NULL,
	[NoteData] [nvarchar](500) NULL,
	[CreatedByAppCode] [nvarchar](100) NULL,
	[CreatedByUserCode] [nvarchar](50) NULL,
	[CreatedDate] [datetimeoffset](7) NULL,
	[UpdatedByAppCode] [nvarchar](100) NULL,
	[UpdatedByUserCode] [nvarchar](50) NULL,
	[UpdatedDate] [datetimeoffset](7) NULL,
	[RowVersion] [timestamp] NOT NULL,
 CONSTRAINT [PK_Language] PRIMARY KEY CLUSTERED 
(
	[Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

INSERT [Core].[Language] ([Code], [Name], [IsActive]) 
	VALUES (N'en-US', N'English (United States)', 1)
INSERT [Core].[Language] ([Code], [Name], [IsActive]) 
	VALUES (N'th-TH', N'Thai', 1)
Go

CREATE TABLE [Core].[Menu](
	[Code] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[ParentCode] [nvarchar](50) NULL,
	[ResourceUID] [uniqueidentifier] NULL CONSTRAINT [DF__Menu__ReferenceI__38845C1C]  DEFAULT (newid()),
	[Ordinary] [int] NULL,
	[Command] [nvarchar](50) NULL,
	[IsActive] [bit] NULL,
	[IsHidden] [bit] NULL CONSTRAINT [DF_Menu_IsHidden]  DEFAULT ((0)),
	[NoteData] [nvarchar](500) NULL,
	[CreatedByAppCode] [nvarchar](100) NULL,
	[CreatedByUserCode] [nvarchar](50) NULL,
	[CreatedDate] [datetimeoffset](7) NULL,
	[UpdatedByAppCode] [nvarchar](100) NULL,
	[UpdatedByUserCode] [nvarchar](50) NULL,
	[UpdatedDate] [datetimeoffset](7) NULL,
	[RowVersion] [timestamp] NOT NULL,
 CONSTRAINT [PK_Menu] PRIMARY KEY CLUSTERED 
(
	[Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


CREATE TABLE [Core].[MenuTranslate](
	[Code] [nvarchar](50) NOT NULL,
	[LanguageCode] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](100) NULL,
	[NoteData] [nvarchar](500) NULL,
	[CreatedByAppCode] [nvarchar](100) NULL,
	[CreatedByUserCode] [nvarchar](50) NULL,
	[CreatedDate] [datetimeoffset](7) NULL,
	[UpdatedByAppCode] [nvarchar](100) NULL,
	[UpdatedByUserCode] [nvarchar](50) NULL,
	[UpdatedDate] [datetimeoffset](7) NULL,
	[RowVersion] [timestamp] NOT NULL,
 CONSTRAINT [PK_MenuTranslate] PRIMARY KEY CLUSTERED 
(
	[Code] ASC,
	[LanguageCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [Core].[MenuTranslate]  WITH CHECK ADD  CONSTRAINT [FK_MenuTranslate_Language] FOREIGN KEY([LanguageCode])
REFERENCES [Core].[Language] ([Code])
GO

ALTER TABLE [Core].[MenuTranslate] CHECK CONSTRAINT [FK_MenuTranslate_Language]
GO

ALTER TABLE [Core].[MenuTranslate]  WITH CHECK ADD  CONSTRAINT [FK_MenuTranslate_Menu] FOREIGN KEY([Code])
REFERENCES [Core].[Menu] ([Code])
GO

ALTER TABLE [Core].[MenuTranslate] CHECK CONSTRAINT [FK_MenuTranslate_Menu]
GO


