/*
USE [DB_Name]
GO
*/
/****** Object:  Table [Core].[Member]    Script Date: 12/11/2560 16:04:34 ******/
SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [Core].[Member](
	[GroupId] uniqueidentifier NOT NULL,
	[UserId] uniqueidentifier NOT NULL,
	[NoteData] nvarchar(500),
	[CreatedByAppCode] nvarchar(100),
	[CreatedByUserCode] nvarchar(50),
	[CreatedDate] datetimeoffset(7),
	[UpdatedByAppCode] nvarchar(100),
	[UpdatedByUserCode] nvarchar(50),
	[UpdatedDate] datetimeoffset(7),
	[RowVersion] timestamp NOT NULL
 CONSTRAINT [PK_User_Group] PRIMARY KEY CLUSTERED 
(
	[GroupId] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [Core].[Member]  WITH CHECK ADD  CONSTRAINT [FK_Member_User] FOREIGN KEY([GroupId])
REFERENCES [Core].[User] ([Id])
GO

ALTER TABLE [Core].[Member] CHECK CONSTRAINT [FK_Member_User]
GO

ALTER TABLE [Core].[Member]  WITH CHECK ADD  CONSTRAINT [FK_Member_User1] FOREIGN KEY([UserId])
REFERENCES [Core].[User] ([Id])
ON DELETE CASCADE
GO

ALTER TABLE [Core].[Member] CHECK CONSTRAINT [FK_Member_User1]
GO


