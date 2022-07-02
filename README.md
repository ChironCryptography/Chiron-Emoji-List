# Chiron Cryptography, Inc.

### Chiron Unicode List
A simple tool that downloads a list of emoji from unicode.org and generates a specified data structure to read it from.

### Usage
Currently this tool only supports a static C# file, if interest is shown support for other languages/formats can be added. You _**should not**_ link this project as a reference in your project, rather, you should use the .cs files it generates. Simply download the following file and add it to your project.

 - [C#](https://github.com/jamieyello/Chiron-Unicode-List/blob/master/Output/CS/UnicodeList.cs)

 If you want to make sure the list is up to date, run the tool to output a new up-to-date file.

### Example
#### C#
```
foreach (var unicode in UnicodeList.All) Console.WriteLine(unicode.Variations.First().Description);
```
