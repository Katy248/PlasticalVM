namespace VirtualMachine.PlasticalObjects;
internal class PlasticalWarning : PlasticalObject
{
    public PlasticalWarning(string message)
    {
        Message = message;
    }
    public readonly string Message;
    public override bool AsBool() => false;

    public override char AsChar() => (char)0;

    public override decimal AsNumber() => 0m;
    public override string ToString()
    {
        return Message;
    }
}
