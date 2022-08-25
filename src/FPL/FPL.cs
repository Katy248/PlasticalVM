using LanguageInterfaces;

namespace FPL;
public class FunctionalPlasticalLanguage : ILanguageTranslator
{
    public string GetVMCode(string sourseCode)
    {
        var tokens = Lexer.GetTokens(sourseCode, Tokens, Comment);
        var vmCode = Parser.GetVMCode(tokens);
        return vmCode;
    }
    public static readonly Dictionary<string, TokenType> Tokens = new Dictionary<string, TokenType>()
    {
        {@"^[\[][^\[\]]*[\]]", TokenType.VMCommand },
        {@"^[\(]", TokenType.OpenBlockBracket },
        {@"^[\)]", TokenType.CloseBlockBracket },
        {@"^[\?]", TokenType.IfKeyWord },
        {@"^[\;]", TokenType.ElseKeyWord },
        {@"^((:)|(->))", TokenType.FunctionDefine },
        {@"^([\'][^\'\n]*[\'])|^([""][^""\n]*[""])", TokenType.String },
        {@"^[\d]+[\.]?[\d]*", TokenType.Number },
        {@"^[\~][^\s]+[\.][^\s]+", TokenType.AddingLibFile},
        {@"^[^\s\)\(\:\?\|\[\]\~]+", TokenType.Function },
        {@"^[^\s]+", TokenType.UnknownToken},
    };
    /// <summary>
    /// Regex to find commentaries in souse code.
    /// </summary>
    public static readonly string Comment = @"({#)([^}]*)(#})|([#][^\n]*[\n])";
}
