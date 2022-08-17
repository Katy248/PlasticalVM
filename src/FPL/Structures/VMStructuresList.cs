using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPL.Structures;
internal class VMStructuresList : IVMCodeStorer
{
    public VMStructuresList(Function baseFunction)
    {
        BaseFunction = baseFunction;
        Functions.Add(BaseFunction);
        BaseFunction.IsBaseFunction = true;
    }

    public Function BaseFunction { get; init; }

    private List<Function> Functions = new();
    public void AddFunction(Function function) => Functions.Add(function);
    public Function GetLastFunction => Functions.Last();
    public void AddCommand(Command command) => GetLastFunction.CommandBlock.Add(command);
    public string GetVMCode() => Function.GetFunctionsVmCode(Functions);
}
