using System.Net;

namespace Chiron.UnicodeList
{
    public static class Program
    {
        public static async Task Main(string[] args) {
            Console.WriteLine("Downloading list of emojis from unicode.org...");
            var text = await HttpHelper.DownloadFileAsText("https://unicode.org/Public/emoji/14.0/emoji-sequences.txt");

            Console.WriteLine("Parsing downloaded data...");
            var data = UnicodeFile.Parse(text);

            Console.WriteLine("Generating cs file...");
            var code = CodeGeneration.CSharp.Generate(data);
            File.WriteAllText("UnicodeList.cs", code);

            Console.WriteLine("Wrote output to 'UnicodeList.cs'.");
            Console.WriteLine("Done.");
        }
    }
}

