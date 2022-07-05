using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Chiron.UnicodeListGen
{
    internal static class UnicodeFile
    {
        public enum FileType {
            sequences,
            zwj_sequences,
        }

        /// <summary> Parse emoji-ordering.txt </summary>
        public static List<string> ParseOrder(string text) {
            var res = new List<string>();
            using var sr = new StringReader(text);
            string codepoints;
            while ((codepoints = sr.ReadLine()) != null) {
                codepoints = Regex.Replace(codepoints, @"#.*", "").Split(';').FirstOrDefault()?.Trim();
                if (string.IsNullOrEmpty(codepoints)) continue;
                res.AddRange(ParseCode(codepoints));
            }
            return res;
        }

            /// <summary> Indexed by description. </summary>
            public static void Parse(string text, FileType file, Dictionary<string, Unicode> res) {
            using var sr = new StringReader(text);
            string line;
            while ((line = sr.ReadLine()) != null) {
                // Remove comments (#)
                line = Regex.Replace(line, @"#.*", "").Trim();
                if (string.IsNullOrEmpty(line)) continue;
                var data = line.Split(';', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
                Unicode entry = null;
                if (file == FileType.sequences) entry = ParseLine(data);
                if (file == FileType.zwj_sequences) entry = ParseZwjLine(data);
                if (entry == null) continue;

                var desc = entry.Variations.First().Description;
                if (res.TryGetValue(desc, out var existing_entry)) existing_entry.Variations.AddRange(entry.Variations);
                else res.Add(desc, entry);
            }
        }

        // Example data: string[3] { "231A..231B", "Basic_Emoji", "watch" }
        static Unicode ParseLine(string[] data) {
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

        // Example data: string[3] { "1F468 200D 2764 FE0F 200D 1F468", "RGI_Emoji_ZWJ_Sequence", "couple with heart: man, man" }
        static Unicode ParseZwjLine(string[] data) {
            var res = new Unicode();
            foreach (var code in ParseCode(data[0])) {
                var zwj = data[2].Split(':');
                var tags = zwj.Length == 1 ? Array.Empty<string>() : (from t in zwj[1].Split(',') select t.Trim()).ToArray();

                res.Variations.Add(new() {
                    CodePoint = code,
                    TypeField = Enum.Parse<UnicodeVariation.TypeFieldType>(data[1]),
                    Description = zwj[0],
                    Tags = tags
                });
            }

            return res;
        }

        public static IEnumerable<string> ParseCode(string code_line) {
            StringBuilder sb = new();
            foreach (var code in code_line.Split(' ')) {
                if (code.Contains("..")) {
                    var range = code.Split("..");
                    int first = Convert.ToInt32("0x" + range[0].ToLower().Replace("u+", ""), 16);
                    int last = Convert.ToInt32("0x" + range[1].ToLower().Replace("u+", ""), 16);
                    for (int i = first; i <= last; i++) yield return ((char)i).ToString();
                }
                else sb.Append((char)Convert.ToInt32(("0x" + code.ToLower().Replace("u+", "")), 16));
            }
            yield return sb.ToString();
        }
    }
}
