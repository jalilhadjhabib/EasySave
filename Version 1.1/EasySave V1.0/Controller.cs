using System;
using System.Collections.Generic;
using System.Text;

namespace EasySave_V1._0
{
	class Controller
	{
		private Model model;
		private View view;

		public Controller()
		{
			//The model and the view can be instantiate in the controller, or in the main program(see graphicalApp)
			model = new Model();
			view = new View();

			//Linking the controller to the view, so the view is able to notice the controller when the user gives a valid input
			view.SetController(this);
			view.Logo();
			view.displayMenu();
			view.choseMenuOption();
			Console.ReadLine();

		}
	}
}