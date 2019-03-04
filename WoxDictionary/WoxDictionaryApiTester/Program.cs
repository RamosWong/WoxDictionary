using System;
using System.Collections.Generic;
using GoogleDictionaryApiMiddleware;

namespace WoxDictionaryApiTester
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Enter word to define >> ");
                var word = Console.ReadLine();

                List<Meaning> definitions = DefineWord(word);
                foreach (var meaning in definitions)
                {
                    Console.WriteLine(meaning.PartOfSpeech);
                    Console.WriteLine(string.Format("Definition: {0}", meaning.Definition));
                    Console.WriteLine(string.Format("Example: {0}", meaning.Example));

                    // synonyms might be null
                    if (meaning.Synonyms != null)
                        Console.WriteLine(string.Format("Synonyms: {0}", string.Join(", ", meaning.Synonyms)));

                    Console.WriteLine("\r\n");
                }
            }            
        }

        private static List<Meaning> DefineWord(string word)
        {
            DictionaryMiddleware dictionary = new DictionaryMiddleware();
            var definitions = dictionary.ProcessDefinition(word);
            List<Meaning> meanings = new List<Meaning>();

            foreach (var definition in definitions)
            {
                meanings.AddRange(dictionary.ProcessMeaning(definition.Meaning));
            }

            return meanings;
        }
    }
}
