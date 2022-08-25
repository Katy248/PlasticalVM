using CommandLine;

namespace ConsoleOptions;

[Verb("run", HelpText="Execute project in the specified directory.")]
public class RunOptions
{
    [Value(1,Required = true, HelpText = "Path to project directory.", MetaName ="Directory path")]
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
[Verb("runfile", HelpText = "Execute specified file.")]
public class RunFileOptions
{
    [Value(1, Required = true, HelpText = "Path to file.", MetaName = "File path")]
    public string? FilePath { get; set; }
    public static int Action(RunFileOptions option)
    {
        if (File.Exists(option.FilePath))
        {
            try
            {
                LanguageInterfaces.PlasticalRunner.RunFile(option.FilePath);
                return 0;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
            }
        }
        else
        {
            Console.Error.WriteLine($"File \"{option.FilePath}\" was not found.");
        }
        return -1;
    }
}