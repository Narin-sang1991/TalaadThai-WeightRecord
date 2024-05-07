/*
USE [DatabaseName]
Go
*/
/****** Object:  Table [Core].[FileInfo]    Script Date: 11/25/2017 10:40:59 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [Core].[FileInfo](
	[Id] [uniqueidentifier] NOT NULL,
	[FileName] [nvarchar](50) NOT NULL,
	[RelationId] [uniqueidentifier] NULL,
	[RelationType] [int] NULL,
	[ExtensionId] [uniqueidentifier] NULL,
	[IsActive] [bit] NULL,
	[NoteData] nvarchar(500),
	[CreatedByAppCode] nvarchar(100),
	[CreatedByUserCode] nvarchar(50),
	[CreatedDate] datetimeoffset(7),
	[UpdatedByAppCode] nvarchar(100),
	[UpdatedByUserCode] nvarchar(50),
	[UpdatedDate] datetimeoffset(7),
	[RowVersion] timestamp NOT NULL
 CONSTRAINT [PK_FileInfo] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [Core].[FileInfo] WITH CHECK ADD  CONSTRAINT [Fk_FileInfo_FileExtension] FOREIGN KEY([ExtensionId])
REFERENCES [Core].[FileExtension] ([Id])
GO

ALTER TABLE [Core].[FileInfo] CHECK CONSTRAINT [Fk_FileInfo_FileExtension]
GO





