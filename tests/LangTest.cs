using VirtualMachine;
using FPL;
using LanguageInterfaces;

namespace tests;
internal class LangTest
{
    public static void RunTest()
    {
        var sourseCode = File.ReadAllText("tests/plastical-hello-world.fpl");
        var vmCode = new FunctionalPlasticalLanguage().GetVMCode(sourseCode);
        try
        {
            var vmCode2 = PlasticalRunner.GetVMCode("tests/plastical-hello-world.fpl");

            VM.Run(vmCode2);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        
    }
}
