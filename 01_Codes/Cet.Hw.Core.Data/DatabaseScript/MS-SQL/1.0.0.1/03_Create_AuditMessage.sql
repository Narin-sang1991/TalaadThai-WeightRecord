/*
USE [DB_Name]
GO
*/
/****** Object:  Table [Core].[AuditLogCategory]    Script Date: 12/11/2560 15:13:08 ******/
SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [Core].[AuditLogCategory](
	[Code] [varchar](50) NOT NULL,
	[Name] [varchar](100) NULL,
	[NoteData] nvarchar(500),
	[CreatedByAppCode] nvarchar(100),
	[CreatedByUserCode] nvarchar(50),
	[CreatedDate] datetimeoffset(7),
	[UpdatedByAppCode] nvarchar(100),
	[UpdatedByUserCode] nvarchar(50),
	[UpdatedDate] datetimeoffset(7),
	[RowVersion] timestamp NOT NULL
 CONSTRAINT [PK_AuditLogCategory] PRIMARY KEY CLUSTERED 
(
	[Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


CREATE TABLE [Core].[AuditLogMessage](
	[Code] [varchar](50) NOT NULL,
	[LevelNumber] [int] NULL,
	[Template] [varchar](500) NULL,
	[AuditLogCategoryCode] [varchar](50) NULL,
	[NoteData] nvarchar(500),
	[CreatedByAppCode] nvarchar(100),
	[CreatedByUserCode] nvarchar(50),
	[CreatedDate] datetimeoffset(7),
	[UpdatedByAppCode] nvarchar(100),
	[UpdatedByUserCode] nvarchar(50),
	[UpdatedDate] datetimeoffset(7),
	[RowVersion] timestamp NOT NULL
 CONSTRAINT [PK_AuditLogMessage] PRIMARY KEY CLUSTERED 
(
	[Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [Core].[AuditLogMessage]  WITH CHECK ADD  CONSTRAINT [FK_AuditLogMessage_AuditLogCategory] FOREIGN KEY([AuditLogCategoryCode])
REFERENCES [Core].[AuditLogCategory] ([Code])
GO

ALTER TABLE [Core].[AuditLogMessage] CHECK CONSTRAINT [FK_AuditLogMessage_AuditLogCategory]
GO



