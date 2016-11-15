/*
 * Description: Populates the SecurityUser table with some initial users.
 *
 * Author:		Samuel Slade
 * Date:		15/11/2016
 */
INSERT INTO [dbo].[SecurityUser] ([Username], [Password], [CreatedDate])
SELECT U.[Username], U.[Password], GETDATE()
FROM (
	SELECT 'Admin' AS [Username], 'Th34dm1nP455w0rd' AS [Password]
	UNION ALL
	SELECT 'BasicUser' AS [Username], 'test' AS [Password]
) AS U
WHERE NOT EXISTS (
	SELECT TOP 1 1 FROM [dbo].[SecurityUser]
	WHERE [Username] = U.[Username]
);