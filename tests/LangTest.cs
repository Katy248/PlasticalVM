using VirtualMachine;

namespace tests;
internal class LangTest
{
    public static void RunTest()
    {
        var code = FPL.FPL.GetVMCode(File.ReadAllText("tests/plastical-hello-world.fpl"));
        VM.Run(code);
    }
}
