using System;

namespace Mixins
{
    internal class Program
    {
        private static void Main()
        {
            var doc = new Document();
            Document spDoc = new SpecialDocument();
            doc.Store();
            spDoc.Store();
            Console.ReadLine();
        }
    }
}
