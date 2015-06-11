create table Salary (
	id				int		identity (1, 1) not null,
	employee_id		int		not null,
	currency		int		not null,
	annual_amount	bigint	not null
)