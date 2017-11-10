using System;
using System.Runtime.Serialization;

namespace RomanConverter
{
    [Serializable]
    public class InvalidRomanNumeralCharacterException : Exception
    {
        public InvalidRomanNumeralCharacterException()
        {
        }

        public InvalidRomanNumeralCharacterException(string message) : base(message)
        {
        }

        public InvalidRomanNumeralCharacterException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidRomanNumeralCharacterException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}