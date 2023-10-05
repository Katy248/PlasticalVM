namespace FPL;
public record Token(string Text, TokenType Type);
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
