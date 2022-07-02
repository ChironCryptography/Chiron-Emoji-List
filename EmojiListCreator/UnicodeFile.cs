using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Chiron.UnicodeList
{
    internal static class UnicodeFile
    {
        public static List<Unicode> Parse(string text) {
            var res = new List<Unicode>();
            using var sr = new StringReader(text);
            string line;
            while ((line = sr.ReadLine()) != null) {
                var entry = ParseLine(line);
                if (entry != null) res.Add(entry);
            }
            return res;
        }

        static Unicode ParseLine(string line) {
            // Remove comments (#)
            line = Regex.Replace(line, @"#.*", "").Trim();
            if (string.IsNullOrEmpty(line)) return null;

            // Example data: string[3] { "231A..231B", "Basic_Emoji", "watch" }
            var data = line.Split(';', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

            var res = new Unicode();
            foreach (var code in ParseCode(data[0])) {
                res.Variations.Add(new() { 
                    CodePoint = code,
                    TypeField = Enum.Parse<UnicodeVariation.TypeFieldType>(data[1]),
                    Description = data[2]
                });
            }
            return res;
        }

        public static IEnumerable<int> ParseCode(string code_line) {
            foreach (var code in code_line.Split(' ')) {
                if (code.Contains("..")) {
                    var range = code.Split("..");
                    int first = Convert.ToInt32("0x" + range[0].ToLower(), 16);
                    int last = Convert.ToInt32("0x" + range[1].ToLower(), 16);
                    for (int i = first; i <= last; i++) yield return i;
                    yield break;
                }
                else yield return Convert.ToInt32("0x" + code, 16);
            }
        }
    }
}
