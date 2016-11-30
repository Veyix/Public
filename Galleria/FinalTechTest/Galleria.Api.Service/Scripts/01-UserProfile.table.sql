CREATE TABLE [dbo].[UserProfile] (
	[Id]					INT					NOT NULL IDENTITY (1, 1),
	[CompanyId]				INT					NOT NULL,
	[Title]					VARCHAR(50)			NOT NULL,
	[Forename]				VARCHAR(100)		NOT NULL,
	[Surname]				VARCHAR(100)		NOT NULL,
	[DateOfBirth]			DATE				NOT NULL,
	[CreatedDate]			DATETIME2			NOT NULL
);

ALTER TABLE [dbo].[UserProfile]
ADD CONSTRAINT [PK_dbo-UserProfile:Id]
	PRIMARY KEY CLUSTERED ([Id] ASC);

ALTER TABLE [dbo].[UserProfile]
ADD CONSTRAINT [DF_dbo-UserProfile:CreatedDate]
	DEFAULT (GETDATE()) FOR [CreatedDate];