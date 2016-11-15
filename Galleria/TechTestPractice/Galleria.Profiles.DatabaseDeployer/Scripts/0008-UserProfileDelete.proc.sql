IF EXISTS (
	SELECT TOP 1 1 FROM sys.procedures AS P
	INNER JOIN sys.schemas AS S ON S.[schema_id] = P.[schema_id]
	WHERE S.[name] = 'dbo'
	AND P.[name] = 'UserProfileDelete'
)
	/* Drop the procedure before we re-create it */
	DROP PROCEDURE [dbo].[UserProfileDelete];
GO

/*
 * Description: Deletes the specified user profile record.
 *
 * Parameters:
 *	@intUserId	- The Id of the user profile record to be deleted.
 *
 * Author:		Samuel Slade
 * Date:		10/11/2016
 */
CREATE PROCEDURE [dbo].[UserProfileDelete] (
	@intUserId INT
)
AS
BEGIN
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED;
	SET NOCOUNT ON;

	DELETE FROM [dbo].[UserProfile]
	WHERE [Id] = @intUserId;
END;