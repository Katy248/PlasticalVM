using FPL;

namespace TFPL;
public class TextFPL
{
    protected TextFPL() {}
    public static string GetVMCode(string sourseCode)
    {
        var tokens = Lexer.GetTokens(sourseCode, Tokens, Comment);
        var vmCode = TFPL.Parser.GetVMCode(tokens);
        return vmCode;
    }
    public static readonly Dictionary<string, TokenType> Tokens = new Dictionary<string, TokenType>()
    {
        {@"^[\[][^\[\]]*[\]]", TokenType.VMCommand },
        {@"^[\{]", TokenType.OpenBlockBracket },
        {@"^[\}]", TokenType.CloseBlockBracket },
        {@"^(if)", TokenType.IfKeyWord },
        {@"^else", TokenType.ElseKeyWord },
        {@"^def", TokenType.FunctionDefine },
        {@"^([\'][^\n\']*[\'])" +"([\"][^\\n\"]*[\"])", TokenType.String },
        {@"^[\d]+[\.]?[\d]*", TokenType.Number },
        {@"^[\~][^\s]+[\.][^\s]+", TokenType.AddingLibFile},
        {@"^(true)|(false)", TokenType.Boolean},
        {@"^(load)[\s]+[^\s\)\(\:\?\|\[\]\~]+", TokenType.Function },
        {@"^[^\s]+", TokenType.UnknownToken},
    };
    /// <summary>
    /// Regex to find commentaries in souse code.
    /// </summary>
    public static readonly string Comment = @"({#)([^}]*)(#})|([#][^\n]*[\n])";
}
