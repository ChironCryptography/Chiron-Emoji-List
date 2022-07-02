namespace Chiron.UnicodeList
{
    public class UnicodeVariation
    {
        public enum TypeFieldType {
            Basic_Emoji,
            Emoji_Keycap_Sequence,
            RGI_Emoji_Flag_Sequence,
            RGI_Emoji_Tag_Sequence,
            RGI_Emoji_Modifier_Sequence,
        }

        public int CodePoint { get; set; }
        public TypeFieldType TypeField { get; set; }
        public string Description { get; set; }
    }
}

