using VirtualMachine;
using FPL.Structures;
using FPL;

namespace tests;
internal class LangTest
{
    public static void RunTest()
    {
        var code = FPL.FPL.GetVMCode(File.ReadAllText("test/TestFPLCode.txt"));
        VM.Run(code);
    }
}
