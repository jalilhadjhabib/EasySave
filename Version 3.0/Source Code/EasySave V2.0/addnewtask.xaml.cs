using System;
using System.IO;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Resources;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.WindowsAPICodePack.Dialogs;
using Newtonsoft.Json;
using System.Diagnostics;

namespace EasySave_V2._0
{
    


    
    /// <summary>
    /// Logique d'interaction pour addnewtask.xaml
    /// </summary>
    public partial class addnewtask : Page
    {  
        public addnewtask()
        {
            InitializeComponent();

        }

        private void Loadsave_Click(object sender, RoutedEventArgs e)
        {
            Progress win2 = new Progress();
            win2.Show();

        }
    }
  }

