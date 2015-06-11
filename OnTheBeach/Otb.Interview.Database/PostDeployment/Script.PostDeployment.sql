-- Make sure we deploy these scripts in the right order to maintain referential integrity
:r .\Script.Currency.sql
:r .\Script.Role.sql
:r .\Script.Employee.sql
:r .\Script.Salary.sql