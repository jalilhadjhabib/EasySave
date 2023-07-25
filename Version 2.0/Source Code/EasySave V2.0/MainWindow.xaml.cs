using System;
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
using System.IO;
using Microsoft.Win32;
using System.Threading;

namespace EasySave_V2._0
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
            Main.Content = new welcome();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //button to close the software
            try
            {
                Close();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
        }

        //private void btnOpenFile_Click(object sender, RoutedEventArgs e)
        //{
        //    var dialog = new System.Windows.Forms.FolderBrowserDialog();
        //    System.Windows.Forms.DialogResult result = dialog.ShowDialog();
        //    srcPath.Text = dialog.SelectedPath;

        //}

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        private void addnewtask_Click(object sender, RoutedEventArgs e)
        {
            //Button to go to new save
            Main.Content = new addnewtask();
        }
        private void logs_Click(object sender, RoutedEventArgs e)
        {
            //Button to go to logs
            Main.Content = new Logs();
        }
        
        private void launchtask_Click(object sender, RoutedEventArgs e)
        {
        }
        private void process_checker_Click(object sender, RoutedEventArgs e)
        {
            //Button to go to process checker
            Main.Content = new process_checker();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            //Button to go cryptosoft page
            Main.Content = new encrypt();
        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            Main.Content = new welcome();
        }

        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            
        }
        
    }
}
