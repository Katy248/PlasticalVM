namespace VirtualMachine;

public class PlasticalArray : PlasticalObject
{
    public PlasticalArray(decimal value)
    {
        Value = value;
    }
    public readonly decimal Value;
    public override bool AsBool() => Value == 0;
    public override decimal AsNumber() => Value;
    public override char AsChar() => (char)(int)Value;
}