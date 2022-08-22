using VirtualMachine;
using FPL.Structures;
using FPL;
using LanguageInterfaces;
namespace PlasticalVM;


/* 
 * Комманды:
 * 
 *  Run + any file path 
 *  MakeVmFile + not vm sourse file path
 *  UpdateVmFile + vm sourse file (optimize sourse file)
 */

internal class Program
{
    static void Main(string[] args)
    {
        Console.Title = "Plastical";
        PlasticalRunner.Translators.Add(".fpl", new FunctionalPlasticalLanguage());

        if (args.Length == 0) return;
        if (!string.IsNullOrEmpty(args[0]))
        {
            try
            {
                var vmCode2 = PlasticalRunner.GetVMCode(args[0]);

                VM.Run(vmCode2);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        //tests.LangTest.RunTest();
        Console.ReadKey();
    }
}
