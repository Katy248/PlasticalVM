using VirtualMachine;

namespace tests;
internal class LangTest
{
    public static void RunTest()
    {
        var code = FPL.FPL.GetVMCode(File.ReadAllText("tests/TestTFPLCode.tfpl"));
        VM.Run(code);
    }
}
