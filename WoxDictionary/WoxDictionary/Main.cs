using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Wox.Plugin;
using NHunspell;
using Syn.WordNet;

using System.IO;
using System.Reflection;
using GoogleDictionaryApiMiddleware;

namespace WoxDictionary
{
    public class Main : IPlugin
    {
        Hunspell hunspell;
        // WordNetEngine wordNet;
        
        DictionaryMiddleware dictionaryMiddleware;
        List<Meaning> meanings;

        public void Init(PluginInitContext context)
        {
            // where the DLL files will be when the plugin is loaded
            String assemblyFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            // what to do for startup
            hunspell = new Hunspell();
            hunspell.Load(assemblyFolder + "/en_GB.aff", assemblyFolder + "/en_GB.dic");
            /*
            wordNet = new WordNetEngine();
            wordNet.LoadFromDirectory(assemblyFolder + "/wordNet");
            */            
        }

        public List<Result> Query(Query query)
        {
            List<Result> results = new List<Result>();
            dictionaryMiddleware = new DictionaryMiddleware();
            meanings = new List<Meaning>();

            string word = query.Search;
            string result = string.Empty;

            string subtitle = string.Empty;
            // what to do when user invokes
            bool correctSpelling = hunspell.Spell(word);
           
            if (!correctSpelling)
            {
                subtitle = string.Format("Could not find any results\r\nDid you mean: {0}", hunspell.Suggest(query.Search)[0]);
            }
            else
            {
                var definitions = dictionaryMiddleware.ProcessDefinition(word);                

                foreach (var definition in definitions)
                {
                    meanings.AddRange(dictionaryMiddleware.ProcessMeaning(definition.Meaning));
                }
            }
            /*
            if (correctSpelling)
            {
                var synSetList = wordNet.GetSynSets(word);
                var isDefined = false;
                var thesLabelIsPrinted = false;

                foreach (var synSet in synSetList)
                {
                    if (!isDefined)
                    {
                        result += "Definition: ";
                        result += synSet.Gloss;
                        isDefined = true;
                    }

                    if (!thesLabelIsPrinted)
                    {
                        result += "\r\nThesaurus: ";
                        thesLabelIsPrinted = true;
                    }
                    
                }

                foreach (var synSet in synSetList)
                {
                    var synonyms = string.Join(", ", synSet.Words);

                    result += synonyms;
                }
                }
            */            


            if (!correctSpelling)
            {
                results.Add(new Result()
                {
                    Title = query.Search,
                    SubTitle = subtitle,
                    IcoPath = "images\\dict.png",  //relative path to your plugin directory
                    Action = e =>
                    {
                        // after user select the item

                        // return false to tell Wox don't hide query window, otherwise Wox will hide it automatically
                        return false;
                    }
                });
            }
            else
            {
                foreach (var meaning in meanings)
                {                    
                    if (meaning.Example != null && meaning.Synonyms != null)
                    {
                        subtitle = string.Format("Examples: {0}\r\nSynonyms: {1}", meaning.Example, string.Join(", ", meaning.Synonyms));
                    }
                    else if (meaning.Example != null)
                    {
                        subtitle = string.Format("Examples: {0}", meaning.Example);
                    }

                    results.Add(new Result()
                    {
                        Title = string.Format("{0}: {1}", meaning.PartOfSpeech, meaning.Definition),
                        SubTitle = subtitle,
                        IcoPath = "images\\dict.png",  //relative path to your plugin directory
                        Action = e =>
                        {
                            // after user select the item

                            // return false to tell Wox don't hide query window, otherwise Wox will hide it automatically
                            return false;
                        }
                    });
                }
            }
            
            return results;
        }
    }
}
