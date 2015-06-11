create function GetAnnualSalaries (@EmployeeName nvarchar(255))
returns table
as
return (

	-- Collect the annual salaries of each employee in GBP
	-- Note: Filter by the employee name if we have one, otherwise default to retrieving all records.
	select e.id employee_id
		 , e.name employee_name
		 , c.unit local_currency
		 , convert(decimal(18, 2), s.annual_amount) local_annual_salary
		 , convert(decimal(8, 2), round(s.annual_amount / c.conversion_factor, 2)) converted_annual_salary
	from Employee e
	inner join Salary s on s.employee_id = e.id
	inner join Currency c on c.id = s.currency
	where isnull(@EmployeeName, e.name) = e.name
)