alter table Salary
add constraint FK_Salary_Currency
	foreign key (currency)
	references Currency (id)
		on update no action
		on delete no action