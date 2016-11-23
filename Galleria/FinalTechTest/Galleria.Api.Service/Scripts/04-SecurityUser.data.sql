INSERT INTO [dbo].[SecurityUser] ([Username], [Password], [Role])
SELECT 'guest', 'test', 'BasicUser' UNION ALL
SELECT 'admin', '4dm1n', 'Administrator';