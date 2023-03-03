using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace Application.Common.Exceptions
{
    [ExcludeFromCodeCoverage]
    public class NotSortableFieldException: Exception
    {
        public NotSortableFieldException()
        {
        }

        protected NotSortableFieldException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public NotSortableFieldException(string? message) : base(message)
        {
        }

        public NotSortableFieldException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}