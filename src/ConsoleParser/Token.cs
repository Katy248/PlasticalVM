using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleParser;
/// <summary>
/// Console token.
/// </summary>
public class Token
{
    /// <summary>
    /// Constructor of console token.
    /// </summary>
    /// <param name="text">Token's text.</param>
    /// <param name="type">Token's type.</param>
    public Token(string text, TokenType type) =>
        (Text, Type) = (text, type);

    /// <summary>
    /// Token's text.
    /// </summary>
    public readonly string Text;
    /// <summary>
    /// Token's type.
    /// </summary>
    public readonly TokenType Type;
    /// <inheritdoc/>
    public override string ToString() =>
        Type.ToString() + ":\t\t" + $"\" {Text} \"";
}
/// <summary>
/// Types of console tokens.
/// </summary>
public enum TokenType
{
    /// <summary>
    /// Path to file.
    /// </summary>
    PathToken,
    /// <summary>
    /// Some command.
    /// </summary>
    CommandToken,
    /// <summary>
    /// Command modifier.
    /// </summary>
    ModifierToken,
    /// <summary>
    /// Unknown token
    /// </summary>
    UnknownToken,
}
