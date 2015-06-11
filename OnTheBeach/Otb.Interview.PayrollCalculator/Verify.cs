using System;

namespace Otb.Interview.PayrollCalculator
{
    /// <summary>
    /// A class that provides support for verifying the validity of basic conditions.
    /// </summary>
    public static class Verify
    {
        /// <summary>
        /// Checks that the given value is not null and throws an instance of the
        /// <see cref="ArgumentNullException"/> class if it is.
        /// </summary>
        /// <param name="value">The value to be verified.</param>
        /// <param name="valueName">The name of the value under verification.</param>
        public static void NotNull(object value, string valueName)
        {
            if (value == null)
            {
                throw new ArgumentNullException(valueName);
            }
        }

        /// <summary>
        /// Checks that the given value is not a null or empty string and thros an
        /// instance of the <see cref="ArgumentException"/> class if it is.
        /// </summary>
        /// <param name="value">The value to be verified.</param>
        /// <param name="valueName">The name of the value under verification.</param>
        public static void ValidString(string value, string valueName)
        {
            if (String.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException(valueName);
            }
        }
    }
}