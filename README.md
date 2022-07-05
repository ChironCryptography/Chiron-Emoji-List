# Chiron Cryptography, Inc.

_**WARNING:**_ This package is still in development. It most likely won't work right.

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

#### C# Blazor
[Install the Nuget package.](https://www.nuget.org/packages/Chiron.EmojiList/)

```
<p>p @Chiron.EmojiList.Airplane</p>

@foreach (var e in Chiron.EmojiList.All) {
    <p>
    @foreach (var v in e.Variations) {
        @v
    }
    </p>
}
```

### Notes
If you are missing emoji, they are displaying as boxes/something else, then you need to make sure that you have a font that can handle them all. [Twemoji](https://twemoji.twitter.com/) is a good open-source font that solves this.
