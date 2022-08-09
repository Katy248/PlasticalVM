using VirtualMachine;
namespace FPL.Structures;

internal class Command: IVMCodeStorer
{
    #region Constructors

    protected Command() : this("") {}
    public Command(string vmCode)
    {
        VMCode = vmCode;
    }
    public Command(Token token)
    {
        switch (token.Type)
        {
            case TokenType.Number:
                VMCode = $"{VM.Push} {token.Text}";
                break;
            case TokenType.String:
                VMCode = $"{VM.Push} {token.Text.Replace('\'', ' ')}";
                break;
            case TokenType.Boolean:
                VMCode = $"{VM.Push} {token.Text.Trim()}";
                break;
            case TokenType.Function:
                VMCode = $"{VM.Call} {token.Text}";
                break;
            default:
                VMCode = "///there parse error///";
                break;
        }
    }

    #endregion

    public string VMCode { get; set; }
    public virtual string GetVMCode() => VMCode;
}