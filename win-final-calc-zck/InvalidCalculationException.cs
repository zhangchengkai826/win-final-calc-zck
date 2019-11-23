using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace win_final_calc_zck
{
    class InvalidCalculationException: ApplicationException
    {
        public InvalidCalculationException(string msg) : base(msg) { }
    }
}
