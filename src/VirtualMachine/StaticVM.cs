using VirtualMachine.PlasticalObjects;

namespace VirtualMachine;
public partial class VM
{
    public static void Run(string sourseCode, int stackCapacity = 10000)
    {
        processedVM = new VM(stackCapacity);
        processedVM.Run(sourseCode);
    }
    public static readonly PlasticalObject ZeroObject = EmptyStack;

    private static VM processedVM;

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
        {Equal, EqualAction},
        {NonEqual, NonEqualAction },
        {More, MoreAction},
        {Less, LessAction},
        {MoreOrEqual, MoreOrEqualAction},
        {LessOrEqual, LessOrEqualAction },
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
    public const string Equal = "equal";
    public const string NonEqual = "nonequal";
    public const string More = "more";
    public const string Less = "less";
    public const string MoreOrEqual = "moreorequal";
    public const string LessOrEqual = "lessorequal";
    public const string AsNum = "asnum";
    public const string AsBool = "asbool";
    public const string AsChar = "aschar";
    //end other later

    #endregion

    #region VM Lang Actions
    protected static void PopAction(string args)
    {
        processedVM.DataStack.Pop();
    }
    protected static void PushAction(string arg)
    {
        processedVM.DataStack.Push(PlasticalObjectBuilder.GetPlasticalObject(arg));
    }
    protected static void DupAction(string arg)
    {
        int.TryParse(arg.Trim(), out int index);
        processedVM.DataStack.Dup(index);
    }
    protected static void CallAction(string args)
    {
        GoToLabel(args);
    }
    protected static void ReturnAction(string args)
    {
        if (processedVM.CallStack.Count > 0)
        {
            processedVM._currentLine = processedVM.CallStack.Pop();
        }
        else
        {
            EndAction(default);
        }
    }
    protected static void EndAction(string? args)
    {
        processedVM._codeLines.Clear();
        processedVM._currentLine = int.MaxValue;
    }
    protected static void CallIfAction(string args)
    {
        string[] labels = args.Split(' ');
        if (processedVM.DataStack.Pop()?.AsBool?? false)
        {
            GoToLabel(labels.First());
        }
        else if (labels.Length > 2)
        {
            GoToLabel(labels.Last());
        }
    }
    protected static void WriteAction(string args)
    {
        Console.WriteLine(processedVM.DataStack.Peek()?.ToString());
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
        processedVM.DataStack.Push(
            new PlasticalNumber(
                (processedVM.DataStack.Pop()?.AsNumber?? 0m) +
                (processedVM.DataStack.Pop()?.AsNumber?? 0m)
                )
            );
    }
    protected static void SubAction(string arg)
    {
        processedVM.DataStack.Push(
            new PlasticalNumber(
                (processedVM.DataStack.Pop()?.AsNumber?? 0m) -
                (processedVM.DataStack.Pop()?.AsNumber?? 0m)
                )
            );
    }
    protected static void MulAction(string arg)
    {
        processedVM.DataStack.Push(
            new PlasticalNumber(
                (processedVM.DataStack.Pop()?.AsNumber?? 0m) *
                (processedVM.DataStack.Pop()?.AsNumber?? 0m)
                )
            );
    }
    protected static void DivAction(string arg)
    {
        var num2 = (processedVM.DataStack.Pop()?.AsNumber?? 0m);
        var num1 = (processedVM.DataStack.Pop()?.AsNumber?? 0m);

        processedVM.DataStack.Push(
            num2 != 0 ? new PlasticalNumber(num1 / num2) : DivisionByZero);
    }
    protected static void MoreAction(string arg)
    {
        var num2 = (processedVM.DataStack.Peek()?.AsNumber?? 0m);
        var num1 = (processedVM.DataStack.Peek(1)?.AsNumber?? 0m);

        processedVM.DataStack.Push(new PlasticalBoolean(num1 > num2));
    }
    protected static void LessAction(string arg)
    {
        var num2 = (processedVM.DataStack.Peek()?.AsNumber?? 0m);
        var num1 = (processedVM.DataStack.Peek(1)?.AsNumber?? 0m);

        processedVM.DataStack.Push(new PlasticalBoolean(num1 < num2));
    }
    protected static void EqualAction(string arg)
    {
        var num2 = (processedVM.DataStack.Peek()?.AsNumber?? 0m);
        var num1 = (processedVM.DataStack.Peek(1)?.AsNumber?? 0m);

        processedVM.DataStack.Push(new PlasticalBoolean(num1 == num2));
    }
    protected static void NonEqualAction(string arg)
    {
        var num2 = (processedVM.DataStack.Peek()?.AsNumber?? 0m);
        var num1 = (processedVM.DataStack.Peek(1)?.AsNumber?? 0m);

        processedVM.DataStack.Push(new PlasticalBoolean(num1 != num2));
    }
    protected static void MoreOrEqualAction(string arg)
    {
        var num2 = (processedVM.DataStack.Peek()?.AsNumber?? 0m);
        var num1 = (processedVM.DataStack.Peek(1)?.AsNumber?? 0m);

        processedVM.DataStack.Push(new PlasticalBoolean(num1 >= num2));
    }
    protected static void LessOrEqualAction(string arg)
    {
        var num2 = (processedVM.DataStack.Peek()?.AsNumber?? 0m);
        var num1 = (processedVM.DataStack.Peek(1)?.AsNumber?? 0m);

        processedVM.DataStack.Push(new PlasticalBoolean(num1 <= num2));
    }
    #endregion

    #region VM Type Actions

    protected static void AsNumAction(string arg)
    {
        var obj = processedVM.DataStack.Pop();
        if (obj != null)
        {
            processedVM.DataStack.Push(new PlasticalNumber(obj.AsNumber));
        }
        else
        {
            processedVM.DataStack.Push(EmptyStack);
        }
    }
    protected static void AsBoolAction(string arg)
    {
        var obj = processedVM.DataStack.Pop();
        if (obj != null)
        {
            processedVM.DataStack.Push(new PlasticalBoolean(obj.AsBool));
        }
        else
        {
            processedVM.DataStack.Push(EmptyStack);
        }
    }
    protected static void AsCharAction(string arg)
    {
        var obj = processedVM.DataStack.Pop();
        if (obj != null)
        {
            processedVM.DataStack.Push(new PlasticalChar(obj.AsChar));
        }
        else
        {
            processedVM.DataStack.Push(EmptyStack);
        }
    }

    #endregion

    #region Warning Messages

    static readonly PlasticalWarning LabelDoesNotExist = new PlasticalWarning("Label desn't exist");
    static readonly PlasticalWarning DivisionByZero = new PlasticalWarning("Division by zero");
    static readonly PlasticalWarning EmptyStack = new PlasticalWarning("Stack is empty");

    #endregion
}
