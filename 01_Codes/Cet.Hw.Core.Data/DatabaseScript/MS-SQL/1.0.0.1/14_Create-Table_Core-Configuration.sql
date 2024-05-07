

CREATE TABLE [Core].[ConfigurationCategory]
(

	[NoteData] nvarchar(500),
	[CreatedByAppCode] nvarchar(100),
	[CreatedByUserCode] nvarchar(50),
	[CreatedDate] datetimeoffset(7),
	[UpdatedByAppCode] nvarchar(100),
	[UpdatedByUserCode] nvarchar(50),
	[UpdatedDate] datetimeoffset(7),
	[RowVersion] timestamp NOT NULL,
	
	[Id] uniqueidentifier NOT NULL,
	[Code] nvarchar(100) NOT NULL,
	[Name] nvarchar(255) NOT NULL,
	[IsAccessWrite] bit  NOT NULL default 1,
	
 CONSTRAINT [PK_ConfigurationCategory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


CREATE TABLE [Core].[Configuration]
(

	[NoteData] nvarchar(500),
	[CreatedByAppCode] nvarchar(100),
	[CreatedByUserCode] nvarchar(50),
	[CreatedDate] datetimeoffset(7),
	[UpdatedByAppCode] nvarchar(100),
	[UpdatedByUserCode] nvarchar(50),
	[UpdatedDate] datetimeoffset(7),
	[RowVersion] timestamp NOT NULL,
	
	[Id] uniqueidentifier NOT NULL,
	[ConfigCategoryId] uniqueidentifier NOT NULL,
	[Code] nvarchar(100) NOT NULL,
	[Name] nvarchar(255) NOT NULL,
	[Description] nvarchar(500),
	[AllowClientAccess] bit  NOT NULL default 0,
	[ConfigurationValue] nvarchar(500),
	
 CONSTRAINT [PK_Configuration] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


ALTER TABLE [Core].[Configuration] ADD CONSTRAINT [FK_ConfigurationCate_Configuration]
	FOREIGN KEY ([ConfigCategoryId]) REFERENCES [Core].[ConfigurationCategory] ([Id]) ON DELETE No Action ON UPDATE No Action
;




