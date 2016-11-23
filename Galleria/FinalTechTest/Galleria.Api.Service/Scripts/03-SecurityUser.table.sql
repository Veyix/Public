CREATE TABLE [dbo].[SecurityUser] (
	[Id]			INT				NOT NULL IDENTITY (1, 1),
	[Username]		VARCHAR(50)		NOT NULL,
	[Password]		VARCHAR(50)		NOT NULL,
	[Role]			VARCHAR(100)	NOT NULL,
	[CreatedDate]	DATETIME2		NOT NULL
);

ALTER TABLE [dbo].[SecurityUser]
ADD CONSTRAINT [PK_dbo-SecurityUser:Id]
	PRIMARY KEY CLUSTERED ([Id] ASC);

ALTER TABLE [dbo].[SecurityUser]
ADD CONSTRAINT [DF_dbo-SecurityUser:CreatedDate]
	DEFAULT (GETDATE()) FOR [CreatedDate];

CREATE UNIQUE NONCLUSTERED INDEX [IX_dbo-SecurityUser:Username]
	ON [dbo].[SecurityUser] ([Username]);