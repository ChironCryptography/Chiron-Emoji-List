using System.Net;
using System.Reflection;

namespace Chiron.EmojiListGen
{
    public static class Program
    {
        public static async Task Main(string[] args) {
            Console.WriteLine("Downloading list of emoji from unicode.org...");
            var sequences = await HttpHelper.DownloadFileAsText("https://unicode.org/Public/emoji/14.0/emoji-sequences.txt");
            var zwy_sequences = await HttpHelper.DownloadFileAsText("https://unicode.org/Public/emoji/14.0/emoji-zwj-sequences.txt");
            var emoji_ordering = await HttpHelper.DownloadFileAsText("https://unicode.org/emoji/charts/emoji-ordering.txt");

            Console.WriteLine("Parsing downloaded data...");
            var emoji = new Dictionary<string, Unicode>();
            UnicodeFile.Parse(sequences, UnicodeFile.FileType.sequences, emoji);
            UnicodeFile.Parse(zwy_sequences, UnicodeFile.FileType.zwj_sequences, emoji);
            var emoji_l = (from d in emoji select d.Value).ToList();

            var emoji_ordering_l = UnicodeFile.ParseOrder(emoji_ordering);

            Console.WriteLine("Generating cs file...");
            var code = CodeGeneration.CSharp.Generate(emoji_l, emoji_ordering_l);
            File.WriteAllText("CodeGen.cs", code);

            Console.WriteLine("Wrote output to 'CodeGen.cs'.");

            Console.WriteLine("Done.");
        }
    }
}

