using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Threading;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using System.Linq;

namespace EasySave_V1._0
{
	class Model
	{

		public int save_counter = 0;
		public string usrInput1;
		public  string lg;
		public int save_loader = 0;
		public string name;
		public static bool test_diff;
		public static string sourceDirectoryPath;

		public static string targetDirectoryPath;

		//public static string targetDirectoryInfo;

		//Display Instruction for the user
		public void displayInstruction()
		{
            switch (lg)
            {
				case "1":
			Console.WriteLine("Instructions: \n" + "1. Choose new save \n" + "2. Enter a name for your save\n" + "3. Entre source directory \n" + "4. Enter destination directory \n" + "5. Choose a Save type (Full or Differential) \n");
					break;
				case "2":
					Console.WriteLine("Instructions: \n " + " 1. Choisissez une nouvelle sauvegarde \n " + " 2. Entrez un nom pour votre sauvegarde \n " + " 3. Entre le répertoire source \n " + " 4. Entrez le répertoire de destination \n " + " 5. Choisissez un type de sauvegarde (complet ou différentiel) \n");

					break;
            }
		}
		public  void chooselg()
		{

			string english = "Type 1 then \"Enter\" for English\n\n";
			Console.SetCursorPosition((Console.WindowWidth - english.Length) / 2, Console.CursorTop);
			Console.WriteLine(english);
			string french = "Tapez 2 puis \"Enterée\" pour le Français";
			Console.SetCursorPosition((Console.WindowWidth - french.Length) / 2, Console.CursorTop);
			Console.WriteLine(french);
			lg = Console.ReadLine();

			do
			{

				lg = Console.ReadLine();
				if (!string.IsNullOrEmpty(lg))
				{
					//Console.WriteLine("OK");
				}
				else
				{
					Console.WriteLine("Please, choose a language\n");
					Console.WriteLine("Veuillez Choisir une langue");
				}
			} while (string.IsNullOrEmpty(lg));

		}
		//Function backup that execute the backup processus
		public void Backup()
		{
            //string strRegex = @"^(([\w]\:|\\)+((\\[a-zA-Z0-9\\-]+)+)|([a-zA-Z0-9\\-]+))(\.[a-zA-Z0-9]+|[\x30-\x39]*[\x41-\x5A]*[\x61-\x7A]*[\x2D\x5F]*)$";
            //Regex re = new Regex(strRegex);
            switch (lg)
            {
				case "1":
					Console.WriteLine("\n" + "Please, specify the name for your save \n");
					break;
				case "2":
					Console.WriteLine("\n" + "Veuillez indiquer le nom de votre sauvegarde \n");
					break;
            }
			
			name = "";
			//Getting input for source directory 
			do
			{
				switch (lg)
                {
					case "1":
						Console.Write("Name: ");
						break;
					case "2":
						Console.Write("Nom: ");
						break;
                }
			
				name = Console.ReadLine();
				if (!string.IsNullOrEmpty(name))
				{
					//Console.WriteLine("OK");
				}
				else
				{
					switch (lg)
					{
						case "1":
							Console.WriteLine("Please, specify the name");
							break;
						case "2":
							Console.WriteLine("Merci de préciser le nom");
							break;
					}
					
				}
			} while (string.IsNullOrEmpty(name));

			switch (lg)
			{
				case "1":
			Console.WriteLine("\n" + "Please, specify the path to the repertory you want to save: \n" + @"A path is something that looks like this: \home\user\desktop\folder1 " + "\n");
					break;
				case "2":
					Console.WriteLine("\n" + "Veuillez indiquer le chemin d'accès au répertoire que vous souhaitez enregistrer: \n" + @" Un chemin est quelque chose qui ressemble à ceci: \home\user\desktop\folder1 " + "\n");

					break;
			}


			do
			{
				switch (lg)
				{
					case "1":
				Console.Write("Source Directory: ");
						break;
					case "2":
						Console.Write("Répertoire source: ");

						break;
				}

				sourceDirectoryPath = @"" + Console.ReadLine();
				if (!string.IsNullOrEmpty(sourceDirectoryPath))
				{
					//Console.WriteLine("OK");
				}
				else
				{
					switch (lg)
					{
						case "1":
					Console.WriteLine("A source directory is required to proceed to next step");
							break;
						case "2":
							Console.WriteLine(" Un répertoire source est requis pour passer à l'étape suivante");

							break;
					}
				}
			} while (string.IsNullOrEmpty(sourceDirectoryPath));
			//checking for valid source directories from user input
			if (Directory.Exists(sourceDirectoryPath))
			{
				
				switch (lg)
				{
					case "1":
				Console.WriteLine("\n" + "Please, specify the path where you want your repertory to be saved: \n");
						break;
					case "2":
						Console.WriteLine("\n" + " Veuillez spécifier le chemin où vous souhaitez que votre répertoire soit enregistré: \n");

						break;
				}


				do
				{
					switch (lg)
					{
						case "1":
					Console.Write("Target Directory: ");
							break;
						case "2":
							Console.Write("Répertoire cible ");

							break;
					}

					targetDirectoryPath = @"" + Console.ReadLine();
					if (!string.IsNullOrEmpty(targetDirectoryPath))
					{
						//Console.WriteLine("OK");
					}
					else
					{
						
						switch (lg)
						{
							case "1":
						Console.WriteLine("A target directory is required to proceed to next step");
								break;
							case "2":
								Console.WriteLine(" Un répertoire cible est requis pour passer à l'étape suivante");

								break;
						}
					}
				} while (string.IsNullOrEmpty(targetDirectoryPath));
			}
			//sends back to menu if directory doesn't exist
			else
			{
				switch (lg)
				{
					case "1":
						Console.WriteLine("Source Directory Doesn't exist");
						Console.WriteLine("Press any key to go back to Menu");

						break;
					case "2":
						Console.WriteLine(" Source Directory Doesn't exist");
						Console.WriteLine(" Appuyez sur n'importe quelle touche pour revenir au menu");

						break;
				}
				Console.ReadKey();
				goback_to_menu();

			}
			//checking for valid target directories from user input
			if (Directory.Exists(targetDirectoryPath))
			{
				switch (lg)
				{
					case "1":
				Console.WriteLine("Please, choose the type of save you want to use for your operation: \n" + "1: Full Save \n" + "2: Differential Save \n");
						break;
					case "2":
						Console.WriteLine(" Veuillez choisir le type de sauvegarde que vous souhaitez utiliser pour votre opération: \n" + "1: Sauvgarde complete \n" + "2: Sauvgarde différentielle \n");

						break;
				}
				usrInput1 = Console.ReadLine();
			}
			//sends back to menu if directory doesn't exist
			else
			{
				
				switch (lg)
				{
					case "1":
				Console.WriteLine("Targeted Directory doesn't exist");
						break;
					case "2":
						Console.WriteLine(" Targeted Directory doesn't exist");
						break;
				}
				View goback = new View();
				switch (lg)
				{
					case "1":
				Console.WriteLine("Press any key to go back to Menu");
						break;
					case "2":
						Console.WriteLine(" Appuyez sur n'importe quelle touche pour revenir au menu");
						break;
				}
				Console.ReadKey();
				goback_to_menu();
			}


			//decalred variables to fetch directory info and use them more simply for calls in method
			var sourceDirectoryInfo = new DirectoryInfo(sourceDirectoryPath);

			var targetDirectoryInfo = new DirectoryInfo(targetDirectoryPath);

			//using this variable to check for directory existence 

			bool directoryExists = Directory.Exists(sourceDirectoryPath);


			Stopwatch stopwatch = new Stopwatch();

			stopwatch.Start(); //Start to calculate the process time of the function in ms
			//making sure the 2 directories are different
			if (String.Equals(sourceDirectoryPath, targetDirectoryPath))//sends back to menu if directories are equals
			{
				
				switch (lg)
				{
					case "1":
						Console.WriteLine("Source Path and Target Path can't be the same" + "\n");
						Console.WriteLine("Press any key to go back to Menu");

						break;
					case "2":
						Console.WriteLine(" Le chemin source et le chemin cible ne peuvent pas être identiques" + "\n");
						Console.WriteLine(" Appuyez sur n'importe quelle touche pour revenir au menu");

						break;
				}

				Console.ReadKey();
				goback_to_menu();
			}
			switch (usrInput1)
			{
				//takes user input if 1 calls Full backup method if 2 calls Diff backup method
				case "1":
					if (directoryExists) //make sure directory exists
					{

						Full_backup(sourceDirectoryInfo, targetDirectoryInfo);
						switch (lg)
						{
							case "1":
						Console.WriteLine("Total size of copied files: {0} Ko.", DirSize(new DirectoryInfo(targetDirectoryPath)));
								break;
							case "2":
						Console.WriteLine("Taille totale des fichiers copiés: {0} Ko.", DirSize(new DirectoryInfo(targetDirectoryPath)));

								break;
						}
						save_loader = 1;
						Save_counter(save_counter++);

					}
					else//sends back to menu if directory doesn't exist
					{
						switch (lg)
						{
							case "1":
						Console.WriteLine("Source Directory doesn't exist");
						Console.WriteLine("Press any key to go back to Menu");
								break;
							case "2":
								Console.WriteLine("Source Directory doesn't exist");
								Console.WriteLine("Press any key to go back to Menu");

								break;
						}
						Console.ReadKey();
						goback_to_menu();
					}
					break;
				case "2":
					if (directoryExists)//make sure directory exists
					{

						diff_backup(sourceDirectoryInfo, targetDirectoryInfo);
						test_diff = false;
						switch (lg)
						{
							case "1":
						Console.WriteLine("Total size of files: {0} Ko.", DirSize(new DirectoryInfo(targetDirectoryPath)));
								break;
							case "2":
								Console.WriteLine("Taille totale des fichiers copiés: {0} Ko.", DirSize(new DirectoryInfo(targetDirectoryPath)));
								break;
						}
						save_loader = 2;
						Save_counter(save_counter++);

					}
					else//sends back to menu if directory doesn't exist
					{
						switch (lg)
						{
							case "1":
						Console.WriteLine("Source path does not exist!");
						Console.WriteLine("Press any key to go back to Menu");
								break;
							case "2":
								Console.WriteLine(" Le chemin source n'existe pas!");
								Console.WriteLine(" Press any key to go back to Menu");
								break;
						}
						Console.ReadKey();
						goback_to_menu();
					}
					break;

			}
			stopwatch.Stop();
			int fileCount = Directory.GetFiles(targetDirectoryPath, "*.*", SearchOption.AllDirectories).Length;
			var files = Directory.GetFiles(sourceDirectoryPath, "*.*");
			string backup_input = usrInput1;
			string backup_Type = "";
			if (usrInput1 == "1")
			{
				backup_Type = "Full_BackUp";
			}
			else if (usrInput1 == "2")
			{
				backup_Type = "Differential_BackUp";
			}
			DateTime timer = DateTime.Now;
			foreach (string file in files)
			{

				var info = new FileInfo(file);
				var size = info.Length;
				long length = size / 1024;
				int total_files_copied = fileCount;
				logs stu = new logs()
				{
					//Logs file content
					name = name,
					timeData = timer,
					sourcePath = sourceDirectoryPath,
					destinationPath = targetDirectoryPath,
					sizeFile_KO = length,
					backup_Type = backup_Type,
					transferTime_in_Ms = stopwatch.ElapsedMilliseconds,
					total_files_copied = fileCount,
				};
				string strResultJson = JsonConvert.SerializeObject(stu, Formatting.Indented);
				File.AppendAllText(@".\Logs\Logs.json", strResultJson);


			}

			foreach (string file in files)
			{

				var info = new FileInfo(file);
				var size = info.Length;
				long length = size / 1024;
				int nbFiles = Directory.GetFiles(sourceDirectoryPath, "*", SearchOption.AllDirectories).Length;
				int total_files_copied = fileCount;
				state st = new state()
				{
					//State File content
					name = name,
					timeData = timer,
					sourcePath = sourceDirectoryPath,
					destinationPath = targetDirectoryPath,
					sizeFile_KO = length,
					backup_Type = backup_Type,
					nbFiles = nbFiles,

				};
				string strResultJson2 = JsonConvert.SerializeObject(st, Formatting.Indented);
				File.AppendAllText(@".\State\State_File.json", strResultJson2);

			}

		}
		//Function to open logs file

