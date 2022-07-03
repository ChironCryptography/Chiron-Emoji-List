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
