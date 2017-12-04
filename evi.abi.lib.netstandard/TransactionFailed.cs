using System;
using System.Runtime.Serialization;

namespace eVi.abi.lib.pcl
{
    [Serializable]
    public class TransactionFailed : Exception
    {
        public TransactionFailed()
        {
        }

        public TransactionFailed(string message) : base(message)
        {
        }

        public TransactionFailed(Exception innerException) : base(null, innerException)
        {
        }

        public TransactionFailed(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected TransactionFailed(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}