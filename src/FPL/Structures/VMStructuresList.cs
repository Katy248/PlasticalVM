using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPL.Structures;
internal class VMStructuresList : IVMCodeStorer
{
    public VMStructuresList() : this(new Function()) { }
    public VMStructuresList(Function baseFunction)
    {
        BaseFunction = baseFunction;
        Functions.Add(BaseFunction);
        BaseFunction.IsBaseFunction = true;

        functionsStack = new Stack<Function>();
        functionsStack.Push(baseFunction);
    }
    public Function BaseFunction { get; init; }
    private List<Function> Functions = new();

    Stack<Function> functionsStack;
    
    public void AddFunction(Function function)
    {
        Functions.Add(function);
        functionsStack.Push(function);
    }
    public void CloseFunction() => functionsStack.Pop();
    public Function GetLastFunction => functionsStack.Peek();
    public void AddCommand(Command command) => GetLastFunction.CommandBlock.Add(command);
    public string GetVMCode() => Function.GetFunctionsVmCode(Functions);
}
