alter table Salary
add constraint FK_Salary_Employee
	foreign key (employee_id)
	references Employee (id)
		on update no action
		on delete no action