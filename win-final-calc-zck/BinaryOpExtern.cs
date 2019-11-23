using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace win_final_calc_zck
{
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate double BinaryOpExtern(double a, double b);
}
