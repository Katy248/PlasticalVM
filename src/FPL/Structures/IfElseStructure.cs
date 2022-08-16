using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPL.Structures;
internal class IfElseStructure : Command
{
    public IfElseStructure(Function ifFunc)
    {
        ifFunction = ifFunc;
    }
    #region Fields

    private Function? ifFunction;
    private Function? elseFunction;

    #endregion
    public Function IfFunction 
    { 
        get 
        {
            if (ifFunction == null)
                ifFunction = new Function(Names.GetName(), new CommandBlock());
            return ifFunction; 
        } 
    }
    public Function ElseFunction
    {
        get
        {
            if (elseFunction == null)
                elseFunction = new Function(Names.GetName(), new CommandBlock());
            return elseFunction;
        }
    }
    public override string GetVMCode()
    {
        return $"{VirtualMachine.VM.CallIf} { ifFunction.Name } { elseFunction?.Name }";
    }
}
