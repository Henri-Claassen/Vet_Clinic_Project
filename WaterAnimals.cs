using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG_281_Project
{
    internal class WaterAnimals : Animal, IAnimalMethods
    {
        private int gilCount;
        private int finCount;

        public WaterAnimals()
        {

        }

        public WaterAnimals(string ownerName, int ownerID, int ownerCoverageNr, string ownerCoverageName, string ownerCoveragePackage, string animalName, int animalAge, double animalWeight, int animalID, string animalGender, string dietType, int gilCount, int finCount)
            : base(ownerName, ownerID, ownerCoverageNr, ownerCoverageName, ownerCoveragePackage, animalName, animalAge, animalWeight, animalID, animalGender, dietType)
        {
            this.gilCount = gilCount;
            this.finCount = finCount;
        }

        public int GilCount
        {
            get { return gilCount; }
            set { gilCount = value; }
        }
        public int FinCount
        {
            get { return finCount; }
            set { finCount = value; }
        }

        public void createAppointment(DateTime date)
        {
            Appointments.Add(date);
            Console.WriteLine($"Appointment created for {AnimalName} on {date:yyyy-MM-dd HH:mm}");
        }
        public void rescheduleAppointment(int index, DateTime newDate)
        {
            if (index >= 0 && index < Appointments.Count)
            {
                Appointments[index] = newDate;
                Console.WriteLine($"Appointment rescheduled to {newDate:yyyy-MM-dd HH:mm}");
            }
            else
            {
                Console.WriteLine("Invalid appointment index.");
            }
        }
        public void cancelAppointment(int index)
        {
            if (index >= 0 && index < Appointments.Count)
            {
                var removed = Appointments[index];
                Appointments.RemoveAt(index);
                Console.WriteLine($"Appointment on {removed:yyyy-MM-dd HH:mm} has been cancelled.");
            }
            else
            {
                Console.WriteLine("Invalid appointment index.");
            }
        }

        public void viewAppointments()
        {
            if (Appointments.Count == 0)
            {
                Console.WriteLine($"No appointments scheduled for {AnimalName}.");
            }
            else
            {
                Console.WriteLine($"Appointments for {AnimalName}:");
                for (int i = 0; i < Appointments.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {Appointments[i]:yyyy-MM-dd HH:mm}");
                }
            }
        }

        public double calcDosage(Medication med)
        {
            double dosage = AnimalWeight * med.DosagePerKG;
            Console.WriteLine($"Dosage for {AnimalName} (WaterAnimal) with weight {AnimalWeight}kg: {dosage} mg of {med.MedName}");
            return dosage;
        }

    }//End of WaterAnimal class
}//End of namespace
