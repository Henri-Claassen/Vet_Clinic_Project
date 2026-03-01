using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG_281_Project
{
    #region MainMenu
    enum main_Menu
    {
        Add_Animal = 0,
        View_All_Animals,
        Search_Animal,
        View_Med_Inventory,
        Calc_Med_Dosage,
        Add_Employee,
        Add_Login,
        Manage_Appointments,
        Calc_Emp_Salary,
        Exit_Program
    }
    #endregion

    #region Choose Animal
    enum chooseAnimal
    {
        Add_Mammal,
        Add_Reptile,
        Add_Bird,
        Add_Water_Animal
    }
    #endregion

    #region Display Animal Owner
    enum displayAnimalOwner
    {
        Display_Animal,
        Display_Owner
    }
    #endregion

    #region Search Animal Owner
    enum searchAnimalOwner
    {
        Search_Animal,
        Search_Owner
    }
    #endregion

    #region View Meds
    enum viewMeds
    {
        Add_Med,
        View_Meds_Storage,
        Search_Med
    }
    #endregion

    #region Pick Task Employee
    enum pickTaskEmployee
    {
        Add_Employee,
        View_Employee,
        Search_Employee
    }
    #endregion

    #region Pick Employee
    enum pickEmployee
    {
        Vet,
        Manager,
        Assistant_Nurse
    }
    #endregion

    #region AppointmentMenu
    enum appointmentMenu
    {
        Create_Appointment,
        Reschedule_Appointment,
        Cancel_Appointment,
        View_Appointments,
        Back_To_Menu
    }
    #endregion

    #region CalculationsMenu
    enum calculationsMenu
    {
        Calc_Dosage,
        Check_Stock,
        Update_Stock
    }
    #endregion

    #region Add Remove Stock Menu
    enum addRemoveStock
    {
        Add,
        Remove
    }
    #endregion
    internal class Menu
    {
        #region menu
        private int SelectedIndex;
        private string[] Options;
        private string Prompt;

        public Menu(string prompt, string[] options)
        {
            Prompt = prompt;
            Options = options;
            SelectedIndex = 0;
        }

        private void DisplayOptions()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(Prompt);

            Console.WriteLine("\n");

            for (int i = 0; i < Options.Length; i++)
            {
                string currentOption = Options[i];
                string prefix;
                string suffix;

                if (i == SelectedIndex)
                {
                    prefix = "->";
                    suffix = "<-";
                    Console.ForegroundColor = ConsoleColor.Magenta;
                }
                else
                {
                    prefix = " ";
                    suffix = " ";
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                }


                Console.WriteLine($"\t\t\t\t\t\t{prefix} {currentOption} {suffix}");
            }
            Console.ResetColor();
            Console.CursorVisible = false;
        }
        public int Run()
        {

            ConsoleKey keypressed;
            do
            {
                Console.Clear();
                DisplayOptions();

                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                keypressed = keyInfo.Key;

                //Update SelectedIndex based on arrow keys
                if (keypressed == ConsoleKey.UpArrow)
                {
                    SelectedIndex--;
                    if (SelectedIndex == -1)
                    {
                        SelectedIndex = Options.Length - 1;
                    }
                }
                else if (keypressed == ConsoleKey.DownArrow)
                {
                    SelectedIndex++;
                    if (SelectedIndex == Options.Length)
                    {
                        SelectedIndex = 0;
                    }
                }
            }
            while (keypressed != ConsoleKey.Enter);

            return SelectedIndex;
        }



        #endregion
    }

}

