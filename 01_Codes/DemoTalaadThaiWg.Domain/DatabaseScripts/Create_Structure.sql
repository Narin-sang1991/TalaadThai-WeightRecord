-- Mm.DocumentRunningNo definition

-- Drop table

-- DROP TABLE Mm.DocumentRunningNo GO

CREATE TABLE Core.DocumentRunningNo (
	Id uniqueidentifier DEFAULT newid() NOT NULL,
	Prefix nvarchar(20) COLLATE Thai_CI_AS NOT NULL,
	RunningNo int NOT NULL,
	DocumentType nvarchar(255) COLLATE Thai_CI_AS NULL,
	NoteData nvarchar(500) COLLATE Thai_CI_AS NULL,
	CreatedByAppCode nvarchar(100) COLLATE Thai_CI_AS NULL,
	CreatedByUserCode nvarchar(50) COLLATE Thai_CI_AS NULL,
	CreatedDate datetimeoffset NULL,
	UpdatedByAppCode nvarchar(100) COLLATE Thai_CI_AS NULL,
	UpdatedByUserCode nvarchar(50) COLLATE Thai_CI_AS NULL,
	UpdatedDate datetimeoffset NULL,
	RowVersion timestamp NOT NULL,
	OuId uniqueidentifier NULL,
	CONSTRAINT PK_DocumentRunningNo_Id PRIMARY KEY (Id)
) ;


-- Mm.Measuring definition

-- Drop table

-- DROP TABLE Mm.Measuring GO

CREATE TABLE Mm.Measuring (
	CreatedByAppCode nvarchar(100) COLLATE Thai_CI_AS NULL,
	CreatedByUserCode nvarchar(50) COLLATE Thai_CI_AS NULL,
	CreatedDate datetimeoffset NULL,
	UpdatedByAppCode nvarchar(100) COLLATE Thai_CI_AS NULL,
	UpdatedByUserCode nvarchar(50) COLLATE Thai_CI_AS NULL,
	UpdatedDate datetimeoffset NULL,
	RowVersion timestamp NOT NULL,
	NoteData nvarchar(500) COLLATE Thai_CI_AS NULL,
	Id uniqueidentifier NOT NULL,
	BusinessEntityId uniqueidentifier NULL,
	[Type] int NOT NULL,
	[No] nvarchar(50) COLLATE Thai_CI_AS NULL,
	[Date] datetimeoffset NULL,
	[Status] int NOT NULL,
	[ReferenceNo] nvarchar(100) COLLATE Thai_CI_AS NULL,
	[LicensePlateNo] nvarchar(100) COLLATE Thai_CI_AS NOT NULL,
	CONSTRAINT Pk_Measuring PRIMARY KEY (Id)
)
CREATE INDEX IXFK_Measuring_BusinessEntity ON Mm.Measuring (BusinessEntityId);


-- Mm.MeasuringMoveItem definition

-- Drop table

-- DROP TABLE Mm.MeasuringMoveItem GO

CREATE TABLE Mm.MeasuringMoveItem (
	CreatedByAppCode nvarchar(100) COLLATE Thai_CI_AS NULL,
	CreatedByUserCode nvarchar(50) COLLATE Thai_CI_AS NULL,
	CreatedDate datetimeoffset NULL,
	UpdatedByAppCode nvarchar(100) COLLATE Thai_CI_AS NULL,
	UpdatedByUserCode nvarchar(50) COLLATE Thai_CI_AS NULL,
	UpdatedDate datetimeoffset NULL,
	RowVersion timestamp NOT NULL,
	NoteData nvarchar(500) COLLATE Thai_CI_AS NULL,
	Id uniqueidentifier NOT NULL,
	MeasuringId uniqueidentifier NOT NULL,
	SeqNo bigint NOT NULL,
	ProductBarcode nvarchar(255) COLLATE Thai_CI_AS NULL,
	ProductName nvarchar(500) COLLATE Thai_CI_AS NULL,
	UnitPrice decimal(18,8) NOT NULL,
	UnitPerRatio decimal(18,8) NOT NULL,
	NetWeight decimal(18,8) NOT NULL,
	TareWeight decimal(18,8) NOT NULL,
	WeightUnitCode nvarchar(50) NOT NULL,
	GatewayItemType int NOT NULL,
	IsDeleted bit DEFAULT 0 NOT NULL,
	CONSTRAINT Pk_MeasuringMoveItem PRIMARY KEY (Id)
)
CREATE INDEX IXFK_MeasuringMoveItem_Measuring ON Mm.MeasuringMoveItem (MeasuringId);


-- Mm.MeasuringMoveItem foreign keys

ALTER TABLE Mm.MeasuringMoveItem ADD CONSTRAINT FK_MeasuringMoveItem_Measuring FOREIGN KEY (MeasuringId) REFERENCES Mm.Measuring(Id);