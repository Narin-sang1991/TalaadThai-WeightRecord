/*
USE [DB_Name]
GO
*/
/****** Object:  Table [Core].[AuditLog]    Script Date: 12/11/2560 15:11:17 ******/
SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [Core].[AuditLog](
	[Id] [uniqueidentifier] NOT NULL,
	[AuditLogMessageCode] [varchar](50) NULL,
	[Message] [varchar](8000) NULL,
	[EventDate] [datetime] NULL,
	[NoteData] nvarchar(500),
	[CreatedByAppCode] nvarchar(100),
	[CreatedByUserCode] nvarchar(50),
	[CreatedDate] datetimeoffset(7),
	[UpdatedByAppCode] nvarchar(100),
	[UpdatedByUserCode] nvarchar(50),
	[UpdatedDate] datetimeoffset(7),
	[RowVersion] timestamp NOT NULL
 CONSTRAINT [PK_AuditLog] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [Core].[AuditLog]  WITH CHECK ADD  CONSTRAINT [FK_AuditLog_AuditLogMessage] FOREIGN KEY([AuditLogMessageCode])
REFERENCES [Core].[AuditLogMessage] ([Code])
GO

ALTER TABLE [Core].[AuditLog] CHECK CONSTRAINT [FK_AuditLog_AuditLogMessage]
GO


