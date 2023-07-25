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
using Microsoft.Win32;
using System.Diagnostics;
using System.Threading;


namespace EasySave_V2._0
{
    public partial class encrypt : Page
    {
        public encrypt()
        {
            InitializeComponent();
        }

        //Start cryptosoft.exe while clicking on the button
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Process processus1 = Process.Start(@"C:\EasySave\Version 2.0\Source Code\CryptoSoft\CryptoSoft\bin\Debug\netcoreapp3.1\CryptoSoft.exe");
        }

        
    }
}
