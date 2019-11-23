using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace win_final_calc_zck
{
    public static class Util
    {
        public static string RetrieveTextFromBtn(Button btn)
        {
            var tb = btn.Content as TextBlock;
            if (tb == null)
            {
                throw new InvalidOperationException();
            }
            return tb.Text;
        }
        public static string DoubleToStr(double a, int precision)
        {
            return "";
        }
    }
}
