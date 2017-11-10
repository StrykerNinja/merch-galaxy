namespace MerchantNotes
{
    using System;
    using Chain;
    using System.Linq;
    using System.Collections.Generic;

    class Program
    {
        private static ParsingRuleBase[] _parsingRules = new ParsingRuleBase[]
        {
            new CurrencyAssignmentParsingRule(),
            new MaterialAssignmentParsingRule(),
            new QuestionParsingRule()
        };
        private static Dictionary<string, char> _currencyConversion = new Dictionary<string, char>() { { "glob", 'i' }, { "prok", 'v' }, { "pish", 'x' }, { "tegj", 'l' } };
        private static HashSet<string> _materials = new HashSet<string>() { "silver", "gold", "iron" };

        static void Main(string[] args)
        {
            Console.WriteLine("Merchant Notes running, enter exit to quit...");
            Console.Write(">");
            var text = Console.ReadLine().ToLowerInvariant();
            while (text != "exit")
            {
                try
                {
                    var parseRule = _parsingRules.SingleOrDefault(pr => pr.Handles(text));
                    if (parseRule == null)
                    {
                        Console.WriteLine("I have no idea what you are talking about");
                    }
                    else
                    {
                        var output = parseRule.Parse(text, _currencyConversion, _materials);
                        if (!string.IsNullOrWhiteSpace(output))
                        {
                            Console.WriteLine(output);
                        }
                    }
                }
                catch (InvalidOperationException)
                {
                    Console.WriteLine("Note was entered in an allowable format; however matched too many possibilities");
                }
                Console.Write(">");
                text = Console.ReadLine().ToLowerInvariant();
            }
        }
    }
}
