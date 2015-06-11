create procedure Employee_GetStaff
as
begin

	-- Get all staff level employees and order by who earns the most
	-- Note: We need to go off the converted salary to ensure each salary is of equal unit value.
	; with StaffEmployees (EmployeeId)
	as (
		select e.id
		from Employee e
		inner join [Role] r on r.id = e.role_id
		where r.name = 'Staff'
	)

	select s.*
	from GetAnnualSalaries(NULL) s
	inner join StaffEmployees e on e.EmployeeId = s.employee_id
	order by s.converted_annual_salary desc
end