		//Full backup Function
		private static void Full_backup(DirectoryInfo source, DirectoryInfo target)
		{
			
			Directory.CreateDirectory(target.FullName); //gets the targeted directory to copy files to

			foreach (var file in source.GetFiles()) // for every file in the source directory 
			{
				Thread.Sleep(50);
				// Use static Path methods to extract only the file name from the path.
				file.CopyTo(Path.Combine(target.FullName, file.Name), true);
				long size = file.Length / 1024;
				Console.WriteLine($"{file.FullName} :" + " " + $"{size}" + "Ko" + " " + "Has been copied to" + " " + $"{target}");


			}
			//process subdirectories
			foreach (var sourceSubdirectory in source.GetDirectories())
			{
				Thread.Sleep(50);
				var targetSubdirectory = target.CreateSubdirectory(sourceSubdirectory.Name);
				Full_backup(sourceSubdirectory, targetSubdirectory);

			}

		}

		//Differential backup
		private static void diff_backup(DirectoryInfo source, DirectoryInfo target )
		{
		
			string[] originalFiles = Directory.GetFiles(sourceDirectoryPath, "*", SearchOption.AllDirectories);

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
						Thread.Sleep(50);
						Console.WriteLine($"{originalFile.FullName} :" + " " + $"{size}" + "Ko" + " " + "Has been copied to" + " " + $"{target}");



					}

				}

