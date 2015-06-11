using System;
using System.Data;

namespace Otb.Interview.PayrollCalculator
{
    /// <summary>
    /// A class that provides functionality to support working with data readers.
    /// </summary>
    public static class DataReaderExtensions
    {
        /// <summary>
        /// Gets the value from the reader with the specified column name.
        /// </summary>
        /// <typeparam name="T">The type of the value to retrieve.</typeparam>
        /// <param name="reader">The data reader from which to get the value.</param>
        /// <param name="columnName">The name of the column containing the value.</param>
        /// <returns>The value read from the given reader.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="reader"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="columnName"/> is a null
        /// or empty string.</exception>
        public static T GetValue<T>(this IDataReader reader, string columnName)
        {
            Verify.NotNull(reader, "reader");
            Verify.ValidString(columnName, "columnName");

            int ordinal = reader.GetOrdinal(columnName);
            return (T)reader.GetValue(ordinal);
        }
    }
}