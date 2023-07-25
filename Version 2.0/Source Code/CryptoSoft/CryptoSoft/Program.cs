using System;
using System.IO;
using System.Text;
using System.Diagnostics;
using Newtonsoft.Json;

namespace test_encrypt
{
    class Program
    {
        class logs
        {

            public long Crypto_Time_In_MS
            {
                get;
                set;
            }
        }
            public static string dir;
        public static string key;
        public static string usr_ext;
        public static string[] files;

        static void Main(string[] args)
        {
            //Logo of CryptoSoft
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(@"
                                                         
                                                        
                      
                                     ██████╗██████╗ ██╗   ██╗██████╗ ████████╗ ██████╗ 
                                    ██╔════╝██╔══██╗╚██╗ ██╔╝██╔══██╗╚══██╔══╝██╔═══██╗
                                    ██║     ██████╔╝ ╚████╔╝ ██████╔╝   ██║   ██║   ██║
                                    ██║     ██╔══██╗  ╚██╔╝  ██╔═══╝    ██║   ██║   ██║
                                    ╚██████╗██║  ██║   ██║   ██║        ██║   ╚██████╔╝
                                     ╚═════╝╚═╝  ╚═╝   ╚═╝   ╚═╝        ╚═╝    ╚═════╝ 
                                                   
                                            ███████╗ ██████╗ ███████╗████████╗                 
                                            ██╔════╝██╔═══██╗██╔════╝╚══██╔══╝                 
                                            ███████╗██║   ██║█████╗     ██║                    
                                            ╚════██║██║   ██║██╔══╝     ██║                    
                                            ███████║╚██████╔╝██║        ██║                    
                                            ╚══════╝ ╚═════╝ ╚═╝        ╚═╝                    
                                                   
                                                                  
                      
                                 
                                                                                                                  
            ");
            //Let user specify a path
            Console.WriteLine("\n" + "Please, specify the File Directory path: \n");
            //User must enter a path
            do
            {
                Console.Write("File Directory: ");
                dir = @"" + Console.ReadLine();
                if (!string.IsNullOrEmpty(dir))
                {
                    //Console.WriteLine("OK");
                }
                else
                {
                    Console.WriteLine("A file directory is required to proceed to next step");
                }
            } while (string.IsNullOrEmpty(dir));

            Console.WriteLine("\n" + "Please, specify the file Extension: \n");
            //User must enter a file extension
            do
            {
                Console.Write("File Extension :");
                usr_ext = @"" + Console.ReadLine();
                if (!string.IsNullOrEmpty(usr_ext))
                {
                    //Console.WriteLine("OK");
                }
                else
                {
                    Console.WriteLine("A file extension is required to proceed to next step");
                }
            } while (string.IsNullOrEmpty(usr_ext));

            Console.WriteLine("\n" + "Please, specify  the key to your file encryption: \n");
            //User must specify the key
            do
            {
                Console.Write("Key Encryption :");
                key = @"" + Console.ReadLine();
                if (!string.IsNullOrEmpty(key))
                {
                    //Console.WriteLine("OK");
                }
                else
                {
                    Console.WriteLine("A file extension is required to proceed to next step");
                }
            } while (string.IsNullOrEmpty(key));



            Stopwatch crypto = new Stopwatch();

            crypto.Start();// Start to Calculate the the encryptation time
            files = Directory.GetFiles(dir, "*" + usr_ext, SearchOption.AllDirectories);

            if (usr_ext == ".txt")
            {

                foreach (string file in files)

                {
                    string s = file;
                    char[] msg = s.ToCharArray();
                    char[] pw = key.ToCharArray();
                    int i = 0;
                    string enc = "";

                    for (int j = 0; j < s.Length; j++)
                    {
                        char c = (char)((int)msg[j] ^ (int)pw[i++ % pw.Length]);

                        enc = enc + c;
                        Console.WriteLine("value of encryption : " + " " + $"{enc}");
                    }
                    File.Delete(file);
                    File.WriteAllText(file, enc);
                    Console.WriteLine($"{file}" + " " + "Has been encrypted");
                    // Open the file to read from.
                    //string readText = File.ReadAllText(fname);
                }
                crypto.Stop();

            }

            else if (usr_ext == ".docx")
            {

                foreach (string file in files)

                {
                    string s = file;
                    char[] msg = s.ToCharArray();
                    char[] pw = key.ToCharArray();
                    int i = 0;
                    string enc = "";

                    for (int j = 0; j < s.Length; j++)
                    {
                        char c = (char)((int)msg[j] ^ (int)pw[i++ % pw.Length]);

                        enc = enc + c;
                        Console.WriteLine("value of encryption : " + " " + $"{enc}");
                    }
                    File.Delete(file);
                    File.WriteAllText(file, enc);
                    Console.WriteLine($"{file}" + " " + "Has been encrypted");
                    // Open the file to read from.
                    //string readText = File.ReadAllText(fname);
                }
            }
            else if (usr_ext != ".txt")
            {
                Console.WriteLine("No txt files found in " + " " + $"{dir}");
            }
            
            logs stu = new logs()
            {
                //Add encrypt time in logs file
                
                Crypto_Time_In_MS = crypto.ElapsedMilliseconds,
            };
            string strResultJson = JsonConvert.SerializeObject(stu, Formatting.Indented);
            File.AppendAllText(@"C:\EasySave\Version 2.0\Source Code\EasySave V2.0\bin\Debug\netcoreapp3.1\Logs\Logs.json", strResultJson);

            Console.WriteLine("Your File Has Been Enrypted! \n");
            Console.WriteLine("Press Any Key To Exit");
            Console.ReadLine();

        }

    }

}



