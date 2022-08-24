using ConsoleOptions;
using CommandLine;

namespace PlasticalVM;
internal class Program
{
    static int Main(string[] args)
    {
        LanguageInterfaces.PlasticalRunner.Translators.Add(".fpl", new FPL.FunctionalPlasticalLanguage());

        return Parser.Default.ParseArguments<RunOptions, RunFileOptions>(args)
            .MapResult(
            (RunOptions opts) => RunOptions.Action(opts),
            (RunFileOptions opts) => RunFileOptions.Action(opts),
            err => { return 1; }
            );
    }
}
