using System;

namespace TempustScript.InterpreterException
{
    [Serializable]
    public class InvalidBlockException : Exception
    {
        public InvalidBlockException() : base() { }
        public InvalidBlockException(string message) : base(message) { }
        public InvalidBlockException(string message, Exception inner) : base(message, inner) { }
        protected InvalidBlockException(System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
