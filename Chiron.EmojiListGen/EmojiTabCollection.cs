namespace Chiron
{
    public class EmojiTabCollection : IList<UnicodeVariation>
    {
        public UnicodeVariation this[int index] { get => ((IList<UnicodeVariation>)Emoji)[index]; set => ((IList<UnicodeVariation>)Emoji)[index] = value; }
        public UnicodeVariation Representitive { get; init; }
        public List<UnicodeVariation> Emoji { get; init; } = new();

        public EmojiTabCollection() { }
        public EmojiTabCollection(IEnumerable<UnicodeVariation> items) => Emoji = new(items);

        #region IList
        public int Count => ((ICollection<UnicodeVariation>)Emoji).Count;
        public bool IsReadOnly => ((ICollection<UnicodeVariation>)Emoji).IsReadOnly;
        public void Add(UnicodeVariation item) => Emoji.Add(item);
        public void Clear() => Emoji.Clear();
        public bool Contains(UnicodeVariation item) => Emoji.Contains(item);
        public void CopyTo(UnicodeVariation[] array, int arrayIndex) => Emoji.CopyTo(array, arrayIndex);
        public IEnumerator<UnicodeVariation> GetEnumerator() => Emoji.GetEnumerator();
        public int IndexOf(UnicodeVariation item) => Emoji.IndexOf(item);
        public void Insert(int index, UnicodeVariation item) => Emoji.Insert(index, item);
        public bool Remove(UnicodeVariation item) => Emoji.Remove(item);
        public void RemoveAt(int index) => Emoji.RemoveAt(index);
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => Emoji.GetEnumerator();
        #endregion
    }
}

