namespace MerchantNotes.Chain
{
    using System.Collections.Generic;

    public abstract class ParsingRuleBase
    {
        public abstract bool Handles(string input);
        public abstract string Process(string input, Dictionary<string, string> currencyConverters, Dictionary<string, float> materials);
    }
}
