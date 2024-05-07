/*
USE [DatabaseName]
Go
*/
/****** Object:  Table [Core].[FileExtension]    Script Date: 11/25/2017 10:40:42 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [Core].[FileExtension](
	[Id] [uniqueidentifier] NOT NULL CONSTRAINT [DF_FileExtension_Id]  DEFAULT (newid()),
	[FileType] [varchar](50) NOT NULL,
	[MimeType] [varchar](200) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[NoteData] nvarchar(500),
	[CreatedByAppCode] nvarchar(100),
	[CreatedByUserCode] nvarchar(50),
	[CreatedDate] datetimeoffset(7),
	[UpdatedByAppCode] nvarchar(100),
	[UpdatedByUserCode] nvarchar(50),
	[UpdatedDate] datetimeoffset(7),
	[RowVersion] timestamp NOT NULL,
 CONSTRAINT [PK_FileExtension] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


