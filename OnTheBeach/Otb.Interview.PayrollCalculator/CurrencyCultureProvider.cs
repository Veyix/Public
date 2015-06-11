using System;
using System.Collections.Generic;
using System.Globalization;

namespace Otb.Interview.PayrollCalculator
{
    /// <summary>
    /// A class that provides culture information for specific currency definitions.
    /// </summary>
    public static class CurrencyCultureProvider
    {
        private static readonly Dictionary<string, CultureInfo> _cultureInfoByCurrency =
            new Dictionary<string, CultureInfo>(StringComparer.InvariantCultureIgnoreCase);

        static CurrencyCultureProvider()
        {
            _cultureInfoByCurrency["GBP"] = CultureInfo.GetCultureInfo("en-gb");
            _cultureInfoByCurrency["USD"] = CultureInfo.GetCultureInfo("en-us");
            _cultureInfoByCurrency["Rocks"] = CultureInfo.GetCultureInfo("fr-FR");
            _cultureInfoByCurrency["Sweets"] = CultureInfo.GetCultureInfo("ja-JP");
            _cultureInfoByCurrency["Credits"] = CultureInfo.GetCultureInfo("zh-CHS");
        }

        /// <summary>
        /// Gets the culture information for the specified currency type.
        /// </summary>
        /// <param name="currency">The type of currency for which to get the culture information.</param>
        /// <returns>The culture information for the currency, if found; otherwise the current culture.</returns>
        /// <exception cref="ArgumentException">Thrown when <paramref name="currency"/> is a null or empty string.</exception>
        public static CultureInfo GetCurrencyCulture(string currency)
        {
            Verify.ValidString(currency, "currency");

            CultureInfo culture;
            if (!_cultureInfoByCurrency.TryGetValue(currency, out culture))
            {
                // Fallback to the current culture
                return CultureInfo.CurrentCulture;
            }

            return culture;
        }
    }
}