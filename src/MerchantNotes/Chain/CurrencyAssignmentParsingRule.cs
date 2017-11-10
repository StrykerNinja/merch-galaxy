﻿namespace MerchantNotes.Chain
{
    using System.Collections.Generic;

    public class CurrencyAssignmentParsingRule : ParsingRuleBase
    {
        public override bool Handles(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return false;
            }
            input = input.ToLowerInvariant();
            if (input.EndsWith("?"))
            {
                return false;
            }
            if (!input.Contains(" is "))
            {
                return false;
            }
            if (input.Contains(" credits"))
            {
                return false;
            }
            return true;
        }

        public override string Parse(string input, Dictionary<string, char> currencyConverters, HashSet<string> materials)
        {
            return "42";
        }
    }
}