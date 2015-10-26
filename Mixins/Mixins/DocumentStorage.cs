using System;

namespace Mixins
{
    [TargetType(typeof (Document))]

    //[TargetType(typeof(SpecialDocument))]
    public class DocumentStorage : IDocumentStorage
    {
        public void Store(Document doc)
        {
            Console.WriteLine("DocumentStorage store {0}", doc.Id);
        }
    }
}