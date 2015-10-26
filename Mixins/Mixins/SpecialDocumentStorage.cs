using System;

namespace Mixins
{
    [TargetType(typeof(SpecialDocument))]
    public class SpecialDocumentStorage : IDocumentStorage
    {
        public void Store(Document doc)
        {
            Console.WriteLine("SpecialDocumentStorage store {0}", doc.Id);
        }
    }
}