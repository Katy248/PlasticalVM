namespace FPL;
internal class Token
{
    public Token(string text, TokenType type) =>
        (Text, Type) = (type == TokenType.String ? text : text.Trim(), type);

    public readonly string Text;
    public readonly TokenType Type;
    public override string ToString() => 
        Type.ToString() + ":\t\t" + $"\" {Text} \"";
}
/// <summary>
/// Types of token.
/// </summary>
public enum TokenType
{
    VMCommand,
    OpenBlockBracket,
    CloseBlockBracket,
    IfKeyWord,
    ElseKeyWord,
    FunctionDefine,
    AddingLibFile,
    String,
    Number,
    Boolean,
    Function,
    UnknownToken,
}
