using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WinMaths.src.utils
{
    public class Utils
    {
        // CONSTANTS
        /*
         * OK = 0
         * OKCancel = 1
         * YesNo = 2;
         * YesNoCancel = 3;
         */
        public void ShowMessageBox(String outputMessage, String windowTitle, int defaultButton)
        {
            MessageBoxButton mb = new MessageBoxButton();

            switch (defaultButton)
            {
                case 0: mb = MessageBoxButton.OK; break;
                case 1: mb = MessageBoxButton.OKCancel; break;
                case 2: mb = MessageBoxButton.YesNo; break;
                case 3: mb = MessageBoxButton.YesNoCancel; break;
                default: mb = MessageBoxButton.OK; break;
            }

            MessageBox.Show(outputMessage, windowTitle, mb);
        }
    }
}
