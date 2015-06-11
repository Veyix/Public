using System;

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
                Console.WriteLine("No argument supplied. Please provide an employee name.");

                return;
            }

            // Get the payroll information for the specified employee
            var engine = new PayrollEngine();

            string employeeName = args[0];
            var information = engine.RunEmployeePayroll(employeeName);

            if (information == null)
            {
                Console.WriteLine("Employee '{0}' was not found. Please provide a valid employee name.", employeeName);

                return;
            }

            // Display the information to the user
            Console.WriteLine("Payment Information:");
            Console.WriteLine("====================");
            Console.WriteLine("Employee:\t\t{0} ({1})", employeeName, information.EmployeeId);
            Console.WriteLine("Annual Salary ({0}):\t{1}", information.LocalCurrency, information.LocalAnnualSalary);
            Console.WriteLine("Annual Salary (GBP):\t{0}", information.ConvertedAnnualSalary);
        }
    }
}