				else
				{

					Directory.CreateDirectory(destFile.DirectoryName);
					originalFile.CopyTo(destFile.FullName, false);

					test_diff = true;
					long size = originalFile.Length / 1024;
					Thread.Sleep(50);
					Console.WriteLine($"{originalFile.FullName} :" + " " + $"{size}" + "Ko" + " " + "Has been copied to" + " " + $"{target}");



				}

			});

			if (test_diff == false)
			{
				Thread.Sleep(50);
				Console.WriteLine("\nNo new files to backup , No action taken");

			}
		}
		public void loadSave()
		{
			
			if (save_loader == 1)
			{

				int fileCount = Directory.GetFiles(targetDirectoryPath, "*.*", SearchOption.AllDirectories).Length; //using filecount to count how many files got copied
				switch (lg)
				{
					case "1":
						Console.WriteLine("last save info : " + "\n" +
							"Name of save: " + $"{name}" + " \n" +
							"Type of save: " + "Full Backup" + "\n");
						Console.WriteLine("Source Directory was: " + $"{sourceDirectoryPath}" + "\n" +
							"Target Directory was: " + $"{targetDirectoryPath}" + "\n" +
							"Total Copied Files: " + $"{fileCount}" + "\n" +
							"Total size of copied files: ", DirSize(new DirectoryInfo(targetDirectoryPath))); //using dirSize to print total size of copied files
						break;
					case "2":
						Console.WriteLine("dernières informations de sauvegarde: " + "\n" +
					"Nom de la sauvgarde: " + $"{name}" + " \n" +
					"Type de sauvgarde: " + "Sauvgarde complete" + "\n");
						Console.WriteLine("Repertoire source était: " + $"{sourceDirectoryPath}" + "\n" +
							"Répertoire cible était: " + $"{targetDirectoryPath}" + "\n" +
							"totale des fichiers copiés " + $"{fileCount}" + "\n" +
							"Tptale de taille des fichiers copiés: ", DirSize(new DirectoryInfo(targetDirectoryPath))); //using dirSize to print total size of copied files
						break;
				}
				
			}
			if (save_loader == 2)
			{
				int fileCount = Directory.GetFiles(targetDirectoryPath, "*.*", SearchOption.AllDirectories).Length;
				switch (lg)
				{
					case "2":
				Console.WriteLine("dernières informations de sauvegarde: : " + "\n" +
						"Nom de la sauvegarde: " + $"{name}" + " \n" +
						"Type of save: " + "Sauvegarde différentielle" + "\n");
				Console.WriteLine("Répertoire source était: " + $"{sourceDirectoryPath}" + "\n" +
					"Répertoire cible était: " + $"{targetDirectoryPath}" + "\n" +
					"Totale fichiers copiés " + $"{fileCount}" + "\n" +
					"Total taille des fichiers copiés: {0} Ko.", DirSize(new DirectoryInfo(targetDirectoryPath)));
						break;
					case "1":
						Console.WriteLine("last save info : " + "\n" +
								"Name of save: " + $"{name}" + " \n" +
								"Type of save: " + "Differential Backup" + "\n");
						Console.WriteLine("Source Directory was: " + $"{sourceDirectoryPath}" + "\n" +
							"Target Directory was: " + $"{targetDirectoryPath}" + "\n" +
							"Total Copied Files: " + $"{fileCount}" + "\n" +
							"Total size of copied files: {0} Ko.", DirSize(new DirectoryInfo(targetDirectoryPath)));

						break;
				}
			}
			else if (save_loader == 0)
			{
				switch (lg)
				{
					case "1":
				Console.WriteLine("You haven't done any save yet, Press 2 to start a new Save | Or press 0 to go back to menu");
						break;
					case "2":
						Console.WriteLine(" Vous n'avez pas encore effectué de sauvegarde, appuyez sur 2 pour démarrer une nouvelle sauvegarde | Ou appuyez sur 0 pour retourner au menu");
						break;
				}
				usrInput1 = Console.ReadLine();
			}
			if (usrInput1 == "2")
			{
				switch (lg)
				{
					case "1":
				Console.WriteLine("Starting Backup process...");
						break;
					case "2":
						Console.WriteLine(" Démarrage du processus de sauvegarde...");
						break;
				}
				Thread.Sleep(250);
				Backup();
			}
			else if (usrInput1 == "0")
			{
				switch (lg)
				{
					case "1":
				Console.WriteLine("Going back to menu...");
						break;
					case "2":
						Console.WriteLine("Retour au menu...");
						break;
				}
				Thread.Sleep(750);
				goback_to_menu();
			}

		}


		private void goback_to_menu() //method used to send back user to fresh menu interface
		{
			View goback = new View();
			Console.Clear();
			goback.Logo();
			goback.displayMenu();
			goback.choseMenuOption();
		}



		public void Save_counter(int v) //counter to limit backup usage to 5 process per Console session
		{
			
			if (save_counter == 5)
			{
				switch (lg)
				{
					case "1":
				Console.WriteLine(
					"===================================================== \n" +
					"ATTENTION:" + " " + "You have reached the max of 5 Backups" + "\n" +
					"====================================================="
					);
				Console.WriteLine("Press any key to exit program");
						break;
					case "2":
				Console.WriteLine(
					"===================================================== \n" +
					"ATTENTION:" + " " + " Vous avez atteint le maximum de 5 sauvegardes" + "\n" +
					"====================================================="
					);
				Console.WriteLine("Appuyez sur n'importe quelle touche pour quitter le programme");
						break;
				}
				Console.ReadKey();
				Environment.Exit(0);
			}
			else
			{
				int remaining = 5 - save_counter;
				switch (lg)
				{
					case "1":
				Console.WriteLine(
					"====================================== \n" +
					"NOTE:" + " " + "You have " + $"{remaining}" + " " + "Backups Left to use \n" +
					"======================================"
					);
						break;
					case "2":
						Console.WriteLine(
							"====================================== \n" +
							"NOTE:" + " " + "Vous avez " + $"{remaining}" + " " + " Sauvegardes restantes à utiliser \n" +
							"======================================"
							);
						break;
				}
			}


		}
		public long DirSize(DirectoryInfo d) //method used to calculate size of the directory user just saved
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
	}
}