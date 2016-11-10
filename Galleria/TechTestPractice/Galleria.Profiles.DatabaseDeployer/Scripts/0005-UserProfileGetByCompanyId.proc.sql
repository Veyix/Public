IF EXISTS (
	SELECT TOP 1 1 FROM sys.procedures AS P
	INNER JOIN sys.schemas AS S ON S.[schema_id] = P.[schema_id]
	WHERE S.[name] = 'dbo'
	AND P.[name] = 'UserProfileGetByCompanyId'
)
	/* Drop the procedure before we re-create it */
	DROP PROCEDURE [dbo].[UserProfileGetByCompanyId];
GO

/*
 * Description: Retrieves all of the user profiles for the specified company.
 *
 * Parameters:
 *	@intCompanyId - The Id of the company for which to find all user profiles.
 *
 * Author:		Samuel Slade
 * Date:		09/11/2016
 */
CREATE PROCEDURE [dbo].[UserProfileGetByCompanyId] (
	@intCompanyId INT
)
AS
BEGIN
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED;
	SET NOCOUNT ON;

	/* Find all matching profiles and return the most recently added first */
	SELECT [Id]
		 , [CompanyId]
		 , [Title]
		 , [Forename]
		 , [Surname]
		 , [DateOfBirth]
		 , [CreatedDate]
		 , [LastChangedDate]
	FROM [dbo].[UserProfile]
	WHERE [CompanyId] = @intCompanyId
	ORDER BY [CreatedDate] DESC;
END;