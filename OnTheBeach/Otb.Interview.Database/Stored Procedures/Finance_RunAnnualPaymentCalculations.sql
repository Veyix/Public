create procedure Finance_RunAnnualPaymentCalculations
as
begin

	-- Collect the annual salaries of each employee in GBP
	select e.id
		 , e.name
		 , round(s.annual_amount / c.conversion_factor, 2) annual_salary
	from Employee e
	inner join Salary s on s.employee_id = e.id
	inner join Currency c on c.id = s.currency
end