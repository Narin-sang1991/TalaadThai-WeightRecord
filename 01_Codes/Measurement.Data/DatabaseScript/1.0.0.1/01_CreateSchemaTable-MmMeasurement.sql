
CREATE SCHEMA [Mm]
Go
 
CREATE TABLE [Mm].[Measuring](
	[CreatedByAppCode] nvarchar(100),
	[CreatedByUserCode] nvarchar(50),
	[CreatedDate] datetimeoffset(7),
	[UpdatedByAppCode] nvarchar(100),
	[UpdatedByUserCode] nvarchar(50),
	[UpdatedDate] datetimeoffset(7),
	[RowVersion] timestamp NOT NULL,
	[NoteData] nvarchar(500),

	[Id] uniqueidentifier NOT NULL,
	[BusinessEntityId] uniqueidentifier,
	[Type] int NOT NULL,
	[No] nvarchar(50),
	[Date] DateTimeOffset(7),
	[Status] int NOT NULL,
	[ReferenceNo] nvarchar(100)
)
Go
ALTER TABLE [Mm].[Measuring] 
 ADD CONSTRAINT [Pk_Measuring]
	PRIMARY KEY CLUSTERED ([Id])
;
Go
CREATE INDEX [IXFK_Measuring_BusinessEntity] 
 ON [Mm].[Measuring] ([BusinessEntityId] ASC)
Go

CREATE TABLE [Mm].[Unit]
(
	[Id] uniqueidentifier NOT NULL,
	[Abbreviation] nvarchar(50) NOT NULL,
	[Name] nvarchar(255) NOT NULL,
	[IsActive] bit NOT NULL DEFAULT 1,

	[NoteData] nvarchar(500),
	[CreatedByAppCode] nvarchar(100),
	[CreatedByUserCode] nvarchar(50),
	[CreatedDate] datetimeoffset(7),
	[UpdatedByAppCode] nvarchar(100),
	[UpdatedByUserCode] nvarchar(50),
	[UpdatedDate] datetimeoffset(7),
	[RowVersion] timestamp NOT NULL
)
;
Go

ALTER TABLE [Mm].[Unit] 
 ADD CONSTRAINT [PK_Unit]
	PRIMARY KEY CLUSTERED ([Id])
;
Go

CREATE TABLE [Mm].[MeasuringMoveItem](
	[CreatedByAppCode] nvarchar(100),
	[CreatedByUserCode] nvarchar(50),
	[CreatedDate] datetimeoffset(7),
	[UpdatedByAppCode] nvarchar(100),
	[UpdatedByUserCode] nvarchar(50),
	[UpdatedDate] datetimeoffset(7),
	[RowVersion] timestamp NOT NULL,
	[NoteData] nvarchar(500),

	[Id] uniqueidentifier NOT NULL,
	[MeasuringId] uniqueidentifier NOT NULL,
	[SeqNo] bigint NOT NULL,
	[ProductBarcode] nvarchar(255),
	[ProductName] nvarchar(500),
	[UnitPrice] decimal(18,8) NOT NULL,
	[UnitPerRatio] decimal(18,8) NOT NULL,
	[NetWeight] decimal(18,8) NOT NULL,
	[TareWeight] decimal(18,8) NOT NULL,
	[WeightUnitId] uniqueidentifier NOT NULL,
	[GatewayItemType] int NOT NULL,
	[IsDeleted] bit NOT NULL DEFAULT 0
)
Go


ALTER TABLE [Mm].[MeasuringMoveItem] 
 ADD CONSTRAINT [Pk_MeasuringMoveItem]
	PRIMARY KEY CLUSTERED ([Id])
Go


CREATE INDEX [IXFK_MeasuringMoveItem_Measuring] 
 ON [Mm].[MeasuringMoveItem] ([MeasuringId] ASC)
Go

ALTER TABLE [Mm].[MeasuringMoveItem] ADD CONSTRAINT [FK_MeasuringMoveItem_Measuring]
	FOREIGN KEY ([MeasuringId]) REFERENCES [Mm].[Measuring] ([Id]) ON DELETE No Action ON UPDATE No Action
Go

CREATE INDEX [IXFK_MeasuringMovement_WgUnit] 
 ON [Mm].[MeasuringMoveItem] ([WeightUnitId] ASC)
Go
ALTER TABLE [Mm].[MeasuringMoveItem] ADD CONSTRAINT [FK_MeasuringMovement_WgUnit]
	FOREIGN KEY ([WeightUnitId]) REFERENCES [Mm].[Unit] ([Id]) ON DELETE No Action ON UPDATE No Action
Go