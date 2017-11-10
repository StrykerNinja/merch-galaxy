namespace MerchantNotes.Chain
{
    using System.Collections.Generic;

    public abstract class ParsingRuleBase
    {
        public abstract bool Handles(string input);
        public abstract string Parse(string input, Dictionary<string, char> currencyConverters, HashSet<string> materials);
    }
}
