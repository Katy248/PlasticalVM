namespace VirtualMachine.PlasticalObjects;

public class PlasticalEnumeration : PlasticalObject
{
    public PlasticalEnumeration() { }
    public PlasticalEnumeration(PlasticalObject obj)
    {
        EnumerationStack.Push(obj);
    }
    public PlasticalEnumeration(IEnumerable<PlasticalObject> objs, bool isReversed = false)
    {
        if (!isReversed) objs = objs.Reverse();
        foreach (var obj in objs)
        {
            EnumerationStack.Push(obj);
        }
    }
    private Stack<PlasticalObject> EnumerationStack = new Stack<PlasticalObject>(new PlasticalNumber(0m));
    public override bool AsBool => EnumerationStack.Pop()?.AsBool ?? false;
    public override decimal AsNumber => EnumerationStack.Pop()?.AsNumber ?? 0m;
    public override char AsChar => EnumerationStack.Pop()?.AsChar ?? (char)0;
    public override string ToString()
    {
        string result = "";
        foreach (var i in EnumerationStack)
        {
            result += i.AsChar;
        }
        return result;
    }
    public override bool Equals(object? obj)
    {
        if (obj == null) return false;
        if (!(obj is PlasticalObject)) return false;
        if (!(obj is PlasticalEnumeration enumObj)) 
            return false;
        else
            return enumObj.EnumerationStack.SequenceEqual(this.EnumerationStack);
    }
    public override int GetHashCode() => base.GetHashCode();
}