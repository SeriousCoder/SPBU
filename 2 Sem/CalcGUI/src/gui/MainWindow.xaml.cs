using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using CalcLib;
using gui;
using Button = System.Windows.Controls.Button;


namespace CalcUI
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private CalcClass _calc;

        public MainWindow()
        {
            InitializeComponent();

            _calc = new CalcClass();

            b0.Click += bSymbol_Click;
            b1.Click += bSymbol_Click;
            b2.Click += bSymbol_Click;
            b3.Click += bSymbol_Click;
            b4.Click += bSymbol_Click;
            b5.Click += bSymbol_Click;
            b6.Click += bSymbol_Click;
            b7.Click += bSymbol_Click;
            b8.Click += bSymbol_Click;
            b9.Click += bSymbol_Click;
            ToadButton.Click += bSymbol_Click;
            EButton.Click += bSymbol_Click;
            PiButton.Click += bSymbol_Click;
            PlusButton.Click += bSymbol_Click;
            MinusButton.Click += bSymbol_Click;
            DivButton.Click += bSymbol_Click;
            MultiButton.Click += bSymbol_Click;
            PowButton.Click += bSymbol_Click;
            SqrtButton.Click += bSymbol_Click;
            OpenButton.Click += bSymbol_Click;
            CloseButton.Click += bSymbol_Click;
            SinButton.Click += bSymbol_Click;
            CosButton.Click += bSymbol_Click;
            TanButton.Click += bSymbol_Click;
            LogButton.Click += bSymbol_Click;
            FactButton.Click += bSymbol_Click;
            XButton.Click += bSymbol_Click;
            ClearButton.Click += bClear_Click;
            EditButton.Click += bEdit_Click;
            EnterButton.Click += bEnter_Click;

            ToGraghButton.Click += bBuildGraph_Click;
        }

        void bBuildGraph_Click(object sender, RoutedEventArgs e)
        {
            var graph = new Form1(this.TextBuf.Text);

            this.IsEnabled = false;
            graph.Show();

            while (graph.IsDisposed)
            {
                
            }

            this.IsEnabled = true;
        }

        void bEnter_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.TextBuf.Text != "")
                {
                    _calc.ParsToPolish(this.TextBuf.Text);
                    this.TextBuf.Text = _calc.Calc().ToString();
                }
            }
            catch (Exception)
            {
                this.TextBuf.Text = "Error";
            }
            
        }

        void bEdit_Click(object sender, RoutedEventArgs e)
        {
            if (TextBuf.Text.Length != 0)
            {
                this.TextBuf.Text = this.TextBuf.Text.Substring(0, TextBuf.Text.Length - 1);
            }
        }

        void bClear_Click(object sender, RoutedEventArgs e)
        {
            this.TextBuf.Text = "";
        }

        void bSymbol_Click(object sender, RoutedEventArgs e)
        {
            Int_Click(((Button) sender).Tag.ToString());
        }

        void Int_Click(string s)
        {
            this.TextBuf.Text += s;
        }
    }
}
