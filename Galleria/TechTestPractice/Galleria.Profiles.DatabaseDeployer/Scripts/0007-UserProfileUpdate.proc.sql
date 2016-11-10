IF EXISTS (
	SELECT TOP 1 1 FROM sys.procedures AS P
	INNER JOIN sys.schemas AS S ON S.[schema_id] = P.[schema_id]
	WHERE S.[name] = 'dbo'
	AND P.[name] = 'UserProfileUpdate'
)
	/* Drop the procedure before we re-create it */
	DROP PROCEDURE [dbo].[UserProfileUpdate];
GO

/*
 * Description: Updates an existing user profile record with the given information.
 *
 * Parameters:
 *	@intUserId		- The Id of the user profile record to update.
 *	@intCompanyId	- The Id of the company to which the user belongs.
 *	@strTitle		- The user's preferred title.
 *	@strForename	- The user's first name.
 *	@strSurname		- The user's last name.
 *	@dteDateOfBirth	- The user's date of birth.
 *
 * Author:		Samuel Slade
 * Date:		10/11/2016
 */
CREATE PROCEDURE [dbo].[UserProfileUpdate] (
	@intUserId		INT,
	@intCompanyId	INT,
	@strTitle		VARCHAR(50),
	@strForename	VARCHAR(100),
	@strSurname		VARCHAR(100),
	@dteDateOfBirth	DATE
)
AS
BEGIN
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED;
	SET NOCOUNT ON;

	UPDATE [dbo].[UserProfile]
	SET [CompanyId]		= @intCompanyId,
		[Title]			= @strTitle,
		[Forename]		= @strForename,
		[Surname]		= @strSurname,
		[DateOfBirth]	= @dteDateOfBirth
	WHERE [Id] = @intUserId;
END;