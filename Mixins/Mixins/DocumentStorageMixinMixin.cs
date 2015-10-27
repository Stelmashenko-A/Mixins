using System;

namespace Mixins
{
    [TargetType(typeof (Document))]

    public class DocumentStorageMixinMixin : IDocumentStorageMixin
    {
        public void Store(Document doc)
        {
            Console.WriteLine("DocumentStorageMixinMixin store {0}", doc.Id);
        }
    }
}