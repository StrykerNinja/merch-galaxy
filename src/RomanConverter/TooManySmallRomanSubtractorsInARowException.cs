using System;
using System.Runtime.Serialization;

namespace RomanConverter
{
    [Serializable]
    public class TooManySmallRomanSubtractorsInARowException : Exception
    {
        public TooManySmallRomanSubtractorsInARowException()
        {
        }

        public TooManySmallRomanSubtractorsInARowException(string message) : base(message)
        {
        }

        public TooManySmallRomanSubtractorsInARowException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected TooManySmallRomanSubtractorsInARowException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}