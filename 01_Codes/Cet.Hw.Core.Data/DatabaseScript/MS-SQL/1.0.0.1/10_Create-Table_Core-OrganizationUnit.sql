IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[Core].[OrganizationUnit]') AND OBJECTPROPERTY(id, 'IsUserTable') = 1) 
DROP TABLE [Core].[OrganizationUnit]
;

CREATE TABLE [Core].[OrganizationUnit]
(
	[Id] uniqueidentifier NOT NULL,
	[Code] varchar(10) NOT NULL,
	[Name] varchar(255),
	[ParentId] uniqueidentifier,
	[TaxID] nvarchar(50) NOT NULL,
	[Address] nvarchar(max) NOT NULL,
	[TelNo] nvarchar(50),
	[FaxNo] nvarchar(50),
	[Email] nvarchar(100),
	[IsActive] bit NOT NULL DEFAULT 0,
	[Path] varchar(max) NOT NULL,
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

ALTER TABLE [Core].[OrganizationUnit] 
 ADD CONSTRAINT [PK_OrganizationUnit]
	PRIMARY KEY CLUSTERED ([Id])
;
