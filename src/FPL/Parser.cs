using System;
using System.Collections.Generic;
using System.Linq;
using FPL.Structures;
using System.Threading.Tasks;

namespace FPL;
internal class Parser
{
    public static string GetVMCode(IEnumerable<Token> sourseTokens) => new Parser(sourseTokens).Parse();
    protected int currentTokenIndex = 0;
    protected Token currentToken => tokens[currentTokenIndex];
    protected List<Token> tokens;
    protected int bracketCount = 0;
    protected List<string> LibrariesParsedCode = new();
    VMStructuresList StructuresList = new (new Function());
    public Parser(IEnumerable<Token> sourseTokens)
    {
        tokens = sourseTokens.ToList();
    }
    public string Parse()
    {
        //blocks.Push(StructuresList.BaseFunction.CommandBlock);

        ParseUnknown();

        while (Next() != null)
        {
            ParseUnknown();
        }
        string vmCode = StructuresList.GetVMCode();
        foreach (var lib in LibrariesParsedCode)
        {
            vmCode += lib;
        }
        return vmCode;
    }
    void ParseUnknown()
    {
        switch (currentToken.Type)
        {
            case TokenType.VMCommand:
                    ParseVMCommand();
                break;
            
            case TokenType.IfKeyWord:
                    ParseIfConstruction();
                break;
            case TokenType.String:
            case TokenType.Number:
            case TokenType.Boolean:
                    ParseSimpleCommand();
                break;
            case TokenType.Function:
                {
                    if (MatchNext(TokenType.FunctionDefine)) ParseFuncDeclare();
                    else ParseSimpleCommand();
                }
                break;
            case TokenType.AddingLibFile:
                ParseLibraryAddition();
                break;
            case TokenType.ElseKeyWord:
            case TokenType.OpenBlockBracket:
            case TokenType.CloseBlockBracket:
            case TokenType.FunctionDefine:
            case TokenType.UnknownToken:
            default:
                break;
        }
    }
    void ParseVMCommand()
    {
        string vmCommand = currentToken.Text.Replace('[', ' ').Replace(']', ' ').Trim();
        StructuresList.AddCommand(new Command(vmCommand));
    }
    void ParseSimpleCommand()
    {
        StructuresList.AddCommand(new Command(currentToken));
    }
    void ParseFuncDeclare()
    {
        StructuresList.CloseFunction();
        StructuresList.AddFunction(new Function(currentToken.Text, new CommandBlock()));
    }
    void ParseIfConstruction()
    {
        StructuresList.AddCommand(new IfElseCommand(Next(),Next()));
    }
    void ParseLibraryAddition()
    {
        string libName = currentToken.Text.Remove(0, 1); // Remove "~"
        if (File.Exists(libName))
            LibrariesParsedCode.Add(LanguageInterfaces.PlasticalRunner.GetVMCode(libName));
    }
    protected bool Match(TokenType type)
    {
        return Match(currentToken, type);
    }
    protected bool MatchNext(TokenType type)
    {
        if (currentTokenIndex == tokens.Count - 1) return false;
        return Match(tokens[currentTokenIndex + 1], type);
    }
    protected bool Match(Token checkedToken, TokenType type) => checkedToken.Type == type;
    void NextUntilMatch(TokenType type)
    {
        while (!Match(type))
            if(Next()==null) return;
    }
    protected Token? Next()
    {
        if (currentTokenIndex == tokens.Count - 1) return null;
        currentTokenIndex++;
        return tokens[currentTokenIndex];
        //throw new IndexOutOfRangeException();
    }
}
