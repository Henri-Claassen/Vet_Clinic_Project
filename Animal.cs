using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG_281_Project
{
    internal abstract class Animal : Owner 
    {
        private string animalName;
        private int animalAge;
        private double animalWeight; //Changed to double instead of Int in the original class diagram
        private int animalID;
        private string animalGender;
        private string dietType;
        private double dosage; // Added dosage to store how much medicine each animal should get was not on original class diagram

        
        public List<DateTime> Appointments { get; private set; } = new List<DateTime>(); //List to store appointments

        public Animal()
        {

        }
        public Animal(string ownerName, int ownerID, int ownerCoverageNr, string ownerCoverageName, string ownerCoveragePackage, string animalName, int animalAge, double animalWeight, int animalID, string animalGender, string dietType)
            : base(ownerName, ownerID, ownerCoverageNr, ownerCoverageName, ownerCoveragePackage)
        {
            this.animalName = animalName;
            this.animalAge = animalAge;
            this.animalWeight = animalWeight;
            this.animalID = animalID;
            this.animalGender = animalGender;
            this.dietType = dietType;
        }

        public string AnimalName
        {
            get { return animalName; }
            set { animalName = value; }
        }

        public int AnimalAge
        {
            get { return animalAge; }
            set { animalAge = value; }
        }

        public double AnimalWeight
        {
            get { return animalWeight; }
            set { animalWeight = value; }
        }

        public int AnimalID
        {
            get { return animalID; }
            set { animalID = value; }
        }

        public string AnimalGender
        {
            get { return animalGender; }
            set { animalGender = value; }
        }

        public string DietType
        {
            get { return dietType; }
            set { dietType = value; }
        }
        public double Dosage
        {
            get { return dosage; }
            set { dosage = value; }
        }
    }//End of animal class
}//End of namespace

