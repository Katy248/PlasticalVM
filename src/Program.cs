using ConsoleOptions;
using CommandLine;

namespace PlasticalVM;
internal class Program
{
    static int Main(string[] args)
    {
        LanguageInterfaces.PlasticalRunner.Translators.Add(".fpl", new FPL.FunctionalPlasticalLanguage());

        try
        {
            return Parser.Default.ParseArguments<RunOptions, RunFileOptions>(args)
            .MapResult(
            (RunOptions opts) => RunOptions.Action(opts),
            (RunFileOptions opts) => RunFileOptions.Action(opts),
            err => { return 1; }
            );
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        return -2;
        
    }
}
