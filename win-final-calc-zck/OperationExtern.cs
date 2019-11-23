using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace win_final_calc_zck
{
    public static class OperationExtern
    {
        [DllImport("operation.dll", EntryPoint = "add", CallingConvention = CallingConvention.StdCall)]
        public static extern double Add(double a, double b);
        [DllImport("operation.dll", EntryPoint = "sub", CallingConvention = CallingConvention.StdCall)]
        public static extern double Sub(double a, double b);
        [DllImport("operation.dll", EntryPoint = "mul", CallingConvention = CallingConvention.StdCall)]
        public static extern double Mul(double a, double b);
        [DllImport("operation.dll", EntryPoint = "div", CallingConvention = CallingConvention.StdCall)]
        public static extern double Div(double a, double b);
    }
}
