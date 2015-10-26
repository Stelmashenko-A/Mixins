using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Mixins
{
    public static class DocumentMixin<T>
    {
        private static readonly Dictionary<Type, Func<T>> Factories;

        private static readonly Exception InitException;

        private static readonly List<Type> ObjectsWithDuplicateBindings = new List<Type>();

        static DocumentMixin()
        {
            try
            {
                var allTypes = Assembly.GetExecutingAssembly()
                    .GetTypes();

                var s = allTypes.Select(
                    t =>
                        new
                        {
                            Type = t,
                            Attributes =
                                t.GetCustomAttributes(typeof (TargetTypeAttribute), true).ToArray()
                        }).ToArray();

                var pFacts = s.Where(obj => obj.Attributes.Length != 0).ToArray();
                
                var list = new Dictionary<object, Type>();
                foreach (var variable in pFacts)
                {
                    foreach (var item in variable.Attributes)
                    {
                        if (!list.ContainsKey(item))
                        {
                            list.Add(item, variable.Type);
                        }

                        if (list[item] != variable.Type)
                        {
                            ObjectsWithDuplicateBindings.Add(((TargetTypeAttribute) item).BoundClass);
                        }
                    }
                }

                Factories = new Dictionary<Type, Func<T>>();
                foreach (var type in list)
                {

                    Factories.Add(((TargetTypeAttribute) type.Key).BoundClass,
                        () => (T) Activator.CreateInstance(type.Value));
                }
            }
            catch (Exception ex)
            {
                InitException = ex;

            }
        }

        public static T GetItemFor(Type docType)
        {
            if (InitException != null)
                throw new ApplicationException(
                    string.Format("DocumentMixin exception: {0}", InitException.Message), InitException);
            if (!Factories.ContainsKey(docType))
            {
                var tmp = Factories.Keys.First(x => docType.IsSubclassOf(x));
                if (tmp == null)
                {
                    throw new ApplicationException(
                        string.Format("DocumentMixin exception: {0}have no binding", docType.FullName));
                }
                return Factories[tmp].Invoke();
            }
            return Factories[docType].Invoke();
        }
    }

    public static class DocumentMixin
    {
        public static void Store(this Document doc)
        {
            DocumentMixin<IDocumentStorage>.GetItemFor(doc.GetType()).Store(doc);
        }
    }
}