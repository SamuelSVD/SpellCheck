using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SpellCheck
{
    class SpellCheckUtils
    {
        static WordList wordList;
        public static string PATH = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + "\\words.xml";
        public static List<SpellMistake> CheckText(string text)
        {
            List<SpellMistake> sm = new List<SpellMistake>();
            string[] lines = text.Split(
                new string[] { "\n" },
                StringSplitOptions.None
            );
            for (int i = 0; i<lines.Length; i++) {
                string l = lines[i].Trim();
                if (l.Length > 0) {
                    sm.AddRange(CheckLine(l, i + 1));
                }
            }
            return sm;
        }
        public static List<SpellMistake> CheckLine(string line, int lineNumber)
        {
            List<SpellMistake> sm = new List<SpellMistake>();
            string[] words = line.Split(
                new string[] { " " },
                StringSplitOptions.None
            );
            foreach (string word in words) {
                if (!WordIsValid(word)) {
                    sm.Add(new SpellMistake(lineNumber, word));
                }
            }
            return sm;
        }
        public static bool WordIsValid(string word)
        {
            if (wordList == null) {
                LoadWordList();
            }
            string w = Clean(word);
            return (w == "") || (wordList.words.BinarySearch(w.ToLower())>=0) || (wordList.casesensitive.BinarySearch(w) >=0);
        }
        public static string Clean(string word)
        {
            if (word == "") {
                word = "";
            } else if (word.EndsWith(".") || word.EndsWith(",") || word.EndsWith(":") || word.EndsWith(")") || word.EndsWith(",")) {
                word = word.Remove(word.Length - 1, 1);
                word = Clean(word);
            } else if (word.StartsWith("(")) {
                word = word.Remove(0, 1);
                word = Clean(word);
            } else if (word.StartsWith("<") && word.EndsWith(">")) {
                word = "";
            } else if (word.StartsWith("\\") && !word.StartsWith("\\<")) {
                word = "";
                //word = word.Remove(0, 1);
                //word = Clean(word);
            } else if (!isAlphaNumeric(word) || isNumber(word)) {
                word = "";
            }
            return word;
        }
        public static bool isAlphaNumeric(string strToCheck)
        {
            Regex rg = new Regex(@"^[a-zA-Z0-9\s,]*$");
            return rg.IsMatch(strToCheck);
        }
        public static bool isNumber(string str)
        {
            return Regex.IsMatch(str, @"^\d+$");
        }
        public static void LoadWordList()
        {
            wordList = XMLUtils.LoadFromFile<WordList>(PATH);
            if (wordList == null) {
                wordList = new WordList();
                XMLUtils.SaveToFile(wordList, PATH);
            }
            wordList.Sort();
        }
        public static void LoadWordListFromJSON()
        {
            wordList = new WordList();
            string fileContent = File.ReadAllText("words.json");
            string[] lines = fileContent.Split(
                new string[] { "\n" },
                StringSplitOptions.None
            );
            for (int i = 0; i < lines.Length; i++) {
                string line = lines[i];
                if (line.StartsWith("  \"")) {
                    string word = line.Replace("  \"", "").Replace("\": 1,", "");
                    wordList.words.Add(word);
                }
            }
            XMLUtils.SaveToFile(wordList, PATH);
        }
    }
}
