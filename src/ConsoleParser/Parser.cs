using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace ConsoleParser;
public class Parser
{
    /// <summary>
    /// Return <see cref="IEnumerable{T}"/> of <see cref="Token"/>.
    /// </summary>
    /// <param name="args">Array programm arguments.</param>
    /// <returns></returns>
    public IEnumerable<Token> GetTokens(IEnumerable<string> args)
    {
        foreach (string arg in args)
        {
            
            if (Regex.Match(arg, "([/\\]?)([A-Za-z-_]+[/\\])+([A-Za-z-_]+[.][A-Za-z-_]+)").Value == arg)
                yield return new Token(arg, TokenType.PathToken);
            else if (Regex.IsMatch(arg, "[A-Za-z-_]+"))
                yield return new Token(arg.ToLower(), TokenType.CommandToken);
            /*else if (Regex.Match(arg, "[-][-]?[A-Za-z]+").Value == arg)
                yield return new Token(arg, TokenType.ModifierToken);*/
            else
                yield return new Token(arg, TokenType.UnknownToken);
        }
    }
    public void Parse(string[] args)
    {
        if (args.Length == 0) return;

        var tokens = GetTokens(args);
        if (tokens.FirstOrDefault(_ => _.Type == TokenType.UnknownToken) != default)
            throw new Exception("Unknowh token in the line");

        var commands = tokens.Where(_=>_.Type==TokenType.CommandToken).ToList();
        if (commands.Count > 1)
            throw new Exception($"Arguments contains more than one command");

        var paths = tokens.Where(_ => _.Type == TokenType.PathToken).ToList();
        if (paths.Count > 1)
            throw new Exception($"Arguments contains more than one path to file");
        ParseCommand(commands.First(), paths.FirstOrDefault());
    }
    private void ParseCommand(Token command, Token? path)
    {
        if (Commands.TryGetValue(command.Text, out var Act))
        {
            Act?.Invoke(path);
        }
        else
        {
            throw new Exception("Unknown command.");
        }
    }


    #region Commands

    public Dictionary<string, Action<Token?>> Commands = new Dictionary<string, Action<Token?>>()
    {
        {"run", RunAction},
    };

    static void RunAction(Token? path)
    {
        if (path is not null)
        {
            try
            {
                LanguageInterfaces.PlasticalRunner.Run(path.Text);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }

    #endregion
}
