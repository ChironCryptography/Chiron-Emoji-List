using System.Net;
using System.Reflection;

namespace Chiron.UnicodeListGen
{
    public static class Program
    {
        public static async Task Main(string[] args) {
            Console.WriteLine("Downloading list of emojis from unicode.org...");
            var sequences = await HttpHelper.DownloadFileAsText("https://unicode.org/Public/emoji/14.0/emoji-sequences.txt");
            var zwy_sequences = await HttpHelper.DownloadFileAsText("https://unicode.org/Public/emoji/14.0/emoji-zwj-sequences.txt");

            Console.WriteLine("Parsing downloaded data...");
            var res = new Dictionary<string, Unicode>();
            UnicodeFile.Parse(sequences, UnicodeFile.FileType.sequences, res);
            UnicodeFile.Parse(zwy_sequences, UnicodeFile.FileType.zwj_sequences, res);
            var res_l = (from d in res select d.Value).ToList();

            Console.WriteLine("Generating cs file...");
            var code = CodeGeneration.CSharp.Generate(res_l);
            File.WriteAllText("CodeGen.cs", code);

            Console.WriteLine("Wrote output to 'CodeGen.cs'.");
            Console.WriteLine("Done.");
        }
    }
}

