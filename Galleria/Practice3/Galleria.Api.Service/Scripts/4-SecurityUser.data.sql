insert into dbo.SecurityUser (Username, [Password], Roles)
select 'guest', 'test', 'BasicUser' union all
select 'admin', '4dm1n', 'Administrator';