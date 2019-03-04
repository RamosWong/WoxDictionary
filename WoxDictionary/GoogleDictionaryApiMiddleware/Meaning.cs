using System;
using System.Collections.Generic;
using System.Text;

namespace GoogleDictionaryApiMiddleware
{
    public class Meaning
    {
        public string PartOfSpeech;
        public string Definition;
        public string Example;
        public List<string> Synonyms;

        public Meaning(string partOfSpeech, string definition, string example, List<string> synonyms)
        {
            PartOfSpeech = partOfSpeech;
            Definition = definition;
            Example = example;
            Synonyms = synonyms;
        }
    }
}
