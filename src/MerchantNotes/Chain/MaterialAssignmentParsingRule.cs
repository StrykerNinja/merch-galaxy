namespace MerchantNotes.Chain
{
    using System;
    using System.Collections.Generic;

    public class MaterialAssignmentParsingRule : ParsingRuleBase
    {
        public override bool Handles(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return false;
            }
            input = input.ToLowerInvariant();
            if (input.EndsWith("?") || input.StartsWith("how"))
            {
                return false;
            }
            if (!input.Contains(" is "))
            {
                return false;
            }
            if (!input.EndsWith(" credits"))
            {
                return false;
            }
            var parts = input.Split(new[] { " is " }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length != 2 )
            {
                return false;
            }
            return true;
        }

        public override string Process(string input, Dictionary<string, string> currencyConverters, Dictionary<string, float> materials)
        {
            var parts = input.Split(new[] { " is " }, StringSplitOptions.RemoveEmptyEntries);
            var subjectParts = parts[0].Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            var materialkey = subjectParts[subjectParts.Length - 1];
            var roman = string.Empty;
            for (int i = 0; i < subjectParts.Length - 1; i++)
            {
                var currencyKey = subjectParts[i];
                if (!currencyConverters.ContainsKey(currencyKey))
                {
                    return Program.ERROR_MESSAGE;
                }
                roman += currencyConverters[currencyKey];
            }
            int multiplier = 0;
            try
            {
                // NOTE: would normally Inject this converter from IoC
                multiplier = new RomanConverter.Converter().GetIntFromRoman(roman);
            }
            catch (Exception)
            {
                return Program.ERROR_MESSAGE;
            }
            var totalString = parts[1].Replace(" credits", "");
            int total = 0;
            if (!int.TryParse(totalString, out total))
            {
                return Program.ERROR_MESSAGE;
            }
            if (multiplier == 0 || total == 0)
            {
                return Program.ERROR_MESSAGE;
            }
            var value = total / (float)multiplier;
            if (materials.ContainsKey(materialkey))
            {
                materials.Remove(materialkey);
            }
            materials.Add(materialkey, value);
            return string.Empty;
        }
    }
}
