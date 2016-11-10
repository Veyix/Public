/*
 * Description: Creates a new table to record user profiles.
 *
 * Author:		Samuel Slade
 * Date:		09/11/2016
 */
IF NOT EXISTS (
	SELECT TOP 1 1 FROM INFORMATION_SCHEMA.TABLES
	WHERE [TABLE_SCHEMA] = N'dbo'
	AND [TABLE_NAME] = N'UserProfile'
)
BEGIN
	CREATE TABLE [dbo].[UserProfile] (
		[Id]				INT				NOT NULL IDENTITY (1, 1),
		[Title]				VARCHAR(50)		NOT NULL,
		[Forename]			VARCHAR(100)	NOT NULL,
		[Surname]			VARCHAR(100)	NOT NULL,
		[DateOfBirth]		DATE			NOT NULL,
		[CreatedDate]		DATETIME2		NOT NULL,
		[LastChangedDate]	DATETIME2		NOT NULL
	);

	ALTER TABLE [dbo].[UserProfile]
	ADD CONSTRAINT [PK_dbo-UserProfile:Id]
		PRIMARY KEY CLUSTERED ([Id] ASC);

	ALTER TABLE [dbo].[UserProfile]
	ADD CONSTRAINT [DF_dbo-UserProfile:CreatedDate]
		DEFAULT (GETDATE()) FOR [CreatedDate];

	ALTER TABLE [dbo].[UserProfile]
	ADD CONSTRAINT [DF_dbo-UserProfile:LastChangedDate]
		DEFAULT (GETDATE()) FOR [LastChangedDate];
END;