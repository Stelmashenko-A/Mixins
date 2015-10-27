using System;

namespace Mixins.Exceptions
{
    public class DuplicateBindingsException : ApplicationException
    {
        public DuplicateBindingsException(string message) : base(message)
        {
        }
    }
}