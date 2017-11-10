using System;
using System.Runtime.Serialization;

namespace RomanConverter
{
    [Serializable]
    public class InvalidRomanNumeralSubtractorException : Exception
    {
        public InvalidRomanNumeralSubtractorException()
        {
        }

        public InvalidRomanNumeralSubtractorException(string message) : base(message)
        {
        }

        public InvalidRomanNumeralSubtractorException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidRomanNumeralSubtractorException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}