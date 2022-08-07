namespace VirtualMachine.PlasticalObjects;

public class PlasticalEnumeration : PlasticalObject
{
    public PlasticalEnumeration() { }
    public PlasticalEnumeration(PlasticalObject obj)
    {
        EnumerationStack.Push(obj);
    }
    public PlasticalEnumeration(IEnumerable<PlasticalObject> objs)
    {
        objs = objs.Reverse();
        foreach (var obj in objs)
        {
            EnumerationStack.Push(obj);
        }
    }
    private DataStack<PlasticalObject> EnumerationStack = new DataStack<PlasticalObject>(new PlasticalNumber(0m));
    public override bool AsBool() => EnumerationStack.Pop()?.AsBool() ?? false;
    public override decimal AsNumber() => EnumerationStack.Pop()?.AsNumber() ?? 0m;
    public override char AsChar() => EnumerationStack.Pop()?.AsChar() ?? (char)0;
    public override string ToString()
    {
        string result = "";
        foreach (var i in EnumerationStack)
        {
            result += i.AsChar();
        }
        return result;
    }
}