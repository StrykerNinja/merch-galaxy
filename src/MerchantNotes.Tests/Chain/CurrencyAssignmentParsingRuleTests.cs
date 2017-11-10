namespace MerchantNotes.Tests.Chain
{
    using MerchantNotes.Chain;
    using Shouldly;

    public class CurrencyAssignmentParsingRuleTests
    {
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
        public void ShouldNotHandleWhenTextHasCreditsWithSpaceBefore()
        {
            // arrange
            var textToTest = "has creditswithspacebefore";
            var rule = new CurrencyAssignmentParsingRule();

            // act
            var result = rule.Handles(textToTest);

            // assert
            result.ShouldBe(false);
        }

        public void ShouldHandleWhenTextHasIsWithSpacesBeforeAndAfterNoQuestionMarkOrCredits()
        {
            // arrange
            var textToTest = "has is and not much else";
            var rule = new CurrencyAssignmentParsingRule();

            // act
            var result = rule.Handles(textToTest);

            // assert
            result.ShouldBe(true);
        }
    }
}
