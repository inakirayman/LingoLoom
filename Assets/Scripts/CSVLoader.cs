using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

namespace LingoLoom
{
    public class CSVLoader
    {
        private TextAsset _csvFile;
        private const char _lineSeperator = '\n';
        private const char _surround = '"';
        private string[] _fieldSeperator = { "\",\"" };

        public void LoadCSV()
        {
            _csvFile = Resources.Load<TextAsset>("localistation");

           
        }

        public Dictionary<string, string> GetDictionaryValues(string attributeId)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();

            string[] lines = _csvFile.text.Split(_lineSeperator);

            int attributeIndex = -1;

            string[] headers = lines[0].Split(_fieldSeperator, System.StringSplitOptions.None);

            for(int i = 0; i < headers.Length; i++)
            {
                if (headers[i].Contains(attributeId))
                {
                    attributeIndex = i;
                    break;
                }
            }

            Regex  CSVParser = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");

            for(int i = 1; i < lines.Length; i++)
            {
                string line = lines[i];
                string[] fields = CSVParser.Split(line);

                for(int f= 0; f < fields.Length; f++)
                {
                    fields[f] = fields[f].TrimStart(' ', _surround);
                    fields[f] = fields[f].TrimEnd(_surround);

                }

                if (fields.Length > attributeIndex)
                {
                    var key = fields[0];
                    if (dictionary.ContainsKey(key)) { continue; }
                    var value = fields[attributeIndex];
                    dictionary.Add(key, value);
                }
            }
            return dictionary;
        }
    }
}

