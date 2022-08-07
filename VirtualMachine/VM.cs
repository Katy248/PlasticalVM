using VirtualMachine.PlasticalObjects;

namespace VirtualMachine;
public partial class VM
{
    #region Constructors

    public VM(int stackCapacity)
    {
        dataStack = new DataStack<PlasticalObject>(ZeroObject, stackCapacity);
        CallStack = new(stackCapacity);

        processedVM = this;
    }
    /// <summary>
    /// Useless default constructor.
    /// </summary>
    private VM() { }

    #endregion

    private readonly DataStack<PlasticalObject> dataStack;
    private readonly Stack<int> CallStack;
    private List<string> _codeLines;
    public int _currentLine;

    public void Run(string code)
    {
        //code = Regex.Replace(code, @"({#)([^}]*)(#})|([#][^\n]*[\n])", "");
        _codeLines = code.Trim().Split('\n').ToList();
        for (_currentLine = 0; _currentLine < _codeLines.Count; _currentLine++)
        {
            if (_codeLines[_currentLine].Trim() != "")
            {
                string command = _codeLines[_currentLine].Trim().Split(' ').First();
                string args = _codeLines[_currentLine].Trim().Replace(command, "").Trim();
                if (VMCommands.TryGetValue(command, out Action<string> action))
                {
                    action(args);
                    if (command == End) return;
                }
                else
                {
                    //add errors
                }
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
    }
}