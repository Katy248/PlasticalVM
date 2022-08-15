using VirtualMachine;
using FPL.Structures;
using FPL;
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
        var code = FPL.FPL.GetVMCode(File.ReadAllText("test/TestTFPLCode.txt"));
        VM.Run(code);
        Console.ReadKey();
    }
}
