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
using System.Windows.Threading;
using Nito.AsyncEx;

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
    public partial class Progress : Window
    {
        //gets the targeted directory to copy files to
        PauseTokenSource m_pauseTokeSource = new PauseTokenSource();
        CancellationTokenSource cts = new CancellationTokenSource();
        public int save_loader = 0;
        public string name;
        public static string sourceDirectoryPath;
        public static string sourceDirectoryInfo;
        public static string[] originalFiles;
        public static string targetDirectoryPath;
        public static string targetDirectoryInfo;
        public static bool test_diff;
        string backup_Type;
        public int num = 0;
        public static string pross2;
        public static bool check2;
        Stopwatch stopWatch = new Stopwatch();
       




        public Progress()
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
        private async void Submit_Click(Object send, RoutedEventArgs e)
        {
           

            if (process_checker.check == true)
            {
                //Display to user that a specific is launched and he can't perform a save
                submit_btn.IsEnabled = false;
                string running = process_checker.pross + " " + "is running you can't perform any save task";
                string runningtlt = "Error";
                MessageBox.Show(running, runningtlt, MessageBoxButton.OK, MessageBoxImage.Error);

            }
            else if (process_checker.check == false)
            {
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
                        MessageBox.Show(erfs, fstl, MessageBoxButton.OK, MessageBoxImage.Error);
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
                        string samepathtit = "Error";
                        MessageBox.Show(samepatherro, samepathtit, MessageBoxButton.OK, MessageBoxImage.Error);
                        return;

                    }
                    else
                    {
                       
                        var sourceDirectoryInfo = new DirectoryInfo(srcPath.Text);
                        var targetDirectoryInfo = new DirectoryInfo(dstPath.Text);
                        save_loader = 1;
                        Progress save1 = new Progress();
                        
                        stopWatch.Start();
                        var progress = new Progress<int>(value =>

                        {
                            _progressBar.Value = value;
                            _textBlock.Text = $"{value}%";
                        });
                        

                        try
                        { 
                            
                        await Task.Run(() => save1.Full_backup(sourceDirectoryInfo, targetDirectoryInfo, progress,  cts.Token, m_pauseTokeSource.Token));

                        _textBlock.Text = "Finished";
                        stopWatch.Stop();
                        string message = "Full Backup Done!";
                        string title = "Succes!";
                        MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Information);
                        Logs();
                        State();
                        }
                        catch (OperationCanceledException ex)
                        {
                            _textBlock.Text = "Task Cancelled!";
                        }
                        finally
                        {
                            cts.Dispose();
                        }
                    }

                }
                else if (comboBox.Text == "Differential Save")
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
                        //Progress Progress = new Progress();
                        //Progress.Show();
                        sourceDirectoryPath = srcPath.Text;
                        targetDirectoryPath = dstPath.Text;
                        var sourceDirectoryInfo = new DirectoryInfo(sourceDirectoryPath);
                        var targetDirectoryInfo = new DirectoryInfo(targetDirectoryPath);
                        save_loader = 2;
                        Progress save2 = new Progress();
                        stopWatch.Start();// start to calcul the time in ms of the save
                        var progress = new Progress<int>(value =>

                        {
                            _progressBar.Value = value;
                            _textBlock.Text = $"{value}%";
                        });
                        try
                        {
                            await Task.Run(() => Diff_backup(sourceDirectoryInfo, targetDirectoryInfo, progress, cts.Token, m_pauseTokeSource.Token));

                            _textBlock.Text = "Finished";
                            stopWatch.Stop();// stop the calcul
                            test_diff = false;
                            string message = "Differential Backup Done!";
                            string title = "Succes!";
                            MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Information);
                            Logs();//start logs function
                            State();//start State function
                        }
                        catch (OperationCanceledException ex)
                        {
                            _textBlock.Text = "Task Cancelled!";
                        }
                        finally
                        {
                            cts.Dispose();
                        }
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
        public async Task Diff_backup(DirectoryInfo source, DirectoryInfo target, IProgress<int> progress, CancellationToken token, PauseToken pauseToken)
        {
            await pauseToken.WaitWhilePausedAsync();


            originalFiles = Directory.GetFiles(sourceDirectoryPath, "*", SearchOption.AllDirectories);

                Array.ForEach(originalFiles, async (originalFileLocation) =>
                {

                    FileInfo originalFile = new FileInfo(originalFileLocation);
                    FileInfo destFile = new FileInfo(originalFileLocation.Replace(sourceDirectoryPath, targetDirectoryPath));

                    if (destFile.Exists)
                    {
                        if (originalFile.Length > destFile.Length)
                        {
                            originalFile.CopyTo(destFile.FullName, true);
                            await pauseToken.WaitWhilePausedAsync();
                            int size = (int)(originalFile.Length / 1024);
                            for (int i = 0; i < size; i++)
                            {
                                Thread.Sleep(100);
                                var precentComplete = (i * 100) / size;
                                progress.Report(precentComplete);
                                if (token.IsCancellationRequested)
                                {
                                    string message = "Task has been canceled";
                                    string title = "Task canceled";
                                    MessageBox.Show(message, title);
                                    token.ThrowIfCancellationRequested();
                                    return;


                                }
                            }


                        }

                    }

                    else
                    {

                        Directory.CreateDirectory(destFile.DirectoryName);
                        originalFile.CopyTo(destFile.FullName, false);
                        test_diff = true;
                        int size = (int)(originalFile.Length / 1024);
                        for (int i = 0; i < size; i++)
                        {
                            Thread.Sleep(100);
                            var precentComplete = (i * 100) / size;
                            progress.Report(precentComplete);
                            if (token.IsCancellationRequested)
                            {
                                string message = "Task has been canceled";
                                string title = "Task canceled";
                                MessageBox.Show(message, title);
                                token.ThrowIfCancellationRequested();
                                return;


                            }
                        }

                    }

               });

        }
        //Full backup function
        public async Task Full_backup(DirectoryInfo source, DirectoryInfo target, IProgress<int> progress, CancellationToken token, PauseToken pauseToken)
        {
           
            foreach (var file in source.GetFiles()) // for every file in the source directory 
                {
                await pauseToken.WaitWhilePausedAsync();
                // Use static Path methods to extract only the file name from the path.
                file.CopyTo(System.IO.Path.Combine(target.FullName, file.Name), true);
                     num++;
                     int size = (int)(file.Length / 1024);
                    

                for (int i = 0; i < size; i++)
                    {
                        Thread.Sleep(100);
                        var precentComplete = (i * 100) / size;
                        progress.Report(precentComplete);
                    
                if (token.IsCancellationRequested)
                {
                    string message = "Task has been canceled";
                    string title = "Task canceled";
                    MessageBox.Show(message, title);
                    token.ThrowIfCancellationRequested();
                    return;


                 }
                }
            }
                //process subdirectories
                foreach (var sourceSubdirectory in source.GetDirectories())
                {
                DirectoryInfo info = new DirectoryInfo(sourceDirectoryPath);
                long totalSize = info.EnumerateFiles().Sum(file => file.Length);
                var targetSubdirectory = target.CreateSubdirectory(sourceSubdirectory.Name);
                await Full_backup(sourceSubdirectory, targetSubdirectory, progress, token, pauseToken);
                int size = (int)(totalSize / 1024);
                for (int i = 0; i < size; i++)
                {
                    Thread.Sleep(100);
                    var precentComplete = (i * 100) / size;
                    progress.Report(precentComplete);
                    if (token.IsCancellationRequested)
                    {
                        string message = "Task has been canceled";
                        string title = "Task canceled";
                        MessageBox.Show(message, title);
                        token.ThrowIfCancellationRequested();
                        return;


                    }
                }
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
            string info = "Name :" + " " + name + "\n\nSave Type:" + " " + backup_Type + "\n\nSource Path:" + " " + sourceDirectoryPath + "\n\nTarget Path:" + " " + targetDirectoryPath;
            string title = "Last Save Informations";
            MessageBox.Show(info, title, MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Pause(object sender, RoutedEventArgs e)
        {
            m_pauseTokeSource.IsPaused = true;
        }
        private void Resume(object sender, RoutedEventArgs e)
        {
            m_pauseTokeSource.IsPaused = false;
        }
        private void Cancel(object sender, RoutedEventArgs e)
        {
            cts.Cancel();
        }
        private void checkBtn(object sender, RoutedEventArgs e)
        {

            //check if process is launched or not
            if (Process.GetProcessesByName(pross2).Length > 0)
            {

                MessageBox.Show(pross2 + " is running");
                check2 = true;

            }
            else if (string.IsNullOrEmpty(textBoxcheck2.Text))
            {
                //User must specify the name of a process
                string ermsgname = "Please, specify the name of your process";
                string ername = "Error";
                MessageBox.Show(ermsgname, ername, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (Process.GetProcessesByName(pross2).Length == 0)
            {
                MessageBox.Show(pross2 + " " + "is not running");
                check2 = false;
            }
            if (check2 == true)
            {
                m_pauseTokeSource.IsPaused = !m_pauseTokeSource.IsPaused;

            }
            else if (check2 == false)
            {
                m_pauseTokeSource.IsPaused = false;

            }
        }
            private void textBox2check(object sender, TextChangedEventArgs e)
        {
            pross2 = textBoxcheck2.Text;
        }

        
    }
}

