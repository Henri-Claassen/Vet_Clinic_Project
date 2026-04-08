using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace PRG_281_Project
{
    internal class MenuMethods
    {
        private static IList<Owner> ownerList = new List<Owner>()
        {
            new Owner("Ben",12,1234,"Outsurance","Deluxe package")
        };
        private static IList<Animal> animalList = new List<Animal>()
        {
            new Mammals("Ben",12,1234,"Outsurance","Deluxe package","Spotty",12,30,1,"Male","Omnivore","Fluffy",4)
        };
        private static IList<Employee> employeeList = new List<Employee>()
        {
            new Vet("John","Wick",1,40,"Specialised in dogs"),
            new Manager("James","Bond",2,20,"Specialised in dogs","Pretoria"),
            new AssistantNurse("Call the","Bondulance",3,35,"Feeding animals")
        };

        public delegate void DisplayPopUpHandler(string animalName);
        public event DisplayPopUpHandler DisplayPopUp;

        #region calcID
        private int calcID(string input)
        {
            int IDGenerated = 0;
            if(input is "owner")
            {
                IDGenerated = ownerList.Count + 1;
            }
            else if (input is "animal")
            {
                IDGenerated = animalList.Count + 1;
            }
            else if (input is "employee")
            {
                IDGenerated = employeeList.Count + 1;
            }

                return IDGenerated;
        }
        #endregion

        #region AddAnimal
        public void AddAnimal()
        {
            bool enterOwnerSuccess = false, ownerAddSuccess = false;
            int ownerID = 0, coverageNr = 0;
            string ownerName = "", coverageName = "", coveragePackage = "";
            while (!enterOwnerSuccess)
            {
                try
                {
                    Console.Write("Enter Owner ID: ");
                    ownerID = int.Parse(Console.ReadLine());
                    enterOwnerSuccess = true;
                }
                catch (FormatException)
                {
                    Console.Clear();
                    Console.WriteLine("Invalid format please enters numbers where required");
                }
                catch (ArgumentException ex)
                {
                    Console.Clear();
                    Console.WriteLine(ex.Message);
                }
                catch (Exception)
                {
                    Console.Clear();
                    Console.WriteLine("Unexpected error please try again");
                }
            }
            // Search for owner
            Owner currentOwner = ownerList.FirstOrDefault(o => o.OwnerID.Equals(ownerID));

            if (currentOwner == null)
            {
                Console.Clear();
                // Owner not found, create new one
                while (!ownerAddSuccess)
                {
                    try
                    {
                        Console.WriteLine("Owner not found please add owner:");
                        Console.WriteLine($"Saved owner ID: {ownerID}");

                        Console.Write("Enter Owner Name: ");
                        ownerName = Console.ReadLine();

                        Console.Write("Enter Coverage Number: ");
                        coverageNr = int.Parse(Console.ReadLine());

                        Console.Write("Enter Coverage Name: ");
                        coverageName = Console.ReadLine();

                        Console.Write("Enter Coverage Package: ");
                        coveragePackage = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(ownerName) || string.IsNullOrWhiteSpace(coverageName) || string.IsNullOrWhiteSpace(coveragePackage))
                        {
                            Console.Clear();
                            Console.WriteLine("Owner Name and CoverageName and CoveragePackage cannot be empty");
                            continue;
                        }
                        ownerAddSuccess = true;
                    }
                    catch (FormatException)
                    {
                        Console.Clear();
                        Console.WriteLine("Invalid format please enters numbers where required");
                    }
                    catch (ArgumentException ex)
                    {
                        Console.Clear();
                        Console.WriteLine(ex.Message);
                    }
                    catch (Exception)
                    {
                        Console.Clear();
                        Console.WriteLine("Unexpected error please try again");
                    }
                }

                currentOwner = new Owner(ownerName, ownerID, coverageNr, coverageName, coveragePackage);
                ownerList.Add(currentOwner);

                Console.Clear();
                Console.WriteLine($"New owner created: {currentOwner.OwnerName}");
            }
            else
            {
                Console.Clear();
                Console.WriteLine($"Owner found: {currentOwner.OwnerName}");
            }
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
            string prompt = "                                       Use arrow keys to select animal type:";
            string[] options = { "Add Mammal", "Add Reptile", "Add Bird", "Add Water Animal" };
            // Select animal type 
            Menu chooseAnimal = new Menu( prompt, options );
            chooseAnimal typeChoice = (chooseAnimal)(chooseAnimal.Run());
            Console.CursorVisible = true;
            Console.Clear();
            // Common animal info
            bool animalAddSuccess = false;
            string animalName = "", diet = "";
            int animalAge = 0;
            double weight = 0;
            while (!animalAddSuccess)
            {
                try
                {
                    Console.Write("Enter Animal Name: ");
                    animalName = Console.ReadLine();

                    Console.Write("Enter Animal Age: ");
                    animalAge = int.Parse(Console.ReadLine());

                    Console.Write("Enter Animal Diet: ");
                    diet = Console.ReadLine();

                    Console.Write("Enter Animal Weight (kg): ");
                    weight = double.Parse(Console.ReadLine());
                    if (string.IsNullOrWhiteSpace(animalName) || string.IsNullOrWhiteSpace(diet))
                    {
                        Console.Clear();
                        Console.WriteLine("Animal Name and Diet cannot be empty");
                        continue;
                    }
                    animalAddSuccess = true;
                }
                catch (FormatException)
                {
                    Console.Clear();
                    Console.WriteLine("Invalid format please enters numbers where required");
                }
                catch (ArgumentException ex)
                {
                    Console.Clear();
                    Console.WriteLine(ex.Message);
                }
                catch (Exception)
                {
                    Console.Clear();
                    Console.WriteLine("Unexpected error please try again");
                }
            }
            bool genderAccepted = false;
            string gender = "";
            while (genderAccepted == false)
            {
                Console.Write("Enter Animal Gender (M/F): ");
                gender = Console.ReadLine();

                if (gender.Equals("M", StringComparison.OrdinalIgnoreCase))
                {
                    gender = "Male";
                    genderAccepted = true;
                }
                else if (gender.Equals("F", StringComparison.OrdinalIgnoreCase))
                {
                    gender = "Female";
                    genderAccepted = true;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Invalid gender. Please enter either M = Male or F = Female.");
                }
            }
            Animal newAnimal = null;

            // Create specific type
            switch (typeChoice)
            {
                case PRG_281_Project.chooseAnimal.Add_Mammal:
                    bool addMammalSuccess = false;
                    string fur = "";
                    int legs = 0;
                    while (!addMammalSuccess)
                    {
                        try
                        {
                            Console.Write("Enter Fur Type: ");
                            fur = Console.ReadLine();
                            Console.Write("Enter Leg Count: ");
                            legs = int.Parse(Console.ReadLine());
                            if (string.IsNullOrWhiteSpace(fur))
                            {
                                Console.Clear();
                                Console.WriteLine("Fur cannot be empty");
                                continue;
                            }
                            addMammalSuccess = true;
                        }
                        catch (FormatException)
                        {
                            Console.Clear();
                            Console.WriteLine("Invalid format please enters numbers where required");
                        }
                        catch (ArgumentException ex)
                        {
                            Console.Clear();
                            Console.WriteLine(ex.Message);
                        }
                        catch (Exception)
                        {
                            Console.Clear();
                            Console.WriteLine("Unexpected error please try again");
                        }
                    }

                    int MammalID = calcID("animal");
                    newAnimal = new Mammals(currentOwner.OwnerName, currentOwner.OwnerID, currentOwner.OwnerCoverageNr, currentOwner.OwnerCoverageName, currentOwner.OwnerCoveragePackage, animalName, animalAge, weight, MammalID, gender, diet, fur, legs);//more eparameters because we are passing only 6 whenit should be 11
                    break;
                case PRG_281_Project.chooseAnimal.Add_Reptile:
                    bool addReptileSuccess = false;
                    string scale = "";
                    while (!addReptileSuccess)
                    {
                        try
                        {
                            Console.Write("Enter Scale Type: ");
                            scale = Console.ReadLine();
                            if (string.IsNullOrWhiteSpace(scale))
                            {
                                Console.Clear();
                                Console.WriteLine("Scale cannot be empty");
                                continue;
                            }
                            addReptileSuccess = true;
                        }
                        catch (FormatException)
                        {
                            Console.Clear();
                            Console.WriteLine("Invalid format please enters numbers where required");
                        }
                        catch (ArgumentException ex)
                        {
                            Console.Clear();
                            Console.WriteLine(ex.Message);
                        }
                        catch (Exception)
                        {
                            Console.Clear();
                            Console.WriteLine("Unexpected error please try again");
                        }
                    }

                    int ReptileID = calcID("animal");
                    newAnimal = new Reptiles(currentOwner.OwnerName, currentOwner.OwnerID, currentOwner.OwnerCoverageNr, currentOwner.OwnerCoverageName, currentOwner.OwnerCoveragePackage, animalName, animalAge, weight, ReptileID, gender, diet, scale);
                    break;
                case PRG_281_Project.chooseAnimal.Add_Bird:
                    bool addBirdSuccess = false;
                    double wings = 0;
                    while (!addBirdSuccess)
                    {
                        try
                        {
                            Console.Write("Enter Wingspan: ");
                            wings = double.Parse(Console.ReadLine());
                            addBirdSuccess = true;
                        }
                        catch (FormatException)
                        {
                            Console.Clear();
                            Console.WriteLine("Invalid format please enters numbers where required");
                        }
                        catch (ArgumentException ex)
                        {
                            Console.Clear();
                            Console.WriteLine(ex.Message);
                        }
                        catch (Exception)
                        {
                            Console.Clear();
                            Console.WriteLine("Unexpected error please try again");
                        }
                    }

                    int BirdID = calcID("animal");
                    newAnimal = new Birds(currentOwner.OwnerName, currentOwner.OwnerID, currentOwner.OwnerCoverageNr, currentOwner.OwnerCoverageName, currentOwner.OwnerCoveragePackage, animalName, animalAge, weight, BirdID, gender, diet, wings);
                    break;
                case PRG_281_Project.chooseAnimal.Add_Water_Animal:
                    bool addWaterAnimalSuccess = false;
                    int gills = 0, fins = 0;
                    while (!addWaterAnimalSuccess)
                    {
                        try
                        {
                            Console.Write("Enter Gill Count: ");
                            gills = int.Parse(Console.ReadLine());
                            Console.Write("Enter Fin Count: ");
                            fins = int.Parse(Console.ReadLine());
                            addWaterAnimalSuccess = true;
                        }
                        catch (FormatException)
                        {
                            Console.Clear();
                            Console.WriteLine("Invalid format please enters numbers where required");
                        }
                        catch (ArgumentException ex)
                        {
                            Console.Clear();
                            Console.WriteLine(ex.Message);
                        }
                        catch (Exception)
                        {
                            Console.Clear();
                            Console.WriteLine("Unexpected error please try again");
                        }
                    }

                    int WaterAnimalID = calcID("animal");
                    newAnimal = new WaterAnimals(currentOwner.OwnerName, currentOwner.OwnerID, currentOwner.OwnerCoverageNr, currentOwner.OwnerCoverageName, currentOwner.OwnerCoveragePackage, animalName, animalAge, weight, WaterAnimalID, gender, diet, gills, fins);
                    break;
            }

            if (newAnimal != null)
            {
                animalList.Add(newAnimal);

                // If Owner class has a Pets list, add animal to it
                if (currentOwner.Pets == null)
                {
                    currentOwner.Pets = new List<Animal>();
                }//End of IF

                currentOwner.Pets.Add(newAnimal);

                Console.WriteLine($"Animal {newAnimal.AnimalName} added to owner {currentOwner.OwnerName}.");
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }
        #endregion

        #region View Animals
        public void ViewAnimals()
        {
            string prompt = @"                  
                                            ______ _           _             
                                            |  _  (_)         | |            
                                            | | | |_ ___ _ __ | | __ _ _   _ 
                                            | | | | / __| '_ \| |/ _` | | | |
                                            | |/ /| \__ \ |_) | | (_| | |_| |
                                            |___/ |_|___/ .__/|_|\__,_|\__, |
                                                        | |             __/ |
                                                        |_|            |___/ 
";
            string[] options = { "To Display animals", "To display owners" };

            Menu displayAnimals = new Menu(prompt, options);
            displayAnimalOwner ViewChoice = (displayAnimalOwner)(displayAnimals.Run());
            Console.Clear();
            switch (ViewChoice)
            {
                case displayAnimalOwner.Display_Animal:
                    if (animalList.Count == 0)
                    {
                        Console.WriteLine("No animals found");
                    }
                    else
                    {
                        Console.WriteLine("Animals in the system :");
                        foreach (var animal in animalList)
                        {
                            if (animal is Mammals mammal)
                            {
                                Console.WriteLine($"Name: {mammal.AnimalName}");
                                Console.WriteLine($"Mammal Id: {mammal.AnimalID}");
                                Console.WriteLine($"Mammal Age: {mammal.AnimalAge}");
                                Console.WriteLine($"Mammal Weight: {mammal.AnimalWeight}");
                                Console.WriteLine($"Mammal Diet: {mammal.DietType}");
                                Console.WriteLine($"Mammal Fur Type: {mammal.FurType}");
                                Console.WriteLine($"Mammal Leg Count: {mammal.LegCount}");
                                Console.WriteLine("----------------------");
                            }
                            else if (animal is Reptiles reptile)
                            {
                                Console.WriteLine($"Name: {reptile.AnimalName}");
                                Console.WriteLine($"Reptile Id: {reptile.AnimalID}");
                                Console.WriteLine($"Reptile Age: {reptile.AnimalAge}");
                                Console.WriteLine($"Reptile Weight: {reptile.AnimalWeight}");
                                Console.WriteLine($"Reptile Diet: {reptile.DietType}");
                                Console.WriteLine($"Reptile Scale Type: {reptile.ScaleType}");
                                Console.WriteLine("----------------------");
                            }
                            else if (animal is Birds bird)
                            {
                                Console.WriteLine($"Name: {bird.AnimalName}");
                                Console.WriteLine($"Bird Id: {bird.AnimalID}");
                                Console.WriteLine($"Bird Age: {bird.AnimalAge}");
                                Console.WriteLine($"Bird Weight: {bird.AnimalWeight}");
                                Console.WriteLine($"Bird Diet: {bird.DietType}");
                                Console.WriteLine($"Bird Wingspan: {bird.WingSpan}");
                                Console.WriteLine("----------------------");
                            }
                            else if (animal is WaterAnimals wateranimal)
                            {
                                Console.WriteLine($"Name: {animal.AnimalName}");
                                Console.WriteLine($"Wateranimal Id: {wateranimal.AnimalID}");
                                Console.WriteLine($"Wateranimal Age: {wateranimal.AnimalAge}");
                                Console.WriteLine($"Wateranimal Weight: {wateranimal.AnimalWeight}");
                                Console.WriteLine($"Wateranimal Diet: {wateranimal.DietType}");
                                Console.WriteLine($"Wateranimal Gil Count: {wateranimal.GilCount}");
                                Console.WriteLine($"Wateranimal Fin Count: {wateranimal.FinCount}");
                                Console.WriteLine("----------------------");
                            }

                        }
                    }
                    break;
                case displayAnimalOwner.Display_Owner:
                    if (ownerList.Count == 0)
                    {
                        Console.WriteLine("No Owners found");
                    }
                    else
                    {
                        Console.WriteLine("Owners In the system :");
                        foreach (var owner in ownerList)
                        {
                            Console.WriteLine($"Owner Name:{owner.OwnerName} ");
                            Console.WriteLine($"Owner insurance Coverage:{owner.OwnerCoverageName} ");
                            Console.WriteLine($"Owner Coverage Number: {owner.OwnerCoverageNr}");
                            Console.WriteLine($"Owner Coverage Package {owner.OwnerCoveragePackage} ");
                            Console.WriteLine("----------------------");
                        }
                    }
                    break;
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }
        #endregion

        #region Search Animal
        public void SearchAnimal()
        {
            string prompt =
                @" 
                                             _____                     _     
                                            /  ___|                   | |    
                                            \ `--.  ___  __ _ _ __ ___| |__  
                                             `--. \/ _ \/ _` | '__/ __| '_ \ 
                                            /\__/ /  __/ (_| | | | (__| | | |
                                            \____/ \___|\__,_|_|  \___|_| |_|
";
            string[] options = { "To Search for animals", "To Search for owners" };
            Menu searchAnimals = new Menu(prompt, options);
            searchAnimalOwner searchanimalowner = (searchAnimalOwner)(searchAnimals.Run());
            switch (searchanimalowner)
            {
                case searchAnimalOwner.Search_Animal:
                    bool searchAnimalSuccess = false;
                    int searchAnimal = 0;
                    if (animalList.Count == 0)
                    {
                        Console.WriteLine(@"Please add animals first");
                    }
                    else
                    {
                        Console.CursorVisible = true;
                        Console.Clear();
                        while (!searchAnimalSuccess)
                        {
                            try
                            { 
                                Console.WriteLine("Enter an animal ID to search");
                                searchAnimal = int.Parse(Console.ReadLine());
                                searchAnimalSuccess = true;
                            }
                            catch (FormatException)
                            {
                                Console.Clear();
                                Console.WriteLine("Invalid format please enters numbers where required");
                            }
                            catch (ArgumentException ex)
                            {
                                Console.Clear();
                                Console.WriteLine(ex.Message);
                            }
                            catch (Exception)
                            {
                                Console.Clear();
                                Console.WriteLine("Unexpected error please try again");
                            }
                        }
                        bool found = false;

                        foreach (var animal in animalList)
                        {
                            if (searchAnimal == animal.AnimalID)
                            {
                                if (animal is Mammals mammal)
                                {
                                    Console.WriteLine($"Name: {mammal.AnimalName}");
                                    Console.WriteLine($"Mammal Id: {mammal.AnimalID}");
                                    Console.WriteLine($"Mammal Age: {mammal.AnimalAge}");
                                    Console.WriteLine($"Mammal Weight: {mammal.AnimalWeight}");
                                    Console.WriteLine($"Mammal Diet: {mammal.DietType}");
                                    Console.WriteLine($"Mammal Fur Type: {mammal.FurType}");
                                    Console.WriteLine($"Mammal Leg Count: {mammal.LegCount}");
                                    Console.WriteLine("----------------------");
                                }
                                else if (animal is Reptiles reptile)
                                {
                                    Console.WriteLine($"Name: {reptile.AnimalName}");
                                    Console.WriteLine($"Reptile Id: {reptile.AnimalID}");
                                    Console.WriteLine($"Reptile Age: {reptile.AnimalAge}");
                                    Console.WriteLine($"Reptile Weight: {reptile.AnimalWeight}");
                                    Console.WriteLine($"Reptile Diet: {reptile.DietType}");
                                    Console.WriteLine($"Reptile Scale Type: {reptile.ScaleType}");
                                    Console.WriteLine("----------------------");
                                }
                                else if (animal is Birds bird)
                                {
                                    Console.WriteLine($"Name: {bird.AnimalName}");
                                    Console.WriteLine($"Bird Id: {bird.AnimalID}");
                                    Console.WriteLine($"Bird Age: {bird.AnimalAge}");
                                    Console.WriteLine($"Bird Weight: {bird.AnimalWeight}");
                                    Console.WriteLine($"Bird Diet: {bird.DietType}");
                                    Console.WriteLine($"Bird Wingspan: {bird.WingSpan}");
                                    Console.WriteLine("----------------------");
                                }
                                else if (animal is WaterAnimals wateranimal)
                                {
                                    Console.WriteLine($"Name: {animal.AnimalName}");
                                    Console.WriteLine($"Wateranimal Id: {wateranimal.AnimalID}");
                                    Console.WriteLine($"Wateranimal Age: {wateranimal.AnimalAge}");
                                    Console.WriteLine($"Wateranimal Weight: {wateranimal.AnimalWeight}");
                                    Console.WriteLine($"Wateranimal Diet: {wateranimal.DietType}");
                                    Console.WriteLine($"Wateranimal Gil Count: {wateranimal.GilCount}");
                                    Console.WriteLine($"Wateranimal Fin Count: {wateranimal.FinCount}");
                                    Console.WriteLine("----------------------");
                                }
                                found = true;
                                break;
                            }
                        }

                        if (!found)
                        {
                            Console.WriteLine($"No animal found with ID: {searchAnimal}");
                        }
                    }
                    break;
                case searchAnimalOwner.Search_Owner:
                    bool searchOwnerSuccess = false;
                    int searchOwner = 0;
                    if (ownerList.Count == 0)
                    {
                        Console.WriteLine(@"Please add owners first");
                    }
                    else
                    {
                        Console.CursorVisible = true;
                        Console.Clear();
                        while (!searchOwnerSuccess)
                        {
                            try
                            {
                                Console.WriteLine("Enter an owner ID to search");
                                searchOwner = int.Parse(Console.ReadLine());
                                searchOwnerSuccess = true;
                            }
                            catch (FormatException)
                            {
                                Console.Clear();
                                Console.WriteLine("Invalid format please enters numbers where required");
                            }
                            catch (ArgumentException ex)
                            {
                                Console.Clear();
                                Console.WriteLine(ex.Message);
                            }
                            catch (Exception)
                            {
                                Console.Clear();
                                Console.WriteLine("Unexpected error please try again");
                            }
                        }
                        bool found = false;

                        foreach (var owner in ownerList)
                        {
                            if (searchOwner == owner.OwnerID)
                            {
                                Console.WriteLine("Owner found!");
                                Console.WriteLine($"Owner Name: {owner.OwnerName}");
                                Console.WriteLine($"Owner ID: {owner.OwnerID}");
                                Console.WriteLine($"Owner Coverage Name: {owner.OwnerCoverageName}");
                                Console.WriteLine($"Owner Coverage Number: {owner.OwnerCoverageNr}");
                                Console.WriteLine($"Owner Coverage Package: {owner.OwnerCoveragePackage}");
                                found = true;
                                break;
                            }
                        }

                        if (!found)
                        {
                            Console.WriteLine($"No owner found with ID: {searchOwner}");
                        }
                    }
                    break;
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
        #endregion

        #region View Medicine
        public void MedsInventory()
        {
            string prompt = @"
                                         __  __          _ _      _            
                                        |  \/  |        | (_)    (_)           
                                        | \  / | ___  __| |_  ___ _ _ __   ___ 
                                        | |\/| |/ _ \/ _` | |/ __| | '_ \ / _ \
                                        | |  | |  __/ (_| | | (__| | | | |  __/
                                        |_|  |_|\___|\__,_|_|\___|_|_| |_|\___|
                                        
                                        
";
            string[] options = { "Adding medicine", "View all medication", "Search Medication" };

            Menu medView = new Menu(prompt, options);
            viewMeds choice = (viewMeds)(medView.Run());

            switch (choice)
            {
                case viewMeds.Add_Med:
                    Console.CursorVisible = true;
                    Medication meds = new Medication();
                    bool addMedSuccess = false;
                    string name = "", code = "", desc = "";
                    double dosage = 0;
                    int stock = 0;
                    Console.Clear();
                    while (!addMedSuccess)
                    {
                        try
                        {
                            Console.WriteLine("=== ADD NEW MEDICATION ===");

                            Console.Write("Enter Medication Name: ");
                            name = Console.ReadLine();

                            Console.Write("Enter Medication Code: ");
                            code = Console.ReadLine();

                            Console.Write("Enter Medication dosage: ");
                            dosage = double.Parse(Console.ReadLine());

                            Console.Write("Enter Medication Description: ");
                            desc = Console.ReadLine();

                            Console.Write("Enter Quantity in stock: ");
                            stock = int.Parse(Console.ReadLine());

                            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(desc)|| string.IsNullOrWhiteSpace(code))
                            {
                                Console.Clear();
                                Console.WriteLine("Name and desc and code cannot be empty");
                                continue;
                            }
                            addMedSuccess = true;
                        }
                        catch (FormatException)
                        {
                            Console.Clear();
                            Console.WriteLine("Invalid format please enters numbers where required");
                        }
                        catch (ArgumentException ex)
                        {
                            Console.Clear();
                            Console.WriteLine(ex.Message);
                        }
                        catch (Exception)
                        {
                            Console.Clear();
                            Console.WriteLine("Unexpected error please try again");
                        }
                    }
                    meds.MedicineList.Add(new Medication(name, code, dosage, desc, stock));
                    Console.WriteLine($"\n{name} medicine Added successfully!");
                    break;
                case viewMeds.View_Meds_Storage:
                    Medication showMeds = new Medication();
                    showMeds.displayStock();
                    break;
                case viewMeds.Search_Med:
                    Console.CursorVisible = true;
                    Medication medSearch = new Medication();
                    string searchCode = "";
                    bool found = false, boolSearchMedSuccess = false;
                    Console.Clear();
                    while (!boolSearchMedSuccess) 
                    {
                        try
                        {
                            Console.Clear();
                            Console.WriteLine("=== Search Medications ===");
                            Console.Write("Enter Medication Code: ");
                            searchCode = Console.ReadLine();
                            if (string.IsNullOrWhiteSpace(searchCode))
                            {
                                Console.Clear();
                                Console.WriteLine("Search code cannot be empty");
                                continue;
                            }
                        }
                        catch (FormatException)
                        {
                            Console.Clear();
                            Console.WriteLine("Invalid format please enters numbers where required");
                        }
                        catch (ArgumentException ex)
                        {
                            Console.Clear();
                            Console.WriteLine(ex.Message);
                        }
                        catch (Exception)
                        {
                            Console.Clear();
                            Console.WriteLine("Unexpected error please try again");
                        }
                    }
                    foreach (var item in medSearch.MedicineList)
                    {
                        if (item.MedCode == searchCode)
                        {
                            Console.WriteLine($"Medicine Name: {item.MedName}");
                            Console.WriteLine($"Medicine code: {item.MedCode}");
                            Console.WriteLine($"Medicine dosage per kg: {item.MedDesc}");
                            Console.WriteLine($"Medicine description: {item.DosagePerKG}");
                            Console.WriteLine($"Medicine in stock: {item.MedsInStock}");
                            found = true;
                            break;
                        }
                    }
                    if (found == false)
                    {
                        Console.WriteLine("No such item was found");
                    }
                    break;
            }//End of switch statement
            Console.CursorVisible = false;
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }
        #endregion

        #region Calc Dosage
        public void CalculateDosage()
        {
            string prompt = @"
                                 _____       _            _       _   _                 
                                /  __ \     | |          | |     | | (_)                
                                | /  \/ __ _| | ___ _   _| | __ _| |_ _  ___  _ __  ___ 
                                | |    / _` | |/ __| | | | |/ _` | __| |/ _ \| '_ \/ __|
                                | \__/\ (_| | | (__| |_| | | (_| | |_| | (_) | | | \__ \
                                 \____/\__,_|_|\___|\__,_|_|\__,_|\__|_|\___/|_| |_|___/                                                      
";
            string[] options = { "Calculate Dosage","Check Stock of all items", "Update Stock" };
            Menu calcMenu = new Menu(prompt, options);
            calculationsMenu choice = (calculationsMenu)(calcMenu.Run());
            Console.Clear();
            switch (choice)
            {
                case calculationsMenu.Calc_Dosage:
                    //Checks if there are any animals or medications stored in the list
                    if (animalList.Count == 0 || Medication.medicineList.Count == 0)
                    {
                        Console.WriteLine("You need at least one animal and one medication in the system");
                        Thread.Sleep(2000);
                        return;
                    }

                    Console.WriteLine("=== Dosage calculation ===");
                    //User selects the number for Animal
                    string animalPrompt = "\t\t\t\t\t\t\tSelect Animal:";
                    string[] animalOptions = animalList.Select(a => $"Animal: {a.AnimalName} (Weight: {a.AnimalWeight}kg)").ToArray();
                    Menu animalDisplay = new Menu(animalPrompt, animalOptions);
                    int animalChoice = animalDisplay.Run();
                    Animal selectedAnimal = animalList[animalChoice];

                    //User selects the number for Medication
                    string medPrompt = "\t\t\t\t\t\t\tSelect Medication:";
                    string[] medOptions = Medication.medicineList.Select(m => $"Medicine: {m.MedName} Dosage: {m.DosagePerKG}mg/kg").ToArray();
                    Menu medDisplay = new Menu(medPrompt, medOptions);
                    int medChoice = medDisplay.Run();
                    Medication selectedMed = Medication.medicineList[medChoice];
                    Console.Clear();
                    double dosage = ((IAnimalMethods)selectedAnimal).calcDosage(selectedMed);
                    Console.WriteLine("\nPress any key to exit...");
                    Console.ReadKey();
                    break;
                case calculationsMenu.Check_Stock:
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine(@"
                                                    _____ _             _    
                                                   /  ___| |           | |   
                                                   \ `--.| |_ ___   ___| | __
                                                    `--. \ __/ _ \ / __| |/ /
                                                   /\__/ / || (_) | (__|   < 
                                                   \____/ \__\___/ \___|_|\_\                    
                          ");
                    Console.ResetColor();
                    foreach (var item in Medication.medicineList)
                    {
                        Console.WriteLine("\t\t\t\t\t\t================================");
                        Console.WriteLine($"\t\t\t\t\t\t{item.MedName} has {item.MedsInStock} items in stock");
                    }
                    Console.WriteLine("\n\t\t\t\t\t\t    Press any key to exit...");
                    Console.ReadKey();
                    break;
                case calculationsMenu.Update_Stock:
                    Console.Clear();
                    string updatePrompt = @"
                                     _   _           _       _         _____ _             _    
                                    | | | |         | |     | |       /  ___| |           | |   
                                    | | | |_ __   __| | __ _| |_ ___  \ `--.| |_ ___   ___| | __
                                    | | | | '_ \ / _` |/ _` | __/ _ \  `--. \ __/ _ \ / __| |/ /
                                    | |_| | |_) | (_| | (_| | ||  __/ /\__/ / || (_) | (__|   < 
                                     \___/| .__/ \__,_|\__,_|\__\___| \____/ \__\___/ \___|_|\_\
                                          | |                                                   
                                          |_|                                                   
";
                    string[] updateOptions = Medication.medicineList.Select(m => $"Medicine: {m.MedName} In Stock: {m.MedsInStock} items").ToArray();
                    Menu updateStock = new Menu(updatePrompt, updateOptions);
                    int updateMedNr = updateStock.Run();
                    Medication medSelected = Medication.medicineList[updateMedNr];               
                    Console.Clear();
                    Console.WriteLine($"Medication selected: {Medication.medicineList[updateMedNr]}");

                    string addRemovePrompt = "\t\t\t\t\t\t Add Or Remove?";
                    string[] addRemoveOptions = {"Add items","Remove items" };
                    Menu addRemoveMenu = new Menu(addRemovePrompt, addRemoveOptions);
                    addRemoveStock choose = (addRemoveStock)addRemoveMenu.Run();

                    switch (choose)
                    {
                        case addRemoveStock.Add:
                            bool addStockSuccess = false;
                            int addMeds = 0;
                            while (!addStockSuccess)
                            {
                                try
                                {
                                    Console.WriteLine("Please enter how many items you are adding");
                                    addMeds = int.Parse(Console.ReadLine());
                                    addStockSuccess = true;
                                }
                                catch (FormatException)
                                {
                                    Console.Clear();
                                    Console.WriteLine("Invalid format please enters numbers where required");
                                }
                                catch (ArgumentException ex)
                                {
                                    Console.Clear();
                                    Console.WriteLine(ex.Message);
                                }
                                catch (Exception)
                                {
                                    Console.Clear();
                                    Console.WriteLine("Unexpected error please try again");
                                }
                            }
                            medSelected.PopupMessage -= medSelected.stockRunOutDisplay;
                            medSelected.PopupMessage -= medSelected.stockDisplay;
                            medSelected.PopupMessage += medSelected.stockDisplay;
                            medSelected.addStock(medSelected, addMeds);
                            break;
                        case addRemoveStock.Remove:
                            bool removeStockSuccess = false;
                            int removeMeds = 0;
                            while (!removeStockSuccess)
                            {
                                try
                                {
                                    Console.WriteLine("Please enter how many items you are removing");
                                    removeMeds = int.Parse(Console.ReadLine());
                                    removeStockSuccess = true;
                                }
                                catch (FormatException)
                                {
                                    Console.Clear();
                                    Console.WriteLine("Invalid format please enters numbers where required");
                                }
                                catch (ArgumentException ex)
                                {
                                    Console.Clear();
                                    Console.WriteLine(ex.Message);
                                }
                                catch (Exception)
                                {
                                    Console.Clear();
                                    Console.WriteLine("Unexpected error please try again");
                                }
                            }      
                            medSelected.PopupMessage -= medSelected.stockRunOutDisplay;
                            medSelected.PopupMessage -= medSelected.stockDisplay;
                            medSelected.PopupMessage += medSelected.stockDisplay;
                            medSelected.removeStock(medSelected, removeMeds);
                            break;
                    }//End of switch
                    break;
            } 
        }
        #endregion

        #region Add Employee
        public void AddEmployee()
        {
            string empPrompt = @"
                                        ______ _      _      _____         _    
                                        | ___ (_)    | |    |_   _|       | |   
                                        | |_/ /_  ___| | __   | | __ _ ___| | __
                                        |  __/| |/ __| |/ /   | |/ _` / __| |/ /
                                        | |   | | (__|   <    | | (_| \__ \   < 
                                        \_|   |_|\___|_|\_\   \_/\__,_|___/_|\_\
                                        
                                        
";
            string[] empOptions = { "Add Employee", "View All Employess", "Search Employee" };

            Menu pickTaskEmp = new Menu(empPrompt, empOptions);
            pickTaskEmployee employeeTaskChoice = (pickTaskEmployee)(pickTaskEmp.Run());
            Console.CursorVisible = true;
            switch (employeeTaskChoice)
            {
                case pickTaskEmployee.Add_Employee:
                    string employeeName ="";
                    string employeeSurname="";
                    int employeeID =0;
                    int hoursWorked =0;
                    Console.Clear();
                    bool success = false;
                    while (!success)
                    {
                        try
                        {
                            Console.WriteLine("ADD EMPLOYEE ");
                            Console.Write("Enter Employee Name: ");
                            employeeName = Console.ReadLine();

                            Console.Write("Enter Employee Surname: ");
                            employeeSurname = Console.ReadLine();

                            Console.Write("Enter Employee ID: ");
                            employeeID = int.Parse(Console.ReadLine());

                            Console.Write("Enter Hours Worked: ");
                            hoursWorked = int.Parse(Console.ReadLine());

                            success = true;
                        }
                        catch (FormatException)
                        {
                            Console.Clear();
                            Console.WriteLine("Invalid format please enters numbers where required");
                        }
                        catch (ArgumentException ex)
                        {
                            Console.Clear();
                            Console.WriteLine(ex.Message);
                        }
                        catch (Exception)
                        {
                            Console.Clear();
                            Console.WriteLine("Unexpected error please try again");
                        }
                    }
                    
                    Console.WriteLine("Press any key to continue");
                    Console.ReadKey();
                    Console.WriteLine("\nSelect Employee Type:");
                    string prompt = @"
                                 _____                _                       
                                |  ___|              | |                      
                                | |__ _ __ ___  _ __ | | ___  _   _  ___  ___ 
                                |  __| '_ ` _ \| '_ \| |/ _ \| | | |/ _ \/ _ \
                                | |__| | | | | | |_) | | (_) | |_| |  __/  __/
                                \____/_| |_| |_| .__/|_|\___/ \__, |\___|\___|
                                               | |             __/ |          
                                               |_|            |___/           
";
                    string[] options = { "Vet", "Manager", "Assistant Nurse" };
                    Menu pickEmp = new Menu(prompt, options);
                    pickEmployee employeeTypeChoice = (pickEmployee)(pickEmp.Run());
                    Console.CursorVisible = true;
                    Employee newEmployee = null;
                    switch (employeeTypeChoice)
                    {
                        case pickEmployee.Vet:
                            bool vetSuccessAdd = false;
                            Console.Clear();
                            while (!vetSuccessAdd)
                            {
                                try
                                {

                                    Console.Write("Enter Specialisation: ");
                                    string specialisation = Console.ReadLine();
                                    if (string.IsNullOrWhiteSpace(specialisation))
                                    {
                                        Console.Clear();
                                        Console.WriteLine("Specilisation cannot be empty");
                                        continue;
                                    }
                                    newEmployee = new Vet(employeeName, employeeSurname, employeeID, hoursWorked, specialisation);
                                    vetSuccessAdd = true;
                                }
                                catch (FormatException)
                                {
                                    Console.Clear();
                                    Console.WriteLine("Invalid format please enters numbers where required");
                                }
                                catch (ArgumentException ex)
                                {
                                    Console.Clear();
                                    Console.WriteLine(ex.Message);
                                }
                                catch (Exception ex)
                                {
                                    Console.Clear();
                                    Console.WriteLine($"Unexpected error please try again {ex.Message}");
                                }
                            }//End of while loop
                            break;
                        case pickEmployee.Manager:
                            bool managerSuccessAdd = false;
                            Console.Clear();
                            while (!managerSuccessAdd)
                            {
                                try
                                {

                                    Console.Write("Enter Manager Title: ");
                                    string managerTitle = Console.ReadLine();
                                    Console.Write("Enter Department: ");
                                    string department = Console.ReadLine();
                                    if (string.IsNullOrWhiteSpace(managerTitle) || string.IsNullOrWhiteSpace(department))
                                    {
                                        Console.Clear();
                                        Console.WriteLine("Specilisation and managertitle cannot be empty");
                                        continue;
                                    }
                                    newEmployee = new Manager(employeeName, employeeSurname, employeeID, hoursWorked, managerTitle, department);
                                    managerSuccessAdd = true;
                                }
                                catch (FormatException)
                                {
                                    Console.Clear();
                                    Console.WriteLine("Invalid format please enters numbers where required");
                                }
                                catch (ArgumentException ex)
                                {
                                    Console.Clear();
                                    Console.WriteLine(ex.Message);
                                }
                                catch (Exception ex)
                                {
                                    Console.Clear();
                                    Console.WriteLine($"Unexpected error please try again {ex.Message}");
                                }
                            }//End of while loop
                            break;
                        case pickEmployee.Assistant_Nurse:
                            bool assistantSuccessAdd = false;
                            Console.Clear();
                            while (!assistantSuccessAdd)
                            {
                                try
                                {
                                    Console.Write("Enter Role: ");
                                    string role = Console.ReadLine();
                                    if (string.IsNullOrWhiteSpace(role))
                                    {
                                        Console.Clear();
                                        Console.WriteLine("Specilisation cannot be empty");
                                        continue;
                                    }
                                    newEmployee = new AssistantNurse(employeeName, employeeSurname, employeeID, hoursWorked, role);
                                    assistantSuccessAdd = true;
                                }
                                catch (FormatException)
                                {
                                    Console.Clear();
                                    Console.WriteLine("Invalid format please enters numbers where required");
                                }
                                catch (ArgumentException ex)
                                {
                                    Console.Clear();
                                    Console.WriteLine(ex.Message);
                                }
                                catch (Exception ex)
                                {
                                    Console.Clear();
                                    Console.WriteLine($"Unexpected error please try again {ex.Message}");
                                }
                            }//End of while loop
                            break;
                    }//End of employeetype switch

                    if (newEmployee != null)
                    {
                        employeeList.Add(newEmployee);
                        Console.WriteLine($"\nEmployee {newEmployee.EmployeeName} {newEmployee.EmployeeSurname} added successfully!");
                        Console.WriteLine($"Employee ID: {newEmployee.EmployeeID}");
                        Console.WriteLine($"Type: {newEmployee.GetType().Name}");
                    }

                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                    Console.Clear();
                    break;
                case pickTaskEmployee.View_Employee:
                    Console.Clear();
                    foreach (var item in employeeList)
                    {
                        Console.WriteLine("==================Employee==================");
                        if (item is Vet vetItem)
                        {
                            Console.WriteLine($"Vet name: {vetItem.EmployeeName}");
                            Console.WriteLine($"Vet surname: {vetItem.EmployeeSurname}");
                            Console.WriteLine($"Vet ID: {vetItem.EmployeeID}");
                            Console.WriteLine($"Vet hours worked: {vetItem.HoursWorked}");
                            Console.WriteLine($"Vet specialisation: {vetItem.Specialisation}");
                        }
                        else if (item is Manager manItem)
                        {
                            Console.WriteLine($"Manager name: {manItem.EmployeeName}");
                            Console.WriteLine($"Manager surname: {manItem.EmployeeSurname}");
                            Console.WriteLine($"Manager ID: {manItem.EmployeeID}");
                            Console.WriteLine($"Manager hours worked: {manItem.HoursWorked}");
                            Console.WriteLine($"Manager title: {manItem.ManagerTitle}");
                            Console.WriteLine($"Manager department: {manItem.Department}");
                        }
                        else if (item is AssistantNurse assistantItem)
                        {
                            Console.WriteLine($"Assistant name: {assistantItem.EmployeeName}");
                            Console.WriteLine($"Assistant surname: {assistantItem.EmployeeSurname}");
                            Console.WriteLine($"Assistant ID: {assistantItem.EmployeeID}");
                            Console.WriteLine($"Assistant hours worked: {assistantItem.HoursWorked}");
                            Console.WriteLine($"Assistant title: {assistantItem.Role}");
                        }
                    }//End of foreach loop
                    Console.WriteLine("\nPress any key to exit...");
                    Console.ReadKey();
                    break;
                case pickTaskEmployee.Search_Employee:
                    Console.Clear();
                    Console.CursorVisible = true;
                    bool found = false;
                    bool empSuccessAdd = false;
                    int empID = 0;
                    while (!empSuccessAdd)
                    {
                        try
                        {
                            Console.WriteLine("Please enter the ID of the employe you are looking for");
                            empID = int.Parse(Console.ReadLine());
                            empSuccessAdd = true;
                        }
                        catch (FormatException)
                        {
                            Console.Clear();
                            Console.WriteLine("Invalid format please enters numbers where required");
                        }
                        catch (ArgumentException ex)
                        {
                            Console.Clear();
                            Console.WriteLine(ex.Message);
                        }
                        catch (Exception ex)
                        {
                            Console.Clear();
                            Console.WriteLine($"Unexpected error please try again {ex.Message}");
                        }
                    }
                    foreach (var item in employeeList)
                    {
                        if (item.EmployeeID == empID)
                        {
                            if (item is Vet vetItem)
                            {
                                Console.WriteLine($"Vet name: {vetItem.EmployeeName}");
                                Console.WriteLine($"Vet surname: {vetItem.EmployeeSurname}");
                                Console.WriteLine($"Vet ID: {vetItem.EmployeeID}");
                                Console.WriteLine($"Vet hours worked: {vetItem.HoursWorked}");
                                Console.WriteLine($"Vet specialisation: {vetItem.Specialisation}");
                            }
                            else if (item is Manager manItem)
                            {
                                Console.WriteLine($"Manager name: {manItem.EmployeeName}");
                                Console.WriteLine($"Manager surname: {manItem.EmployeeSurname}");
                                Console.WriteLine($"Manager ID: {manItem.EmployeeID}");
                                Console.WriteLine($"Manager hours worked: {manItem.HoursWorked}");
                                Console.WriteLine($"Manager title: {manItem.ManagerTitle}");
                                Console.WriteLine($"Manager department: {manItem.Department}");
                            }
                            else if (item is AssistantNurse assistantItem)
                            {
                                Console.WriteLine($"Assistant name: {assistantItem.EmployeeName}");
                                Console.WriteLine($"Assistant surname: {assistantItem.EmployeeSurname}");
                                Console.WriteLine($"Assistant ID: {assistantItem.EmployeeID}");
                                Console.WriteLine($"Assistant hours worked: {assistantItem.HoursWorked}");
                                Console.WriteLine($"Assistant title: {assistantItem.Role}");
                            }
                            found = true;
                        }//End of IF
                    }//End of foreach
                    if (found == false)
                    {
                        Console.WriteLine("No employee found matching ID");
                    }
                    Console.WriteLine("Press any key to exit");
                    Console.ReadKey();
                    break;
            }

        }
        #endregion

        #region Add Login
        public void AddLogin(UserLogin userlogin)
        {
            Console.Clear();
            Console.CursorVisible = true;
            string name = "", password ="", confirmPassword ="";
            bool addToStorage, addLoginSuccess = false;
            do
            {
                addToStorage = true;
                addLoginSuccess = false;
                while (!addLoginSuccess)
                {  
                    try
                    {
                        Console.WriteLine("Create an account:");
                        Console.WriteLine("Enter username:");
                        name = Console.ReadLine();
                        Console.WriteLine("Enter password:");
                        password = Console.ReadLine();
                        Console.WriteLine("Confirm password:");
                        confirmPassword = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(confirmPassword)) 
                        {
                            Console.Clear();
                            Console.WriteLine("Name, password and confirmpassword cannot be empty");
                            continue;
                        }
                        addLoginSuccess = true;
                    }
                    catch (FormatException)
                    {
                        Console.Clear();
                        Console.WriteLine("Invalid format please enters numbers where required");
                    }
                    catch (ArgumentException ex)
                    {
                        Console.Clear();
                        Console.WriteLine(ex.Message);
                    }
                    catch (Exception)
                    {
                        Console.Clear();
                        Console.WriteLine("Unexpected error please try again");
                    }
                }
                
                if (confirmPassword == password)
                {
                    foreach (var stor in userlogin.UniqueStorage)
                    {
                        if (stor.Item1 == name)
                        {
                            Console.Clear();
                            Console.WriteLine("Username already exists");
                            addToStorage = false;
                            break;
                        }
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Passwords do not match");
                    addToStorage = false;
                }
            }
            while (addToStorage == false);


            if (addToStorage == true)
            {
                userlogin.UniqueStorage.Add((name, password));
            }
        }
        #endregion

        #region Calc Employee Salary
        public void CalcEmpSalary()
        {
            Console.WriteLine("CALCULATE EMPLOYEE SALARY");

            if (employeeList.Count == 0)
            {
                Console.WriteLine("There are no employees added to the system.");
            }
            else
            {
                Console.WriteLine("Available Employees:");
                Console.WriteLine("==================");

                string prompt = @"
                                                 _____       _            _           
                                                /  ___|     | |          (_)          
                                                \ `--.  __ _| | __ _ _ __ _  ___  ___ 
                                                 `--. \/ _` | |/ _` | '__| |/ _ \/ __|
                                                /\__/ / (_| | | (_| | |  | |  __/\__ \
                                                \____/ \__,_|_|\__,_|_|  |_|\___||___/      
                                    Please select one of the employees to calculate their salary
";

                string[] options = employeeList.Select(e => $"Employee: {e.EmployeeName} {e.EmployeeSurname} with ID: {e.EmployeeID}").ToArray();

                Menu dynamicList = new Menu(prompt, options);

                int empChoice = dynamicList.Run();
                Console.Clear();
                if (empChoice >= 0 && empChoice <= employeeList.Count)
                {
                    Employee selectedEmp = employeeList[empChoice];

                    // Calculate salary based on employee type
                    if (selectedEmp is Vet)
                    {
                        Vet vet = (Vet)selectedEmp;
                        vet.calcSalary();
                        Console.WriteLine($"\nSalary for {vet.EmployeeName} {vet.EmployeeSurname}:");
                        Console.WriteLine($"Hours Worked: {vet.HoursWorked}");
                        Console.WriteLine($"Rate: R1000 per hour");
                        Console.WriteLine($"Total Salary: R{vet.Salary}");
                    }
                    else if (selectedEmp is Manager)
                    {
                        Manager manager = (Manager)selectedEmp;
                        manager.calcSalary();
                        Console.WriteLine($"\nSalary for {manager.EmployeeName} {manager.EmployeeSurname}:");
                        Console.WriteLine($"Hours Worked: {manager.HoursWorked}");
                        Console.WriteLine($"Rate: R5000 per hour");
                        Console.WriteLine($"Total Salary: R{manager.Salary}");
                    }
                    else if (selectedEmp is AssistantNurse)
                    {
                        AssistantNurse nurse = (AssistantNurse)selectedEmp;
                        nurse.calcSalary();
                        Console.WriteLine($"\nSalary for {nurse.EmployeeName} {nurse.EmployeeSurname}:");
                        Console.WriteLine($"Hours Worked: {nurse.HoursWorked}");
                        Console.WriteLine($"Rate: R200 per hour");
                        Console.WriteLine($"Total Salary: R{nurse.Salary}");
                    }
                    else
                    {
                        // Generic employee calculation
                        Console.WriteLine($"\nSalary for {selectedEmp.EmployeeName} {selectedEmp.EmployeeSurname}:");
                        Console.WriteLine($"Hours Worked: {selectedEmp.HoursWorked}");
                        Console.WriteLine("Note: Specific rate not set for this employee type.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid employee selection.");
                }
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }
        #endregion

        #region Manage Appointment
        public void ManageAppointment()
        {
            if (animalList.Count == 0)
            {
                Console.WriteLine("No animals available.");
                Thread.Sleep(2000);
                return;
            }

            Console.WriteLine("\nSelect an animal:");
            string prompt = @"
                                ___                    _       _                        _       
                               / _ \                  (_)     | |                      | |      
                              / /_\ \_ __  _ __   ___  _ _ __ | |_ _ __ ___   ___ _ __ | |_ ___ 
                              |  _  | '_ \| '_ \ / _ \| | '_ \| __| '_ ` _ \ / _ \ '_ \| __/ __|
                              | | | | |_) | |_) | (_) | | | | | |_| | | | | |  __/ | | | |_\__ \
                              \_| |_/ .__/| .__/ \___/|_|_| |_|\__|_| |_| |_|\___|_| |_|\__|___/
                                    | |   | |                                                   
                                    |_|   |_|                                                   
";
            string[] options = animalList.Select(a => $"Animal: {a.AnimalName} with ID: {a.AnimalID}").ToArray();
            
            Menu dynamicList = new Menu(prompt, options);
            
            int choice = dynamicList.Run();
            
            IAnimalMethods selectedAnimal = (IAnimalMethods)animalList[choice];

            bool exit = false;
            while (!exit)
            {
                string appointmentPrompt = $"\n\t\t\t\t\t\tAppointments for {animalList[choice].AnimalName}:";
                string[] appointmentOptions = { "Create Appointment","Reschedule Appointment","Cancel Appointment","View Appointments","Back to Main Menu" };

                Menu appMenu = new Menu(appointmentPrompt, appointmentOptions);
                appointmentMenu input = (appointmentMenu)(appMenu.Run());

                switch (input)
                {
                    case appointmentMenu.Create_Appointment:
                        Console.Write("Enter appointment date (yyyy-MM-dd): ");
                        string dateD = Console.ReadLine();
                        Console.Write("Enter appointment time (HH:mm): ");
                        string dateT = Console.ReadLine();
                        if (DateTime.TryParse(dateD, out DateTime parsedDate) && TimeSpan.TryParse(dateT, out TimeSpan parsedTime))
                        {
                            DateTime combinedDateTime = parsedDate.Date + parsedTime;
                            selectedAnimal.createAppointment(combinedDateTime);
                            DisplayPopUp -= AppointmentAdded;
                            DisplayPopUp -= AppointmentChanged;
                            DisplayPopUp -= AppointmentCancelled;
                            DisplayPopUp += AppointmentAdded;
                            OnDisplayPopUp(animalList[choice].AnimalName);
                        }
                        else
                        {
                            Console.WriteLine("Invalid date or time format.");
                        }
                        break;
                    case appointmentMenu.Reschedule_Appointment:
                        Console.Clear();
                        selectedAnimal.viewAppointments();

                        Console.Write("Enter appointment number to reschedule: ");
                        if (int.TryParse(Console.ReadLine(), out int indexRes))
                        {
                            Console.Write("Enter New appointment date (yyyy-MM-dd): ");
                            string newDateD = Console.ReadLine();
                            Console.Write("Enter New appointment time (HH:mm): ");
                            string newDateT = Console.ReadLine();
                            if (DateTime.TryParse(newDateD, out DateTime newParsedDate) && TimeSpan.TryParse(newDateT, out TimeSpan newParsedTime))
                            {
                                DateTime newDateTime = newParsedDate.Date + newParsedTime;
                                selectedAnimal.rescheduleAppointment(indexRes - 1, newDateTime);
                                DisplayPopUp -= AppointmentAdded;
                                DisplayPopUp -= AppointmentChanged;
                                DisplayPopUp -= AppointmentCancelled;
                                DisplayPopUp += AppointmentChanged;
                                OnDisplayPopUp(animalList[choice].AnimalName);
                            }
                            else
                            {
                                Console.WriteLine("Invalid date or time format.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid number.");
                        }
                        break;
                    case appointmentMenu.Cancel_Appointment:
                        Console.Clear();
                        selectedAnimal.viewAppointments();
                        
                        Console.Write("Enter appointment number to cancel: ");
                        if (int.TryParse(Console.ReadLine(), out int indexCancel))
                        {
                            selectedAnimal.cancelAppointment(indexCancel - 1);
                            DisplayPopUp -= AppointmentAdded;
                            DisplayPopUp -= AppointmentChanged;
                            DisplayPopUp -= AppointmentCancelled;
                            DisplayPopUp += AppointmentCancelled;
                            OnDisplayPopUp(animalList[choice].AnimalName);
                        }
                        else
                        {
                            Console.WriteLine("Invalid number.");
                        }
                        break;
                    case appointmentMenu.View_Appointments:
                        Console.Clear();
                        selectedAnimal.viewAppointments();
                        break;
                    case appointmentMenu.Back_To_Menu:
                        exit = true;
                        return;
                }
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
            }//End of while loop
        }//End of manage appointment
        #endregion

        #region DisplayPopUp
        public void OnDisplayPopUp(string animalName)
        {
            DisplayPopUp?.Invoke(animalName);
        }
        public void AppointmentAdded(string animalName)
        {
            MessageBox.Show($"Appointment has been created for {animalName}");
        }
        public void AppointmentChanged(string animalName)
        {
            MessageBox.Show($"Appointment has been rescheduled for {animalName}");
        }
        public void AppointmentCancelled(string animalName)
        {
            MessageBox.Show($"Appointment has been cancelled for {animalName}");
        }
        #endregion

        #region Exit Program
        public void ExitProgram()
        {
            string pacman;

            Console.Clear();
            System.Threading.Thread.Sleep(200);
            Console.WriteLine("             *********         ");
            System.Threading.Thread.Sleep(200);
            Console.WriteLine("          ***************      ");
            System.Threading.Thread.Sleep(200);
            Console.WriteLine("        *******************    ");
            System.Threading.Thread.Sleep(200);
            Console.WriteLine("       *********************   ");
            System.Threading.Thread.Sleep(200);
            Console.WriteLine("      ***********************  ");
            System.Threading.Thread.Sleep(200);
            Console.WriteLine("      ***********************  ");
            System.Threading.Thread.Sleep(200);
            Console.WriteLine("      ***********************  ");
            System.Threading.Thread.Sleep(200);
            Console.WriteLine("       *********************   ");
            System.Threading.Thread.Sleep(200);
            Console.WriteLine("        *******************    ");
            System.Threading.Thread.Sleep(200);
            Console.WriteLine("          ***************      ");
            System.Threading.Thread.Sleep(200);
            Console.WriteLine("             *********         ");
            System.Threading.Thread.Sleep(300);

            pacman = "";
            for (int k = 0; k < 40; k++)
            {

                if (k == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                }
                if (k == 10)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                }
                if (k == 29)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                }

                if (k % 2 == 0)
                {
                    Console.Clear();
                    pacman = "  " + pacman;
                    Console.WriteLine(pacman + "             *********         ");
                    Console.WriteLine(pacman + "          ***************      ");
                    Console.WriteLine(pacman + "        *******************    ");
                    Console.WriteLine(pacman + "       *****************    ");
                    Console.WriteLine(pacman + "      *************               ");
                    Console.WriteLine(pacman + "      ***********                 ");
                    Console.WriteLine(pacman + "      *************               ");
                    Console.WriteLine(pacman + "       *****************    ");
                    Console.WriteLine(pacman + "        *******************    ");
                    Console.WriteLine(pacman + "         ***************       ");
                    Console.WriteLine(pacman + "            *********          ");
                    if (k >= 10 && k <= 29)
                    {
                        System.Threading.Thread.Sleep(90);
                    }
                    else
                    {
                        System.Threading.Thread.Sleep(125);
                    }
                }
                if (k % 2 == 1)
                {
                    Console.Clear();
                    pacman = "  " + pacman;
                    Console.WriteLine(pacman + "             *********         ");
                    Console.WriteLine(pacman + "          ***************      ");
                    Console.WriteLine(pacman + "        *******************    ");
                    Console.WriteLine(pacman + "       *********************   ");
                    Console.WriteLine(pacman + "      ***********************  ");
                    Console.WriteLine(pacman + "      ***********************  ");
                    Console.WriteLine(pacman + "      ***********************  ");
                    Console.WriteLine(pacman + "       *********************   ");
                    Console.WriteLine(pacman + "        *******************    ");
                    Console.WriteLine(pacman + "          ***************      ");
                    Console.WriteLine(pacman + "             *********         ");
                    if (k >= 10 && k <= 29)
                    {
                        System.Threading.Thread.Sleep(90);
                    }
                    else
                    {
                        System.Threading.Thread.Sleep(125);
                    }
                }
            }//End of first loop

            string pacmanBack;
            pacmanBack = "                                                                                          ";
            for (int k = 0; k < 44; k++)
            {

                if (k == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                }
                if (k == 10)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                }
                if (k == 29)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                }

                if (k % 2 == 0)
                {
                    Console.Clear();
                    pacmanBack = pacmanBack.Substring(2);
                    Console.WriteLine("");
                    Console.WriteLine("");
                    Console.WriteLine("");
                    Console.WriteLine("");
                    Console.WriteLine("");
                    Console.WriteLine("");
                    Console.WriteLine("");
                    Console.WriteLine("");
                    Console.WriteLine("");
                    Console.WriteLine("");
                    Console.WriteLine("");
                    Console.WriteLine(pacmanBack + "             *********         ");
                    Console.WriteLine(pacmanBack + "          ***************      ");
                    Console.WriteLine(pacmanBack + "        *******************    ");
                    Console.WriteLine(pacmanBack + "           *****************   ");
                    Console.WriteLine(pacmanBack + "                *************  ");
                    Console.WriteLine(pacmanBack + "                   *********** ");
                    Console.WriteLine(pacmanBack + "                *************  ");
                    Console.WriteLine(pacmanBack + "           *****************   ");
                    Console.WriteLine(pacmanBack + "        *******************    ");
                    Console.WriteLine(pacmanBack + "         ***************       ");
                    Console.WriteLine(pacmanBack + "            *********          ");
                    if (k >= 10 && k <= 29)
                    {
                        System.Threading.Thread.Sleep(90);
                    }
                    else
                    {
                        System.Threading.Thread.Sleep(125);
                    }
                }
                if (k % 2 == 1)
                {
                    Console.Clear();
                    pacmanBack = pacmanBack.Substring(2);
                    Console.WriteLine("");
                    Console.WriteLine("");
                    Console.WriteLine("");
                    Console.WriteLine("");
                    Console.WriteLine("");
                    Console.WriteLine("");
                    Console.WriteLine("");
                    Console.WriteLine("");
                    Console.WriteLine("");
                    Console.WriteLine("");
                    Console.WriteLine("");
                    Console.WriteLine(pacmanBack + "             *********         ");
                    Console.WriteLine(pacmanBack + "          ***************      ");
                    Console.WriteLine(pacmanBack + "        *******************    ");
                    Console.WriteLine(pacmanBack + "       *********************   ");
                    Console.WriteLine(pacmanBack + "      ***********************  ");
                    Console.WriteLine(pacmanBack + "      ***********************  ");
                    Console.WriteLine(pacmanBack + "      ***********************  ");
                    Console.WriteLine(pacmanBack + "       *********************   ");
                    Console.WriteLine(pacmanBack + "        *******************    ");
                    Console.WriteLine(pacmanBack + "          ***************      ");
                    Console.WriteLine(pacmanBack + "             *********         ");
                    if (k >= 10 && k <= 29)
                    {
                        System.Threading.Thread.Sleep(90);
                    }
                    else
                    {
                        System.Threading.Thread.Sleep(125);
                    }
                }
            }

            Console.ResetColor();
            System.Threading.Thread.Sleep(400);
            Console.Clear();
            Console.WriteLine("Group Members: ");
            Console.WriteLine("Henri Claassen");
            Console.WriteLine("Dante Coshiwe");
            Console.WriteLine("Masilo Pudikabekwa");
            System.Threading.Thread.Sleep(4000);

            Console.WriteLine("\nClosing...");
            Console.Write("■");
            System.Threading.Thread.Sleep(1000);
            Console.Write("■");
            System.Threading.Thread.Sleep(1000);
            Console.Write("■");
            System.Threading.Thread.Sleep(1000);
            Console.Write("■");
            System.Threading.Thread.Sleep(1000);
            Console.Write("■");
            System.Threading.Thread.Sleep(1000);
            Console.Write("■");
            System.Threading.Thread.Sleep(1000);
            Console.Write("■");
            System.Threading.Thread.Sleep(1000);
            Console.WriteLine(" ");
            Console.ResetColor();
            Console.WriteLine("You have exited the program, Goodbye!");
            System.Threading.Thread.Sleep(2000);
            Environment.Exit(0);
        }
        #endregion
    }
}
