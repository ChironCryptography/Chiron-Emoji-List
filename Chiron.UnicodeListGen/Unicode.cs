namespace Chiron.UnicodeListGen
{
    public class Unicode
    {
        public Unicode() { }
        public Unicode(IEnumerable<UnicodeVariation> variations) => Variations = new(variations);
        public List<UnicodeVariation> Variations { get; set; } = new();
    }
}

