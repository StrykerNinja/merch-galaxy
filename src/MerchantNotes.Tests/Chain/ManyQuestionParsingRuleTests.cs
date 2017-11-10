namespace MerchantNotes.Tests.Chain
{
    using MerchantNotes.Chain;
    using Shouldly;
    using System.Collections.Generic;

    public class ManyQuestionParsingRuleTests
    {
        public void ShouldNotHandleWhenTextIsNull()
        {
            // arrange
            string textToTest = null;
            var rule = new ManyQuestionParsingRule();

            // act
            var result = rule.Handles(textToTest);

            // assert
            result.ShouldBe(false);
        }
        public void ShouldNotHandleWhenTextIsEmpty()
        {
            // arrange
            var textToTest = string.Empty;
            var rule = new ManyQuestionParsingRule();

            // act
            var result = rule.Handles(textToTest);

            // assert
            result.ShouldBe(false);
        }
        public void ShouldNotHandleWhenTextIsWhitespace()
        {
            // arrange
            var textToTest = " ";
            var rule = new ManyQuestionParsingRule();

            // act
            var result = rule.Handles(textToTest);

            // assert
            result.ShouldBe(false);
        }
        public void ShouldNotHandleWhenTextDoesNotEndWithQuestionMark()
        {
            // arrange
            var textToTest = "doesnotendwithquestionmark";
            var rule = new ManyQuestionParsingRule();

            // act
            var result = rule.Handles(textToTest);

            // assert
            result.ShouldBe(false);
        }
        public void ShouldNotHandleWhenTextDoesNotStartWithHowManyCreditsIs()
        {
            // arrange
            var textToTest = "not how many credits is ";
            var rule = new ManyQuestionParsingRule();

            // act
            var result = rule.Handles(textToTest);

            // assert
            result.ShouldBe(false);
        }

        public void ShouldHandleWhenTextHasQuestionMarkAndStartsWithHowManyCreditsIs()
        {
            // arrange
            var textToTest = "how many credits is ?";
            var rule = new ManyQuestionParsingRule();

            // act
            var result = rule.Handles(textToTest);

            // assert
            result.ShouldBe(true);
        }

        public void ShouldReturnErrorMessageWhenNoValidMaterial()
        {
            // arrange
            var textToTest = "how many credits is nope?";
            var materials = new Dictionary<string, float> { { "material", 12.5f } };
            var rule = new ManyQuestionParsingRule();

            // act
            var result = rule.Process(textToTest, null, materials);

            // assert
            result.ShouldBe(Program.ERROR_MESSAGE);
        }
        public void ShouldReturnErrorMessageWhenNoValidCurrency()
        {
            // arrange
            var textToTest = "how many credits is nope material?";
            var materials = new Dictionary<string, float> { { "material", 12.5f } };
            var currencies = new Dictionary<string, string> { { "curr", "Z" } };
            var rule = new ManyQuestionParsingRule();

            // act
            var result = rule.Process(textToTest, currencies, materials);

            // assert
            result.ShouldBe(Program.ERROR_MESSAGE);
        }

        public void ShouldReturnCorrectMessageWhenValidMaterialAndValidCurrency()
        {
            // arrange
            var textToTest = "how many credits is curr material?";
            var materials = new Dictionary<string, float> { { "material", 12.5f } };
            var currencies = new Dictionary<string, string> { { "curr", "X" } };
            var rule = new ManyQuestionParsingRule();
            var expected = "curr material is 125 Credits";

            // act
            var result = rule.Process(textToTest, currencies, materials);

            // assert
            result.ShouldBe(expected);
        }
    }
}
