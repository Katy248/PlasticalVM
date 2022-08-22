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
    Stack<CommandBlock> blocks = new();
    protected int bracketCount = 0;
    protected List<string> LibrariesParsedCode = new();
    VMStructuresList StructuresList = new (new Function());
    public Parser(IEnumerable<Token> sourseTokens)
    {
        tokens = sourseTokens.ToList();
    }
    public string Parse()
    {
        blocks.Push(StructuresList.BaseFunction.CommandBlock);

        ParseUnknown();

        while (currentTokenIndex < tokens.Count - 1)
        {
            Next();
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
                {
                    ParseVMCommand();
                }
                break;
            case TokenType.OpenBlockBracket:
                {
                    bracketCount++;
                }
                break;
            case TokenType.CloseBlockBracket:
                {
                    if (bracketCount > 0) bracketCount--;
                    else blocks.Pop();
                }
                break;
            case TokenType.IfKeyWord:
                {
                    ParseIfConstruction();
                }
                break;
            case TokenType.ElseKeyWord:
                {
                    ParseElseConstruction();
                }
                break;

            case TokenType.String:
            case TokenType.Number:
            case TokenType.Boolean:
                {
                    ParseSimpleCommand();
                }
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
        StructuresList.AddFunction(new Function(currentToken.Text, new CommandBlock()));
        blocks.Push(StructuresList.GetLastFunction.CommandBlock);
        NextUntilMatch(TokenType.OpenBlockBracket);
    }
    void ParseIfConstruction()
    {
        var ifFunc = new Function();
        StructuresList.AddCommand(new IfElseStructure(ifFunc));
        StructuresList.AddFunction(ifFunc);

        blocks.Push(ifFunc.CommandBlock);

        NextUntilMatch(TokenType.OpenBlockBracket);
    }
    void ParseElseConstruction()
    {
        if (StructuresList.GetLastFunction.CommandBlock.GetLastCommand() is IfElseStructure ifElse)
        {
            StructuresList.AddFunction(ifElse.ElseFunction);
            blocks.Push(ifElse.ElseFunction.CommandBlock);
            NextUntilMatch(TokenType.OpenBlockBracket);
        }
        else
        {
            NextUntilMatch(TokenType.OpenBlockBracket);
            blocks.Push(new CommandBlock());
        }
    }
    void ParseLibraryAddition()
    {
        string libName = currentToken.Text.Remove(0, 1); // Remove "~"
        if (File.Exists(libName))
            LibrariesParsedCode.Add(new FunctionalPlasticalLanguage().GetVMCode(File.ReadAllText(libName)));
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
            Next();
    }
    protected Token? Next()
    {
        currentTokenIndex++;
        if (currentTokenIndex != tokens.Count - 1)
        {
            return tokens[currentTokenIndex];
        }
        return null;
        //throw new IndexOutOfRangeException();
    }
}
