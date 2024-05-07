/*
USE [DB_Name]
GO
*/
/****** Object:  Table [Core].[UserToken]    Script Date: 12/11/2560 15:00:52 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [Core].[UserToken](
	[UserId] uniqueidentifier NOT NULL,
	[Token] [varchar](200) NULL,
	[NoteData] nvarchar(500),
	[CreatedByAppCode] nvarchar(100),
	[CreatedByUserCode] nvarchar(50),
	[CreatedDate] datetimeoffset(7),
	[UpdatedByAppCode] nvarchar(100),
	[UpdatedByUserCode] nvarchar(50),
	[UpdatedDate] datetimeoffset(7),
	[RowVersion] timestamp NOT NULL
 CONSTRAINT [PK_UserToken] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


