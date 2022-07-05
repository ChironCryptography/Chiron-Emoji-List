using System.Globalization;
using System.Text;
using static Chiron.UnicodeVariation;

namespace Chiron.UnicodeListGen.CodeGeneration
{
    internal static class CSharp
    {
        const string HEAD =
            "// ----------------------------------------------------------\n" +
            "// This file is was auto-generated using Chiron.EmojiList.\n" +
            "// ----------------------------------------------------------\n\n";

        const string BASE =
            "namespace Chiron\n" +
            "{\n" +
            "    public static class EmojiList\n" +
            "    {\n" +
            "@content\n" +
            "        /// <summary> All contained unicode characters. </summary>\n" +
            "        public static Unicode[] All { get; } = (from p in typeof(EmojiList).GetProperties(System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public) where p.PropertyType == typeof(Unicode) select (Unicode)p.GetValue(null)).ToArray();\n" +
            "        \n" +
            "        static Dictionary<string, UnicodeVariation> CodePointLookup { get; } = CreateCodePointLookup();\n" +
            "        \n" +
            "        static Dictionary<string, UnicodeVariation> CreateCodePointLookup() {\n" +
            "            var res = new Dictionary<string, UnicodeVariation>();\n" +
            "            foreach (var u in All) {\n" +
            "                foreach (var v in u.Variations) {\n" +
            "                    if (res.ContainsKey(v.CodePoint)) continue;\n" +
            "                    res.Add(v.CodePoint, v);\n" +
            "                }\n" +
            "            }\n" +
            "            return res;\n" +
            "        }\n" +
            "        \n" +
            "        public static UnicodeVariation[] AllOrdered { get; } = Ordered.Where(s => CodePointLookup.ContainsKey(s)).Select(s => CodePointLookup[s]).ToArray();\n" +
            "        \n" +
            "        static int OrderedIndexOfEmoji(string emoji) {\n" +
            "            int i = 0;\n" +
            "            foreach (var e in AllOrdered) {\n" +
            "                if (e.CodePoint == emoji) return i;\n" +
            "                i++;\n" +
            "            }\n" +
            "            return -1;\n" +
            "        }\n" +
            "        \n" +
            "        public static EmojiTabCollections Categories { get; } = new() {\n" +
            "            Faces = new(AllOrdered[OrderedIndexOfEmoji(\"😀\")..OrderedIndexOfEmoji(\"🙊\")], \"Faces\", \"😀\"),\n" +
            "            Heart = new(AllOrdered[OrderedIndexOfEmoji(\"💋\")..OrderedIndexOfEmoji(\"💤\")], \"Emotions\", \"🧡\"),\n" +
            "            Hands = new(AllOrdered[OrderedIndexOfEmoji(\"👋\")..OrderedIndexOfEmoji(\"🫦\")], \"Body Parts\", \"👋\"),\n" +
            "            People = new(AllOrdered[OrderedIndexOfEmoji(\"👶\")..OrderedIndexOfEmoji(\"🦲\")], \"People\", \"👪\"),\n" +
            "            Animals = new(AllOrdered[OrderedIndexOfEmoji(\"🐵\")..OrderedIndexOfEmoji(\"🦠\")], \"Animals\", \"🐰\"),\n" +
            "            Plants = new(AllOrdered[OrderedIndexOfEmoji(\"💐\")..OrderedIndexOfEmoji(\"🫘\")], \"Plants\", \"💐\"),\n" +
            "            Food = new(AllOrdered[OrderedIndexOfEmoji(\"🍇\")..OrderedIndexOfEmoji(\"🫙\")], \"Food\", \"🍇\"),\n" +
            "            Places = new(AllOrdered[OrderedIndexOfEmoji(\"🏺\")..OrderedIndexOfEmoji(\"🚞\")], \"Places\", \"🌆\"),\n" +
            "            Transportation = new(AllOrdered[OrderedIndexOfEmoji(\"🚋\")..OrderedIndexOfEmoji(\"🧳\")], \"Transportation\", \"🚋\"),\n" +
            "            Time = new(AllOrdered[OrderedIndexOfEmoji(\"⌛\")..OrderedIndexOfEmoji(\"🕦\")], \"Time\", \"⌛\"),\n" +
            "            Astral = new(AllOrdered[OrderedIndexOfEmoji(\"🌑\")..OrderedIndexOfEmoji(\"⚡\")], \"Astral\", \"🌑\"),\n" +
            "            Misc = new(AllOrdered[OrderedIndexOfEmoji(\"⛄\")..OrderedIndexOfEmoji(\"🪪\")], \"Misc\", \"⛄\"),\n" +
            "            Signs = new(AllOrdered[OrderedIndexOfEmoji(\"🏧\")..OrderedIndexOfEmoji(\"🔲\")], \"Signs\", \"🏧\"),\n" +
            "            Flags = new(AllOrdered[OrderedIndexOfEmoji(\"🏁\")..OrderedIndexOfEmoji(\"🏴󠁧󠁢󠁷󠁬󠁳󠁿\")], \"Flags\", \"🏁\"),\n" +
            "        };\n" +
            "    }\n" +
            "}\n";

        static string Unicode_cs { get; } = File.ReadAllText("Unicode.cs");
        static string UnicodeVariation_cs { get; } = File.ReadAllText("UnicodeVariation.cs");
        static string CodePointFormatter_cs { get; } = File.ReadAllText("CodePointFormatter.cs");
        static string EmojiTabCollection_cs { get; } = File.ReadAllText("EmojiTabCollection.cs");
        static string EmojiTabCollections_cs { get; } = File.ReadAllText("EmojiTabCollections.cs");

        static string GetUnicodeField(Unicode unicode) {
            var name =
                unicode.Variations.First().Description
                .Replace('"', ' ')
                .Replace('-', ' ');

            name = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(name.ToLower())
                .MakeAlphaNumeric(true)
                .Replace(" ", "");
            
            if (char.IsNumber(name[0])) name = "_" + name;

            var res = "        public static Unicode " + name + " { get; } = " + GetNewUnicode(unicode) + ";";
            return res;
        }

        static string GetNewUnicode(Unicode unicode) {
            var @base = "new Unicode(new UnicodeVariation[] { @new_variations })";
            var new_variations = string.Join(", ", (from v in unicode.Variations select 
                $"new UnicodeVariation() {{ " +
                    $"CodePoint = {v.CodePoint.ToLiteral()}, " +
                    $"Description = {v.Description.ToLiteral()}, " +
                    $"TypeField = {nameof(UnicodeVariation)}.{nameof(TypeFieldType)}.{v.TypeField}, " +
                    $"Tags = new string[] {{ {string.Join(", ", (from t in v.Tags select t.ToLiteral()))} }}" +
                $"}}"));
            return @base.Replace("@new_variations", new_variations);
        }

        static string GetOrderingField(List<string> ordering) => 
            $"        static List<string> Ordered = new() {{ {string.Join(", ", from o in ordering select o.ToLiteral())} }};\n";
        
        public static string Generate(List<Unicode> data, List<string> ordering) {
            StringBuilder content = new();
            foreach (var unicode in data) content.Append(GetUnicodeField(unicode) + '\n');
            content.Append(GetOrderingField(ordering));
            return HEAD + Unicode_cs + UnicodeVariation_cs + EmojiTabCollection_cs + EmojiTabCollections_cs + CodePointFormatter_cs + BASE.Replace("@content", content.ToString());
        }
    }
}
