using System;
using System.Data;
using System.Data.SqlClient;

namespace Otb.Interview.PayrollCalculator
{
    /// <summary>
    /// A class that handles the processing of payroll.
    /// </summary>
    public sealed class PayrollEngine
    {
        private readonly string _connectionString;

        /// <summary>
        /// Initializes a new instance of the <see cref="PayrollEngine"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string to the data source.</param>
        /// <exception cref="ArgumentException">Thrown when <paramref name="connectionString"/> is a
        /// null or empty string.</exception>
        public PayrollEngine(string connectionString)
        {
            Verify.ValidString(connectionString, "connectionString");

            _connectionString = connectionString;
        }

        /// <summary>
        /// Runs the payment calculations for the specified employee.
        /// </summary>
        /// <param name="employeeName">The name of the employee.</param>
        /// <returns>Payment information calculated for the specified employee.</returns>
        /// <exception cref="ArgumentException">Thrown when <paramref name="employeeName"/>
        /// is a null or empty string.</exception>
        /// <exception cref="InvalidOperationException">Thrown when the calculation operation failed.</exception>
        public EmployeePaymentInformation RunEmployeePayroll(string employeeName)
        {
            Verify.ValidString(employeeName, "employeeName");

            try
            {
                return RunEmployeePayrollCore(employeeName);
            }
            catch (SqlException exception)
            {
                string description = String.Format("Failed to run payroll for {0}. See inner exception for details.", employeeName);
                throw new InvalidOperationException(description, exception);
            }
        }

        private EmployeePaymentInformation RunEmployeePayrollCore(string employeeName)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "Finance_RunAnnualPaymentCalculations @EmployeeName";
                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@EmployeeName", employeeName).DbType = System.Data.DbType.AnsiString;

                // Execute the command and read the payment information
                using (var reader = command.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        string description = String.Format("Failed to resolve payment information for {0}.", employeeName);
                        throw new InvalidOperationException(description);
                    }

                    return CreateFromReader(reader);
                }
            }
        }

        private static EmployeePaymentInformation CreateFromReader(IDataReader reader)
        {
            var information = new EmployeePaymentInformation();

            information.EmployeeId = reader.GetValue<int>("employee_id");
            information.EmployeeName = reader.GetValue<string>("employee_name");
            information.LocalCurrency = reader.GetValue<string>("local_currency");
            information.LocalAnnualSalary = reader.GetValue<decimal>("local_annual_salary");
            information.ConvertedAnnualSalary = reader.GetValue<decimal>("converted_annual_salary");

            return information;
        }
    }
}