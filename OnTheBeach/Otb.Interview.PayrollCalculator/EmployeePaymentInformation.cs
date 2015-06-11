namespace Otb.Interview.PayrollCalculator
{
    /// <summary>
    /// A class that contains information about the calculated payment figures for an employee.
    /// </summary>
    public sealed class EmployeePaymentInformation
    {
        /// <summary>
        /// Gets or sets the Id of the employee to which the payment information is related.
        /// </summary>
        public int EmployeeId { get; set; }

        /// <summary>
        /// Gets or sets the name of the employee to which the payment information is related.
        /// </summary>
        public string EmployeeName { get; set; }

        /// <summary>
        /// Gets or sets the local currency used for this employee.
        /// </summary>
        public string LocalCurrency { get; set; }

        /// <summary>
        /// Gets or sets the annual salary paid to the employee in their local currency.
        /// </summary>
        public decimal LocalAnnualSalary { get; set; }

        /// <summary>
        /// Gets or sets the annual salary paid to the employee in GBP.
        /// </summary>
        public decimal ConvertedAnnualSalary { get; set; }

        /// <summary>
        /// Gets a formatted string representation of the local annual salary using the culture specific to the currency.
        /// </summary>
        /// <returns>A string representation of the local currency salary.</returns>
        public string GetLocalAnnualSalaryFormatted()
        {
            var culture = CurrencyCultureProvider.GetCurrencyCulture(LocalCurrency);
            return LocalAnnualSalary.ToString("C2", culture);
        }
    }
}