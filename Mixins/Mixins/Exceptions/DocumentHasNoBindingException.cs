using System;

namespace Mixins.Exceptions
{
    public class DocumentHasNoBindingException : ApplicationException
    {
        public DocumentHasNoBindingException(string message) : base(message)
        {
        }
    }
}