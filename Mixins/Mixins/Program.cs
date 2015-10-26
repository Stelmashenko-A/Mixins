using System;

namespace Mixins
{
    internal class Program
    {
        private static void Main()
        {
            try
            {
                var doc = new Document();
                Document spDoc = new SpecialDocument();
                doc.Store();
                spDoc.Store();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();
        }
    }
}
