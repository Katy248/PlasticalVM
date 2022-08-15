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
    private Dictionary<string, int> _labels = new Dictionary<string, int>();
    public void Run(string code)
    {
        _codeLines = code.Trim().Split('\n').ToList();

        for (_currentLine = 0; _currentLine < _codeLines.Count; _currentLine++)
        {
            Span<string> words = _codeLines[_currentLine].Trim().Split(' ');
            if (words.Length == 2 && words[0] == VM.Label)
                _labels.Add(words[1], _currentLine);
        }
        for (_currentLine = 0; _currentLine < _codeLines.Count; _currentLine++)
        {
            if (_codeLines[_currentLine].Trim() == "") continue;

            string command = _codeLines[_currentLine].Trim().Split(' ').First();
            string args = _codeLines[_currentLine].Trim().Replace(command, "").Trim();
            if (VMCommands.TryGetValue(command, out Action<string>? action))
                action(args);
            else {}
        }
    }
    protected static void GoToLabel(string label)
    {
        if(processedVM._labels.TryGetValue(label, out int linePose))
            processedVM._currentLine = linePose;
        else
            processedVM.DataStack.Push(LabelDoesNotExist);
    }
}