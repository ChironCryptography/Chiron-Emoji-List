# Chiron Cryptography, Inc.

_**WARNING:**_ This package is still in development and you may encounter issues. If you find an issue, please document it under [Issues](https://github.com/ChironCryptography/Chiron-Emoji-List/issues).

### Chiron Emoji List
This package generates a list of all emoji unicode characters and their tags. It also includes a code generation tool which can automatically update the list using data published by unicode.org. This provides an optimized way to call, transfer, or generate emoji based on their values.

### Usage
#### C#
[Install the Nuget package.](https://www.nuget.org/packages/Chiron.EmojiList/)

```
// Note: Your console may not be able to display emojis.
Console.WriteLine(Chiron.EmojiList.Locomotive);

foreach (var c in Chiron.EmojiList.Categories) {
    Console.WriteLine(c.Name + c.Representitive.ToString())
    foreach (var e in c) {
        Console.WriteLine(e);
    }
}
```

#### C# Blazor
[Install the Nuget package.](https://www.nuget.org/packages/Chiron.EmojiList/)

```
<p>p @Chiron.EmojiList.Locomotive</p>

@foreach (var c in Chiron.EmojiList.Categories) {
    <p>@c.Name @c.Representitive</p>
    @foreach (var e in c) {
        <p style="display: inline-block;">@e</p>
    }
}
```

### Notes
If you encounter missing emoji or the emoji display as boxes, then those emoji may not be supported by your selected font. To solve this, you should designate a font to use when displaying emojis. One good example of a font to use for this purpose is [Twemoji](https://twemoji.twitter.com/), an open-source emoji font.

One practical application of this package is to provide emoji as a special character option when a user is creating or entering a password. This can strengthen the complexity of the user's password, but be mindful that emoji are treated as two Unicode characters, meaning the program must be adjusted to treat the emoji unicode characters as one distinct character for the sake of enforcing a minimum password length. Without that distinction, a user may bypass a 12-character minimum password length by entering only 6 emoji.
