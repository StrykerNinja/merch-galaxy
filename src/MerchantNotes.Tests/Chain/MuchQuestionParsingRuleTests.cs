namespace MerchantNotes.Tests.Chain
{
    using MerchantNotes.Chain;
    using Shouldly;
    using System.Collections.Generic;

    public class MuchQuestionParsingRuleTests
    {
        public void ShouldNotHandleWhenTextIsNull()
        {
            // arrange
            string textToTest = null;
            var rule = new MuchQuestionParsingRule();

            // act
            var result = rule.Handles(textToTest);

            // assert
            result.ShouldBe(false);
        }
        public void ShouldNotHandleWhenTextIsEmpty()
        {
            // arrange
            var textToTest = string.Empty;
            var rule = new MuchQuestionParsingRule();

            // act
            var result = rule.Handles(textToTest);

            // assert
            result.ShouldBe(false);
        }
        public void ShouldNotHandleWhenTextIsWhitespace()
        {
            // arrange
            var textToTest = " ";
            var rule = new MuchQuestionParsingRule();

            // act
            var result = rule.Handles(textToTest);

            // assert
            result.ShouldBe(false);
        }
        public void ShouldNotHandleWhenTextDoesNotEndWithQuestionMark()
        {
            // arrange
            var textToTest = "doesnotendwithquestionmark";
            var rule = new MuchQuestionParsingRule();

            // act
            var result = rule.Handles(textToTest);

            // assert
            result.ShouldBe(false);
        }
        public void ShouldNotHandleWhenTextDoesNotStartWithHowMuchIs()
        {
            // arrange
            var textToTest = "not how much is ";
            var rule = new MuchQuestionParsingRule();

            // act
            var result = rule.Handles(textToTest);

            // assert
            result.ShouldBe(false);
        }

        public void ShouldHandleWhenTextHasQuestionMarkAndStartsWithHowMuchIs()
        {
            // arrange
            var textToTest = "how much is ?";
            var rule = new MuchQuestionParsingRule();

            // act
            var result = rule.Handles(textToTest);

            // assert
            result.ShouldBe(true);
        }

        public void ShouldReturnErrorMessageWhenNoValidCurrency()
        {
            // arrange
            var textToTest = "how much is nope?";
            var currencies = new Dictionary<string, string> { { "curr", "Z" } };
            var rule = new MuchQuestionParsingRule();

            // act
            var result = rule.Process(textToTest, currencies, null);

            // assert
            result.ShouldBe(Program.ERROR_MESSAGE);
        }

        public void ShouldReturnCorrectMessageWhenValidCurrency()
        {
            // arrange
            var textToTest = "how much is curr?";
            var currencies = new Dictionary<string, string> { { "curr", "X" } };
            var rule = new MuchQuestionParsingRule();
            var expected = "curr is 10";

            // act
            var result = rule.Process(textToTest, currencies, null);

            // assert
            result.ShouldBe(expected);
        }

        public void ShouldReturnCorrectMessageWhenMultipleValidCurrency()
        {
            // arrange
            var textToTest = "how much is pish tegj glob glob?";
            var currencies = new Dictionary<string, string> { { "pish", "X" }, { "tegj", "L" }, { "glob", "I" } };
            var rule = new MuchQuestionParsingRule();
            var expected = "pish tegj glob glob is 42";

            // act
            var result = rule.Process(textToTest, currencies, null);

            // assert
            result.ShouldBe(expected);
        }
    }
}
