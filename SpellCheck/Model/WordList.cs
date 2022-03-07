using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SpellCheck
{
    public class WordList
    {
        public List<string> words = new List<string>();
        public List<string> casesensitive = new List<string>();
        public void Sort()
        {
            words.Sort();
            casesensitive.Sort();
        }
    }
}
