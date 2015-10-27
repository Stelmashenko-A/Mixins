using System;

namespace Mixins
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]

    public class TargetTypeAttribute : Attribute
    {
        public Type BoundClass { get; private set; }

        public TargetTypeAttribute(Type boundClass)
        {
            BoundClass = boundClass;
        }
    }
}