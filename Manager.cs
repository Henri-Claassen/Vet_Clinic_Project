using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG_281_Project
{
    #region Manager
    internal class Manager : Employee
    {
        private string managerTitle;
        private string department;
        private const double rate = 5000;

        public Manager()
        {

        }
        public Manager(string employeeName, string employeeSurname, int employeeID, int hoursWorked, string managerTitle, string department) : base(employeeName, employeeSurname, employeeID, hoursWorked)
        {
            this.managerTitle = managerTitle;
            this.department = department;
        }

        public string ManagerTitle
        {
            get { return managerTitle; }
            set { managerTitle = value; }
        }

        public string Department
        {
            get { return department; }
            set { department = value; }
        }

        public override void calcSalary()
        {
            Salary = rate * HoursWorked;//If not make field protected in abstract class
        }

    }//End of manager class
#endregion

}//End of namespace
