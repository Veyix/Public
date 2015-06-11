create procedure Finance_RunAnnualPaymentCalculations(
	@EmployeeName nvarchar(255) = null
)
as
begin

	select *
	from GetAnnualSalaries(@EmployeeName)
end