using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace win_final_calc_zck
{
    public class IncorrectInputFormatException : ApplicationException
    {
        public IncorrectInputFormatException(string msg) : base(msg) { }
    }
}
