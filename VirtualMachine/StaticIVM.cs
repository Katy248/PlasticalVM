using VirtualMachine.PlasticalObjects;

namespace VirtualMachine;
public partial class VM
{
    public static void Run(string sourseCode, int stackCapacity = 10000)
    {
        var vm = new VM(stackCapacity);
        vm.Run(sourseCode);
    }
    public static readonly PlasticalObject ZeroObject = new PlasticalWarning(EmptyStack);

    private static VM processedVM = new();

    public static readonly Dictionary<string, Action<string>> VMCommands = new Dictionary<string, Action<string>>()
    {
        { Pop, PopAction},
        { Push, PushAction},
        { Dup, DupAction},
        { Call, CallAction},
        { Return, ReturnAction},
        { End, EndAction},
        /*{ Read, ReadAction},*/
        { Write, WriteAction},
        { CallIf, CallIfAction},
        {Add, AddAction },
        {Sub, SubAction},
        { Mul, MulAction},
        { Div, DivAction},
        { AsNum, AsNumAction },
        { AsBool, AsBoolAction },
        {AsChar, AsCharAction},
    };

    #region VM Lang Commands

    public const string Pop = "pop";
    public const string Push = "push";
    public const string Dup = "dup";
    public const string Call = "call";
    public const string Return = "return";
    public const string End = "end";
    public const string Read = "read";
    public const string Write = "write";
    public const string Label = "label";
    public const string CallIf = "callif";
    public const string Add = "add";
    public const string Sub = "sub";
    public const string Mul = "mul";
    public const string Div = "div";
    public const string AsNum = "asnum";
    public const string AsBool = "asbool";
    public const string AsChar = "aschar";
    //end other later

    #endregion

    #region VM Lang Actions
    protected static void PopAction(string args)
    {
        processedVM.dataStack.Pop();
    }
    protected static void PushAction(string arg)
    {
        processedVM.dataStack.Push(PlasticalObjectBuilder.GetPlasticalObject(arg));
    }
    protected static void DupAction(string arg)
    {
        processedVM.dataStack.Push(processedVM.dataStack.Peek() ?? ZeroObject);
    }
    protected static void CallAction(string args)
    {
        GoToLabel(args);
    }
    protected static void ReturnAction(string args)
    {
        processedVM._currentLine = processedVM.CallStack.Pop();
    }
    protected static void EndAction(string args)
    {
        processedVM._codeLines.Clear();
    }
    protected static void CallIfAction(string args)
    {
        string[] labels = args.Split(' ');
        if (processedVM.dataStack.Pop()?.AsBool() ?? false)
        {
            GoToLabel(labels.First());
        }
        else if (labels.Length > 2)
        {
            GoToLabel(labels.Last());
        }
    }
    /*protected static void ReadAction(string args)
    {

        if (args == "fromfile")
        {
            StreamReader sr = null;
            try
            {
                sr = new StreamReader(dataStack.Pop().ToString());
                dataStack.Push(sr.ReadToEnd());
            }
            finally
            {
                if (sr != null) sr.Close();
            }
        }
        else if (args == "fromfile00")
        {
            StreamReader sr = null;
            try
            {
                sr = new StreamReader(dataStack.Pop().ToString());
                dataStack.Push(sr.Read());
            }
            finally
            {
                if (sr != null) sr.Close();
            }
        }
        else if (args == "fromfile--")
        {
            StreamReader sr = null;
            try
            {
                sr = new StreamReader(dataStack.Pop().ToString());
                dataStack.Push(sr.ReadLine());
            }
            finally
            {
                if (sr != null) sr.Close();
            }
        }
        else
        {
            dataStack.Push(VMHelp.ConvertStringToItsType(Console.ReadLine()));
        }
    }*/
    protected static void WriteAction(string args)
    {
        Console.WriteLine(processedVM.dataStack.Peek()?.ToString());
        /*if (args == "tofile")
        {
            StreamWriter sw = null;
            try
            {
                sw = new StreamWriter(dataStack.Pop().ToString());
                sw.Write(dataStack.Peek());
            }
            finally
            {
                if (sw != null) sw.Close();
            }
        }
        else if (args == "tofile+")
        {
            StreamWriter sw = null;
            try
            {
                sw = new StreamWriter(dataStack.Pop().ToString());
                sw.WriteLine(dataStack.Peek());
            }
            finally
            {
                if (sw != null) sw.Close();
            }
        }
        else
        {
            dataStack.TryPeek(out object result);
            Console.WriteLine(result);
        }
*/
    }
    #endregion

    #region VM Math Actions
    protected static void AddAction(string arg)
    {
        processedVM.dataStack.Push(
            new PlasticalNumber(
                (processedVM.dataStack.Pop()?.AsNumber() ?? 0m) +
                (processedVM.dataStack.Pop()?.AsNumber() ?? 0m)
                )
            );
    }
    protected static void SubAction(string arg)
    {
        processedVM.dataStack.Push(
            new PlasticalNumber(
                (processedVM.dataStack.Pop()?.AsNumber() ?? 0m) -
                (processedVM.dataStack.Pop()?.AsNumber() ?? 0m)
                )
            );
    }
    protected static void MulAction(string arg)
    {
        processedVM.dataStack.Push(
            new PlasticalNumber(
                (processedVM.dataStack.Pop()?.AsNumber() ?? 0m) *
                (processedVM.dataStack.Pop()?.AsNumber() ?? 0m)
                )
            );
    }
    protected static void DivAction(string arg)
    {
        var num2 = (processedVM.dataStack.Pop()?.AsNumber() ?? 0m);
        var num1 = (processedVM.dataStack.Pop()?.AsNumber() ?? 0m);

        processedVM.dataStack.Push(
            num2 != 0 ? new PlasticalNumber(num1 / num2) : new PlasticalWarning(DivisionByZero));
    }
    #endregion

    #region VM Type Actions

    protected static void AsNumAction(string arg)
    {
        var obj = processedVM.dataStack.Pop();
        if (obj != null)
        {
            processedVM.dataStack.Push(new PlasticalNumber(obj.AsNumber()));
        }
        else
        {
            processedVM.dataStack.Push(new PlasticalWarning(EmptyStack));
        }
    }
    protected static void AsBoolAction(string arg)
    {
        var obj = processedVM.dataStack.Pop();
        if (obj != null)
        {
            processedVM.dataStack.Push(new PlasticalBoolean(obj.AsBool()));
        }
        else
        {
            processedVM.dataStack.Push(new PlasticalWarning(EmptyStack));
        }
    }
    protected static void AsCharAction(string arg)
    {
        var obj = processedVM.dataStack.Pop();
        if (obj != null)
        {
            processedVM.dataStack.Push(new PlasticalChar(obj.AsChar()));
        }
        else
        {
            processedVM.dataStack.Push(new PlasticalWarning(EmptyStack));
        }
    }

    #endregion

    #region Warning Messages

    const string DivisionByZero = "Division by zero";
    const string EmptyStack = "Stack is empty";

    #endregion
}
