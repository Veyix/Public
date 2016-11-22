create table dbo.SecurityUser (
	Id int not null identity (1, 1) primary key clustered,
	Username varchar(100) not null,
	[Password] varchar(100) not null,
	Roles varchar(100) not null
);

create unique nonclustered index [IX_dbo-SecurityUser:Username]
	on dbo.SecurityUser ([Username]);