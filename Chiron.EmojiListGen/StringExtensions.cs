using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chiron.EmojiListGen
{
    internal static class StringExtensions
    {
        public static string MakeAlphaNumeric(this string s, bool allow_space) {
            StringBuilder sb = new();
            foreach (var c in s) {
                if (c >= 'A' && c <= 'Z') sb.Append(c);
                if (c >= 'a' && c <= 'z') sb.Append(c);
                if (c >= '0' && c <= '9') sb.Append(c);
                if (allow_space && c == ' ') sb.Append(c);
            }
            return sb.ToString();
        }

        // https://stackoverflow.com/questions/323640/can-i-convert-a-c-sharp-string-value-to-an-escaped-string-literal
        public static string ToLiteral(this string input) {
            using (var writer = new StringWriter()) {
                using (var provider = CodeDomProvider.CreateProvider("CSharp")) {
                    provider.GenerateCodeFromExpression(new CodePrimitiveExpression(input), writer, null);
                    return writer.ToString();
                }
            }
        }
    }
}
