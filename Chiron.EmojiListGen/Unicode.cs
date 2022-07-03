namespace Chiron
{
    public class Unicode
    {
        public Unicode() { }
        public Unicode(IEnumerable<UnicodeVariation> variations) => Variations = new(variations);
        public List<UnicodeVariation> Variations { get; set; } = new();

        public override string ToString() => 
            char.ConvertFromUtf32(Variations.First().CodePoint);

        public string ToCodePointRepresentation() =>
            CodePointFormatter.ToCodePointRepresentation(Variations.First().CodePoint);
    }
}

