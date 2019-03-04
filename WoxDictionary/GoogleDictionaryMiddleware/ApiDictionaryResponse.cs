using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoogleDictionaryApiMiddleware
{
    public class ApiDictionaryResponse
    {
        [JsonProperty("word")]
        public string Word { get; set; }

        [JsonProperty("phonetic")]
        public string Phonetic { get; set; }

        [JsonProperty("meaning")]
        public PartsOfSpeech Meaning { get; set; }
    }

    public class PartsOfSpeech
    {
        [JsonProperty("noun")]
        public List<PartsOfSpeechComponents> Noun { get; set; }

        [JsonProperty("verb")]
        public List<PartsOfSpeechComponents> Verb { get; set; }

        [JsonProperty("pronoun")]
        public List<PartsOfSpeechComponents> Pronoun { get; set; }

        [JsonProperty("adjective")]
        public List<PartsOfSpeechComponents> Adjective { get; set; }

        [JsonProperty("preposition")]
        public List<PartsOfSpeechComponents> Preposition { get; set; }

        [JsonProperty("exclamation")]
        public List<PartsOfSpeechComponents> Exclamation { get; set; }
    }

    public class PartsOfSpeechComponents
    {
        [JsonProperty("definition")]
        public string Definition { get; set; }

        [JsonProperty("example")]
        public string Example { get; set; }

        [JsonProperty("synonyms")]
        public List<string> Synonyms { get; set; }
    }
}