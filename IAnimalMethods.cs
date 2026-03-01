using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG_281_Project
{
    internal interface IAnimalMethods //Changed IAddApointment to IAnimalMethods and added void DisplayInfo(); and int calcDosage();
    {
        void createAppointment(DateTime date);
        void rescheduleAppointment(int index,DateTime newDate);
        void cancelAppointment(int index);
        void viewAppointments(); //Added to view appointments when created
        double calcDosage(Medication med);
    }//End of IAnimalMethods interface
}//End of namespace
