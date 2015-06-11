set identity_insert [Role] on

insert into [Role] (id, name)
values
	(1, 'Staff'),
	(2, 'Manager'),
	(3, 'Owner')

set identity_insert [Role] off