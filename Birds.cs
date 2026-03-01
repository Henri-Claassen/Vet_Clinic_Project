using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PRG_281_Project
{
    internal class Birds : Animal, IAnimalMethods
    {
        private double wingSpan; //Changed from string on the class diagram to double
        public Birds()
        {

        }
        public Birds(string ownerName, int ownerID, int ownerCoverageNr, string ownerCoverageName, string ownerCoveragePackage, string animalName, int animalAge, double animalWeight, int animalID, string animalGender, string dietType, double wingSpan)
            : base(ownerName, ownerID, ownerCoverageNr, ownerCoverageName, ownerCoveragePackage, animalName, animalAge, animalWeight, animalID, animalGender, dietType)
        {
            this.wingSpan = wingSpan;
        }

        public double WingSpan
        {
            get { return wingSpan; }
            set { wingSpan = value; }
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
            Console.WriteLine($"Dosage for {AnimalName} (Bird) with weight {AnimalWeight}kg: {dosage} mg of {med.MedName}");
            return dosage;
        }

    }//End of birds class
}//End of namespace
