using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VirtualMachine;

namespace FPL.Structures;
internal class Function : IVMCodeStorer
{
    public static string GetFunctionsVmCode(IEnumerable<Function> functions)
    {
        string text = "";
        foreach (var fn in functions)
        {
            text+=fn.GetVMCode();
        }
        return text;
    }
    public Function(string name, CommandBlock block, bool isBaseFunction = false)
    {
        Name = name;
        FunctionCommandBlock = block;
        IsBaseFunction = isBaseFunction;
    }
    public bool IsBaseFunction;
    public string Name { get; set; }
    public CommandBlock FunctionCommandBlock;
    public string GetVMCode()
    {
        return $"\nlabel {Name}\n" + FunctionCommandBlock.GetVMCode() + $"{(IsBaseFunction ? VM.End : VM.Return)}\n";
    }
}
