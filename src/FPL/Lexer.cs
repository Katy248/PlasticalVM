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
    /// Regex to find commentaries in souse code.
    /// </summary>
    static Regex CommentRegex = new Regex(@"({#)([^}]*)(#})|([#][^\n]*[\n])", RegexOptions.Compiled | RegexOptions.Multiline);
    /// <summary>
    /// Replaces commentaries using <see cref="CommentRegex"/>.
    /// </summary>
    /// <param name="code">Code to find commentaries.</param>
    /// <returns>Code without commentaries.</returns>
    public static string PreLex(string code)
    {
        code = CommentRegex.Replace(code, "");
        return code.Trim();
    }
    /// <summary>
    /// Splits code into tokens using <see cref="FPL.Tokens"/>
    /// </summary>
    /// <param name="sourse">Sourse code.</param>
    /// <returns><see cref="IEnumerable{Token}"/> of tokens finded in sourse code.</returns>
    public static IEnumerable<Token> GetTokens(string sourse)
    {
        sourse = PreLex(sourse);
        List<Token> tokens = new List<Token>();
        while (sourse.Length > 0)
        {
            bool find = false;
            foreach (var reg in FPL.Tokens)
            {
                var m = Regex.Match(sourse, reg.Key/*, RegexOptions.Multiline*/);
                if (m.Success)
                {
                    tokens.Add(new Token(m.Value, reg.Value));
                    sourse = sourse.Remove(0, m.Length);
                    find = true;
                    break;
                }
            }
            if (!find)
                sourse = sourse.Remove(0, 1);
        }
        return tokens;
    }
}
