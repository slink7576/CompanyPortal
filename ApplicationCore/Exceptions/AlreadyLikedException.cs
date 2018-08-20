using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ApplicationCore.Exceptions
{
   public class AlreadyLikedException : Exception
   {
        public AlreadyLikedException(int employeeId) : base($"User with id {employeeId} already liked this post!")
        {
        }

        protected AlreadyLikedException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context)
        {
        }

        public AlreadyLikedException(string message) : base(message)
        {
        }

        public AlreadyLikedException(string message, Exception innerException) : base(message, innerException)
        {
        }
   }
}