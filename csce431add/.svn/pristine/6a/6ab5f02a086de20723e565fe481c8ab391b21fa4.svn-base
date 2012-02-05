using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ClassLibrary1
{
    public static class Validation
    {
        /// <summary>
        /// Validates string text. Ensures no spaces or non-alpha characters are used within the text.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private static bool Validate(string text)
        {
            bool bIsValid = false;

            // List of regex's to test against
            Regex space = new Regex(" ");   // Checking for white-space
            Regex alpha = new Regex("[a-z].."); // Checking for alpha 
            Regex numeric = new Regex("[0-9].."); // Checking for numerical values

            bIsValid = alpha.IsMatch(text) && !space.IsMatch(text) && !numeric.IsMatch(text);

            return bIsValid;
        }

        /// <summary>
        /// Validates integer text.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private static bool Validate(int text)
        {
            bool bIsValid = false;

            // Need actual pattern here, this one is just a temp.
            Regex format = new Regex("[0-9][0-9][0-9][0-9][0-9][0-9][0-9]");

            bIsValid = format.IsMatch(Convert.ToString(text));

            return bIsValid;
        }
    }
}
