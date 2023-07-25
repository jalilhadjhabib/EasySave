using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
namespace EasySave_V2._0
{
    /// <summary>
    /// Logique d'interaction pour process_checker.xaml
    /// </summary>
    public partial class process_checker : Page
    {


        public static string pross;
        public static bool check;
        public process_checker()
        {
            InitializeComponent();
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {



            //Process[] processlist = Process.GetProcesses();

            //check if process is launched or not
            if (Process.GetProcessesByName(pross).Length > 0)
            {

                MessageBox.Show(pross + " is running");
                check = true;
               
            }
            else if (string.IsNullOrEmpty(textBox1.Text))
            {
                //User must specify the name of a process
                string ermsgname = "Please, specify the name of your process";
                string ername = "Error";
                MessageBox.Show(ermsgname, ername, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (Process.GetProcessesByName(pross).Length == 0)
            {
                MessageBox.Show(pross + " " + "is not running");
                check = false;
            }
            
        }

        private void textBox1_TextChanged(object sender, TextChangedEventArgs e)
        {
            pross = textBox1.Text;
        }
    }
}