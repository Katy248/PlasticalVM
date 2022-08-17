using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VirtualMachine;

namespace FPL.Structures;
internal class Function : IVMCodeStorer
{
    #region Constructors

    public Function(string name, CommandBlock block, bool isBaseFunction = false)
    {
        this.Name = name;
        this.CommandBlock = block;
        this.IsBaseFunction = isBaseFunction;
    }

    public Function(string name, bool isBaseFunction = false) : this(name, new CommandBlock(), isBaseFunction) {}

    public Function(bool isBaseFunction = false) : this(Names.GetName(), new CommandBlock(), isBaseFunction) { }

    #endregion
    public static string GetFunctionsVmCode(IEnumerable<Function> functions)
    {
        string text = "";
        foreach (var fn in functions) text+=fn.GetVMCode();
        return text;
    }
    
    public bool IsBaseFunction { get; set; }
    public string Name { get; init; }
    public CommandBlock CommandBlock { get; init; }
    public string GetVMCode()
    {
        return $"\nlabel {Name}\n" + CommandBlock.GetVMCode() + $"{(IsBaseFunction ? VM.End : VM.Return)}\n";
    }
}
