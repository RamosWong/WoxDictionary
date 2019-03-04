using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Newtonsoft.Json;

namespace GoogleDictionaryApiMiddleware
{
    public class DictionaryMiddleware
    {
        private string _dictionaryApiUrl = "";
        private string language = "";

        public DictionaryMiddleware()
        {
            Initialise(Constants.ActionUrls.Common.DictionaryApiUrl, Constants.LanguageOptions.English);
        }

        public DictionaryMiddleware(string baseUrl)
        {
            Initialise(baseUrl, Constants.LanguageOptions.English);
        }

        public DictionaryMiddleware(string baseUrl, string language)
        {
            Initialise(baseUrl, language);
        }

        public void Initialise(string baseUrl, string language)
        {
            _dictionaryApiUrl = baseUrl;
            this.language = language;
        }

        public List<ApiDictionaryResponse> ProcessDefinition(string word)
        {
            var requestUrl = string.Format("{0}?{1}={2}&{3}={4}",
                _dictionaryApiUrl,
                Constants.ActionUrls.UrlParamKeys.Define, word,
                Constants.ActionUrls.UrlParamKeys.Language, language);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestUrl);
            request.AutomaticDecompression = DecompressionMethods.GZip;
            string jsonResult = string.Empty;

            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    jsonResult = reader.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                // unable to find definition
                return new List<ApiDictionaryResponse>();
            }

            return JsonConvert.DeserializeObject<List<ApiDictionaryResponse>>(jsonResult);
        }       

        public List<Meaning> ProcessMeaning(PartsOfSpeech meaning)
        {
            var results = new List<Meaning>();
            var meaningsObj = new List<Meaning>();

            if (meaning.Noun != null)
            {
                foreach(var component in meaning.Noun)
                {
                    meaningsObj.Add(new Meaning(
                        Constants.DictionaryResponse.PartsOfSpeech.Noun,
                        component.Definition,
                        component.Example,
                        component.Synonyms));
                }
            }

            if (meaning.Pronoun != null)
            {
                foreach (var component in meaning.Pronoun)
                {
                    meaningsObj.Add(new Meaning(
                        Constants.DictionaryResponse.PartsOfSpeech.Pronoun,
                        component.Definition,
                        component.Example,
                        component.Synonyms));
                }
            }

            if (meaning.Verb != null)
            {
                foreach (var component in meaning.Verb)
                {
                    meaningsObj.Add(new Meaning(
                        Constants.DictionaryResponse.PartsOfSpeech.Verb,
                        component.Definition,
                        component.Example,
                        component.Synonyms));
                }
            }

            if (meaning.Adjective != null)
            {
                foreach (var component in meaning.Adjective)
                {
                    meaningsObj.Add(new Meaning(
                        Constants.DictionaryResponse.PartsOfSpeech.Adjective,
                        component.Definition,
                        component.Example,
                        component.Synonyms));
                }

            }

            if (meaning.Preposition != null)
            {
                foreach (var component in meaning.Preposition)
                {
                    meaningsObj.Add(new Meaning(
                        Constants.DictionaryResponse.PartsOfSpeech.Preposition,
                        component.Definition,
                        component.Example,
                        component.Synonyms));
                }
            }

            if (meaning.Exclamation != null)
            {
                foreach (var component in meaning.Exclamation)
                {
                    meaningsObj.Add(new Meaning(
                        Constants.DictionaryResponse.PartsOfSpeech.Exclamation,
                        component.Definition,
                        component.Example,
                        component.Synonyms));
                }
            }

            foreach (var meaningObj in meaningsObj)
            {
                results.Add(meaningObj);
            }

            return results;
        }
    }
}
