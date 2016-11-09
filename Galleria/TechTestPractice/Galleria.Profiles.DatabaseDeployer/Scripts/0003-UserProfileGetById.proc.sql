IF EXISTS (
	SELECT TOP 1 1 FROM sys.procedures AS P
	INNER JOIN sys.schemas AS S ON S.[schema_id] = P.[schema_id]
	WHERE S.[name] = 'dbo'
	AND P.[name] = 'UserProfileGetById'
)
	/* Drop the procedure before we re-create it */
	DROP PROCEDURE [dbo].[UserProfileGetById];
GO

/*
 * Description: Retrieves the user profile record with a matching user Id.
 *
 * Parameters:
 *	@intUserId - The Id of the user profile to retrieve.
 *
 * Author:		Samuel Slade
 * Date:		09/11/2016
 */
CREATE PROCEDURE [dbo].[UserProfileGetById] (
	@intUserId INT
)
AS
BEGIN
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED;
	SET NOCOUNT ON;

	SELECT [Id]
		 , [CompanyId]
		 , [Title]
		 , [Forename]
		 , [Surname]
		 , [DateOfBirth]
		 , [CreatedDate]
		 , [LastChangedDate]
	FROM [dbo].[UserProfile]
	WHERE [Id] = @intUserId;
END;