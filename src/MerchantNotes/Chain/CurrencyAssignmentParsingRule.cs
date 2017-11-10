namespace MerchantNotes.Chain
{
    using System;
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
            if (input.EndsWith(" credits") || input.EndsWith("?"))
            {
                return false;
            }
            if (!input.Contains(" is "))
            {
                return false;
            }
            var parts = input.Split(new[] { " is " }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length < 2 || parts.Length > 2 || parts[0].Length != 4 || parts[1].Length != 1)
            {
                return false;
            }
            return true;
        }

        public override string Process(string input, Dictionary<string, string> currencyConverters, Dictionary<string, float> materials)
        {
            var parts = input.Split(new[] { " is " }, StringSplitOptions.RemoveEmptyEntries);
            var currency = parts[0];
            var roman = parts[1];
            try
            {
                var val = new RomanConverter.Converter().GetIntFromRoman(roman);
                if (currencyConverters.ContainsKey(currency))
                {
                    currencyConverters.Remove(currency);
                }
                currencyConverters.Add(currency, roman);
                return string.Empty;
            }
            catch (Exception)
            {
                return "I have no idea what you are talking about";
            }
        }
    }
}
