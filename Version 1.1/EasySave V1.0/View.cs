using System;
using System.Collections.Generic;
using System.Text;

namespace EasySave_V1._0
{
	class logs
	{

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
	class state
	{
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
		public int nbFiles
		{
			get;
			set;
		}
		public long sizeFile_KO
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

	}
	class View
	{
		private string menu,
		option1,lg,
		option2,
		option3,
		option4;
		public string usrInput1;
		private bool goodInput;
		private Controller controller;
		private Model model;
		public View()
		{
			model = new Model();
		}

		public Controller Controller
		{
			get
			{
				return Controller;
			}
			set
			{
				Controller = value;
			}
		}
		//Function to print the logo in the console
		public void Logo()
		{
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine(@"
                                                         
                                                        
                      
                           ███████╗ █████╗ ███████╗██╗   ██╗███████╗ █████╗ ██╗   ██╗███████╗
                           ██╔════╝██╔══██╗██╔════╝╚██╗ ██╔╝██╔════╝██╔══██╗██║   ██║██╔════╝
                           █████╗  ███████║███████╗ ╚████╔╝ ███████╗███████║██║   ██║█████╗  
                           ██╔══╝  ██╔══██║╚════██║  ╚██╔╝  ╚════██║██╔══██║╚██╗ ██╔╝██╔══╝  
                           ███████╗██║  ██║███████║   ██║   ███████║██║  ██║ ╚████╔╝ ███████╗
                           ╚══════╝╚═╝  ╚═╝╚══════╝   ╚═╝   ╚══════╝╚═╝  ╚═╝  ╚═══╝  ╚══════╝
                                                                  
                      
                                 
                                                                                                                  
            ");
		}
		//Function to display the menu the logo in the console
		 
		public void displayMenu()
		{
			model.chooselg();
			switch (model.lg)
			{
				case "1": //Display the instructions for the user
					menu = "Welcome to EasySave backup software \n\n";
					option1 = "1: Instructions \n";
					option2 = "2: New save \n";
					option3 = "3: Load last save information \n";
					option4 = "4: Exit \n";
					break;
				case "2":
					menu = "Bienvenue dans le logiciel de sauvegarde EasySave\n\n";
					option1 = "1: Instructions \n";
					option2 = "2: Nouvelle sauvegarde \n";
					option3 = "3: Charger les dernières informations de sauvegarde \n";
					option4 = "4: Quitter \n";
					break;

			}
			

			centerText(menu);
			centerText(option1);
			centerText(option2);
			centerText(option3);
			centerText(option4);

		}
		public void SetController(Controller cont)
		{
			controller = cont;
		}
		//Center the displayed menu
		private static void centerText(String text)
		{
			Console.Write(new string(' ', (Console.WindowWidth - text.Length) / 2));
			Console.WriteLine(text);
		}
		//Function to let the user choose options
		public void choseMenuOption()
		{

			goodInput = false;

			while (!goodInput)
			{

			restartMenuApp:
                switch (model.lg)
                {
					case "1":
						Console.WriteLine("Please choose between 1~4 then press \"Enter\":");
						usrInput1 = Console.ReadLine();
						switch (usrInput1)
						{
							case "1": //Display the instructions for the user
								model.displayInstruction();
								goodInput = false;
								break;
							case "2": //Execute the backup function						
								model.Backup();
								Console.WriteLine("Press any key to return to menu");
								Console.ReadKey();
								Console.WriteLine("\n \n \n \n");
								Logo();

								displayMenu();

								goodInput = false;
								break;
							case "3": //Execute the load save function
								model.loadSave();
								goodInput = false;
								break;

							case "4": //Exit the software
								Console.WriteLine("Closing. \n");
								Environment.Exit(0);
								break;
						}
						break;
					case "2":
						Console.WriteLine("Veuillez choisir entre 1 et 4 puis appuyez sur \"Entrer\":");
						usrInput1 = Console.ReadLine();
						switch (usrInput1)
						{
							case "1": //Display the instructions for the user
								model.displayInstruction();
								goodInput = false;
								break;
							case "2": //Execute the backup function						
								model.Backup();
								Console.WriteLine("Appuyez sur n'importe quelle touche pour revenir au menu");
								Console.ReadKey();
								Console.WriteLine("\n \n \n \n");
								Logo();

								displayMenu();

								goodInput = false;
								break;
							case "3": //Execute the load save function
								model.loadSave();
								goodInput = false;
								break;

							case "4": //Exit the software
								Console.WriteLine("Fermeture. \n");
								Environment.Exit(0);
								break;
						}
						break;
                }
				
			}
		}
	}
}