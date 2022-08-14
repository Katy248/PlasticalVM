namespace VirtualMachine.PlasticalObjects;

public class PlasticalNumber : PlasticalObject
{
    public static readonly PlasticalNumber ZeroObject = new PlasticalNumber(0);
    public PlasticalNumber(decimal value)
    {
        Value = value;
    }
    public readonly decimal Value;
    public override bool AsBool() => Value == 0;
    public override decimal AsNumber() => Value;
    public override char AsChar() => (char)(int)Value;
    public override string ToString() => Value.ToString();
}