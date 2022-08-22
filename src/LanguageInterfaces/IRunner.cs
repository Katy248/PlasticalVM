using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageInterfaces;
public interface ILanguageTranslator
{
    string GetVMCode(string sourse);
}
