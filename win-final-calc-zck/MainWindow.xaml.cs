using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace win_final_calc_zck
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static readonly Dictionary<string, Operation> opMap = new Dictionary<string, Operation>()
        {
            {"+", Operation.ADD },
            {"-", Operation.SUB },
            {"x", Operation.MUL },
            {"/", Operation.DIV },
        };
        private Operation op = Operation.UNSPECIFIED;
        /* when we enter an operation button, e.g. +, the InputView won't be immediately cleared, but will be cleared when user enter the next input */
        private bool prepareToClear = false;
        public MainWindow()
        {
            InitializeComponent();
        }
        private void EnterInput(object sender, RoutedEventArgs e)
        {
            if(prepareToClear)
            {
                Clear(sender, e);
                prepareToClear = false;
            }
            var input = Util.RetrieveTextFromBtn(sender as Button);
            try
            {
                if (input == "." && InputView.Text.Contains("."))
                {
                    throw new IncorrectInputFormatException("Cannot put two dots in a number.");
                }
                if (InputView.Text == "0")
                {
                    if (input == "0")
                    {
                        /* do nothing */
                    }
                    else if (input != ".")
                    {
                        InputView.Text = input; /* overwrite */
                    }
                    else
                    {
                        InputView.Text += input; /* append */
                    }
                }
                else
                {
                    /* append */
                    InputView.Text += input;
                }
            }
            catch(IncorrectInputFormatException ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void Clear(object sender, RoutedEventArgs e)
        {
            InputView.Text = "0";
        }
        private void ClearAll(object sender, RoutedEventArgs e)
        {
            Clear(sender, e);
            op = Operation.UNSPECIFIED;
        }
        private void Backspace(object sender, RoutedEventArgs e)
        {
            if(InputView.Text.Length == 1)
            {
                InputView.Text = "0";
            }
            else
            {
                InputView.Text = InputView.Text.Substring(0, InputView.Text.Length - 1);
            }
        }
        private void DoOperation(object sender, RoutedEventArgs e)
        {
            var input = Util.RetrieveTextFromBtn(sender as Button);
            prepareToClear = true;
            ExprView.Text += string.Format("{0}{1}", InputView.Text, input);
            op = opMap[input];
            //switch (op)
            //{
                
            //}
        }
    }
}
