using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LingoLoom
{
    public class LocalisationSystem
    {
        public enum Language
        {
            English,
            Nederlands
        }


        public static Language language = Language.English;

        private static Dictionary<string, string> _localisedEN;
        private static Dictionary<string, string> _localisedNL;


        public static bool isInit;

        public static void Init()
        {
            CSVLoader csvLoader = new CSVLoader();
            csvLoader.LoadCSV();

            _localisedEN = csvLoader.GetDictionaryValues("en");
            _localisedNL = csvLoader.GetDictionaryValues("nl");

            isInit = true;
        }

        public static string GetLocalisedValue(string key)
        {
            if (!isInit) { Init(); }
            string value = key;

            switch (language)
            {
                case Language.English: _localisedEN.TryGetValue(key, out value); break;
                case Language.Nederlands: _localisedNL.TryGetValue(key, out value); break;
            }

            return value;
        }
    }

}
