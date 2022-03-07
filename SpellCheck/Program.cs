using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpellCheck
{
    class Program
    {
        static void Main(string[] args)
        {
            try {
                if (args.Length == 0) {
                    help();
                    return;
                }
                if (args[0] == "--make") {
                    using (Stream input = new MemoryStream(Encoding.ASCII.GetBytes(Properties.Resources.words_xml)))
                    using (Stream output = File.Create(SpellCheckUtils.PATH)) {
                        input.CopyTo(output);
                    }
                    return;
                }
                if (args.Length < 1) {
                    System.Console.WriteLine("Parameter 1 not provided: Path to file to spell-check");
                    return;
                }
                if (args.Length == 2) {
                    SpellCheckUtils.PATH = args[1];
                }
                if (!File.Exists(SpellCheckUtils.PATH)) {
                    System.Console.WriteLine(String.Format("words.xml not found: {0}", SpellCheckUtils.PATH));
                    return;
                }
                string FilePath = args[0];
                string fileContent = File.ReadAllText(FilePath);
                foreach(SpellMistake sm in SpellCheckUtils.CheckText(fileContent)) {
                    System.Console.WriteLine(String.Format("Line {0}: {1}", sm.line, sm.text));
                }
            } catch (Exception e){
                System.Console.WriteLine(e.Message);
            }
        }
        public static void help()
        {
            System.Console.Write("SpellCheck.exe <path-to-compare> [path-to-dictionary]");
            System.Console.Write("    <path-to-compare>     path to file to spell check");
            System.Console.Write("    [path-to-dictionary]  path to file to use as dictionary");
            System.Console.Write("    --make                optional parameter to create dictionary file (requires internet access)");
            System.Console.Write("");
        }
    }
}
