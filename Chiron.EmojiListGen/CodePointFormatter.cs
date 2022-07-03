namespace Chiron
{
	public static class CodePointFormatter
	{
		public static string ToCodePointRepresentation(int codePoint) => 
			"U+" + ToHexadecimal(codePoint);

		public static string ToHexadecimal(int codePoint) => 
			codePoint.ToString(codePoint < 0x100000 ? codePoint < 0x10000 ? "X4" : "X5" : "X6", System.Globalization.CultureInfo.InvariantCulture);
	}
}

