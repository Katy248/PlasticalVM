using VirtualMachine;
namespace PlasticalVM;

internal class Program
{
    static void Main(string[] args)
    {
        /*Console.WriteLine("Hello, World!");
        DataStack<string> stack = new DataStack<string>("");
        Console.WriteLine("Poped obj is {0} .", stack.Pop());*/
        VM.Run(File.ReadAllText("TestVMCode.txt"));
    }
}
