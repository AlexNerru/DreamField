using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamField.ServiceLayer.Exceptions
{
    /// <summary>
    /// Thrown if it was wrong parameters for ration creation
    /// </summary>
    [Serializable]
    public class RationCreationException : Exception
    {
        public RationCreationException() { }
        public RationCreationException(string message) : base(message) { }
        public RationCreationException(string message, Exception inner) : base(message, inner) { }
        protected RationCreationException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }

    /// <summary>
    /// Thrown if ration was not found in database
    /// </summary>
    [Serializable]
    public class RationNotFoundException : Exception
    {
        public RationNotFoundException() { }
        public RationNotFoundException(string message) : base(message) { }
        public RationNotFoundException(string message, Exception inner) : base(message, inner) { }
        protected RationNotFoundException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
