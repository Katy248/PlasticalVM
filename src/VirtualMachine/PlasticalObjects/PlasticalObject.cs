namespace VirtualMachine.PlasticalObjects;
/// <summary>
/// Plastical VM object.
/// </summary>
public abstract class PlasticalObject
{
    public abstract bool AsBool { get; }
    public abstract decimal AsNumber { get; }
    public abstract char AsChar { get; }
    public override bool Equals(object? obj)
    {
        if (obj == null) return false;
        if (!(obj is PlasticalObject)) return false;
        return ((PlasticalObject)obj).AsNumber == this.AsNumber;
    }
}