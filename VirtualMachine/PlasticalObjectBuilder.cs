using VirtualMachine.PlasticalObjects;

namespace VirtualMachine;
public class PlasticalObjectBuilder
{
    public static PlasticalObject GetPlasticalObject(string arg)
    {
        arg = arg.Trim();
        if (decimal.TryParse(arg, out decimal dresult))
        {
            return new PlasticalNumber(dresult);
        }
        else if (bool.TryParse(arg, out bool bresult))
        {
            return new PlasticalBoolean(bresult);
        }
        else if (arg.Length == 1)
        {
            return new PlasticalChar(arg[0]);
        }
        else
        {
            List<PlasticalChar> chs = new();
            foreach (var c in arg) chs.Add(new PlasticalChar(c));
            return new PlasticalEnumeration(chs);
        }
    }
}