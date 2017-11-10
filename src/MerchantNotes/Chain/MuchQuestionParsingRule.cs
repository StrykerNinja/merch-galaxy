namespace MerchantNotes.Chain
{
    using System;
    using System.Collections.Generic;

    public class MuchQuestionParsingRule : ParsingRuleBase
    {
        public override bool Handles(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return false;
            }
            input = input.ToLowerInvariant();
            if (!input.EndsWith("?") || !input.StartsWith("how much is "))
            {
                return false;
            }
            var parts = input.Split(new[] { " is " }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length != 2)
            {
                return false;
            }
            return true;
        }

        public override string Process(string input, Dictionary<string, string> currencyConverters, Dictionary<string, float> materials)
        {
            input = input.TrimEnd('?');
            var parts = input.Split(new[] { " is " }, StringSplitOptions.RemoveEmptyEntries);
            var predicate = parts[1];
            var predicateParts = predicate.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            var roman = string.Empty;
            for (int i = 0; i < predicateParts.Length; i++)
            {
                var currencyKey = predicateParts[i];
                if (!currencyConverters.ContainsKey(currencyKey))
                {
                    return Program.ERROR_MESSAGE;
                }
                roman += currencyConverters[currencyKey];
            }
            try
            {
                // NOTE: would normally Inject this converter from IoC
                var total = new RomanConverter.Converter().GetIntFromRoman(roman);
                return string.Format("{0} is {1}", predicate, total);
            }
            catch (Exception)
            {
                return Program.ERROR_MESSAGE;
            }
        }
    }
}
