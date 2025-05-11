using System.Collections.Generic;
using System.Text;

namespace Hacky_Carky.Models;

public static class CzechTransliterator
{
    private static readonly Dictionary<char, char> TransliterationMap = new()
    {
        { 'á', 'a' }, { 'č', 'c' }, { 'ď', 'd' }, { 'é', 'e' }, { 'ě', 'e' },
        { 'í', 'i' }, { 'ň', 'n' }, { 'ó', 'o' }, { 'ř', 'r' }, { 'š', 's' },
        { 'ť', 't' }, { 'ú', 'u' }, { 'ů', 'u' }, { 'ý', 'y' }, { 'ž', 'z' },
        { 'Á', 'A' }, { 'Č', 'C' }, { 'Ď', 'D' }, { 'É', 'E' }, { 'Ě', 'E' },
        { 'Í', 'I' }, { 'Ň', 'N' }, { 'Ó', 'O' }, { 'Ř', 'R' }, { 'Š', 'S' },
        { 'Ť', 'T' }, { 'Ú', 'U' }, { 'Ů', 'U' }, { 'Ý', 'Y' }, { 'Ž', 'Z' }
    };

    public static string Transliterate(string input)
    {
        var result = new StringBuilder(input.Length);

        foreach (var ch in input)
        {
            result.Append(TransliterationMap.TryGetValue(ch, out var replacement) ? replacement : ch);
        }

        return result.ToString();
    }
}