# Chiron Cryptography, Inc.

### Chiron Unicode List
A simple tool that downloads a list of emoji from unicode.org and generates a specified data structure to read it from.

### Usage
#### C#
[Install the Nuget package.](https://www.nuget.org/packages/Chiron.UnicodeList/1.0.0)

```
foreach (var unicode in UnicodeList.All) {
    Console.WriteLine(unicode.Variations.First().Description);
}
```
