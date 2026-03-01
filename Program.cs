using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PRG_281_Project
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Code to load a loading bar before user login
            Thread.Sleep(100);//Makes threads load 100milliseconds later so that app can properly load
            Thread circle = new Thread(LoadingBar.CircleBar);
            Thread percentage = new Thread(LoadingBar.percentageLoader);
            circle.Start();
            percentage.Start();
            circle.Join();
            percentage.Join();

            Thread.Sleep(500);
            Console.Clear();

            //Code that instantiates an instance of the userlogin class to display the login
            UserLogin userLogin = new UserLogin();
            userLogin.displayLogin();

            bool Continue = true;
            do
            {
                string prompt = @"
        
                               ___          _                 __   _________       _     
                              /   |  ____  (_)___ ___  ____ _/ /  / ____/ (_)___  (_)____
                             / /| | / __ \/ / __ `__ \/ __ `/ /  / /   / / / __ \/ / ___/
                            / ___ |/ / / / / / / / / / /_/ / /  / /___/ / / / / / / /__  
                           /_/  |_/_/ /_/_/_/ /_/ /_/\__,_/_/   \____/_/_/_/ /_/_/\___/  
                                                              
                (Use the arrow keys to cycle through options and press enter to select an option.";
                string[] options = { "Add an animal", "View All Animals", "Search for animals", "View medical inventory", "Calculate medical dosages", "Add employee", "Add login", "Manage apointments", "Calculate employee salaries", "Exit" };

                //Code that instantiates an instance of the class mainMenu in order to display the main menu
                Menu mainMenu = new Menu(prompt, options);
                main_Menu choice = (main_Menu)(mainMenu.Run());

                //Switch case that selects the value in the enum corresponding to where the menu was clicked
                switch (choice)
                {
                    case main_Menu.Add_Animal:
                        Console.Clear();
                        Console.CursorVisible = true;
                        MenuMethods addAnimal = new MenuMethods();
                        addAnimal.AddAnimal();
                        break;
                    case main_Menu.View_All_Animals:
                        Console.Clear();
                        MenuMethods viewAllAnimals = new MenuMethods();
                        viewAllAnimals.ViewAnimals();
                        break;
                    case main_Menu.Search_Animal:
                        Console.Clear();
                        MenuMethods searchAnimal = new MenuMethods();
                        searchAnimal.SearchAnimal();
                        break;
                    case main_Menu.View_Med_Inventory:
                        Console.Clear();
                        MenuMethods viewMedication = new MenuMethods();
                        viewMedication.MedsInventory();
                        break;
                    case main_Menu.Calc_Med_Dosage:
                        Console.Clear();
                        MenuMethods calcDosage = new MenuMethods();
                        calcDosage.CalculateDosage();
                        break;
                    case main_Menu.Add_Employee:
                        Console.Clear();
                        MenuMethods addEmployee = new MenuMethods();
                        addEmployee.AddEmployee();
                        break;
                    case main_Menu.Add_Login:
                        MenuMethods login = new MenuMethods();
                        login.AddLogin(userLogin);
                        Console.Clear();
                        userLogin.displayLogin();
                        break;
                    case main_Menu.Manage_Appointments:
                        Console.Clear();
                        MenuMethods manageAppointments = new MenuMethods();
                        manageAppointments.ManageAppointment();
                        break;
                    case main_Menu.Calc_Emp_Salary:
                        Console.Clear();
                        MenuMethods calcEmp = new MenuMethods();
                        calcEmp.CalcEmpSalary();
                        break;
                    case main_Menu.Exit_Program:
                        MenuMethods exiter = new MenuMethods();
                        exiter.ExitProgram();
                        break;
                }//End of choice switch case
            }
            while (Continue == true);

        }//End of main
    }//End of internal class
}//End of namespace
