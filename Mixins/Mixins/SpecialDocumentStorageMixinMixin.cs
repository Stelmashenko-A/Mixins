using System;
using Raven.Client;
using Raven.Client.Document;

namespace Mixins
{
    [TargetType(typeof(SpecialDocument))]
    public class SpecialDocumentStorageMixinMixin : IDocumentStorageMixin
    {
        public void Store(Document doc)
        {
            using (IDocumentStore store = new DocumentStore
            {
                Url = "http://localhost:8081/", // server URL
                DefaultDatabase = "Mixins"   // default database
            })
            {
                store.Initialize(); 

                using (IDocumentSession session = store.OpenSession()) 
                {
                    session.Store(doc); 
                    session.SaveChanges(); 
                }
                Console.WriteLine("stored");
            }
        }
    }
}