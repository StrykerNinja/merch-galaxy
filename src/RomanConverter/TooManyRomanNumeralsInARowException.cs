using System;
using System.Runtime.Serialization;

namespace RomanConverter
{
    [Serializable]
    public class TooManyRomanNumeralsInARowException : Exception
    {
        public TooManyRomanNumeralsInARowException()
        {
        }

        public TooManyRomanNumeralsInARowException(string message) : base(message)
        {
        }

        public TooManyRomanNumeralsInARowException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected TooManyRomanNumeralsInARowException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}