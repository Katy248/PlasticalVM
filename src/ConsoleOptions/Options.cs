using CommandLine;
using CommandLine.Text;

namespace ConsoleOptions;

[Verb("run", HelpText="Execute file in specified directory.")]
public class RunOptions
{
    [Value(1,Required = true, HelpText = "Path to project directory.", MetaName ="directoryPath")]
    public string? DirectoryPath { get; set; }
    public static int Action(RunOptions option)
    {
        if (Directory.Exists(option.DirectoryPath))
        {
            try
            {
                LanguageInterfaces.PlasticalRunner.Run(option.DirectoryPath);
                return 0;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
            }
        }
        else
        {
            Console.Error.WriteLine($"Directory \"{option.DirectoryPath}\" was not found.");
        }
        return -1;
    }
}
[Verb("runfile", HelpText = "Execute file.")]
public class RunFileOptions
{
    [Value(1, Required = true, HelpText = "Path to file.", MetaName = "filePath")]
    public string? FilePath { get; set; }
    public static int Action(RunFileOptions option)
    {
        return 0;
    }
}