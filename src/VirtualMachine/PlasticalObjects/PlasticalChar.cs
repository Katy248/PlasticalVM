namespace VirtualMachine.PlasticalObjects;

public class PlasticalChar : PlasticalObject
{
    public PlasticalChar(char value)
    {
        Value = value;
    }
    public readonly char Value;
    public override bool AsBool => Value == 0;
    public override decimal AsNumber => Value;
    public override char AsChar => Value;
    public override string ToString() => Value.ToString();
}