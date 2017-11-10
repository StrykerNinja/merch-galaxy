namespace MerchantNotes.Tests.Chain
{
    using MerchantNotes.Chain;
    using Shouldly;

    public class QuestionParsingRuleTests
    {
        public void ShouldNotHandleWhenTextIsEmpty()
        {
            // arrange
            var textToTest = string.Empty;
            var rule = new QuestionParsingRule();

            // act
            var result = rule.Handles(textToTest);

            // assert
            result.ShouldBe(false);
        }
        public void ShouldNotHandleWhenTextIsWhitespace()
        {
            // arrange
            var textToTest = " ";
            var rule = new QuestionParsingRule();

            // act
            var result = rule.Handles(textToTest);

            // assert
            result.ShouldBe(false);
        }
        public void ShouldNotHandleWhenTextDoesNotEndWithQuestionMark()
        {
            // arrange
            var textToTest = "doesnotendwithquestionmark";
            var rule = new QuestionParsingRule();

            // act
            var result = rule.Handles(textToTest);

            // assert
            result.ShouldBe(false);
        }
        public void ShouldNotHandleWhenTextHasIsWithoutSpaces()
        {
            // arrange
            var textToTest = "hasiswithoutspaces";
            var rule = new QuestionParsingRule();

            // act
            var result = rule.Handles(textToTest);

            // assert
            result.ShouldBe(false);
        }
        public void ShouldNotHandleWhenTextHasIsWithSpaceBefore()
        {
            // arrange
            var textToTest = "has iswithspacebefore";
            var rule = new QuestionParsingRule();

            // act
            var result = rule.Handles(textToTest);

            // assert
            result.ShouldBe(false);
        }
        public void ShouldNotHandleWhenTextHasIsWithSpaceAfter()
        {
            // arrange
            var textToTest = "hasis withspaceafter";
            var rule = new QuestionParsingRule();

            // act
            var result = rule.Handles(textToTest);

            // assert
            result.ShouldBe(false);
        }

        public void ShouldHandleWhenTextHasQuestionMarkAndIsWithSpacesBeforeAndAfter()
        {
            // arrange
            var textToTest = "has is and question mark?";
            var rule = new QuestionParsingRule();

            // act
            var result = rule.Handles(textToTest);

            // assert
            result.ShouldBe(true);
        }
    }
}
