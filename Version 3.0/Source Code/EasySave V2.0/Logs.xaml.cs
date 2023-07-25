using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
using Microsoft.Win32;

namespace EasySave_V2._0
{
    /// <summary>
    /// Logique d'interaction pour Logs.xaml
    /// </summary>
    public partial class Logs : Page
    {
        public Logs()
        {
            InitializeComponent();
        }

        private void OpenLogs(object sender, RoutedEventArgs e)
        {
            //Open logs file
            var p = new Process();
            p.StartInfo = new ProcessStartInfo(@".\Logs\Logs.json")
            {
                UseShellExecute = true
            };
            p.Start();
        }
        private void OpenState(object sender, RoutedEventArgs e)
        {
            //Open state file
            var p = new Process();
             p.StartInfo = new ProcessStartInfo(@".\State\State.json")
             {
                 UseShellExecute = true
             };
             p.Start();
        }
    }
}
