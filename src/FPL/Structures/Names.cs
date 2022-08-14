using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPL.Structures;
public class Names
{
    private static long lastNameNum = 0;
    private static long GetNewNameNum => ++lastNameNum;
    public static string GetName() => $":lb#{GetNewNameNum}:";
}
