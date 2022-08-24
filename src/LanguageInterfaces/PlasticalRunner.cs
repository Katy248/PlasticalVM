using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageInterfaces;
public class PlasticalRunner
{
    public static string GetVMCode(string path)
    {
        var sourseFile = new FileInfo(path);
        if (!sourseFile.Exists) throw new FileNotFoundException($"File \"{sourseFile.FullName}\" doesn't exist.");
        
        if (Translators.TryGetValue(sourseFile.Extension, out var translator))
        {
            return translator.GetVMCode(File.ReadAllText(sourseFile.FullName));
        }
        else
        {
            throw new Exception($"File extension \"{sourseFile.Extension}\" not supported.");
        }
    }
    public static void Run(string directoryPath)
    {
        Directory.SetCurrentDirectory(directoryPath);
        if (File.Exists("--.pproj"))
        {
            foreach (var item in File.ReadAllLines("--.pproj"))
            {
                string vmCode = GetVMCode(item);
                VirtualMachine.VM.Run(vmCode);
            }
        }
        else
        {
            throw new Exception($"In directory \"{directoryPath}\" project file was not found.");
        }
        
    }
    public static void RunFile(string filePath)
    {
        Directory.SetCurrentDirectory(new FileInfo(filePath).Directory.FullName ?? Directory.GetCurrentDirectory());
        string vmCode = GetVMCode(filePath);
        VirtualMachine.VM.Run(vmCode);
    }
    public static Dictionary<string, ILanguageTranslator> Translators = new();
}
