using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VirtualMachine;

namespace FPL;
public class FPL
{
    public static string GetVMCode(string sourseCode)
    {
        var tokens = Lexer.GetTokens(sourseCode);
        using (StreamWriter sw = new StreamWriter("TokensList.txt")) 
            foreach (var t in tokens) sw.WriteLine(t);
        return new Parser(tokens).Parse();
    }
    public static Dictionary<string, TokenType> Tokens = new Dictionary<string, TokenType>()
    {
        {@"^[\[][^\[\]]*[\]]", TokenType.VMCommand },
        {@"^[\(]", TokenType.OpenBlockBracket },
        {@"^[\)]", TokenType.CloseBlockBracket },
        {@"^[\?]", TokenType.IfKeyWord },
        {@"^[\;]", TokenType.ElseKeyWord },
        {@"^[:]", TokenType.FunctionDefine },
        {@"^([\'][^\n\']*[\'])", TokenType.String },
        {@"^[\d]+[\.]?[\d]*", TokenType.Number },
        {@"^[\~][^\s]+[\.][^\s]+", TokenType.AddingLibFile},
        {@"^[^\s\)\(\:\?\|\[\]\~]+", TokenType.Function },
        {@"^[^\s]+", TokenType.UnknownToken},
    };
}
