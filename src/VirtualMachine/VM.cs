using VirtualMachine.PlasticalObjects;

namespace VirtualMachine;
public partial class VM
{
    #region Constructors
    private VM(int stackCapacity)
    {
        DataStack = new VirtualMachine.Stack<PlasticalObject>(ZeroObject, stackCapacity);
        CallStack = new(stackCapacity);
        _currentLine = 0;

    }
    #endregion

    private readonly VirtualMachine.Stack<PlasticalObject> DataStack;
    private readonly System.Collections.Generic.Stack<int> CallStack;
    private List<string> _codeLines { get; set; }
    private int _currentLine;

    public void Run(string code)
    {
        
        //code = Regex.Replace(code, @"({#)([^}]*)(#})|([#][^\n]*[\n])", "");
        _codeLines = code.Trim().Split('\n').ToList();
        for (_currentLine = 0; _currentLine < _codeLines.Count; _currentLine++)
            if (_codeLines[_currentLine].Trim() != "")
            {
                string command = _codeLines[_currentLine].Trim().Split(' ').First();
                string args = _codeLines[_currentLine].Trim().Replace(command, "").Trim();
                if (VMCommands.TryGetValue(command, out Action<string>? action))
                {
                    action(args);
                    if (command == End) return;
                }
                else
                {

                }
            }
    }
    protected static void GoToLabel(string label)
    {
        var labelLine = processedVM._codeLines
            .Select((text, index) => (text, index))
            .FirstOrDefault(line =>
            {
                string[] ln = line.text.Trim().Split(' ');
                return ln.First() == Label & ln.Last() == label;
            });
        if (labelLine != default)
        {
            processedVM.CallStack.Push(processedVM._currentLine);
            processedVM._currentLine = labelLine.index;
        }
        else
        {
            processedVM.DataStack.Push(LabelDoesNotExist);
        }
    }
}