namespace VirtualMachine.PlasticalObjects;

public class PlasticalBoolean : PlasticalObject
{
    public PlasticalBoolean(bool value)
    {
        Value = value;
    }
    public readonly bool Value;
    public override bool AsBool => Value;
    public override decimal AsNumber => Value ? 1m : 0m;
    public override char AsChar => (char)(int)AsNumber;
    public override string ToString() => Value.ToString();
}