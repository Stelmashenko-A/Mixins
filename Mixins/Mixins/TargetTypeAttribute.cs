using System;

namespace Mixins
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]

    public class TargetTypeAttribute : Attribute
    {
        public Type BoundClass { get; private set; }

        public TargetTypeAttribute(Type boundClass)
        {
            if (boundClass != typeof (Document) && !boundClass.IsSubclassOf(typeof (Document)))
                throw new ApplicationException("Bound class must be subclass of Document");
            BoundClass = boundClass;
        }
    }
}