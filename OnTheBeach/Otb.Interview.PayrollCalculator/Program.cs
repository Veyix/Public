using System;
using System.Configuration;

namespace Otb.Interview.PayrollCalculator
{
    /// <summary>
    /// A class that provides the starting point for the application.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// <param name="args">Arguments supplied by the command line.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="args"/> is null.</exception>
        public static void Main(string[] args)
        {
            Verify.NotNull(args, "args");

            // Check for a single parameter as the employee name
            if (args.Length == 0)
            {
                ConsoleHelper.WriteInformation("No argument supplied. Please provide an employee name.");

                return;
            }

            // Get the connection string to the data source
            string connectionString = GetConnectionString();
            if (String.IsNullOrWhiteSpace(connectionString))
            {
                ConsoleHelper.WriteInformation("Failed to resolve the data source connection.");

                return;
            }

            // Check to see if the user just wants to see staff information
            string firstArgument = args[0];
            if (firstArgument.Equals("staff", StringComparison.InvariantCultureIgnoreCase))
            {
                try
                {
                    ShowStaff(connectionString);
                }
                catch (Exception exception)
                {
                    ConsoleHelper.WriteError(exception.Message);
                }

                return;
            }

            try
            {
                RunPayrollCalculations(connectionString, firstArgument);
            }
            catch (Exception exception)
            {
                ConsoleHelper.WriteError(exception.Message);
            }
        }

        private static void RunPayrollCalculations(string connectionString, string employeeName)
        {
            // Get the payroll information for the specified employee
            var engine = new PayrollEngine(connectionString);
            var information = engine.RunEmployeePayroll(employeeName);

            if (information == null)
            {
                ConsoleHelper.WriteInformation("Employee '{0}' was not found. Please provide a valid employee name.", employeeName);

                return;
            }

            // Display the information to the user
            ConsoleHelper.WriteInformation("Payment Information:");
            ConsoleHelper.WriteInformation("====================");
            ConsoleHelper.WriteInformation("Employee:\t\t{0} ({1})", employeeName, information.EmployeeId);
            ConsoleHelper.WriteInformation("Annual Salary ({0}):\t{1}", information.LocalCurrency, information.LocalAnnualSalary);
            ConsoleHelper.WriteInformation("Annual Salary (GBP):\t{0:C2}", information.ConvertedAnnualSalary);
        }

        private static void ShowStaff(string connectionString)
        {
            var engine = new PayrollEngine(connectionString);
            var staffMembers = engine.GetPayrollStaff();

            ConsoleHelper.WriteInformation("Staff Members:");
            ConsoleHelper.WriteInformation("==============");

            foreach (var staffMember in staffMembers)
            {
                ConsoleHelper.WriteInformation("{0} ({1}) - {2:C2}", staffMember.EmployeeName,
                    staffMember.EmployeeId, staffMember.ConvertedAnnualSalary);
            }
        }

        private static string GetConnectionString()
        {
            var connectionStringContainer = ConfigurationManager.ConnectionStrings["PublicDatabase"];
            if (connectionStringContainer == null)
            {
                return null;
            }

            return connectionStringContainer.ConnectionString;
        }
    }
}