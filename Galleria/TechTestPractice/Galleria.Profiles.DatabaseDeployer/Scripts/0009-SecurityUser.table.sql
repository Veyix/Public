/*
 * Description: Creates a table that stores users that are granted access to parts of the system.
 *
 * Author:		Samuel Slade
 * Date:		15/11/2016
 */
IF NOT EXISTS (
	SELECT TOP 1 1 FROM INFORMATION_SCHEMA.TABLES
	WHERE [TABLE_SCHEMA] = N'dbo'
	AND [TABLE_NAME] = N'SecurityUser'
)
BEGIN
	CREATE TABLE [dbo].[SecurityUser] (
		[Id]			INT				NOT NULL IDENTITY (1, 1),
		[Username]		VARCHAR(100)	NOT NULL,
		[Password]		VARCHAR(100)	NOT NULL,
		[CreatedDate]	DATETIME2		NOT NULL
	);

	CREATE UNIQUE NONCLUSTERED INDEX [UNQ_dbo-SecurityUser:Username]
		ON [dbo].[SecurityUser] ([Username]);

	ALTER TABLE [dbo].[SecurityUser]
	ADD CONSTRAINT [DF_dbo-SecurityUser:CreatedDate]
		DEFAULT (GETDATE()) FOR [CreatedDate];
END;