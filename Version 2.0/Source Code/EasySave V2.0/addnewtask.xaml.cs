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
    class logs
    {
        //Getters and Setters
        public DateTime timeData
        {
            get;
            set;
        }
        public string name
        {
            get;
            set;
        }
       public string backup_Type
        {
            get;
            set;
        }
        public string sourcePath
        {
            get;
            set;
        }
        public string destinationPath
        {
            get;
            set;
        }
        public long sizeFile_KO
        {
            get;
            set;
        }
        public long transferTime_in_Ms
        {
            get;
            set;
        }
        public int total_files_copied
        {
            get;
            set;
        }


    }
    /// <summary>
    /// Logique d'interaction pour addnewtask.xaml
    /// </summary>
    public partial class addnewtask : Page
    {
        //gets the targeted directory to copy files to
        public int save_loader = 0;
        public string name;
        public static string sourceDirectoryPath;
        public static string sourceDirectoryInfo;
        public static string[] originalFiles;
        public static string targetDirectoryPath;
        public static string targetDirectoryInfo;
        public static bool test_diff;
        string backup_Type;
        private bool buttonWasClicked = false;
        Stopwatch stopWatch = new Stopwatch();

        public addnewtask(CultureInfo culture)
        {
            InitializeComponent();
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

            ResourceManager rm = new ResourceManager("EasySave V2.0.addnewtask", System.Reflection.Assembly.GetExecutingAssembly());
            label1.Content = rm.GetString("label1.Content");
            label2.Content = rm.GetString("label2.Content");
            label3.Content = rm.GetString("label3.Content");
            label4.Content = rm.GetString("label4.Content");
            label5.Content = rm.GetString("label5.Content");







        }
        public addnewtask()
        {
            InitializeComponent();

        }
        //Source Browse button
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Open the file dialog
            var dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            CommonFileDialogResult result = dialog.ShowDialog();
            if (result == CommonFileDialogResult.Cancel)
            {
                return;
            }
            else
            {
                srcPath.Text = dialog.FileName;
            }
        }


        //Target browse button
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //Open the file dialog
            var dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            CommonFileDialogResult result = dialog.ShowDialog();
            if (result == CommonFileDialogResult.Cancel)
            {
                return;
            }
            else
            {
                dstPath.Text = dialog.FileName;
            }
        }
        private void Submit_Click(Object send, RoutedEventArgs e)
        {
           
            if (process_checker.check == true)
            {
                //Display to user that a specific is launched and he can't perform a save
                submit_btn.IsEnabled = false;
                string running = process_checker.pross + " " + "is running you can't perform any save task";
                string runningtlt = "Error";
                MessageBox.Show(running, runningtlt, MessageBoxButton.OK, MessageBoxImage.Error);

            }
            else if (process_checker.check == false) {
                submit_btn.IsEnabled = true;
                bool srcdirectoryExists = Directory.Exists(sourceDirectoryPath);
            bool dstdirectoryExists = Directory.Exists(targetDirectoryPath);
          
            if (comboBox.Text == "Full Save")
            {
                    //User must enter a name for the save 
                if (string.IsNullOrEmpty(save_name.Text))
                {
                    string ermsgname = "Please, specify a name for your save";
                    string ername = "Error";
                    MessageBox.Show(ermsgname, ername, MessageBoxButton.OK, MessageBoxImage.Error);
                }
                    //User must enter a source directory
                    else if (string.IsNullOrEmpty(srcPath.Text)) 
                {
                    string erfs = "Please Choose a source directory";
                    string fstl = "Error";
                    MessageBox.Show(erfs,fstl, MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                else if (string.IsNullOrEmpty(dstPath.Text))
                {
                    //User must enter a target directory
                    string erfs1 = "Please Choose a target directory";
                    string fstl1 = "Error";
                    MessageBox.Show(erfs1, fstl1, MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                    //User must enter a different source directory/target directory
                    else if (srcPath.Text == dstPath.Text)
                {
                     string samepatherro = "Source directory and Target directory can't be the same";
                     string samepathtit= "Error";
                     MessageBox.Show(samepatherro, samepathtit, MessageBoxButton.OK, MessageBoxImage.Error);
                     return;

                }
                    else
                {
                        //launch the full backup function
                    var sourceDirectoryInfo = new DirectoryInfo(srcPath.Text);
                    var targetDirectoryInfo = new DirectoryInfo(dstPath.Text);
                    save_loader = 1;
                    addnewtask save1 = new addnewtask();
                    stopWatch.Start();
                    save1.Full_backup(sourceDirectoryInfo, targetDirectoryInfo);
                    stopWatch.Stop();
                    string message = "Full Backup Done!";
                    string title = "Succes!";
                    MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Information);
                    Logs();
                    State();
                }
                             
            }         
            else if (comboBox.Text == "Differential Save" ) 
            {
                    //User must enter a name for the save

                    if (string.IsNullOrEmpty(save_name.Text))
                {
                    string ermsgname = "Please, specify a name for your save";
                    string ername = "Error";
                    MessageBox.Show(ermsgname, ername, MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (string.IsNullOrEmpty(srcPath.Text))
                {
                        //User must enter a source directory
                        string erds = "Please Choose a source directory";
                    string dstl = "Error";
                    MessageBox.Show(erds, dstl, MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                    
                }
                else if (string.IsNullOrEmpty(dstPath.Text))
                {
                        //User must enter a 
                        string erds1 = "Please Choose a Target directory";
                    string dstl1 = "Error";
                    MessageBox.Show(erds1, dstl1, MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                    
                }
                    //User must enter a different source/target directory
                    else if (srcPath.Text == dstPath.Text)
                    {
                        string samepatherro1 = "Source directory and Target directory can't be the same";
                        string samepathtit1 = "Error";
                        MessageBox.Show(samepatherro1, samepathtit1, MessageBoxButton.OK, MessageBoxImage.Error);
                        return;

                    }
                    else
                {
                        //Launch the differential function
                        var sourceDirectoryInfo = new DirectoryInfo(srcPath.Text);
                    var targetDirectoryInfo = new DirectoryInfo(dstPath.Text);
                    save_loader = 2;
                    addnewtask save2 = new addnewtask();
                    stopWatch.Start();// start to calcul the time in ms of the save
                    Diff_backup(sourceDirectoryInfo, targetDirectoryInfo);
                    stopWatch.Stop();// stop the calcul
                    test_diff = false;
                    string message = "Differential Backup Done!";
                    string title = "Succes!";
                    MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Information);
                    Logs();//start logs function
                    State();//start State function
                }
            }
            else if (string.IsNullOrEmpty(save_name.Text))
            {
                string ermsgname = "Please, specify a name for your save";
                string ername = "Error";
                MessageBox.Show(ermsgname, ername, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (string.IsNullOrEmpty(comboBox.Text))
            {
                string typeer = "Please Choose a type for your save";
                string typetlt = "Error";
                MessageBox.Show(typeer, typetlt, MessageBoxButton.OK, MessageBoxImage.Error);
                return;

            }
            

            }
        }
        //Differencial backup function
        private void Diff_backup(DirectoryInfo source, DirectoryInfo target)
        {
            sourceDirectoryPath = srcPath.Text;
            targetDirectoryPath = dstPath.Text;
            originalFiles = Directory.GetFiles(sourceDirectoryPath, "*", SearchOption.AllDirectories);

            Array.ForEach(originalFiles, (originalFileLocation) =>
            {
                FileInfo originalFile = new FileInfo(originalFileLocation);
                FileInfo destFile = new FileInfo(originalFileLocation.Replace(sourceDirectoryPath, targetDirectoryPath));

                if (destFile.Exists)
                {
                    if (originalFile.Length > destFile.Length)
                    {
                        originalFile.CopyTo(destFile.FullName, true);
                        long size = originalFile.Length / 1024;
                        

                    }

                }

                else
                {

                    Directory.CreateDirectory(destFile.DirectoryName);
                    originalFile.CopyTo(destFile.FullName, false);
                    test_diff = true;
                    long size = originalFile.Length / 1024;
                    Thread.Sleep(50);
                }

            });
        }
        //Full backup function
        private void Full_backup(DirectoryInfo source, DirectoryInfo target)
        {
            sourceDirectoryPath = srcPath.Text;
            targetDirectoryPath = dstPath.Text;


            foreach (var file in source.GetFiles()) // for every file in the source directory 
            {
                Thread.Sleep(50);
                // Use static Path methods to extract only the file name from the path.
                file.CopyTo(System.IO.Path.Combine(target.FullName, file.Name), true);
                long size = file.Length / 1024;
                //Console.WriteLine($"{file.FullName} :" + " " + $"{size}" + "Ko" + " " + "Has been copied to" + " " + $"{target}");

            }
            //process subdirectories
            foreach (var sourceSubdirectory in source.GetDirectories())
            {
                Thread.Sleep(50);
                var targetSubdirectory = target.CreateSubdirectory(sourceSubdirectory.Name);
                Full_backup(sourceSubdirectory, targetSubdirectory);

            }


        }

        public long DirSize(DirectoryInfo d)
        {
            long size = 0;
            // Add file sizes.
            FileInfo[] fis = d.GetFiles();
            foreach (FileInfo fi in fis)
            {
                size += fi.Length;
            }
            // Add subdirectory sizes.
            DirectoryInfo[] dis = d.GetDirectories();
            foreach (DirectoryInfo di in dis)
            {
                size += DirSize(di);
                size = size / 1024;
            }
            return size;
        }
        //Logs file function
        public void Logs()
        {

            backup_Type = comboBox.Text;
            sourceDirectoryPath = srcPath.Text;
            targetDirectoryPath = dstPath.Text;
            originalFiles = Directory.GetFiles(sourceDirectoryPath, "*", SearchOption.AllDirectories);
            int fileCount = Directory.GetFiles(targetDirectoryPath, "*.*", SearchOption.AllDirectories).Length;
            DateTime timer = DateTime.Now;

            foreach (string file in originalFiles)
            {

                var info = new FileInfo(file);
                var size = info.Length;
                long length = size / 1024;
                int total_files_copied = fileCount;
                logs stu = new logs()
                {
                    //Logs file content
                    name = name,//Name of the save
                    timeData = timer,//Time of the save
                    sourcePath = sourceDirectoryPath,//Source path of the save
                    destinationPath = targetDirectoryPath,//Target path of the save
                    sizeFile_KO = length,//size file 
                    backup_Type = backup_Type,//type of the save
                    transferTime_in_Ms = stopWatch.ElapsedMilliseconds,// transfertime of the save
                    total_files_copied = fileCount,//files saved
                };
                string strResultJson = JsonConvert.SerializeObject(stu, Formatting.Indented);
                File.AppendAllText(@".\Logs\Logs.json", strResultJson);


            }
        }
        //State file function
        public void State()
        {
            backup_Type = comboBox.Text;
            sourceDirectoryPath = srcPath.Text;
            targetDirectoryPath = dstPath.Text;
            originalFiles = Directory.GetFiles(sourceDirectoryPath, "*", SearchOption.AllDirectories);
            int fileCount = Directory.GetFiles(targetDirectoryPath, "*.*", SearchOption.AllDirectories).Length;
            DateTime timer = DateTime.Now;
            foreach (string file in originalFiles)
            {

                var info = new FileInfo(file);
                var size = info.Length;
                long length = size / 1024;
                int total_files_copied = fileCount;
                logs stu = new logs()
                {
                    //State file content
                    name = name, //Name of the save
                    timeData = timer, //Time of the save
                    sourcePath = sourceDirectoryPath, //Source path of the save
                    destinationPath = targetDirectoryPath,//destination path of the save
                    sizeFile_KO = length,//size file 
                    backup_Type = backup_Type,//type of the save
                    transferTime_in_Ms = stopWatch.ElapsedMilliseconds,// transfertime of the save
                    total_files_copied = fileCount,//files saved
                };
                string strResultJson = JsonConvert.SerializeObject(stu, Formatting.Indented);
                File.AppendAllText(@".\State\State.json", strResultJson);


            }
        }

        private void save_name_TextChanged(object sender, TextChangedEventArgs e)
        {
            name = save_name.Text;
        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void Loadsave_Click(object sender, RoutedEventArgs e)
        {
            //Display last save informations
            string info = "Name :" + " " + name + "\n\nSave Type:" + " " + backup_Type + "\n\nSource Path:" + " " + sourceDirectoryPath+ "\n\nTarget Path:" + " " + targetDirectoryPath;
            string title = "Last Save Informations";
            MessageBox.Show(info,title, MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
  }

