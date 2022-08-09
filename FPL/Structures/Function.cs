using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VirtualMachine;

namespace FPL.Structures;
internal class Function : IVMCodeStorer
{
    static List<Function> functions = new();
    public static string GetFunctionsVmCode()
    {
        string text = "";
        foreach (var fn in functions)
        {
            text+=fn.GetVMCode();
        }
        return text;
    }
    public Function(string name, CommandBlock block)
    {
        Name = name;
        FunctionCodeBlock = block;
        functions.Add(this);
    }
    public string Name { get; set; }
    public CommandBlock FunctionCodeBlock;
    public string GetVMCode()
    {
        return $"\nlabel {Name}\n" + FunctionCodeBlock.GetVMCode() + $"{(functions.IndexOf(this) == 0 ? VM.End : VM.Return)}\n";
    }
}
