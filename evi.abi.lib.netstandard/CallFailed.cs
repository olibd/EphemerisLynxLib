using System;
using System.Runtime.Serialization;

namespace eVi.abi.lib.pcl
{
    [Serializable]
    public class CallFailed : Exception
    {
        public CallFailed()
        {
        }

        public CallFailed(string message) : base(message)
        {
        }

        public CallFailed(Exception innerException) : base(null, innerException)
        {
        }

        public CallFailed(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CallFailed(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}