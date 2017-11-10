namespace MerchantNotes.Tests.Chain
{
    using MerchantNotes.Chain;
    using Shouldly;
    using System.Collections.Generic;

    public class MaterialAssignmentParsingRuleTests
    {
        public void ShouldNotHandleWhenTextIsNull()
        {
            // arrange
            string textToTest = null;
            var rule = new MaterialAssignmentParsingRule();

            // act
            var result = rule.Handles(textToTest);

            // assert
            result.ShouldBe(false);
        }
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
        public void ShouldNotHandleWhenTextStartsWithHow()
        {
            // arrange
            var textToTest = "hownowbrowncow";
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

        public void ShouldReturnErrorMessageWhenNoValidCurrency()
        {
            // arrange
            var textToTest = "nope material is num credits";
            var materials = new Dictionary<string, float>();
            var currencies = new Dictionary<string, string> { { "curr", "X" } };
            var rule = new MaterialAssignmentParsingRule();

            // act
            var result = rule.Process(textToTest, currencies, materials);

            // assert
            result.ShouldBe(Program.ERROR_MESSAGE);
        }
        public void ShouldReturnErrorMessageWhenNoValidRoman()
        {
            // arrange
            var textToTest = "curr material is num credits";
            var materials = new Dictionary<string, float>();
            var currencies = new Dictionary<string, string> { { "curr", "Z" } };
            var rule = new MaterialAssignmentParsingRule();

            // act
            var result = rule.Process(textToTest, currencies, materials);

            // assert
            result.ShouldBe(Program.ERROR_MESSAGE);
        }
        public void ShouldReturnErrorMessageWhenInvalidNumberOfCredits()
        {
            // arrange
            var textToTest = "curr material is num credits";
            var materials = new Dictionary<string, float>();
            var currencies = new Dictionary<string, string> { { "curr", "X" } };
            var rule = new MaterialAssignmentParsingRule();

            // act
            var result = rule.Process(textToTest, currencies, materials);

            // assert
            result.ShouldBe(Program.ERROR_MESSAGE);
        }
        public void ShouldReturnErrorMessageWhenZeroCredits()
        {
            // arrange
            var textToTest = "curr material is 0 credits";
            var materials = new Dictionary<string, float>();
            var currencies = new Dictionary<string, string> { { "curr", "X" } };
            var rule = new MaterialAssignmentParsingRule();

            // act
            var result = rule.Process(textToTest, currencies, materials);

            // assert
            result.ShouldBe(Program.ERROR_MESSAGE);
        }

        public void ShouldReturnEmptyStringAndHaveMaterialsUpdated()
        {
            // arrange
            var textToTest = "curr material is 500 credits";
            var materials = new Dictionary<string, float>();
            var currencies = new Dictionary<string, string> { { "curr", "X" } };
            var rule = new MaterialAssignmentParsingRule();

            // act
            var result = rule.Process(textToTest, currencies, materials);

            // assert
            result.ShouldBe(string.Empty);
            materials.ShouldContainKeyAndValue("material", 50f);
        }
    }
}
