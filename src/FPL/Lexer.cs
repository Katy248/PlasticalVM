using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace FPL;
internal class Lexer
{
    /// <summary>
    /// Replaces commentaries using <see cref="CommentRegex"/>.
    /// </summary>
    /// <param name="code">Code to find commentaries.</param>
    /// <param name="commentRegex"></param>
    /// <returns>Code without commentaries.</returns>
    public static string WitoutCommentaries(string code, string commentRegex)
    {
        return code = Regex.Replace(code, commentRegex, "").Trim();
    }
    /// <summary>
    /// Splits code into tokens using <see cref="FPL.Tokens"/>
    /// </summary>
    /// <param name="sourseCode">Sourse code.</param>
    /// <param name="tokensRegexes"></param>
    /// <param name="commentRegex"></param>
    /// <returns><see cref="IEnumerable{Token}"/> of tokens finded in sourse code.</returns>
    public static IEnumerable<Token> GetTokens(string sourseCode, Dictionary<string, TokenType> tokensRegexes, string commentRegex)
    {
        sourseCode = WitoutCommentaries(sourseCode, commentRegex);
        List<Token> tokens = new List<Token>();
        while (sourseCode.Length > 0)
        {
            bool find = false;
            foreach (var reg in tokensRegexes)
            {
                var m = Regex.Match(sourseCode, reg.Key/*, RegexOptions.Multiline*/);
                if (m.Success)
                {
                    tokens.Add(new Token(m.Value, reg.Value));
                    sourseCode = sourseCode.Remove(0, m.Length);
                    find = true;
                    break;
                }
            }
            if (!find)
                sourseCode = sourseCode.Remove(0, 1);
        }
        return tokens;
    }
}
