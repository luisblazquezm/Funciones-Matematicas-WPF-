using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;

namespace WinMaths
{
	public class NumericTextBoxClass : TextBox
	{
        public NumericTextBoxClass()
        {
            this.PreviewTextInput += new TextCompositionEventHandler(NumericTextBox_PreviewTextInput);
        }

        public int IntValue
        {
            get
            {
                return Int32.Parse(this.Text.Replace('.', ','));
            }
        }

        public double DoubleValue
        {
            get
            {
                return Double.Parse(this.Text.Replace('.', ','));
            }
        }

        private void NumericTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex reg = new Regex(@"^-?[0-9]*(\,[0-9]*)?$");

            if ((reg.IsMatch(e.Text) && !(e.Text == "," && ((TextBox)sender).Text.Contains(e.Text)) && !(e.Text == "-" && ((TextBox)sender).Text.Contains(e.Text))))
                e.Handled = false;
            else
                e.Handled = true;
        }
    }
}