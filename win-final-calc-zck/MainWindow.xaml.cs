using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
        private static readonly Dictionary<string, Operation> strToOpMap = new Dictionary<string, Operation>()
        {
            {"+", Operation.ADD },
            {"-", Operation.SUB },
            {"x", Operation.MUL },
            {"/", Operation.DIV },
        };
        private static readonly Dictionary<Operation, BinaryOpExtern> opToFuncMap = new Dictionary<Operation, BinaryOpExtern>()
        {
            { Operation.ADD, OperationExtern.Add },
            { Operation.SUB, OperationExtern.Sub },
            { Operation.MUL, OperationExtern.Mul },
            { Operation.DIV, OperationExtern.Div },
        };
        private static readonly int INPUT_LIMIT = 16;
        private Operation lastOp = Operation.UNSPECIFIED;
        /* use double instead of string or float as internal representation to preserve more precision in a series of operations */
        private double accumulatedResult = 0.0;
        /* when we enter an operation button, e.g. +, the InputView won't be immediately cleared, but will be cleared when user enter the next input */
        private bool justPressOpBtn = false;
        /* map button text to button */
        private Dictionary<string, Button> btnMap = new Dictionary<string, Button>();
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }
        private void EnterInput(object sender, RoutedEventArgs e)
        {
            if(InputView.Text.Length >= INPUT_LIMIT)
            {
                Util.ShowWarning("Too many digits.");
                return;
            }
            if(justPressOpBtn)
            {
                ClearInputView(sender, e);
                justPressOpBtn = false;
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
                Util.ShowWarning(ex.Message);
            }
        }
        private void ClearInputView(object sender, RoutedEventArgs e)
        {
            InputView.Text = "0";
        }
        private void Reset()
        {
            lastOp = Operation.UNSPECIFIED;
            accumulatedResult = 0.0;
            ExprView.Text = "";
        }
        private void ClearAll(object sender, RoutedEventArgs e)
        {
            ClearInputView(sender, e);
            Reset();
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
            var op = strToOpMap[input];
            
            if (justPressOpBtn)
            {
                /* the user just pressed an op button, so this time, only change op but do not perform calculation */
                ExprView.Text = ExprView.Text.Substring(0, ExprView.Text.Length - 1);
                ExprView.Text += input;
            }
            else
            {
                ExprView.Text += string.Format("{0}{1}", InputView.Text, input);
                if (lastOp != Operation.UNSPECIFIED)
                {
                    /* squash */
                    var oprand = Convert.ToDouble(InputView.Text);
                    var opFunc = opToFuncMap[lastOp];
                    var result = opFunc(accumulatedResult, oprand);
                    InputView.Text = result.ToString();
                    accumulatedResult = result;
                }
                else
                {
                    /* assign init val */
                    accumulatedResult = Convert.ToDouble(InputView.Text);
                }
            }

            justPressOpBtn = true;
            lastOp = op;
        }
        private void CalcFinal(object sender, RoutedEventArgs e)
        {
            /* if user does not specify an operation before hand, do nothing */
            if (lastOp != Operation.UNSPECIFIED)
            {
                var oprand = Convert.ToDouble(InputView.Text);
                var opFunc = opToFuncMap[lastOp];
                var result = opFunc(accumulatedResult, oprand);
                InputView.Text = result.ToString();

                /* reset */
                Reset();
            }
        }
        private ICommand _keyCmd;
        public ICommand KeyCmd
        {
            get
            {
                return _keyCmd ?? (_keyCmd = new RelayCommand<string> ( x => {
                    if (btnMap.ContainsKey(x))
                    {
                        var btn = btnMap[x];
                        /* simulate button click programmatically */
                        var setIsPressedFunc = typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic);
                        setIsPressedFunc.Invoke(btn, new object[] { true });
                        btn.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
                        setIsPressedFunc.Invoke(btn, new object[] { false });
                    }
                }));
            }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (var btn in Util.FindVisualChildren<Button>(this))
            {
                var txt = Util.RetrieveTextFromBtn(btn);
                btnMap[txt] = btn;
            }
        }
    }
}
