using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPL.Structures;
internal class IfElse : Command
{
    public IfElse()
    {
        ifElses.Add(this);
    }

    static List<IfElse> ifElses = new();
    public static CommandBlock GetLastIf()
    {
        if (ifElses.Last().IfBlock != null)
        {
            return null;
        }
        else
        {
            ifElses.Last().IfBlock = new CommandBlock();
            return ifElses.Last().IfBlock;
        }
    }
    public static (bool isExist, CommandBlock? elseBlock) GetLastElse()
    {   var ie = ifElses.LastOrDefault(_ => _.ElseBlock == null);
        if (ie != null)
        {
            ie.ElseBlock = new CommandBlock();
            return (true, ie.ElseBlock);
        }
        else return (false, null);
    }
    #region Fields

    private CommandBlock ifBlock;
    private CommandBlock elseBlock;

    #endregion
    public CommandBlock IfBlock 
    { 
        get => ifBlock;
        set 
        {
            ifBlock = value;
            new Function(value.Name, value);
        }
    }
    public CommandBlock ElseBlock
    {
        get => elseBlock;
        set
        {
            elseBlock = value;
            new Function(value.Name, value);
        }
    }
    public override string GetVMCode()
    {
        if (ElseBlock!=null)
        {
            return $"{VirtualMachine.VM.CallIf} {IfBlock.Name} {ElseBlock.Name}";
        }
        return $"{VirtualMachine.VM.CallIf} {IfBlock.Name}";
    }
}
