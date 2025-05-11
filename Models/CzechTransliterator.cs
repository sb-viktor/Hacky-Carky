using System.Collections.Generic;
using System.Text;

namespace Hacky_Carky.Models;

public enum CzechTokenType
{
    Letter,
    Digit,
    Whitespace,
    Punctuation,
    Other
}

public record CzechToken(CzechTokenType Type, string Value);

public static class CzechTransliterator
{
    private static readonly Dictionary<char, char> TransliterationMap = new()
    {
        ['á'] = 'a', ['č'] = 'c', ['ď'] = 'd', ['é'] = 'e', ['ě'] = 'e',
        ['í'] = 'i', ['ň'] = 'n', ['ó'] = 'o', ['ř'] = 'r', ['š'] = 's',
        ['ť'] = 't', ['ú'] = 'u', ['ů'] = 'u', ['ý'] = 'y', ['ž'] = 'z',
        ['Á'] = 'A', ['Č'] = 'C', ['Ď'] = 'D', ['É'] = 'E', ['Ě'] = 'E',
        ['Í'] = 'I', ['Ň'] = 'N', ['Ó'] = 'O', ['Ř'] = 'R', ['Š'] = 'S',
        ['Ť'] = 'T', ['Ú'] = 'U', ['Ů'] = 'U', ['Ý'] = 'Y', ['Ž'] = 'Z'
    };

    public static IEnumerable<CzechToken> Tokenize(string input)
    {
        if (string.IsNullOrEmpty(input))
            yield break;

        var sb = new StringBuilder();
        CzechTokenType? currentType = null;

        foreach (var ch in input)
        {
            var type = GetTokenType(ch);

            if (currentType != type)
            {
                if (sb.Length > 0)
                    yield return new CzechToken(currentType!.Value, sb.ToString());
                sb.Clear();
                currentType = type;
            }
            sb.Append(ch);
        }

        if (sb.Length > 0 && currentType != null)
            yield return new CzechToken(currentType.Value, sb.ToString());
    }

    public static string Transliterate(string input)
    {
        if (string.IsNullOrEmpty(input))
            return string.Empty;

        var result = new StringBuilder();

        foreach (var token in Tokenize(input))
        {
            if (token.Type == CzechTokenType.Letter)
            {
                foreach (var ch in token.Value)
                    result.Append(TransliterationMap.TryGetValue(ch, out var repl) ? repl : ch);
            }
            else
            {
                result.Append(token.Value);
            }
        }

        return result.ToString();
    }

    private static CzechTokenType GetTokenType(char ch) =>
        char.IsLetter(ch) ? CzechTokenType.Letter :
        char.IsDigit(ch) ? CzechTokenType.Digit :
        char.IsWhiteSpace(ch) ? CzechTokenType.Whitespace :
        char.IsPunctuation(ch) ? CzechTokenType.Punctuation :
        CzechTokenType.Other;
}
