IF EXISTS (
	SELECT TOP 1 1 FROM sys.procedures AS P
	INNER JOIN sys.schemas AS S ON S.[schema_id] = P.[schema_id]
	WHERE S.[name] = 'dbo'
	AND P.[name] = 'UserProfileGetAll'
)
	/* Drop the procedure before we re-create it */
	DROP PROCEDURE [dbo].[UserProfileGetAll];
GO

/*
 * Description: Retrieves all user profile records.
 *
 * Author:		Samuel Slade
 * Date:		09/11/2016
 */
CREATE PROCEDURE [dbo].[UserProfileGetAll]
AS
BEGIN
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED;
	SET NOCOUNT ON;

	/* Get all of the records with the most recently added at the top */
	SELECT [Id]
		 , [CompanyId]
		 , [Title]
		 , [Forename]
		 , [Surname]
		 , [DateOfBirth]
		 , [CreatedDate]
		 , [LastChangedDate]
	FROM [dbo].[UserProfile]
	ORDER BY [CreatedDate] DESC;
END;