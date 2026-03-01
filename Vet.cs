using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG_281_Project
{
    #region Vet
    internal class Vet : Employee
    {
        private string specialisation;
        private const double rate = 1000;

        public Vet()
        {

        }
        public Vet(string employeeName, string employeeSurname, int employeeID, int hoursWorked, string specialisation) : base(employeeName, employeeSurname, employeeID, hoursWorked)
        {
            this.specialisation = specialisation;
        }

        public string Specialisation
        {
            get { return specialisation; }
            set { specialisation = value; }
        }

        public override void calcSalary()
        {
            Salary = rate * HoursWorked;
        }

    }//End of vet class
    #endregion

}//End of namespace
