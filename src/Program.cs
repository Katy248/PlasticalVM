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
        //VM.Run(File.ReadAllText("TestVMCode.txt"));
        var code = FPL.FPL.GetVMCode(File.ReadAllText("test/TestFPLCode.txt"));
        using (StreamWriter sw = new StreamWriter("VMRunCodeLog.txt")) sw.Write(code);
        VM.Run(code);
        Console.ReadKey();
    }
}
