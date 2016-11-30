using System;

namespace Galleria.Api.Contract
{
    /// <summary>
    /// A class that provides basic value verification methods.
    /// </summary>
    public static class Verify
    {
        /// <summary>
        /// Throws an <see cref="ArgumentNullException"/> if the given value is null.
        /// </summary>
        /// <param name="value">The value to verify.</param>
        /// <param name="valueName">The name of the value.</param>
        public static void NotNull(object value, string valueName)
        {
            if (value == null)
            {
                throw new ArgumentNullException(valueName);
            }
        }

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> if the given value is null or empty.
        /// </summary>
        /// <param name="value">The value to verify.</param>
        /// <param name="valueName">The name of the value.</param>
        public static void NotNullOrEmpty(string value, string valueName)
        {
            if (String.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException($"The value '{valueName}' must be specified", valueName);
            }
        }
    }
}