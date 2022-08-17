namespace FPL.Structures;

internal class CommandBlock : IVMCodeStorer
{
    #region Constructors
    public CommandBlock() {}
    public CommandBlock(string name)
    {
        Name = name;
    }
    #endregion
    protected List<Command> commands = new List<Command>();
    public string Name { get; set; }
    

    public void Add(Command command)
    {
        if (command == null) return;
        commands.Add(command);
    }
    public Command GetLastCommand()
    {
        return commands.Last();
    }
    public string GetVMCode()
    {
        string code = "\n";
        foreach (Command c in commands)
        {
            code += c.GetVMCode() + '\n';
        }
        return code;
    }
}