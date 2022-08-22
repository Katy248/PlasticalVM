using FPL;
using LanguageInterfaces;

/* 
 * Комманды:
 * 
 *  Run + any file path 
 *  MakeVmFile + not vm sourse file path
 *  UpdateVmFile + vm sourse file (optimize sourse file)
 */
namespace PlasticalVM;
internal class Program
{
    static void Main(string[] args)
    {
        Console.Title = "Plastical";
        PlasticalRunner.Translators.Add(".fpl", new FunctionalPlasticalLanguage());

        //args = new string[] { "tests/plastical-hello-world.fpl","run" };

        new ConsoleParser.Parser().Parse(args);
        Console.ReadKey();
    }
}
