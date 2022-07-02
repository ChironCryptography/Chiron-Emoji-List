using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static Chiron.UnicodeList.UnicodeVariation;

namespace Chiron.UnicodeList.CodeGeneration
{
    internal static class CSharp
    {
        const string HEAD =
            "// ----------------------------------------------------------\n" +
            "// This file is was auto-generated using Chiron.UnicodeList.\n" +
            "// ----------------------------------------------------------\n\n";

        const string BASE =
            "namespace Chiron.UnicodeList\n" +
            "{\n" +
            "    public static class UnicodeList\n" +
            "    {\n" +
            "@content" +
            "    }\n" +
            "}\n";

        static string Unicode_cs { get; } = File.ReadAllText("Unicode.cs");
        static string UnicodeVariation_cs { get; } = File.ReadAllText("UnicodeVariation.cs");

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
                $"new UnicodeVariation() {{ CodePoint = {v.CodePoint}, Description = {v.Description.ToLiteral()}, TypeField = {nameof(UnicodeVariation)}.{nameof(TypeFieldType)}.{v.TypeField} }}"));
            return @base.Replace("@new_variations", new_variations);
        }

        public static string Generate(List<Unicode> data) {
            StringBuilder content = new();
            foreach (var unicode in data) content.Append(GetUnicodeField(unicode) + '\n');
            return HEAD + Unicode_cs + UnicodeVariation_cs + BASE.Replace("@content", content.ToString());
        }
    }
}
