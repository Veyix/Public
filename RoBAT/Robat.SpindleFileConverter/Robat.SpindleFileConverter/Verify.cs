using System;

namespace Robat.SpindleFileConverter
{
    public static class Verify
    {
        public static void NotNull(object value, string valueName)
        {
            if (value == null)
            {
                throw new ArgumentNullException(valueName);
            }
        }

        public static void NotNullOrEmpty(string value, string valueName)
        {
            if (String.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("The given string value cannot be null or empty.", valueName);
            }
        }
    }
}