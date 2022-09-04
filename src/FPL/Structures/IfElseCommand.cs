using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPL.Structures;
internal class IfElseCommand : Command
{
    public IfElseCommand(Token? ifTrueFunction, Token? elseFunction)
    {
        if (ifTrueFunction is not null && ifTrueFunction.Type == TokenType.Function)
        {
            _ifTrueFunction = ifTrueFunction.Text;
        }
        else _ifTrueFunction = "_";

        if (elseFunction is not null && elseFunction.Type == TokenType.Function)
        {
            _elseFunction = elseFunction.Text;
        }
        else _elseFunction = "_";
        
    }
    #region Fields

    private string _ifTrueFunction;
    private string _elseFunction;

    #endregion
    public override string GetVMCode()
    {
        return $"{VirtualMachine.VM.CallIf} { _ifTrueFunction } { _elseFunction }";
    }
}
