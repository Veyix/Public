IF EXISTS (
	SELECT TOP 1 1 FROM sys.procedures AS P
	INNER JOIN sys.schemas AS S ON S.[schema_id] = P.[schema_id]
	WHERE S.[name] = 'dbo'
	AND P.[name] = 'SecurityUserGetByUsername'
)
	/* Drop the procedure before we re-create it */
	DROP PROCEDURE [dbo].[SecurityUserGetByUsername];
GO

/*
 * Description: Retrieves a SecurityUser record based on the given username
 *				when a matching record exists.
 *
 * Author:		Samuel Slade
 * Date:		15/11/2016
 */
CREATE PROCEDURE [dbo].[SecurityUserGetByUsername] (
	@strUsername VARCHAR(100)
)
AS
BEGIN
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED;
	SET NOCOUNT ON;

	SELECT [Id] AS [UserId]
		 , [Username]
		 , [Password]
	FROM [dbo].[SecurityUser]
	WHERE [Username] = @strUsername;
END;