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
    public static void Run(string path)
    {
        string vmCode = GetVMCode(path);
        VirtualMachine.VM.Run(vmCode);
    }
    public static Dictionary<string, ILanguageTranslator> Translators = new();
}
