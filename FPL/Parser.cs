using System;
using System.Collections.Generic;
using System.Linq;
using FPL.Structures;
using System.Threading.Tasks;

namespace FPL;
internal class Parser
{
    protected int currentTokenIndex = 0;
    protected Token currentToken => tokens[currentTokenIndex];
    protected List<Token> tokens;
    Stack<CommandBlock> blocks = new();
    protected int bracketCount = 0;
    protected List<string> LibrariesParsedCode = new();
    public Parser(IEnumerable<Token> sourseTokens)
    {
        tokens = sourseTokens.ToList();
    }
    public string Parse()
    {
        #region First of all
        currentTokenIndex = 0;
        bracketCount = 0;
        blocks.Clear();

        #endregion
        /*foreach (var lib in librariesTokens)
        {
            string library = lib.Text.Remove(0, 1);
            if (File.Exists(library))
            {
                using (StreamReader sr = new StreamReader(library))
                    tokens.AddRange(Lexer.GetTokens(sr.ReadToEnd()));

            }
        }*/
        CommandBlock baseBlock = new();
        new Function(Names.GetName(), baseBlock);
        blocks.Push(baseBlock);

        ParseUnknown();

        while (currentTokenIndex < tokens.Count - 1)
        {
            Next();
            ParseUnknown();
        }
        string vmCode = Function.GetFunctionsVmCode();
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
                    ParseIfElseConstruction();
                }
                break;
            case TokenType.ElseKeyWord:
                {
                    ParseElseConstruction();
                }
                break;

            case TokenType.String:
            case TokenType.Number:
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
        string vmCommand = currentToken.Text.Replace('[', ' ').Replace(']', ' ');
        blocks.Peek().Add(new Command(vmCommand));
    }
    void ParseSimpleCommand()
    {
        blocks.Peek().Add(new Command(currentToken));
    }
    void ParseFuncDeclare()
    {
        var funcBlock = new CommandBlock();
        new Function(currentToken.Text, funcBlock);
        blocks.Push(funcBlock);
        NextUntilMatch(TokenType.OpenBlockBracket);
    }
    void ParseIfElseConstruction()
    {
        //var ifblock = new CodeBlock();

        blocks.Peek().Add(new IfElse());
        blocks.Push(IfElse.GetLastIf());

        NextUntilMatch(TokenType.OpenBlockBracket);
    }
    void ParseElseConstruction()
    {
        if (!IfElse.GetLastElse().isExist)
        {
            blocks.Push(IfElse.GetLastElse().elseBlock);
            NextUntilMatch(TokenType.OpenBlockBracket);
        }
        else
        {
            blocks.Push(new CommandBlock());
        }
    }
    void ParseLibraryAddition()
    {
        string libName = currentToken.Text.Remove(0, 1);
        if (File.Exists(libName))
        {
            var tokens = Lexer.GetTokens(File.ReadAllText(libName));
            LibrariesParsedCode.Add(new Parser(tokens).Parse());
        }
    }
    protected bool Match(TokenType type)
    {
        return Match(currentToken, type);
    }
    protected bool MatchNext(TokenType type)
    {
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
