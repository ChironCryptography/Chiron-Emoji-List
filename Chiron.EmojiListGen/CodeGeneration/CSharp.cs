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
            "        public static List<Unicode> All { get; } = (from p in typeof(EmojiList).GetProperties(System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public) where p.PropertyType == typeof(Unicode) select (Unicode)p.GetValue(null)).ToList();\n" +
            "        \n" +
            "        public static Dictionary<string, UnicodeVariation> CodePointLookup { get; } = CreateCodePointLookup();\n" +
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
            "       \n" +
            "        public static List<UnicodeVariation> AllOrdered { get; } = (from o in Ordered select CodePointLookup[o]).ToList();\n" +
            "    }\n" +
            "}\n";

        static string Unicode_cs { get; } = File.ReadAllText("Unicode.cs");
        static string UnicodeVariation_cs { get; } = File.ReadAllText("UnicodeVariation.cs");
        static string CodePointFormatter_cs { get; } = File.ReadAllText("CodePointFormatter.cs");

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
            return HEAD + Unicode_cs + UnicodeVariation_cs + CodePointFormatter_cs + BASE.Replace("@content", content.ToString());
        }
    }
}
