using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pcmcsvparse
{
    interface ICsvParser
    {
        bool HasColumn(string name);
        float GetMax(string column);
    }
}
