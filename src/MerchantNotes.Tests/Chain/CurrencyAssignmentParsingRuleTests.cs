namespace MerchantNotes.Tests.Chain
{
    using MerchantNotes.Chain;
    using Shouldly;
    using System.Collections.Generic;

    public class CurrencyAssignmentParsingRuleTests
    {
        public void ShouldNotHandleWhenTextIsNull()
        {
            // arrange
            string textToTest = null;
            var rule = new CurrencyAssignmentParsingRule();

            // act
            var result = rule.Handles(textToTest);

            // assert
            result.ShouldBe(false);
        }
        public void ShouldNotHandleWhenTextIsEmpty()
        {
            // arrange
            var textToTest = string.Empty;
            var rule = new CurrencyAssignmentParsingRule();

            // act
            var result = rule.Handles(textToTest);

            // assert
            result.ShouldBe(false);
        }
        public void ShouldNotHandleWhenTextIsWhitespace()
        {
            // arrange
            var textToTest = " ";
            var rule = new CurrencyAssignmentParsingRule();

            // act
            var result = rule.Handles(textToTest);

            // assert
            result.ShouldBe(false);
        }
        public void ShouldNotHandleWhenTextEndsWithCredits()
        {
            // arrange
            var textToTest = "endswith credits";
            var rule = new CurrencyAssignmentParsingRule();

            // act
            var result = rule.Handles(textToTest);

            // assert
            result.ShouldBe(false);
        }
        public void ShouldNotHandleWhenTextEndsWithQuestionMark()
        {
            // arrange
            var textToTest = "endswithquestionmark?";
            var rule = new CurrencyAssignmentParsingRule();

            // act
            var result = rule.Handles(textToTest);

            // assert
            result.ShouldBe(false);
        }
        public void ShouldNotHandleWhenTextHasIsWithoutSpaces()
        {
            // arrange
            var textToTest = "hasiswithoutspaces";
            var rule = new CurrencyAssignmentParsingRule();

            // act
            var result = rule.Handles(textToTest);

            // assert
            result.ShouldBe(false);
        }
        public void ShouldNotHandleWhenTextHasIsWithSpaceBefore()
        {
            // arrange
            var textToTest = "has iswithspacebefore";
            var rule = new CurrencyAssignmentParsingRule();

            // act
            var result = rule.Handles(textToTest);

            // assert
            result.ShouldBe(false);
        }
        public void ShouldNotHandleWhenTextHasIsWithSpaceAfter()
        {
            // arrange
            var textToTest = "hasis withspaceafter";
            var rule = new CurrencyAssignmentParsingRule();

            // act
            var result = rule.Handles(textToTest);

            // assert
            result.ShouldBe(false);
        }
        public void ShouldNotHandleWhenTextHasIsWithSpacesBeforeAndAfterAndOnlyOnePart()
        {
            // arrange
            var textToTest = "has is ";
            var rule = new CurrencyAssignmentParsingRule();

            // act
            var result = rule.Handles(textToTest);

            // assert
            result.ShouldBe(false);
        }
        public void ShouldNotHandleWhenTextHasIsWithSpacesBeforeAndAfterAndSecondPartHasMoreThanOneLetter()
        {
            // arrange
            var textToTest = "has is fred";
            var rule = new CurrencyAssignmentParsingRule();

            // act
            var result = rule.Handles(textToTest);

            // assert
            result.ShouldBe(false);
        }

        public void ShouldHandleWhenFourLettersIsOneLetter()
        {
            // arrange
            var textToTest = "curr is Z";
            var rule = new CurrencyAssignmentParsingRule();

            // act
            var result = rule.Handles(textToTest);

            // assert
            result.ShouldBe(true);
        }

        public void ShouldReturnErrorMessageWhenNoValidCurrency()
        {
            // arrange
            var textToTest = "test is Z";
            var currencies = new Dictionary<string, string> { { "nope", "Z" } };
            var rule = new CurrencyAssignmentParsingRule();

            // act
            var result = rule.Process(textToTest, currencies, null);

            // assert
            result.ShouldBe(Program.ERROR_MESSAGE);
        }
        public void ShouldReturnErrorMessageWhenNoValidNumeral()
        {
            // arrange
            var textToTest = "test is Z";
            var currencies = new Dictionary<string, string> { { "test", "X" } };
            var rule = new CurrencyAssignmentParsingRule();

            // act
            var result = rule.Process(textToTest, currencies, null);

            // assert
            result.ShouldBe(Program.ERROR_MESSAGE);
        }
        public void ShouldReturnEmptyWhenValidCurrencyAndNumeral()
        {
            // arrange
            var textToTest = "test is V";
            var currencies = new Dictionary<string, string>();
            var rule = new CurrencyAssignmentParsingRule();

            // act
            var result = rule.Process(textToTest, currencies, null);

            // assert
            result.ShouldBe(string.Empty);
            currencies.ShouldContainKeyAndValue("test", "V");
        }
    }
}
