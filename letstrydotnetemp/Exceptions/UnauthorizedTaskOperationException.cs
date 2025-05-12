using System;

namespace letstrydotnetemp.Exceptions
{
    public class UnauthorizedTaskOperationException : Exception
    {
        public UnauthorizedTaskOperationException() : base() { }

        public UnauthorizedTaskOperationException(string message) : base(message) { }

        public UnauthorizedTaskOperationException(string message, Exception innerException) 
            : base(message, innerException) { }

        public UnauthorizedTaskOperationException(Guid taskId, Guid userId) 
            : base($"User {userId} is not authorized to perform operations on task {taskId}.") { }
    }
} 