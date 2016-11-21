create table dbo.UserProfile (
	Id int not null identity (1, 1),
	CompanyId int not null,
	Title varchar(50) not null,
	Forename varchar(100) not null,
	Surname varchar(100) not null,
	DateOfBirth date not null
);

alter table dbo.UserProfile
add constraint [PK_dbo-UserProfile:Id]
	primary key clustered ([Id] asc);