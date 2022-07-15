using System;

namespace TempustScript.InterpreterException
{
    [Serializable]
    public class InvalidRegionException : Exception
    {
        public InvalidRegionException() : base() { }
        public InvalidRegionException(string message) : base(message) {}
        public InvalidRegionException(string message, Exception inner) : base(message, inner) { }
        protected InvalidRegionException(System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
