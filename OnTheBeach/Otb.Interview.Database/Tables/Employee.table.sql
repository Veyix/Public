create table Employee (
	id			int					identity (1, 1) not null,
	name		nvarchar(255)		not null,
	role_id		int					not null
)