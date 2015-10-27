using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Mixins.Exceptions;

namespace Mixins
{
    public static class MixinInitializer<T>
    {
        private static readonly Dictionary<Type, Func<T>> Factories;

        private static Exception InitException;

        private static readonly List<Type> ObjectsWithDuplicateBindings = new List<Type>();

        static MixinInitializer()
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

                CheckObjectsWithDuplicateBindings();
                ObjectsWithDuplicateBindings.Clear();
            }
            catch (Exception ex)
            {
                InitException = ex;

            }
        }

        public static T GetItemFor(Type docType)
        {
            if (InitException != null)
            {
                throw InitException;
            }
            if (Factories.ContainsKey(docType)) return Factories[docType].Invoke();

            var tmp = Factories.Keys.First(docType.IsSubclassOf);
            if (tmp == null)
            {
                throw new DocumentHasNoBindingException(docType.FullName);
            }
            return Factories[tmp].Invoke();
        }

        private static void CheckObjectsWithDuplicateBindings()
        {
            if (ObjectsWithDuplicateBindings.Count == 0) return;
            var sb = new StringBuilder();
            foreach (var variable in ObjectsWithDuplicateBindings)
            {
                sb.Append(variable.Name + " ");
            }
            InitException = new DuplicateBindingsException(sb.ToString());
        }
    }
}