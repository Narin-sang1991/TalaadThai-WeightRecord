

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [Mm].[ProcessPlanImported](
	[CreatedByAppCode] [nvarchar](100) NULL,
	[CreatedByUserCode] [nvarchar](50) NULL,
	[CreatedDate] [datetimeoffset](7) NULL,
	[UpdatedByAppCode] [nvarchar](100) NULL,
	[UpdatedByUserCode] [nvarchar](50) NULL,
	[UpdatedDate] [datetimeoffset](7) NULL,
	[RowVersion] [timestamp] NOT NULL,
	[NoteData] [nvarchar](500) NULL,

	[Id] [uniqueidentifier] NOT NULL DEFAULT (newid()),
	[ImportedDate] [datetimeoffset](7) NOT NULL,
	[ImportedNo] [nvarchar](20) NULL,

 CONSTRAINT [PK_ProcessPlanImported_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


CREATE TABLE [Mm].[ProcessPlan](
	[CreatedByAppCode] [nvarchar](100) NULL,
	[CreatedByUserCode] [nvarchar](50) NULL,
	[CreatedDate] [datetimeoffset](7) NULL,
	[UpdatedByAppCode] [nvarchar](100) NULL,
	[UpdatedByUserCode] [nvarchar](50) NULL,
	[UpdatedDate] [datetimeoffset](7) NULL,
	[RowVersion] [timestamp] NOT NULL,
	[NoteData] [nvarchar](500) NULL,

	[Id] [uniqueidentifier] NOT NULL DEFAULT (newid()),
	[ProcessPlanImportedId] [uniqueidentifier] NOT NULL,
	[SeqNo] [bigint] NOT NULL,
	[OrderNo] [nvarchar] (100) NOT NULL,
	[ProcessPlanDate] [datetimeoffset](7) NOT NULL,
	[ProcessMachineCode]  [nvarchar](20) NOT NULL,
	[CoilQty] int NOT NULL,
	[CoilPackingCode] [nvarchar](100) NOT NULL,
	[CoilWeight] [decimal](18,8) NOT NULL,

 CONSTRAINT [PK_ProcessPlan_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


CREATE INDEX [IXFK_Imported_ProcessPlan] 
 ON [Mm].[ProcessPlan] ([ProcessPlanImportedId] ASC)
Go
ALTER TABLE [Mm].[ProcessPlan] ADD CONSTRAINT [FK_Imported_ProcessPlan]
	FOREIGN KEY ([ProcessPlanImportedId]) REFERENCES [Mm].[ProcessPlanImported] ([Id]) ON DELETE No Action ON UPDATE No Action
Go
