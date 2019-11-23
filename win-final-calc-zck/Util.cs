using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

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
        public static IEnumerable<T> FindVisualChildren<T>(DependencyObject o) where T : DependencyObject
        {
            if (o != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(o); i++)
                {
                    var child = VisualTreeHelper.GetChild(o, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }
        public static string ToLimitLengthString(double x, int limit)
        {
            var fmtStr = string.Format("{{0:G{0}}}", limit);
            return string.Format(fmtStr, x);
        }
        public static void ShowWarning(string msg)
        {
            MessageBox.Show(msg, "WARNING", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }
}
