# Chiron Cryptography, Inc.

### Chiron Unicode List
A simple list of all emoji characters and their tags. Included in this repo as well is a code generation tool that can update the list automatically using official unicode.org data.

### Usage
#### C#
[Install the Nuget package.](https://www.nuget.org/packages/Chiron.EmojiList/1.0.1)

```
foreach (var unicode in UnicodeList.All) {
    Console.WriteLine(unicode.Variations.First().Description);
}
```
