namespace MerchantNotes
{
    using System;
    using Chain;
    using System.Linq;
    using System.Collections.Generic;

    public class Program
    {
        private static ParsingRuleBase[] _parsingRules = new ParsingRuleBase[]
        {
            new CurrencyAssignmentParsingRule(),
            new MaterialAssignmentParsingRule(),
            new ManyQuestionParsingRule()
        };
        private static Dictionary<string, string> _currencyConversion = new Dictionary<string, string> { { "glob", "i" }, { "prok", "v" }, { "pish", "x" }, { "tegj", "l" } };
        private static Dictionary<string, float> _materials = new Dictionary<string, float> { { "silver", 17.0f }, { "gold", 14450.0f }, { "iron", 195.5f } };
        public const string ERROR_MESSAGE = "I have no idea what you are talking about";

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
                        Console.WriteLine(ERROR_MESSAGE);
                    }
                    else
                    {
                        var output = parseRule.Process(text, _currencyConversion, _materials);
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
