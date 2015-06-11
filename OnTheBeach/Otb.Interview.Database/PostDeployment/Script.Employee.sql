set identity_insert Employee on

insert into Employee (id, name, role_id)
values
	(1, 'Homer Simpson', 1),
	(2, 'Sterling Archer', 1),
	(3, 'Eric Cartman', 1),
	(4, 'Fred Flintstone', 2),
	(5, 'Professor Farnsworth', 3)

set identity_insert Employee off