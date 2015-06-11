-- Make sure we deploy these scripts in the right order to maintain referential integrity
set identity_insert Currency on

insert into Currency (id, unit, conversion_factor)
values
	(1, 'GBP', 1),
	(2, 'USD', 1.54),
	(3, 'Rocks', 10),
	(4, 'Sweets', 12),
	(5, 'Credits', 8000)

set identity_insert Currency off
set identity_insert [Role] on

insert into [Role] (id, name)
values
	(1, 'Staff'),
	(2, 'Manager'),
	(3, 'Owner')

set identity_insert [Role] off
set identity_insert Employee on

insert into Employee (id, name, role_id)
values
	(1, 'Homer Simpson', 1),
	(2, 'Sterling Archer', 1),
	(3, 'Eric Cartman', 1),
	(4, 'Fred Flintstone', 2),
	(5, 'Professor Farnsworth', 3)

set identity_insert Employee off
set identity_insert Salary on

insert into Salary (id, employee_id, currency, annual_amount)
values
	(1, 1, 2, 22000),
	(2, 2, 2, 150000),
	(3, 3, 4, 60000),
	(4, 4, 3, 900000),
	(5, 5, 5, 5000000000)

set identity_insert Salary off
GO
