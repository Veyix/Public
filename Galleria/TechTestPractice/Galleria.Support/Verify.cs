using System;

namespace Galleria.Support
{
    /// <summary>
    /// A class that provides basic parameter and argument verification methods.
    /// </summary>
    public static class Verify
    {
        /// <summary>
        /// Verifies that the given value is not null and throws a new instance of
        /// the <see cref="ArgumentNullException"/> class if it is.
        /// </summary>
        /// <param name="value">The value to be verified.</param>
        /// <param name="valueName">The name of the value.</param>
        public static void NotNull(object value, string valueName)
        {
            if (value == null)
            {
                throw new ArgumentNullException(valueName);
            }
        }

        /// <summary>
        /// Verifies that the given value is not null or empty and throws a new
        /// instance of the <see cref="ArgumentException"/> class if it is.
        /// </summary>
        /// <param name="value">The value to be verified.</param>
        /// <param name="valueName">The name of the value.</param>
        public static void NotNullOrEmpty(string value, string valueName)
        {
            if (String.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException($"The value '{valueName}' cannot be empty", valueName);
            }
        }
    }
}
