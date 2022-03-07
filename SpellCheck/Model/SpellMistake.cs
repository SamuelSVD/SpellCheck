using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpellCheck
{
    public class SpellMistake
    {
        public int line;
        public string text;
        public SpellMistake(int line, string text)
        {
            this.line = line;
            this.text = text;
        }
    }
}
