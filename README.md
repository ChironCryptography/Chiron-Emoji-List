# Chiron Cryptography, Inc.

### Chiron Unicode List
A simple list of all emoji characters and their tags. Included in this repo as well is a code generation tool that can update the list automatically using official unicode.org data.

### Usage
#### C#
[Install the Nuget package.](https://www.nuget.org/packages/Chiron.EmojiList/)

```
Console.WriteLine(Chiron.EmojiList.Airplane);

foreach (var e in Chiron.EmojiList.All) {
    Console.WriteLine(e);
}
```

### Notes
If you are missing emoji, they are displaying as boxes/something else, then you need to make sure that you have a font that can handle them all. [Twemoji](https://twemoji.twitter.com/) is a good open-source font that solves this.
