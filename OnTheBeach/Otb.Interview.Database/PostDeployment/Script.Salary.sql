set identity_insert Salary on

insert into Salary (id, employee_id, currency, annual_amount)
values
	(1, 1, 2, 22000),
	(2, 2, 2, 150000),
	(3, 3, 4, 60000),
	(4, 4, 3, 900000),
	(5, 5, 5, 5000000000)

set identity_insert Salary off