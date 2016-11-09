/*
 * Description: Adds the CompanyId column to the UserProfile table.
 *
 * Author:		Samuel Slade
 * Date:		09/11/2016
 */
IF NOT EXISTS (
	SELECT TOP 1 1 FROM INFORMATION_SCHEMA.COLUMNS
	WHERE [TABLE_SCHEMA] = N'dbo'
	AND [TABLE_NAME] = N'UserProfile'
	AND [COLUMN_NAME] = N'CompanyId'
)
BEGIN
	ALTER TABLE [dbo].[UserProfile]
	ADD [CompanyId] INT NOT NULL;
END;