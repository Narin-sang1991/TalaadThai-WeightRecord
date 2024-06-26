

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Mm].[DocumentRunningNo](
	[Id] [uniqueidentifier] NOT NULL DEFAULT (newid()),
	[Prefix] [nvarchar](20) NOT NULL,
	[RunningNo] [int] NOT NULL,
	[DocumentType] [nvarchar](255) NULL,
	[NoteData] [nvarchar](500) NULL,
	[CreatedByAppCode] [nvarchar](100) NULL,
	[CreatedByUserCode] [nvarchar](50) NULL,
	[CreatedDate] [datetimeoffset](7) NULL,
	[UpdatedByAppCode] [nvarchar](100) NULL,
	[UpdatedByUserCode] [nvarchar](50) NULL,
	[UpdatedDate] [datetimeoffset](7) NULL,
	[RowVersion] [timestamp] NOT NULL,
	[OuId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_DocumentRunningNo_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
