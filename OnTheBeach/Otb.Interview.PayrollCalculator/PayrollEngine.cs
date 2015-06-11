using System;

namespace Otb.Interview.PayrollCalculator
{
    /// <summary>
    /// A class that handles the processing of payroll.
    /// </summary>
    public sealed class PayrollEngine
    {
        /// <summary>
        /// Runs the payment calculations for the specified employee.
        /// </summary>
        /// <param name="employeeName">The name of the employee.</param>
        /// <returns>Payment information calculated for the specified employee.</returns>
        /// <exception cref="ArgumentException">Thrown when <paramref name="employeeName"/>
        /// is a null or empty string.</exception>
        public EmployeePaymentInformation RunEmployeePayroll(string employeeName)
        {
            Verify.ValidString(employeeName, "employeeName");

            return new EmployeePaymentInformation()
            {
                EmployeeName = employeeName
            };
        }
    }
}