using System;
using System.Runtime.Serialization;

namespace RomanConverter
{
    [Serializable]
    public class InvalidRomanNumeralMultiplierException : Exception
    {
        public InvalidRomanNumeralMultiplierException()
        {
        }

        public InvalidRomanNumeralMultiplierException(string message) : base(message)
        {
        }

        public InvalidRomanNumeralMultiplierException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidRomanNumeralMultiplierException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}