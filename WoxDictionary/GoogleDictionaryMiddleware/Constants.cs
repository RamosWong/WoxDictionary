using System;
using System.Collections.Generic;
using System.Text;

namespace GoogleDictionaryApiMiddleware
{
    public class Constants
    {
        public class ActionUrls
        {
            public class Common
            {
                public const string DictionaryApiUrl = "https://mydictionaryapi.appspot.com/";
            }

            public class UrlParamKeys
            {
                public const string Define = "define";
                public const string Language = "lang";
            }
        }

        public class LanguageOptions
        {
            public const string English = "en";
            public const string Hindi = "hi";
            public const string Spanish = "es";
            public const string French = "fr";
            public const string Russian = "ru";
            public const string German = "de";
            public const string Italian = "it";
            public const string Korean = "ko";
            public const string BrazilianPortuguese = "pt-BR";
            public const string SimplifiedChinese = "zh-CN";
            public const string Arabic = "ar";
            public const string Turkish = "tr";
        }

        public class DictionaryResponse
        {
            public class PartsOfSpeech
            {
                public const string Noun = "Noun";
                public const string Verb = "Verb";
                public const string Pronoun = "Pronoun";
                public const string Adjective = "Adjective";
                public const string Preposition = "Preposition";
                public const string Exclamation = "Exclamation";
            }
        }
        
    }
}
