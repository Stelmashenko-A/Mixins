namespace Mixins
{
    public static class DocumentMixin
    {
        public static void Store(this Document doc)
        {
            MixinInitializer<IDocumentStorageMixin>.GetItemFor(doc.GetType()).Store(doc);
        }
    }
}