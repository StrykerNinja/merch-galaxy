namespace MerchantNotes.Tests.Chain
{
    using MerchantNotes.Chain;
    using Shouldly;

    public class MaterialAssignmentParsingRuleTests
    {
        public void ShouldNotHandleWhenTextIsEmpty()
        {
            // arrange
            var textToTest = string.Empty;
            var rule = new MaterialAssignmentParsingRule();

            // act
            var result = rule.Handles(textToTest);

            // assert
            result.ShouldBe(false);
        }
        public void ShouldNotHandleWhenTextIsWhitespace()
        {
            // arrange
            var textToTest = " ";
            var rule = new MaterialAssignmentParsingRule();

            // act
            var result = rule.Handles(textToTest);

            // assert
            result.ShouldBe(false);
        }
        public void ShouldNotHandleWhenTextEndsWithQuestionMark()
        {
            // arrange
            var textToTest = "endswithquestionmark?";
            var rule = new MaterialAssignmentParsingRule();

            // act
            var result = rule.Handles(textToTest);

            // assert
            result.ShouldBe(false);
        }
        public void ShouldNotHandleWhenTextHasIsWithoutSpaces()
        {
            // arrange
            var textToTest = "hasiswithoutspaces";
            var rule = new MaterialAssignmentParsingRule();

            // act
            var result = rule.Handles(textToTest);

            // assert
            result.ShouldBe(false);
        }
        public void ShouldNotHandleWhenTextHasIsWithSpaceBefore()
        {
            // arrange
            var textToTest = "has iswithspacebefore";
            var rule = new MaterialAssignmentParsingRule();

            // act
            var result = rule.Handles(textToTest);

            // assert
            result.ShouldBe(false);
        }
        public void ShouldNotHandleWhenTextHasIsWithSpaceAfter()
        {
            // arrange
            var textToTest = "hasis withspaceafter";
            var rule = new MaterialAssignmentParsingRule();

            // act
            var result = rule.Handles(textToTest);

            // assert
            result.ShouldBe(false);
        }
        public void ShouldNotHandleWhenTextHasCreditsWithoutSpaces()
        {
            // arrange
            var textToTest = "hascreditswithoutspaces";
            var rule = new MaterialAssignmentParsingRule();

            // act
            var result = rule.Handles(textToTest);

            // assert
            result.ShouldBe(false);
        }

        public void ShouldHandleWhenTextHasIsWithSpacesBeforeAndAfterAndCreditsWithSpaceBefore()
        {
            // arrange
            var textToTest = "has is and credits";
            var rule = new MaterialAssignmentParsingRule();

            // act
            var result = rule.Handles(textToTest);

            // assert
            result.ShouldBe(true);
        }
    }
}
