using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummarizeWebPage
{
    public class KeyProvider
    {
        private const string path = "key.txt";

        public static string GetKey()
        {
            if (File.Exists(path))
            {
                return File.ReadAllText(path);
            }
            else
            {
                Console.WriteLine("But first, please enter your OpenAi api key:");
                string key = Console.ReadLine()!;
                File.WriteAllText(path, key);
                return key;
            }
        }

        public static void DeleteStoredKey()
        {
            if (File.Exists(path))
                File.Delete(path);
        }
    }
}
