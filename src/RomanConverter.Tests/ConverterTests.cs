namespace RomanConverter.Tests
{
    using Shouldly;
    using System;

    public class ConverterTests
    {
        public void ShouldThrowExceptionWhenTryingToPassNull()
        {
            // arrange
            var converter = new Converter();

            // assert
            Should.Throw<ArgumentException>(() => converter.GetIntFromRoman(null));
        }
        public void ShouldThrowExceptionWhenTryingToPassEmptyString()
        {
            // arrange
            var converter = new Converter();

            // assert
            Should.Throw<ArgumentException>(() => converter.GetIntFromRoman(""));
        }
        public void ShouldThrowExceptionWhenTryingToPassWhitespace()
        {
            // arrange
            var converter = new Converter();

            // assert
            Should.Throw<ArgumentException>(() => converter.GetIntFromRoman(" "));
        }

        public void ShouldThrowExceptionWhenTryingToPassInvalidCharacter()
        {
            // arrange
            var converter = new Converter();

            // assert
            Should.Throw<InvalidRomanNumeralCharacterException>(() => converter.GetIntFromRoman("A"));
        }

        public void ShouldReturnOne()
        {
            // arrange
            var converter = new Converter();

            // act
            var result = converter.GetIntFromRoman("I");

            // assert
            result.ShouldBe(1);
        }
        public void ShouldReturnFive()
        {
            // arrange
            var converter = new Converter();

            // act
            var result = converter.GetIntFromRoman("V");

            // assert
            result.ShouldBe(5);
        }
        public void ShouldReturnTen()
        {
            // arrange
            var converter = new Converter();

            // act
            var result = converter.GetIntFromRoman("X");

            // assert
            result.ShouldBe(10);
        }
        public void ShouldReturnFifty()
        {
            // arrange
            var converter = new Converter();

            // act
            var result = converter.GetIntFromRoman("L");

            // assert
            result.ShouldBe(50);
        }
        public void ShouldReturnOneHundred()
        {
            // arrange
            var converter = new Converter();

            // act
            var result = converter.GetIntFromRoman("C");

            // assert
            result.ShouldBe(100);
        }
        public void ShouldReturnFiveHundred()
        {
            // arrange
            var converter = new Converter();

            // act
            var result = converter.GetIntFromRoman("D");

            // assert
            result.ShouldBe(500);
        }
        public void ShouldReturnOneThousand()
        {
            // arrange
            var converter = new Converter();

            // act
            var result = converter.GetIntFromRoman("M");

            // assert
            result.ShouldBe(1000);
        }

        public void ShouldReturnThree()
        {
            // arrange
            var converter = new Converter();

            // act
            var result = converter.GetIntFromRoman("III");

            // assert
            result.ShouldBe(3);
        }
        public void ShouldReturnThirty()
        {
            // arrange
            var converter = new Converter();

            // act
            var result = converter.GetIntFromRoman("XXX");

            // assert
            result.ShouldBe(30);
        }
        public void ShouldReturnThreeHundred()
        {
            // arrange
            var converter = new Converter();

            // act
            var result = converter.GetIntFromRoman("CCC");

            // assert
            result.ShouldBe(300);
        }
        public void ShouldReturnThreeThousand()
        {
            // arrange
            var converter = new Converter();

            // act
            var result = converter.GetIntFromRoman("MMM");

            // assert
            result.ShouldBe(3000);
        }
        
        public void ShouldReturnFour()
        {
            // arrange
            var converter = new Converter();

            // act
            var result = converter.GetIntFromRoman("IV");

            // assert
            result.ShouldBe(4);
        }
        public void ShouldReturnNine()
        {
            // arrange
            var converter = new Converter();

            // act
            var result = converter.GetIntFromRoman("IX");

            // assert
            result.ShouldBe(9);
        }
        public void ShouldThrowExceptionWhenTryingToSubtractOneFromFifty()
        {
            // arrange
            var converter = new Converter();

            // assert
            Should.Throw<InvalidRomanNumeralSubtractorException>(() => converter.GetIntFromRoman("IL"));
        }
        public void ShouldThrowExceptionWhenTryingToSubtractOneFromOneHundred()
        {
            // arrange
            var converter = new Converter();

            // assert
            Should.Throw<InvalidRomanNumeralSubtractorException>(() => converter.GetIntFromRoman("IC"));
        }
        public void ShouldThrowExceptionWhenTryingToSubtractOneFromFiveHundred()
        {
            // arrange
            var converter = new Converter();

            // assert
            Should.Throw<InvalidRomanNumeralSubtractorException>(() => converter.GetIntFromRoman("ID"));
        }
        public void ShouldThrowExceptionWhenTryingToSubtractOneFromOneThousand()
        {
            // arrange
            var converter = new Converter();

            // assert
            Should.Throw<InvalidRomanNumeralSubtractorException>(() => converter.GetIntFromRoman("IM"));
        }

        public void ShouldReturnFourty()
        {
            // arrange
            var converter = new Converter();

            // act
            var result = converter.GetIntFromRoman("XL");

            // assert
            result.ShouldBe(40);
        }
        public void ShouldReturnNinety()
        {
            // arrange
            var converter = new Converter();

            // act
            var result = converter.GetIntFromRoman("XC");

            // assert
            result.ShouldBe(90);
        }
        public void ShouldThrowExceptionWhenTryingToSubtractTenFromFiveHundred()
        {
            // arrange
            var converter = new Converter();

            // assert
            Should.Throw<InvalidRomanNumeralSubtractorException>(() => converter.GetIntFromRoman("XD"));
        }
        
        public void ShouldReturnFourHundred()
        {
            // arrange
            var converter = new Converter();

            // act
            var result = converter.GetIntFromRoman("CD");

            // assert
            result.ShouldBe(400);
        }
        public void ShouldReturnNineHundred()
        {
            // arrange
            var converter = new Converter();

            // act
            var result = converter.GetIntFromRoman("CM");

            // assert
            result.ShouldBe(900);
        }

        public void ShouldThrowExceptionWhenTryingToRepeatFive()
        {
            // arrange
            var converter = new Converter();

            // assert
            Should.Throw<InvalidRomanNumeralMultiplierException>(() => converter.GetIntFromRoman("VV"));
        }
        public void ShouldThrowExceptionWhenTryingToRepeatFifty()
        {
            // arrange
            var converter = new Converter();

            // assert
            Should.Throw<InvalidRomanNumeralMultiplierException>(() => converter.GetIntFromRoman("LL"));
        }
        public void ShouldThrowExceptionWhenTryingToRepeatFiveHundred()
        {
            // arrange
            var converter = new Converter();

            // assert
            Should.Throw<InvalidRomanNumeralMultiplierException>(() => converter.GetIntFromRoman("DD"));
        }

        public void ShouldThrowExceptionWhenTryingToSubtractFive()
        {
            // arrange
            var converter = new Converter();

            // assert
            Should.Throw<InvalidRomanNumeralSubtractorException>(() => converter.GetIntFromRoman("VX"));
        }
        public void ShouldThrowExceptionWhenTryingToSubtractFifty()
        {
            // arrange
            var converter = new Converter();

            // assert
            Should.Throw<InvalidRomanNumeralSubtractorException>(() => converter.GetIntFromRoman("LC"));
        }
        public void ShouldThrowExceptionWhenTryingToSubtractFiveHundred()
        {
            // arrange
            var converter = new Converter();

            // assert
            Should.Throw<InvalidRomanNumeralSubtractorException>(() => converter.GetIntFromRoman("DM"));
        }

        public void ShouldThrowExceptionWhenTryingToSubtractTooManyOnes()
        {
            // arrange
            var converter = new Converter();

            // assert
            Should.Throw<TooManySmallRomanSubtractorsInARowException>(() => converter.GetIntFromRoman("IIV"));
        }
        public void ShouldThrowExceptionWhenTryingToSubtractTooManyTens()
        {
            // arrange
            var converter = new Converter();

            // assert
            Should.Throw<TooManySmallRomanSubtractorsInARowException>(() => converter.GetIntFromRoman("XXL"));
        }
        public void ShouldThrowExceptionWhenTryingToSubtractTooManyOneHundreds()
        {
            // arrange
            var converter = new Converter();

            // assert
            Should.Throw<TooManySmallRomanSubtractorsInARowException>(() => converter.GetIntFromRoman("CCD"));
        }

        public void ShouldThrowExceptionWhenTryingTooManyOnes()
        {
            // arrange
            var converter = new Converter();

            // assert
            Should.Throw<TooManyRomanNumeralsInARowException>(() => converter.GetIntFromRoman("IIII"));
        }
        public void ShouldThrowExceptionWhenTryingTooManyTens()
        {
            // arrange
            var converter = new Converter();

            // assert
            Should.Throw<TooManyRomanNumeralsInARowException>(() => converter.GetIntFromRoman("XXXX"));
        }
        public void ShouldThrowExceptionWhenTryingTooManyOneHundreds()
        {
            // arrange
            var converter = new Converter();

            // assert
            Should.Throw<TooManyRomanNumeralsInARowException>(() => converter.GetIntFromRoman("CCCC"));
        }

        public void ShouldReturnNineteenHundredThree()
        {
            // arrange
            var converter = new Converter();

            // act
            var result = converter.GetIntFromRoman("MCMIII");

            // assert
            result.ShouldBe(1903);
        }

        public void ShouldReturnNineteenHundredFiftyTwo()
        {
            // arrange
            var converter = new Converter();

            // act
            var result = converter.GetIntFromRoman("MCMLII");

            // assert
            result.ShouldBe(1952);
        }

        public void ShouldReturnNineteenHundredSeventyEight()
        {
            // arrange
            var converter = new Converter();

            // act
            var result = converter.GetIntFromRoman("MCMLXXVIII");

            // assert
            result.ShouldBe(1978);
        }

        public void ShouldReturnTheAnswerToTheUltimateQuestionOfLifeTheUniverseAndEverything()
        {
            // arrange
            var converter = new Converter();

            // act
            var result = converter.GetIntFromRoman("XLII");

            // assert
            result.ShouldBe(42);
        }
    }
}
