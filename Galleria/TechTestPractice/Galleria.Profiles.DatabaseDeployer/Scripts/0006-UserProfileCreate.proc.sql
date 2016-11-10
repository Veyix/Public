IF EXISTS (
	SELECT TOP 1 1 FROM sys.procedures AS P
	INNER JOIN sys.schemas AS S ON S.[schema_id] = P.[schema_id]
	WHERE S.[name] = 'dbo'
	AND P.[name] = 'UserProfileCreate'
)
	/* Drop the procedure before we re-create it */
	DROP PROCEDURE [dbo].[UserProfileCreate];
GO

/*
 * Description: Creates a new user profile record using the given information.
 *
 * Parameters:
 *	@intCompanyId	- The Id of the company to which the user belongs.
 *	@strTitle		- The user's preferred title.
 *	@strForename	- The user's first name.
 *	@strSurname		- The user's last name.
 *	@dteDateOfBirth - The user's date of birth.
 *
 * Author:		Samuel Slade
 * Date:		10/11/2016
 */
CREATE PROCEDURE [dbo].[UserProfileCreate] (
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
	
	INSERT INTO [dbo].[UserProfile] (
		[CompanyId],
		[Title],
		[Forename],
		[Surname],
		[DateOfBirth],
		[CreatedDate],
		[LastChangedDate]
	)
	SELECT @intCompanyId
		 , @strTitle
		 , @strForename
		 , @strSurname
		 , @dteDateOfBirth
		 , GETDATE()
		 , GETDATE();
END;